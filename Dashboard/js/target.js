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




function initTarget() {
    var PbTextColor;
    var conpercent;
    var coffpercent;
    var PbInnerText;
    
    if (groups) 
    {
        var contentOfTargetSection = "";
        if (targetMach.length <= 0)
        {
            contentOfTargetSection = "<div>No group selected</div>";
        }
        else
        {
            console.log(targetMach.length);
            for (var group_it = 0; group_it < targetMach.length; ++group_it) 
            {
                if (userConfig[0].piecharttime === "Weekly"){
                    var t = targetTitle[group_it].WeeklyTitle.split('of')[1].trim();
                    t= t.replace('h','');
                    
                    if(t === 0)
                    {
                        conpercent = 0;
                        coffpercent = 0;
                        PbTextColor = "black";
                        PbInnerText = groupsVSmachineHashmap[targetMach[group_it].machine];
                    }
                    else
                    {
                        conpercent = targetMach[group_it].WeeklyJson[0].cycletime * 100 / t;
                        PbTextColor = "white";
                        coffpercent = 100- conpercent;
                        PbInnerText = targetTitle[group_it].WeeklyTitle;
                    }
                    
                    contentOfTargetSection += '<div id="groupTarget' + group_it + '" > <div style="text-align:center; white-space: pre-line;" title="'+ groupsVSmachineHashmap[targetMach[group_it].machine] +'" class="hasTooltip"> ' + targetMach[group_it].machine + '</div><div id="groupTarget' + group_it + 'content" class="progress rounded" style="height:' + heightOfTheCurrentPerfBars + 'em;"> <div class="progress-info" style="color:'+ PbTextColor +'; vertical-align: middle;line-height: 2.5;">'+PbInnerText+ '</div><div id="groupTarget' + group_it + 'con" class="progress-bar progress-bar-success" role="progressbar" style="width:'+conpercent +'%;background:' + GetStatusColor(CON) + ';"></div><div id="groupTarget' + group_it + 'coff" class="progress-bar progress-bar-danger" role="progressbar" style="width:'+coffpercent+'%;background:' + GetStatusColor(COFF) + ';"></div></div></div>';
                }
                else if (userConfig[0].piecharttime === "Monthly") {
                    var t = targetTitle[group_it].MonthlyTitle.split('of')[1].trim();
                    t= t.replace('h','');
                    
                    if(t == 0)
                    {
                        conpercent = 0;
                        coffpercent = 0;
                        PbTextColor = "black";
                        PbInnerText = groupsVSmachineHashmap[targetMach[group_it].machine];
                    }
                    else
                    {
                        conpercent = targetMach[group_it].MonthlyJson[0].cycletime * 100 / t;
                        PbTextColor = "white";
                        coffpercent = 100- conpercent;
                        PbInnerText = targetTitle[group_it].MonthlyTitle;
                    }
                    
                    
                    contentOfTargetSection += '<div id="groupTarget' + group_it + '"><div style="text-align:center; white-space: pre-line;"  data-html="true" title="'+ groupsVSmachineHashmap[targetMach[group_it].machine] +'" class="hasTooltip">  ' + targetMach[group_it].machine + '</div><div  id="groupTarget' + group_it + 'content" class="progress rounded" style="height:' + heightOfTheCurrentPerfBars + 'em;"><div class="progress-info" style="color:'+ PbTextColor +'; vertical-align: middle;line-height: 2.5;">'+PbInnerText+'</div><div id="groupTarget' + group_it + 'con" class="progress-bar progress-bar-success" role="progressbar" style="width:'+conpercent +'%;background:' + GetStatusColor(CON) + ';"></div><div id="groupTarget' + group_it + 'coff" class="progress-bar progress-bar-danger" role="progressbar" style="width:'+coffpercent+'%;background:' + GetStatusColor(COFF) + ';"></div></div></div>';
                 }
            
            
            }
        }
        document.getElementById('targetsPeriod').innerHTML = userConfig[0].piecharttime + ' ';
        document.getElementById("targetBodyId").innerHTML = contentOfTargetSection;
        
    }
    
}


var DisplayableTargetsPerPage = 0;
var currentTargetsPage = 0;

function setTargetLength() {
    if (groups) {
                
        //Retrieve the padding of the body of the "CurrentPerformance" panel.
        var paddingOfTheBodyOfTargetPanel = parseInt($('#targetBodyId').css("paddingTop").replace("px", "")) + parseInt($('#targetBodyId').css("paddingBottom").replace("px", ""));
        var freeheight = $(window).height() - $('#top-bar').outerHeight() - $('#logoSection').outerHeight() - $('#msgFeedid').outerHeight() - $('#targetPanelHead').outerHeight();

        if ($("#piechartid").is(":visible")) {
            freeheight -= $('#piechartid').outerHeight();
        }
        
        if($("#currentperfid").is(":visible")) {
            freeheight -= $('#currentperfid').outerHeight();
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

