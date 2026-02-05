/*jslint browser: true, devel: true */

//Declare global variables
/*global $, CON, COFF, SETUP, userConfig, timelinesHashmap, newestMachinesRecordsUrl, livestatusdelay, timeModulo, timelineHeight, trendEnabled, timelineEnabled, lsByGroup, groups, groupsDefinition, reloadtimer*/

//Declare global functions
/*global GetStatusColor, updateCurrentPerf, updateTimeline, resized, updateDailyBestRSS, updateLegend, panelRotation, nextLegend, nextGroups, Pause, dashboardUnavailable, TestConnection*/

var rowheight = 0;
var rowqty = 0;
var nextLSPage = 0;
var arrowsize = 0;

function calcArrowSize() {
    arrowsize = parseFloat($('body').css("font-size").replace("px", "")) * 1.75;
}

function recalcRowQty() {
    var freeheight = $(window).height() - $("#top-bar").outerHeight() - $("#logoSection").outerHeight() - $("#msgFeedid").outerHeight() - $("#livestatusheader").outerHeight() - 10; //extra 10px padding
    rowqty = Math.floor(((freeheight - (freeheight % rowheight)) / rowheight));
}

var rotationCompleted = false;
var lstimer;

function nextPage() {
    clearTimeout(lstimer);
    if (rotationCompleted && userConfig[0].rotation === 'on') {
        rotationCompleted = false;
        panelRotation();
    } else {
        if (!lsByGroup) {
            displayPageAllMachines();
            UpdatePageAllMachines(1);
            if (nextLSPage === 0) {
                rotationCompleted = true;
            }
        } else if (groups) {
            displayPagePerGroup();
            UpdatePagePerGroup(1);
            if (nextLSPage === 0 && currentLSGroup === 0) {
                rotationCompleted = true;
            }
        } else {
            console.log('Invalid livestatus configuration.');
        }

        lstimer = setTimeout(nextPage, livestatusdelay);
    }
}

function displayPageAllMachines() {
    var cnt = 0;
    for (var machine in livestatusHashmap) {
        if (cnt >= (nextLSPage * rowqty) && cnt < ((nextLSPage * rowqty) + rowqty)) {
            $('.row' + livestatusHashmap[machine].index).show();
        } else {
            $('.row' + livestatusHashmap[machine].index).hide();
        }
        cnt += 1;
    }
    UpdateTimelineScale();
}

function UpdatePageAllMachines(increment) {
    nextLSPage += increment;
    if (nextLSPage * rowqty > Object.keys(livestatusHashmap).length - 1) {
        nextLSPage = 0;
    } else if (nextLSPage < 0) {
        nextLSPage = Math.ceil(Object.keys(livestatusHashmap).length / rowqty) - 1; //returns the lastpage
    }
}

var currentLSGroup = 0; //defines the group to display in the livestatus table
var lsGroups = []; //list of groups to display in table if lsByGroup is enabled

function displayPagePerGroup() {
    var mach_cnt = 0;
    for (var machine in livestatusHashmap) {
        if (groupsDefinition[lsGroups[currentLSGroup]].indexOf(machine) >= 0) {
            if (mach_cnt >= nextLSPage * rowqty && mach_cnt < (nextLSPage * rowqty + rowqty)) {
                $('.row' + livestatusHashmap[machine].index).show();
            } else {
                $('.row' + livestatusHashmap[machine].index).hide();
            }
            mach_cnt += 1;
        } else {
            $('.row' + livestatusHashmap[machine].index).hide();
        }
    }

    UpdateTimelineScale();
    //Update group name indication
    $('#dashboardTitle').text('CSIFlex Dashboard - ' + lsGroups[currentLSGroup]);
}

function UpdatePagePerGroup(increment) {
    nextLSPage += increment;
    if (nextLSPage * rowqty > groupsDefinition[lsGroups[currentLSGroup]].length - 1) {
        nextLSPage = 0;
        currentLSGroup += 1;
        if (currentLSGroup > lsGroups.length - 1) {
            currentLSGroup = 0;
        }
    } else if (nextLSPage < 0) {
        currentLSGroup -= 1;
        if (currentLSGroup < 0) {
            currentLSGroup = lsGroups.length - 1;
        }
        nextLSPage = Math.ceil(groupsDefinition[lsGroups[currentLSGroup]].length / rowqty) - 1;
    }
}


function setPageLength() {
    if ($(window).width() >= 1200) {
        $('#liveStatus').css('paddingBottom', '0em');
        recalcRowQty();
    } else {
        $('#liveStatus').css('paddingBottom', $("#msgFeedid").outerHeight() + 'px'); //message Feed
        rowqty = Object.keys(livestatusHashmap).length;
        Pause();
        ShowAllMachines();
    }
}

function ShowAllMachines() {
    for (var machine in livestatusHashmap) {
        $('.row' + livestatusHashmap[machine].index).show();
    }
}

//add 15minutes to scale if an hour appears close to the end
function scaleFit(starttime, totalhours) {
    var isFit = true;
    var width = document.getElementById('livestatusheader').offsetWidth - 20; //20px padding    

    for (var hours_it = 1; hours_it <= totalhours; hours_it += 1) {
        var hour = Math.floor((starttime / 3600) + hours_it);
        var pos = (hour * 3600 - starttime);
        pos = (pos / (60 * 60 * totalhours) * width);

        //prevent hour to be displayed near beginning or end of the timeline
        var overlaypadding = (width * 0.02); //2% of the width?
        if (pos < overlaypadding || pos > (width - overlaypadding)) {
            isFit = false;
        }
    }
    return isFit;
}

function CreateRuler(starttime, totalhours, isFit) {
    var overlay = "";
    var width = $('#liveStatus').width() - parseFloat($('table tbody td').css('paddingLeft').replace('px', '')) - parseFloat($('table tbody td').css('paddingRight').replace('px', ''));
    for (var halfhours_it = 1; halfhours_it <= totalhours * 2; halfhours_it++) {
        var halfhour = Math.floor((starttime / (1800) + halfhours_it));
        var pos = (halfhour * 1800 - starttime);
        var hour = Math.floor((starttime / 3600) + (halfhours_it / 2));
        var ishour = false;
        if (pos == (hour * 3600 - starttime)) {
            ishour = true;
        }

        if (isFit) {
            pos = (pos / (60 * 60 * totalhours) * width);
        } else {
            pos = (pos / (60 * 60 * totalhours + 60 * 15) * width);
        }

        if (ishour) {
            pos = pos - (2.5); //remove half of tick mark width
            overlay += '<use x="' + pos + '" y="0" xlink:href="#ruler-hour-mark"></use>';
        } else {
            pos = pos - (1.5); //remove half of tick mark width
            overlay += '<use x="' + pos + '" y="0" xlink:href="#ruler-halfhour-mark"></use>';
        }
    }
    //display starttime
    overlay += '<use x="0" y="0" xlink:href="#ruler-hour-mark"></use>';

    //Add horizontal line
    overlay += '<use x="0" y="0" xlink:href="#ruler-horiz"></use>';

    return overlay;
}

function CreateOverlay(starttime, totalhours, isFit) {
    var overlay = "";
    overlay = CreateRuler(starttime, totalhours, isFit);
    var width = $('#liveStatus').width() - parseFloat($('table tbody td').css('paddingLeft').replace('px', '')) - parseFloat($('table tbody td').css('paddingRight').replace('px', ''));

    //display start time
    var starth = Math.floor(starttime / 3600);
    var startm = Math.floor((starttime - (starth * 3600)) / 60);
    var displayStarttime = starth + ':' + startm;
    if (startm < 10) {
        displayStarttime = starth + ':0' + startm;
    }
    var y = getHeightOfText('00:00', '0.55em') / 1.3; //get y position for timeline legend considering the tick marks
    overlay += '<text x="0" y="' + y + '" text-anchor="start" fill="rgb(160,160,160)" dy=".3em" font-size="0.55em">' + displayStarttime + '</text>';

    //prevent hour to be displayed near beginning or end of the timeline
    var overlaypadding = getWidthOfText(displayStarttime, '0.55em') * 0.8; //at 85%, if the text is near the end, it will be clipped a little

    var increment = 1;
    if (width <= 350) {
        increment = 2; //display every 2 hours on small screens
    }

    for (var hours_it = 1; hours_it <= totalhours; hours_it += increment) {
        var hour = Math.floor((starttime / 3600) + hours_it);
        var pos = (hour * 3600 - starttime);
        if (isFit) {
            pos = (pos / (60 * 60 * totalhours) * width);
        } else {
            pos = (pos / (60 * 60 * totalhours + 60 * 15) * width);
        }

        if (pos > overlaypadding && pos < (width - overlaypadding)) {
            overlay += '<text x="' + pos + '" y="' + y + '" text-anchor="middle" fill="rgb(160,160,160)" dy=".3em" font-size="0.55em">' + (hour % timeModulo) + ':00</text>';
        }
    }

    return overlay;
}

function getWidthOfText(txt, fontsize) {
    $("#hiddentext").css({
        'font-size': fontsize,
    });
    var width = $("#hiddentext").html(txt).width();
    $("#hiddentext").html("");
    return width;
}

function getHeightOfText(txt, fontsize) {
    $("#hiddentext").css({
        'font-size': fontsize,
    });
    var height = $("#hiddentext").html(txt).height();
    $("#hiddentext").html("");
    return height;
}


var CellHeight = 0;

function getHeightOfCell() {
    var height = $("#hiddendiv").html("test").outerHeight();
    $("#hiddendiv").html("");
    CellHeight = height;
    return CellHeight;
}


function RefreshConfig(needrefresh) {
    if (needrefresh == 'True') {
        window.location.reload(true);
    }
}

function CreateIndicator(height, angle, statuscolor, arrowcolor, alarm) {

    var isAlarm = false;
    var isWarning = false;

    if (alarm !== null) {
        isAlarm = alarm.startsWith('Fault');
        isWarning = alarm.startsWith('Warning');
    }

    var html = '<svg xmlns="http://www.w3.org/2000/svg" class="indicator" width="' + height + 'px" height="' + CellHeight + 'px" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid"><g class="arrows" transform="translate(50, 50) rotate(' + (angle) + ')"><circle r="50" fill="' + statuscolor + '"/>';

    //if (trendEnabled) {
    html += '<path class="arrow bigarrow ' + ((isAlarm || !trendEnabled) ? 'hide' : 'show') + '" d="m0.25,-2 l 0,-2 4,4 -4,4 0,-2 -4,0 0,-4 Z" transform="scale(10 7.5)" fill="white"/><path class="arrow smallarrow ' + ((isAlarm || !trendEnabled) ? 'hide' : 'show') + '" d="m1,-2 l 0,-2 4,4 -4,4 0,-2 -6,0 0,-4 Z" transform="scale(6.75 5.15)" fill="' + arrowcolor + '"/>';
    //}


    // if (isAlarm || isWarning) {
    //White contour
    html += '</g><g class="exclamation bigexclamation ' + (isAlarm ? 'show' : 'hide') + '" fill="white" transform="scale(1.4) translate(3.5, 3)"><path d="m26.948 37.09c.243 2.531.655 4.412 1.222 5.653.574 1.237 1.593 1.854 3.064 1.854.275 0 .521-.043.765-.093.25.05.495.093.772.093 1.467 0 2.489-.617 3.06-1.854.57-1.241.975-3.122 1.223-5.653l1.306-19.542c.243-3.809.367-6.542.367-8.201 0-2.258-.589-4.02-1.771-5.285-1.186-1.265-2.744-1.896-4.674-1.896-.103 0-.18.023-.281.027-.096-.004-.175-.027-.275-.027-1.934 0-3.489.631-4.673 1.896-1.183 1.267-1.776 3.03-1.776 5.286 0 1.659.121 4.392.368 8.201l1.303 19.541"/><path transform="scale(1.3) translate(-7.25, -14)" d="m32.05 51.74c-1.874 0-3.466.591-4.788 1.773-1.321 1.183-1.983 2.619-1.983 4.305 0 1.903.67 3.401 2 4.489 1.336 1.088 2.894 1.632 4.675 1.632 1.813 0 3.394-.536 4.746-1.611 1.35-1.072 2.025-2.578 2.025-4.508 0-1.686-.646-3.122-1.938-4.305-1.292-1.184-2.871-1.775-4.74-1.775"/></g>';
    //exclamation mark
    html += '</g><g class="exclamation smallexclamation ' + (isAlarm ? 'show' : 'hide') + '" fill="' + (isAlarm ? 'red' : 'orange') + '" transform="scale(1) translate(17.5, 17)"><path transform="translate(0, -4)" d="m26.948 37.09c.243 2.531.655 4.412 1.222 5.653.574 1.237 1.593 1.854 3.064 1.854.275 0 .521-.043.765-.093.25.05.495.093.772.093 1.467 0 2.489-.617 3.06-1.854.57-1.241.975-3.122 1.223-5.653l1.306-19.542c.243-3.809.367-6.542.367-8.201 0-2.258-.589-4.02-1.771-5.285-1.186-1.265-2.744-1.896-4.674-1.896-.103 0-.18.023-.281.027-.096-.004-.175-.027-.275-.027-1.934 0-3.489.631-4.673 1.896-1.183 1.267-1.776 3.03-1.776 5.286 0 1.659.121 4.392.368 8.201l1.303 19.541"/><path transform="scale(1.15) translate(-4, 1)" d="m32.05 51.74c-1.874 0-3.466.591-4.788 1.773-1.321 1.183-1.983 2.619-1.983 4.305 0 1.903.67 3.401 2 4.489 1.336 1.088 2.894 1.632 4.675 1.632 1.813 0 3.394-.536 4.746-1.611 1.35-1.072 2.025-2.578 2.025-4.508 0-1.686-.646-3.122-1.938-4.305-1.292-1.184-2.871-1.775-4.74-1.775"/></g>';
    /*  } else */


    html += '</svg>';
    return html;
}

var ruler_height = 0;

function setRulerHeight() {
    ruler_height = getHeightOfText('00:00', '0.55em') * 1.4; //*1.4 to cover the tick mark      
}

function UpdateRowheight() {
    rowheight = $('#liveStatusTable > tbody > tr > td.colmach').outerHeight(true);
    if (timelineEnabled) {
        rowheight += (timelineHeight + ruler_height);
    }
    setPageLength();
}

function updateDailyBest(mach_values) {
    var tmpCON = 0;
    var tmpMachine;
    for (var i = 0; i < mach_values.length; ++i) {
        var shiftUtilisationData = JSON.parse(mach_values[i].Utilization);
        var conShiftUtilisation;

        try {
            conShiftUtilisation = shiftUtilisationData.CON;
        } catch (err) {
            conShiftUtilisation = 0;
        }

        if (conShiftUtilisation > tmpCON) {
            tmpCON = conShiftUtilisation;
            tmpMachine = mach_values[i].machine;
        }
    }
    updateDailyBestRSS(tmpMachine, tmpCON);
}

function FixPartOverflow() {
    //Fix overflow of partnumbers
    $(".colpart span").each(function (i) {

        $(this).parent().removeClass('mymarquee');
        $(this).css('font-size', '1em');

        var textwidth = getWidthOfText($(this).text(), $('.cell-text').css('font-size')) + 5;
        var containerwidth = $('.colpart').outerWidth(true) - parseFloat($('table tbody td.colpart').css('paddingLeft').replace("px", "")) - parseFloat($('table tbody td.colpart').css('paddingRight').replace("px", "")) - parseFloat($('.cell-text').css('border-left-width').replace("px", "")) - parseFloat($('.cell-text').css('border-right-width').replace("px", ""));


        if (textwidth > containerwidth) {
            if (textwidth * 0.75 > containerwidth) {
                $(this).parent().addClass('mymarquee');
            } else {
                var ratio = (containerwidth / textwidth);
                $(this).css('font-size', ratio.toFixed(4) + 'em');
            }
        }
    });
    //Update animation time to panel rotation
    $('.mymarquee span').css('animation-duration', (livestatusdelay) + 'ms').hide().show(0);
}

function ActivateTooltips() {
    $('.ttip').tooltip({
        html: true
    });
}

var livestatusHashmap = {};

function initializeStatusTable() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            var serverResponse = xhttp.responseText;
            var jsonResponse = JSON.parse(serverResponse);

            getHeightOfCell();
            setRulerHeight();
            calcArrowSize();

            for (var mach_it in jsonResponse.values) {
                $('#liveStatusBody').append(CreateDetailRowStr(mach_it, jsonResponse.values[mach_it]));
                $('#liveStatusBody').append(CreateTimelineRowStr(mach_it, jsonResponse.values[mach_it]));

                //init hashmap
                livestatusHashmap[jsonResponse.values[mach_it].machine] = {
                    index: mach_it,
                    status: jsonResponse.values[mach_it].status,
                    part: jsonResponse.values[mach_it].PartNumber,
                    cnt: jsonResponse.values[mach_it].CycleCount,
                    fovr: (jsonResponse.values[mach_it].feedOverride ? jsonResponse.values[mach_it].feedOverride : 0),
                    sovr: (jsonResponse.values[mach_it].SpindleOverride ? jsonResponse.values[mach_it].SpindleOverride : 0),
                    shiftutil: jsonResponse.values[mach_it].Utilization,
                    arrowangle: jsonResponse.values[mach_it].Trend + 90,
                    progr: jsonResponse.values[mach_it].progression,
                    alarm: jsonResponse.values[mach_it].alarm
                };
            }

            ActivateTooltips();

            updateDailyBest(jsonResponse.values);
            updateLegend(jsonResponse.values);

            FixPartOverflow();
            UpdateRowheight();
            
            updateCurrentPerf(jsonResponse.values);
            updateTimeline(jsonResponse.values);
        }
    };

    xhttp.open("POST", newestMachinesRecordsUrl, false);
    xhttp.send();
}

function CreateDetailRowStr(mach_it, machine_detail) {
    var rclass = (mach_it % 2) === 0 ? "even" : "odd";

    //detail
    var rowhtml = '<tr role="row" class="' + rclass + ' row' + mach_it + '"><td class="colmach">' + CreateMachineCell(mach_it, machine_detail) + '</td><td class="colstatus">' + CreateStatusCell(mach_it, machine_detail) + '</td><td class="colfovr">' + CreateOvrMeter('fovr' + mach_it, machine_detail.feedOverride) + '</td><td class="colsovr">' + CreateOvrMeter('sovr' + mach_it, machine_detail.SpindleOverride) + '</td><td class="colpart">' + CreateStdCell('part' + mach_it, machine_detail.status, machine_detail.PartNumber, machine_detail.alarm) + '</td><td class="colcnt">' + CreateStdCell('cnt' + mach_it, machine_detail.status, machine_detail.CycleCount, machine_detail.alarm) + '</td><td class="collastcyc">' + CreateStdCell('lastcyc' + mach_it, machine_detail.status, machine_detail.LastCycle, machine_detail.alarm) + '</td><td class="colcurcyc">' + CreateStdCell('currcyc' + mach_it, machine_detail.status, machine_detail.CurrentCycle, machine_detail.alarm) + '</td><td class="colelapsed">' + CreateStdCell('elapsed' + mach_it, machine_detail.status, machine_detail.ElapsedTime, machine_detail.alarm) + '</td><td class="colshiftutil">' + CreateShiftUtilizationCell(mach_it, machine_detail) + '</td></tr>';
    return rowhtml;
}

function CreateTimelineRowStr(mach_it, machine_detail) {
    var rclass = (mach_it % 2) === 0 ? "even" : "odd";
    var rowhtml = '';
    //timeline
    if (timelineEnabled) {
        rowhtml += '<tr class="row' + mach_it + '"><td class="timeline ' + rclass + '" colspan="10">' + CreateTimeline(machine_detail.machine) + '</td></tr>';
    }
    return rowhtml;
}

function GetBlinkClass(alarm) {
    var blinkclass = '';
    if (alarm !== null) {
        if (alarm.startsWith('Fault')) {
            blinkclass = 'alarmblink';
        } else if (alarm.startsWith('Warning')) {
            blinkclass = 'warningblink';
        }
    }
    return blinkclass;
}

function CreateMachineCell(index, machine_detail) {
    var statuscolor = GetStatusColor(machine_detail.status);
    var correspondingTrendValue = Math.round(90 - (machine_detail.Trend * 180 / 100));
    var arrowfill = "red";

    if ((machine_detail.Trend > 44) && (machine_detail.Trend < 56)) {
        arrowfill = "yellow";
    } else if (machine_detail.Trend >= 55) {
        arrowfill = "green";
    }

    var tooltip = '';
    if (machine_detail.alarm !== null && machine_detail.alarm_details !== null) {
        var ttipstr = '';
        var tmpalarms = JSON.parse(machine_detail.alarm_details);
        for (var al in tmpalarms) {
            ttipstr += tmpalarms[al] + "<br>";
        }
        tooltip = 'class="ttip" title="' + ttipstr + '"';
    }

    return '<div id="mach' + index + '" ' + tooltip + '>' + CreateIndicator(arrowsize, correspondingTrendValue, statuscolor, arrowfill, machine_detail.alarm) + '<div class="cell-text equipement ' + GetBlinkClass(machine_detail.alarm) + '" style="border-color:' + statuscolor + ';width: -moz-calc(100% - ' + (arrowsize * 0.80) + 'px); width: -webkit-calc(100% - ' + (arrowsize * 0.80) + 'px); width: calc(100% - ' + (arrowsize * 0.80) + 'px);"><span style="margin-left:0.15em;" class="single-line">' + machine_detail.machine + '</span></div></div>';
}

function UpdateIndicator(index, machine_detail) {
    var statuscolor = GetStatusColor(machine_detail.status);
    var correspondingTrendValue = Math.round(90 - (machine_detail.Trend * 180 / 100));
    var arrowfill = "red";

    var isAlarm = false;
    var isWarning = false;

    if (machine_detail.alarm !== null) {
        isAlarm = machine_detail.alarm.startsWith('Fault');
        isWarning = machine_detail.alarm.startsWith('Warning');
    }

    if ((machine_detail.Trend > 44) && (machine_detail.Trend < 56)) {
        arrowfill = "yellow";
    } else if (machine_detail.Trend >= 55) {
        arrowfill = "green";
    }

    $('#mach' + index + ' g.arrows').attr('transform', 'translate(50, 50) rotate(' + correspondingTrendValue + ')');
    $('#mach' + index + ' circle').attr('fill', statuscolor);
    $('#mach' + index + ' path.smallarrow').attr('fill', arrowfill);
    $('#mach' + index + ' g.smallexclamation').attr('fill', (isAlarm ? 'red' : 'orange'));
    //if (isAlarm || isWarning) {
    if (isAlarm) {
        $('#mach' + index + ' g.exclamation').removeClass('hide').addClass('show').show();
        $('#mach' + index + ' path.arrow').removeClass('show').addClass('hide').hide();
    } else {
        $('#mach' + index + ' g.exclamation').removeClass('show').addClass('hide').hide();
        if (trendEnabled) {
            $('#mach' + index + ' path.arrow').removeClass('hide').addClass('show').show();
        }
    }
}

function CreateStatusCell(index, machine_detail) {
	
    var statushtml = "";
    var statuscolor = GetStatusColor(machine_detail.status);
    //Verify if the current status is "CYCLE ON" or "SETUP".
    if (machine_detail.status == SETUP || machine_detail.status == CON) {
        var progr = parseFloat(machine_detail.progression).toFixed(4);
        var gradient = 'linear-gradient(to right,' + statuscolor + ' 0%, ' + statuscolor + ' ' + (progr - 5) + '%, transparent ' + (progr + 5) + '%, transparent 100%)';
        if (progr >= 100) {
            gradient = statuscolor;
        }

        statushtml = '<div class="cell-text rounded ' + GetBlinkClass(machine_detail.alarm) + '" style="border-color:' + statuscolor + ';background:' + gradient + ';"><span id="status' + index + '" class="contrast single-line">' + machine_detail.status + '</span></div>';
    } else if (machine_detail.status == COFF) {
        statushtml = '<div class="cell-text rounded ' + GetBlinkClass(machine_detail.alarm) + '" style="border-color:' + statuscolor + ';background:' + statuscolor + '"><span id="status' + index + '" class="contrast single-line">' + machine_detail.status + '</span></div>';
    } else {
        statushtml = '<div class="cell-text rounded ' + GetBlinkClass(machine_detail.alarm) + '" style="border-color:' + statuscolor + ';background:white"><span id="status' + index + '" class="single-line">' + machine_detail.status + '</span></div>';
    }
    return statushtml;
}

function UpdateStatusCell(index, machine_detail) {
    var statuscolor = GetStatusColor(machine_detail.status);
    var bg = 'white';
    //Verify if the current status is "CYCLE ON" or "SETUP".
	
    if (machine_detail.status == SETUP || machine_detail.status == CON) {
		//if( machine_detail.progression == null) machine_detail.progression = 0;
        var progr = parseFloat(machine_detail.progression).toFixed(4);
		
        var gradient = 'linear-gradient(to right,' + statuscolor + ' 0%, ' + statuscolor + ' ' + (progr - 5) + '%, transparent ' + (progr + 5) + '%, transparent 100%)';
        if (progr >= 100) {
            gradient = statuscolor;
			
        }

        bg = gradient;
        $('#status' + index).addClass('contrast');
    } else if (machine_detail.status == COFF) {
		
        bg = statuscolor;
        $('#status' + index).addClass('contrast');
    } else {
		 
        $('#status' + index).removeClass('contrast');
    }

    $('#status' + index).parent().css('background', bg);
}

function CreateOvrMeter(meter_id, ovr_value) {
    //This variable contains the percentage received from the HTTP server.
    var percentOfTheBar = parseFloat(ovr_value);
    var colorOfTheBar;

    //In our bar, 100% corresponds to 50% of the bar so when we receive a percentage from the HTTP server, we find, in the context of our bar where 100% corresponds to 50% percent or a full bar, what value the percentage received corresponds to.
    var valueOfTheBar;

    valueOfTheBar = percentOfTheBar / 2;

    if (percentOfTheBar >= 0 && percentOfTheBar < 50) {
        colorOfTheBar = "red";
    } else if (percentOfTheBar > 50 && percentOfTheBar <= 80) {
        colorOfTheBar = "yellow";
    } else if (percentOfTheBar > 80 && percentOfTheBar <= 120) {
        colorOfTheBar = "green";
    } else if (percentOfTheBar > 120 && percentOfTheBar <= 200) {
        colorOfTheBar = "red";
    }

    /*return '<div class="progress">' +
        '<div id="' + meter_id + '" class="progress-bar progress-bar-danger" style="width:' + valueOfTheBar + '%;background:' + colorOfTheBar + ';">' +
        '</div>' + '</div>' +
        '<div class="progress-meter"><div class="meter meter-left" style="width: 38%;"><span class="meter-text">0%</span></div><div class="meter meter-left" style="width: 12%;"><span class="meter-text" style="margin-left:-1em;">' + //This is where we put 80%
        '' + '</span></div><div class="meter meter-left" style="width: 12%;"><span class="meter-text" style="margin-left:-0.75em;">100%</span></div><div class="meter meter-left" style="width: 23%;"><span class="meter-text" style="margin-left:0em;">' + //This is where we put 120%
        '' + '</span></div><div class="meter meter-left" style="width: 14%;border-left-width: 0em;"><span class="meter-text" >200%</span></div></div>';*/

    //removed 80% and 120% marks
    return '<div class="progress">' +
        '<div id="' + meter_id + '" class="progress-bar progress-bar-danger" style="width:' + valueOfTheBar + '%;background:' + colorOfTheBar + ';">' +
        '</div>' + '</div>' +
        '<div class="progress-meter"><div class="meter meter-left" style="width: 50%;"><span class="meter-text">0%</span></div><div class="meter meter-left" style="width: 50%;"><span class="meter-text" style="margin-left:-0.75em;">100%</span></div><div class="meter meter-left" style="width: 0%;border-left-width: 0em;"><span class="meter-text" style="margin-left:-2em;">200%</span></div></div>';
}

function UpdateMeter(meter_id, ovr_value) {
    //This variable contains the percentage received from the HTTP server.
    var percentOfTheBar = parseFloat(ovr_value);
    var colorOfTheBar;

    //In our bar, 100% corresponds to 50% of the bar so when we receive a percentage from the HTTP server, we find, in the context of our bar where 100% corresponds to 50% percent or a full bar, what value the percentage received corresponds to.
    var valueOfTheBar;

    valueOfTheBar = percentOfTheBar / 2;

    if (percentOfTheBar >= 0 && percentOfTheBar < 50) {
        colorOfTheBar = "red";
    } else if (percentOfTheBar > 50 && percentOfTheBar <= 80) {
        colorOfTheBar = "yellow";
    } else if (percentOfTheBar > 80 && percentOfTheBar <= 120) {
        colorOfTheBar = "green";
    } else if (percentOfTheBar > 120 && percentOfTheBar <= 200) {
        colorOfTheBar = "red";
    }

    $('#' + meter_id).css('width', valueOfTheBar + '%');
    $('#' + meter_id).css('background', colorOfTheBar);
}

function CreateStdCell(cell_id, status, cell_value, alarm) {
    var html = "";
    if (cell_value.trim().length > 0) {
        html = '<div class="cell-text rounded ' + GetBlinkClass(alarm) + '" style="border-color:' + GetStatusColor(status) + ';"><div><span id="' + cell_id + '">' + cell_value + '</span></div></div>';
    }
    return html;
}

function CreateShiftUtilizationCell(index, machine_detail) {
    //The "Utilization" property contains a string.So, we have to convert this string into a javascript object.
    var shiftUtilisationData = JSON.parse(machine_detail.Utilization);
    var conShiftUtilisation, setupShiftUtilisation, coffShiftUtilisation;
    try {
        conShiftUtilisation = shiftUtilisationData.CON;
        setupShiftUtilisation = shiftUtilisationData.SETUP;
        coffShiftUtilisation = shiftUtilisationData.COFF;
    } catch (err) {
        console.log('Error in shiftUtilization:');
        console.log(shiftUtilisationData);
        conShiftUtilisation = 0;
        setupShiftUtilisation = 0;
        coffShiftUtilisation = 0;
    }

    var conprog = 100 - setupShiftUtilisation - coffShiftUtilisation;
    var setupprog = 100 - coffShiftUtilisation;

    return '<div id="shiftutil' + index + '" class="cell-text rounded ' + GetBlinkClass(machine_detail.alarm) + '" style="border-color:' + GetStatusColor(machine_detail.status) + ';background:linear-gradient(to right, ' + GetStatusColor(CON) + ' ' + conprog + '%, ' + GetStatusColor(SETUP) + ' ' + conprog + '%,' + GetStatusColor(SETUP) + ' ' + setupprog + '%,' + GetStatusColor(COFF) + ' ' + setupprog + '%); color:white;">' + conShiftUtilisation + '%</div>';
}


function UpdateShiftUtilizationCell(index, machine_detail) {
    //The "Utilization" property contains a string.So, we have to convert this string into a javascript object.
    var shiftUtilisationData = JSON.parse(machine_detail.Utilization);
    var conShiftUtilisation, setupShiftUtilisation, coffShiftUtilisation;
    try {
        conShiftUtilisation = shiftUtilisationData.CON;
        setupShiftUtilisation = shiftUtilisationData.SETUP;
        coffShiftUtilisation = shiftUtilisationData.COFF;
    } catch (err) {
        console.log('Error in shiftUtilization:');
        console.log(shiftUtilisationData);
        conShiftUtilisation = 0;
        setupShiftUtilisation = 0;
        coffShiftUtilisation = 0;
    }

    var conprog = 100 - setupShiftUtilisation - coffShiftUtilisation;
    var setupprog = 100 - coffShiftUtilisation;

    $('#shiftutil' + index).text(conShiftUtilisation + '%');
    $('#shiftutil' + index).css('background', 'linear-gradient(to right, ' + GetStatusColor(CON) + ' ' + conprog + '%, ' + GetStatusColor(SETUP) + ' ' + conprog + '%,' + GetStatusColor(SETUP) + ' ' + setupprog + '%,' + GetStatusColor(COFF) + ' ' + setupprog + '%)');
}

function CreateTimeline(machine) {
    var timeline_html = "";
    try {
        var d = new Date();
        var h = d.getHours();
        var m = d.getMinutes();
        var s = d.getSeconds();
        var currenttime = (h * 60 * 60) + (m * 60) + s;
        var elapsedtime = currenttime - timelinesHashmap[machine].starttime;
        //add a buffer to change scaling before the timeline reaches the end
        elapsedtime = elapsedtime * 1.10;

        var totalhours = Math.ceil(elapsedtime / 3600);
        if (totalhours % 2 > 0) {
            totalhours += 1; //we are displaying multiple of 2hours (2h, 4h, 6h, 8h...)
        }
        var isFit = scaleFit(timelinesHashmap[machine].starttime, totalhours);
        var vbScale = (60 * 60 * totalhours);
        if (!isFit) {
            vbScale += 60 * 15; //add 15minutes if scale doesn't fit
        }

        timeline_html = '<div id="tl' + timelinesHashmap[machine].index + '"><div style="height:' + timelineHeight + 'px;width:100%;position:relative;"><figure class="figureanchor"><svg viewBox="0 0 ' + vbScale + ' ' + timelineHeight + '" preserveAspectRatio = "none" width="100%" height="' + timelineHeight + '"><use xlink:href="#svg-' + timelinesHashmap[machine].index + '"/></svg></figure></div>' + '<div style="width:100%;height:' + ruler_height + 'px;"><svg style="width:100%;height:' + ruler_height + 'px;">' + CreateOverlay(timelinesHashmap[machine].starttime, totalhours, isFit) + '</svg></div></div>';
    } catch (e) {
        console.log("Error in CreateTimeline:" + e);
    }
    return timeline_html;
}

function UpdateTimelineScale() {
    for (var tl in timelinesHashmap) {
        var timeline_html = "";
        try {
            var d = new Date();
            var h = d.getHours();
            var m = d.getMinutes();
            var s = d.getSeconds();
            var currenttime = (h * 60 * 60) + (m * 60) + s;
            var elapsedtime = currenttime - timelinesHashmap[tl].starttime;
            //add a buffer to change scaling before the timeline reaches the end
            elapsedtime = elapsedtime * 1.10;

            var totalhours = Math.ceil(elapsedtime / 3600);
            if (totalhours % 2 > 0) {
                totalhours += 1; //we are displaying multiple of 2hours (2h, 4h, 6h, 8h...)
            }
            var isFit = scaleFit(timelinesHashmap[tl].starttime, totalhours);
            var vbScale = (60 * 60 * totalhours);
            if (!isFit) {
                vbScale += 60 * 15; //add 15minutes if scale doesn't fit
            }

            timeline_html = '<div id="tl' + timelinesHashmap[tl].index + '"><div style="height:' + timelineHeight + 'px;width:100%;position:relative;"><figure class="figureanchor"><svg viewBox="0 0 ' + vbScale + ' ' + timelineHeight + '" preserveAspectRatio = "none" width="100%" height="' + timelineHeight + '"><use xlink:href="#svg-' + timelinesHashmap[tl].index + '"/></svg></figure></div>' + '<div style="width:100%;height:' + ruler_height + 'px;"><svg style="width:100%;height:' + ruler_height + 'px;">' + CreateOverlay(timelinesHashmap[tl].starttime, totalhours, isFit) + '</svg></div></div>';

        } catch (e) {
            console.log("Error in UpdateTimelineScale:" + e);
        }
        $('#tl' + timelinesHashmap[tl].index).replaceWith(timeline_html);
    }
}

var missedreloadcount = 0;

function ReloadData() {
    if (missedreloadcount < 5) {
        var xhttp = new XMLHttpRequest();

        xhttp.onreadystatechange = function () {
            if (xhttp.readyState == 4 && xhttp.status == 200) {
                var serverResponse = xhttp.responseText;
                var jsonResponse = JSON.parse(serverResponse);

                RefreshConfig(jsonResponse.Additional_infos[0].config_changed);

                for (var mach_it in jsonResponse.values) {
                    {
                        if (livestatusHashmap[jsonResponse.values[mach_it].machine].arrowangle != jsonResponse.values[mach_it].Trend + 90) {
                            UpdateIndicator(livestatusHashmap[jsonResponse.values[mach_it].machine].index, jsonResponse.values[mach_it]);
                            livestatusHashmap[jsonResponse.values[mach_it].machine].arrowangle = jsonResponse.values[mach_it].Trend + 90;
                        }

                        if (livestatusHashmap[jsonResponse.values[mach_it].machine].progr != jsonResponse.values[mach_it].progression) {
                            UpdateStatusCell(livestatusHashmap[jsonResponse.values[mach_it].machine].index, jsonResponse.values[mach_it]);
                            livestatusHashmap[jsonResponse.values[mach_it].machine].progr = jsonResponse.values[mach_it].progression;
                        }

                        if (livestatusHashmap[jsonResponse.values[mach_it].machine].status != jsonResponse.values[mach_it].status) {
                            UpdateIndicator(livestatusHashmap[jsonResponse.values[mach_it].machine].index, jsonResponse.values[mach_it]);
                            UpdateStatusCell(livestatusHashmap[jsonResponse.values[mach_it].machine].index, jsonResponse.values[mach_it]);
                            $('#status' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).text(jsonResponse.values[mach_it].status);
                            $('.row' + livestatusHashmap[jsonResponse.values[mach_it].machine].index + ' div.cell-text').css('border-color', GetStatusColor(jsonResponse.values[mach_it].status));
                            livestatusHashmap[jsonResponse.values[mach_it].machine].status = jsonResponse.values[mach_it].status;
                        }

                        if (livestatusHashmap[jsonResponse.values[mach_it].machine].fovr != jsonResponse.values[mach_it].feedOverride) {
                            UpdateMeter('fovr' + livestatusHashmap[jsonResponse.values[mach_it].machine].index, jsonResponse.values[mach_it].feedOverride);
                            livestatusHashmap[jsonResponse.values[mach_it].machine].fovr = (jsonResponse.values[mach_it].feedOverride ? jsonResponse.values[mach_it].feedOverride : 0);
                        }

                        if (livestatusHashmap[jsonResponse.values[mach_it].machine].sovr != jsonResponse.values[mach_it].SpindleOverride) {
                            UpdateMeter('sovr' + livestatusHashmap[jsonResponse.values[mach_it].machine].index, jsonResponse.values[mach_it].SpindleOverride);
                            livestatusHashmap[jsonResponse.values[mach_it].machine].sovr = (jsonResponse.values[mach_it].SpindleOverride ? jsonResponse.values[mach_it].SpindleOverride : 0);
                        }

                        if (livestatusHashmap[jsonResponse.values[mach_it].machine].part != jsonResponse.values[mach_it].PartNumber) {
                            $('#part' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).parent().removeClass('mymarquee');
                            $('#part' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).css('font-size', '1em');
                            $('#part' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).text(jsonResponse.values[mach_it].PartNumber.trim());

                            var textwidth = getWidthOfText($(this).text(), $('.cell-text').css('font-size')) + 5;
                            var containerwidth = $('.colpart').outerWidth(true) - parseFloat($('table tbody td.colpart').css('paddingLeft').replace("px", "")) - parseFloat($('table tbody td.colpart').css('paddingRight').replace("px", "")) - parseFloat($('.cell-text').css('border-left-width').replace("px", "")) - parseFloat($('.cell-text').css('border-right-width').replace("px", ""));

                            if (textwidth > containerwidth) {
                                if (textwidth * 0.75 > containerwidth) {
                                    $('#part' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).parent().addClass('mymarquee').hide().show(0);
                                } else {
                                    var ratio = (containerwidth / textwidth);
                                    $('#part' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).css('font-size', ratio.toFixed(4) + 'em');
                                }
                            }
                            livestatusHashmap[jsonResponse.values[mach_it].machine].part = jsonResponse.values[mach_it].PartNumber;
                        }

                        if (livestatusHashmap[jsonResponse.values[mach_it].machine].cnt != jsonResponse.values[mach_it].CycleCount) {
                            $('#cnt' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).text(jsonResponse.values[mach_it].CycleCount);
                            livestatusHashmap[jsonResponse.values[mach_it].machine].cnt = jsonResponse.values[mach_it].CycleCount;
                        }

                        $('#lastcyc' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).text(jsonResponse.values[mach_it].LastCycle);
                        $('#currcyc' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).text(jsonResponse.values[mach_it].CurrentCycle);
                        $('#elapsed' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).text(jsonResponse.values[mach_it].ElapsedTime);

                        if (livestatusHashmap[jsonResponse.values[mach_it].machine].shiftutil != jsonResponse.values[mach_it].Utilization && JSON.parse(jsonResponse.values[mach_it].Utilization) !== null) {
                            UpdateShiftUtilizationCell(livestatusHashmap[jsonResponse.values[mach_it].machine].index, jsonResponse.values[mach_it]);
                            livestatusHashmap[jsonResponse.values[mach_it].machine].shiftutil = jsonResponse.values[mach_it].Utilization;
                        }

                        if (livestatusHashmap[jsonResponse.values[mach_it].machine].alarm != jsonResponse.values[mach_it].alarm) {
                            UpdateIndicator(livestatusHashmap[jsonResponse.values[mach_it].machine].index, jsonResponse.values[mach_it]);
                            $('.row' + livestatusHashmap[jsonResponse.values[mach_it].machine].index + ' div.cell-text').removeClass('alarmblink').removeClass('warningblink');
                            if (GetBlinkClass(jsonResponse.values[mach_it].alarm).length > 0) {
                                $('.row' + livestatusHashmap[jsonResponse.values[mach_it].machine].index + ' div.cell-text').addClass(GetBlinkClass(jsonResponse.values[mach_it].alarm));
                            }

                            //Update the tooltip
                            $('#mach' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).removeClass('ttip');
                            $('#mach' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).attr('data-original-title', '');
                            if (jsonResponse.values[mach_it].alarm_details !== null) {
                                $('#mach' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).addClass('ttip');
                                var ttipstr = '';
                                var tmpalarms = JSON.parse(jsonResponse.values[mach_it].alarm_details);
                                for (var al in tmpalarms) {
                                    ttipstr += tmpalarms[al] + "<br>";
                                }
                                $('#mach' + livestatusHashmap[jsonResponse.values[mach_it].machine].index).attr('data-original-title', ttipstr);
                            }
                            livestatusHashmap[jsonResponse.values[mach_it].machine].alarm = jsonResponse.values[mach_it].alarm;
                        }
                    }
                }
                ActivateTooltips();

                updateDailyBest(jsonResponse.values);
                updateCurrentPerf(jsonResponse.values);
                updateTimeline(jsonResponse.values);
                updateLegend(jsonResponse.values);
                
                missedreloadcount = 0;
            }
        };
        xhttp.open("POST", newestMachinesRecordsUrl, false);
        try {
            xhttp.send();
        } catch (e) {
            console.log('We were unable to retrieve the latest data. Exception:' + e.message);
            missedreloadcount += 1;
        }
    } else {
		
        clearInterval(reloadtimer);
        dashboardUnavailable("Lost connection to server.");
        setInterval(TestConnection, 1000 * 15);  //Test connection every 15 seconds
    }
}
