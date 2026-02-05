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
var machinesConpercentHashmap = {};
var machinesCoffpercentHashmap = {};

function initCurrentPerf() {
    if (groups) {
        var contentOfCurrentPerfSection = "";
        for (var group_it = 0; group_it < currentPerfGroups.length; ++group_it) {
            contentOfCurrentPerfSection += '<div id="group' + group_it + '"><div style="text-align:center;" >' + currentPerfGroups[group_it] + '</div><div id="group' + group_it + 'content" class="progress rounded" style="height:' + heightOfTheCurrentPerfBars + 'em;"><div id="group' + group_it + 'con" class="progress-bar progress-bar-success hasTooltip" role="progressbar" title="" style="width:0%;background:' + GetStatusColor(CON) + ';"></div><div id="group' + group_it + 'coff" class="progress-bar progress-bar-danger hasTooltip" role="progressbar" style="width:100%;background:' + GetStatusColor(COFF) + ';" ></div></div></div>';
        }
        document.getElementById("currentPerfBodyid").innerHTML = contentOfCurrentPerfSection;
    }
}


function updateCurrentPerf(mach_values) {
    
    if (groups) {
        //Init default colors
        var CON_color = GetStatusColor(CON);
        var COFF_color = GetStatusColor(COFF);

        //Add the status for each machine in the hashmap
        for (var status_it in mach_values) {
            machinesHashmap[mach_values[status_it].machine].status = mach_values[status_it].status;
        }
        
        for (var group_it = 0; group_it < currentPerfGroups.length; ++group_it) {
     
            var contentOfCurrentPerfSection = "";
            var cycleOnPortion = 0;
            var conpercentage = 0;
            var coffpercentage = 0;

            //The special case when the name of the group to be displayed is "All Machines".
            if (currentPerfGroups[group_it] == "All Machines") 
            {
                cycleOnPortion = 0;

                for (var k = 0; k < mach_values.length; ++k) 
                {
                    if (mach_values[k].status == CON) 
                    {
                        ++cycleOnPortion;
                    }
                }
                conpercentage = (Math.round(cycleOnPortion * 100 / mach_values.length));
                coffpercentage = 100 - conpercentage;
            } 
            else 
            {
                cycleOnPortion = 0;

                var objectValue = groupsDefinition[currentPerfGroups[group_it]];
                
                var PbOnTooltip = '';
                var PbOffTooltip = '';
                
                for (var mach_it = 0; mach_it < groupsDefinition[currentPerfGroups[group_it]].length; ++mach_it) {
                    var machine = objectValue[mach_it];
                    
                    if (machinesHashmap[machine] !== undefined) 
                    {

                        if (machinesHashmap[machine].status == CON) 
                        {
                            ++cycleOnPortion;
                            PbOnTooltip += machine + '\n';
                        }
                        else
                        {
                            PbOffTooltip += machine + '\n';
                        }
                    }
                }
                
                
                conpercentage = Math.round(cycleOnPortion * 100 / (groupsDefinition[currentPerfGroups[group_it]].length));
                coffpercentage = 100 - conpercentage;

            }
            
            $('#group' + group_it + 'con').width(conpercentage + '%');
            $('#group' + group_it + 'coff').width(coffpercentage + '%');
            $('#group' + group_it + 'coff').attr('title',PbOffTooltip);
            $('#group' + group_it + 'con').attr('title',PbOnTooltip);
        }
    }
}

var DisplayableGroupsPerPage = 0;
var currentGroupsPage = 0;

function setGroupLength() {
    if (groups) {
        //Retrieve the padding of the body of the "CurrentPerformance" panel.
        var paddingOfTheBodyOfCurrentPerfPanel = parseInt($('#currentPerfBodyid').css("paddingTop").replace("px", "")) + parseInt($('#currentPerfBodyid').css("paddingBottom").replace("px", ""));
        var freeheight = $(window).height() - $('#top-bar').outerHeight() - $('#logoSection').outerHeight() - $('#msgFeedid').outerHeight() - $('#currentPerfPanelHead').outerHeight();

        if ($("#piechartid").is(":visible")) {
            freeheight -= $('#piechartid').outerHeight();
        }
        if($('#targetid').is(":visible")) {
            freeheight -= $('#targetid').outerHeight();
        }
            
            
        var reqHeight = $("#group0").outerHeight(true) + parseInt($('#group0content').css("margin-bottom").replace("px", ""));

        if ($(window).width() >= 1200) {
            DisplayableGroupsPerPage = Math.floor(freeheight / reqHeight);
            
        } else {
            DisplayableGroupsPerPage = currentPerfGroups.length;
        }

        currentGroupsPage = 0;
    }
}


function nextGroups() {
    if (groups) {
        for (var group_it = 0; group_it < currentPerfGroups.length; group_it++) {
            if (group_it >= (currentGroupsPage * DisplayableGroupsPerPage) && group_it <= (currentGroupsPage * DisplayableGroupsPerPage + DisplayableGroupsPerPage - 1)) {
                $("#group" + group_it).show();
            } else {
                $("#group" + group_it).hide();
            }
        }

        currentGroupsPage += 1;
        if (currentGroupsPage * DisplayableGroupsPerPage > currentPerfGroups.length - 1) {
            currentGroupsPage = 0;
        }
        setTimeout(nextGroups, currentperfdelay);
    }
}
