/*CURRENTPERF.JS*/
//Calculate Cycle On % of live status per groups

//Remove warning about document usage
/*jslint browser:true, devel:true */

//Declare global variables
/*global $, CON, COFF, currentPerfGroups, groupsDefinition, currentperfdelay, machinesHashmap, groups*/
//Declare post defined functions
/*global GetStatusColor*/

//Height of the bars that are displayed in the "CurrentPerformance" section.
var heightOfTheCurrentPerfBars = 2.2;

function conPercent() {
    
    
    
}

function initTarget() {
    if (groups) {
        var contentOfCurrentPerfSection = "";
        if(targetMach.length <= 0)
        {
            contentOfCurrentPerfSection = "<div>No group selected</div>";
        }
        else
        {
            for (var group_it = 0; group_it < targetMach.length; ++group_it) {
                if(targetTitle[group_it].WeeklyTitle === "" || targetTitle[group_it].WeeklyTitle === null || targetTitle[group_it].MonthlyTitle=== "" || targetTitle[group_it].MonthlyTitle === null)
                {
                    // Do nothing here This Means Target is Not Calulated for particular group
                }
                else
                {
                    if (userConfig[0].piecharttime === "Weekly"){

                        var t = targetTitle[group_it].WeeklyTitle.split('of')[1].trim();
                        t= t.replace('h','');
                        var conpercent ;
                        if(t === 0)
                        {
                            conpercent = 0;
                        }
                        else
                        {
                            var weeklyCycleOn = targetMach[group_it].WeeklyJson.filter(w => w.status === 'CYCLE ON')[0]
                            conpercent = weeklyCycleOn.cycletime * 100 / t
                        }
                        var coffpercent = 100 - conpercent; // Green : rgb(77, 179, 77), Red : rgb(255, 0, 0);

                        // contentOfCurrentPerfSection += '<div id="groupTarget' + group_it + '"><div style="text-align:center;"> ' + targetMach[group_it].machine + '</div><div id="groupTarget' + group_it + 'content" class="progress rounded" style="height:' + heightOfTheCurrentPerfBars + 'em;"> <div class="progress-info" style="color:white;font-weight:bolder; text-align:center;line-height: 2.5;">'+targetTitle[group_it].WeeklyTitle+ '</div><div id="groupTarget' + group_it + 'con" class="progress-bar progress-bar-success" role="progressbar" style="width:'+conpercent +'%;background:' + GetStatusColor(CON) + ';"></div><div id="groupTarget' + group_it + 'coff" class="progress-bar progress-bar-danger" role="progressbar" style="width:'+coffpercent+'%;background:' + GetStatusColor(COFF) + ';"></div></div></div>';// Original : 

                        contentOfCurrentPerfSection += `
                            <div id="groupTarget${group_it}">
                                <div style="text-align:center;">${targetMach[group_it].machine}..</div>
                            
                                <div id="groupTarget${group_it}content" class="progress rounded" style="height:${heightOfTheCurrentPerfBars}em;">
                                    <div class="progress-info" style="color:white;font-weight:bolder; text-align:center;line-height: 2.5;">
                                        ${targetTitle[group_it].WeeklyTitle}
                                    </div>
                                    <div id="groupTarget${group_it}con" class="progress-bar progress-bar-success" role="progressbar" style="width:${conpercent}%;background:${GetStatusColor(CON)};"></div>
                                    <div id="groupTarget${group_it}coff" class="progress-bar progress-bar-danger" role="progressbar" style="width:${coffpercent}%;background:${GetStatusColor(COFF)};"></div>
                                </div>
                            </div>
                        `
                }
                else if (userConfig[0].piecharttime === "Monthly") {
                        var t = targetTitle[group_it].MonthlyTitle.split('of')[1].trim();

                        t= t.replace('h','');

                        var conpercent ;
                        
                        if(t === 0)
                        {
                            conpercent = 0;
                        }
                        else
                        {
                            var monthlyCycleOn = targetMach[group_it].MonthlyJson.filter(w => w.status === 'CYCLE ON')[0]
                            conpercent = monthlyCycleOn.cycletime * 100 / t
                        }
                        var coffpercent = 100- conpercent;
                        
                        // contentOfCurrentPerfSection += '<div id="groupTarget' + group_it + '"><div style="text-align:center;"> ' + targetMach[group_it].machine + '</div><div id="groupTarget' + group_it + 'content" class="progress rounded" style="height:' + heightOfTheCurrentPerfBars + 'em;"><div class="progress-info" style="color:white;font-weight:bolder;vertical-align: middle;line-height: 2.5;">'+targetTitle[group_it].MonthlyTitle+'</div><div id="groupTarget' + group_it + 'con" class="progress-bar progress-bar-success" role="progressbar" style="width:'+conpercent +'%;background:' + GetStatusColor(CON) + ';"></div><div id="groupTarget' + group_it + 'coff" class="progress-bar progress-bar-danger" role="progressbar" style="width:'+coffpercent+'%;background:' + GetStatusColor(COFF) + ';"></div></div></div>'; //Original 

                        contentOfCurrentPerfSection += `
                            <div id="groupTarget${group_it}">
                                <div style="text-align:center;">${targetMach[group_it].machine}..</div>
                            
                                <div id="groupTarget${group_it}content" class="progress rounded" style="height:${heightOfTheCurrentPerfBars}em;">
                                    <div class="progress-info" style="color:white;font-weight:bolder; text-align:center;line-height: 2.5;">
                                        ${targetTitle[group_it].MonthlyTitle}
                                    </div>
                                    <div id="groupTarget${group_it}con" class="progress-bar progress-bar-success" role="progressbar" style="width:${conpercent}%;background:${GetStatusColor(CON)};"></div>
                                    <div id="groupTarget${group_it}coff" class="progress-bar progress-bar-danger" role="progressbar" style="width:${coffpercent}%;background:${GetStatusColor(COFF)};"></div>
                                </div>
                            </div>
                        `
                    }
                }
            }
        }
        document.getElementById('targetsPeriod').innerHTML = userConfig[0].piecharttime + ' ';
        document.getElementById("targetBodyId").innerHTML = contentOfCurrentPerfSection;
    }
}


var DisplayableTargetsPerPage = 0;
var currentTargetsPage = 0;

function setTargetLength() {
    if (groups) {
        //Retrieve the padding of the body of the "CurrentPerformance" panel.
        var paddingOfTheBodyOfTargetPanel = parseInt($('#targetBodyId').css("paddingTop").replace("px", "")) + parseInt($('#targetBodyId').css("paddingBottom").replace("px", ""));
        var freeheight = $(window).height() - $('#top-bar').outerHeight() - $('#logoSection').outerHeight() - $('#msgFeedid').outerHeight() - $('#currentperfid').outerHeight() - $('#targetPanelHead').outerHeight() - paddingOfTheBodyOfTargetPanel - 20;

        if ($("#piechartid").is(":visible")) {
            freeheight -= $('#piechartid').outerHeight();
        }
        var reqHeight = $("#groupTarget0").outerHeight() + parseInt($('#groupTarget0content').css("margin-bottom").replace("px", ""));

        if ($(window).width() >= 1200) {
            DisplayableTargetsPerPage = Math.floor(freeheight / reqHeight);
        } else {
            DisplayableTargetsPerPage = targetMach.length;
        }

        currentTargetsPage = 0;
    }
}


function nextTargets() {
    if (groups) {
        for (var group_it = 0; group_it < targetMach.length; group_it++) {
            if (group_it >= (currentTargetsPage * DisplayableTargetsPerPage) && group_it <= (currentTargetsPage * DisplayableTargetsPerPage + DisplayableTargetsPerPage - 1)) {
                $("#groupTarget" + group_it).show();
            } else {
                $("#groupTarget" + group_it).hide();
            }
        }

        currentTargetsPage += 1;
        if (currentTargetsPage * DisplayableTargetsPerPage > targetMach.length - 1) {
            currentTargetsPage = 0;
        }
        setTimeout(nextTargets, currentperfdelay);
    }
}

