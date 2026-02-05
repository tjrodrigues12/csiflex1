/*jslint browser: true, devel:true */
//Declare global variables
/*global $, userConfig, iframedelay, livestatusdelay:true,  timelineEnabled:true, trendEnabled:true, legendminheight:true, legendmodulo:true, rowqty, currentPerfGroups, DisplayableGroupsPerPage, livestatusHashmap, groups, lstimer:true, paused, lowanimation:true, dateformat:true*/
//Declare global functions
/*global setPageLength, setGroupLength, nextPage, refreshTemperature, UpdateTimelineScale, Start, FixPartOverflow, ShowMediaCtrl*/

function updateUIOptions() {
    dateformat = userConfig[0].dateformat;
   
    
    if (userConfig[0].devicetype === "LR1") {
        lowanimation = true;
    }

    if (userConfig[0].livestatusdelay === "on" && userConfig[0].detail_livestatusdelay !== null) {
        livestatusdelay = parseInt(userConfig[0].detail_livestatusdelay) * 1000;
    }

    if (userConfig[0].scale != 100) {
        var emscale = parseInt(userConfig[0].scale) / 100;
        $('body').css('font-size', emscale + 'em');
    }

    if (userConfig[0].temperature === "off") {
        $("#temperatureSection").hide();
    }

    if (userConfig[0].messages === "off") {
        $("#msgFeedid").hide();
    }
    
    //console.log(userConfig[0].DisplayWhat);
    //console.log(userConfig[0].piechart);
    if (!groups) {
        $('#currentperfid').hide();
         $('#targetid').hide();
    }
    else {
        if(userConfig[0].DisplayWhat === "DisplayPerf")
           {
               $('#targetid').hide();
               //console.log(userConfig[0].DisplayWhat);
    //console.log(userConfig[0].piechart);
           }
        else if(userConfig[0].DisplayWhat === "DisplayTarget")
            {
                $('#currentperfid').css('visibility' ,'hidden');
                
                //console.log('group, display target');
            }
   
        else if(userConfig[0].DisplayWhat === "none")
            {
                $('#currentperfid').hide();
                $('#targetid').hide();
                //console.log(userConfig[0].DisplayWhat);
                console.log(userConfig[0].piechart);
            }
    }
    if (userConfig[0].piechart === "off") {
        $("#piechartid").hide();
    } else {
        if (userConfig[0].piecharttime === "Weekly") {
            $("#currentPeriod").html("Current Week");
        } else {
            $("#currentPeriod").html("Current Month");
        }
    }

    if (userConfig[0].lastcycle === "off") {
        $('.collastcyc').hide();
    }

    if (userConfig[0].currentcycle === "off") {
        $('.colcurcyc').hide();
    }

    if (userConfig[0].elapsedtime === "off") {
        $('.colelapsed').hide();
    }

    $('.timeline').attr('colspan', $('th:visible').length);

    if (userConfig[0].datetime === "off") {
        //Hidding the div would change the bootstrap layout
        $('#time').attr("id", "falseID");
        $('#falseID').text('');
    }

    if (userConfig[0].customlogo === "on" && userConfig[0].detail_customlogo !== null) {
        $("#customImg").attr("src", "img/" + userConfig[0].detail_customlogo);
    }

    if (userConfig[0].IFrame === "on") {
        $("#iframeid").attr('src', userConfig[0].detail_IFrame);
        if (userConfig[0].rotation === "off") {
            $("#liveStatusTable").hide();
            $("#iframeid").show();
            //Hidding the div would change the bootstrap layout
            //We change the id so no child node would be added
            $('#LegendSection').attr("id", "falseID2");
        }
    }

    if (userConfig[0].fullscreen === "on" || (!$("#piechartid").is(":visible") && !$("#currentperfid").is(":visible"))) {
        $("#sidebar").hide();
        $("#liveStatus").attr('class', 'col-lg-12');
    }

    timelineEnabled = (userConfig[0].timeline === 'False' ? false : true);
    trendEnabled = (userConfig[0].trends === 'False' ? false : true);

    UpdateTimelineScale();
    
    
}

var livestatusdisplayed = true;

function panelRotation() {
    if (livestatusdisplayed) {
        $(window).unbind('mousemove', ShowMediaCtrl);
        $("#liveStatusTable").hide();
        $("#iframeid").show();
        livestatusdisplayed = false;
        setTimeout(panelRotation, iframedelay);
    } else {
        $("#iframeid").hide();
        $("#liveStatusTable").show();
        livestatusdisplayed = true;
        $(window).bind('mousemove', ShowMediaCtrl);
        nextPage();
    }
}

function resizeIframe(obj) {
    var freeheight = $(window).height() - $("#top-bar").outerHeight() - $("#logoSection").outerHeight() - $("#msgFeedid").outerHeight() - 20; //20px extra padding    
    obj.style.height = freeheight + 'px';
}


function resized() {
    $("#LegendSection").css("min-height", 0);
    legendminheight = 0; //reset legend min height
    var w = $(window).width();

    if (w < 1400) {
        $(".collastcyc").hide();
    } else if (userConfig[0].lastcycle === "on") {
        $(".collastcyc").show();
    }

    if (w < 1200) {
        $("#piechartid").hide();
    } else if (userConfig[0].piechart === "on") {
        $("#piechartid").show();
    }

    if (w < 1000) {
        $('.colcurcyc').hide();
        legendmodulo = 6;
    } else if (userConfig[0].currentcycle === "on") {
        $('.colcurcyc').show();
        legendmodulo = 8;
    }

    if (w < 950) {
        $('.colfovr').hide();
        $('.colsovr').hide();
        $('.colelapsed').hide();
    } else {
        $('.colfovr').show();
        $('.colsovr').show();
        if (userConfig[0].elapsedtime === "on") {
            $('.colelapsed').show();
        }
    }

    if (w < 860) {
        $('.colshiftutil').hide();
    } else {
        $('.colshiftutil').show();
    }

    if (w < 780) {
        //top bar is hidden at this point
        $('body').animate({
            paddingTop: 0
        });
    } else {
        $('body').animate({
            paddingTop: $('#top-bar').outerHeight()
        });
    }

    if (w < 725) {
        $('.colcnt').hide();
    } else {
        $('.colcnt').show();
    }

    if (w < 580) {
        $('.colpart').hide();
    } else {
        $('.colpart').show();
    }

    $('.timeline').attr('colspan', $('th:visible').length);
    UpdateTimelineScale();

    refreshTemperature();

    FixPartOverflow();

    if (paused) {
        Start();
    }

}

function InitLayout() {
    if (detectIE()) {
        $('#liveStatusTable').css('table-layout', 'auto');
    }
    resized();

    var cnt = 0;
    for (var machine in livestatusHashmap) {
        if (cnt < rowqty) {
            $('.row' + livestatusHashmap[machine].index).show();
        } else {
            $('.row' + livestatusHashmap[machine].index).hide();
        }
        cnt += 1;
    }

    if (groups) {
        setGroupLength();
        setTargetLength();
        for (var group_it = 0; group_it < currentPerfGroups.length; group_it++) {
            if (group_it < DisplayableGroupsPerPage) {
                $("#group" + group_it).show();
            } else {
                $("#group" + group_it).hide();
            }
        }
    }
    //recall UpdateUIOptions
    updateUIOptions();
}


var resizeTimer;
$(window).resize(function () {
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(function () {
        setPageLength();
        resized();
        //setTimeout is used to let the piechart resize before calculating its height
        setTimeout(setGroupLength, 10);
        setTimeout(setTargetLength, 10);
        //RESET SCROLL
        $(this).scrollTop(0);
    }, 300);
});

function dashboardUnavailable(message) {
    $("#temperatureSection").hide();
    $('#time').attr("id", "falseID");
    $('#falseID').text('');

    //$("#LegendSection").hide();
    $('#LegendSection').attr("id", "falseID2");
    $('#falseID2').text('');

    //logo padding
    $("#logoSection").css("paddingTop", $('#top-bar').outerHeight());
    $(window).unbind('mousemove', ShowMediaCtrl);

    var msgfeednode = document.getElementById("msgFeedid");
    while (msgfeednode.firstChild) {
        msgfeednode.removeChild(msgfeednode.firstChild);
    }

    var panelnode = document.getElementById("panelSection");
    while (panelnode.firstChild) {
        panelnode.removeChild(panelnode.firstChild);
    }

    var divnode = document.createElement('div');
    divnode.style.cssText = 'text-align:center;';
    var h = document.createElement("H1"); // Create a <h1> element
    var textnode = document.createTextNode(message); // Create a text node
    h.appendChild(textnode);
    divnode.appendChild(h);
    panelnode.appendChild(divnode);
}

//detect IE returns version of IE or false, if browser is not Internet Explorer
function detectIE() {
    var ua = window.navigator.userAgent;

    var msie = ua.indexOf('MSIE ');
    if (msie > 0) {
        // IE 10 or older => return version number
        return parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
    }

    var trident = ua.indexOf('Trident/');
    if (trident > 0) {
        // IE 11 => return version number
        var rv = ua.indexOf('rv:');
        return parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
    }

    var edge = ua.indexOf('Edge/');
    if (edge > 0) {
        // Edge (IE 12+) => return version number
        return parseInt(ua.substring(edge + 5, ua.indexOf('.', edge)), 10);
    }

    // other browser
    return false;
}
