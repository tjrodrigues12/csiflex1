/*DATAINITIALIZING.JS*/
/*Script for retrieving once the data that will be required many times by others features of the dashboard.
 */

/*jslint browser: true, devel:true*/
//Declare global variables
/*global $, statusColorsHashmap, endUserConfigUrl, COFF, groups:true, lsGroups*/
//Declare global functions
/*global XMLHttpRequest, initRSSFeed, setweeklyMonthlyMachinesStatusHashmap*/


//This variable contains the user configuration.
var userConfig;
//machine list for each group
var groupsDefinition;
//list of the groups to display in the current performance
var currentPerfGroups;
//The list of machines names that appear in the machines records.
var machinesHashmap = {};

//Function for retrieving some initial some data of the records.These data will be used for various operations.This function is called only one time
function initializeData() {
    var configok = false;
    var xhttp = new XMLHttpRequest();

    //Function for retrieve the currents status of all the machines and putting these status in an array
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            var serverResponse = xhttp.responseText;
            var JSONserverResponse = JSON.parse(serverResponse);

            //check if dashboard is defined
            if (JSONserverResponse.config === undefined || JSONserverResponse.config.length <= 0 || JSONserverResponse.COLORS.length <= 0) {
                configok = false; //Dashboard is not configured
            } else {
                //Retrieve the user configuration. 
                userConfig = JSONserverResponse.config;

                //Retrieve the groups of machines.
                groupsDefinition = JSONserverResponse.groups;


                //Retrieve all the status found in all of the records.
                var statusAndColors = JSONserverResponse.COLORS;
                //Fill the hashmap that does the mapping between status and colors.                
                for (var i = 0; i < statusAndColors.length; ++i) {
                    statusColorsHashmap[statusAndColors[i].statut.toUpperCase()] = {
                        color: statusAndColors[i].color
                    };
                }

                var tmplist = JSONserverResponse.tbl_device_machines_groups[0].machines.split(",");
                for (var str_it = 0; str_it < tmplist.length; str_it++) {
                    machinesHashmap[tmplist[str_it].trim()] = {
                        index: str_it,
                        status: COFF
                    };
                }

                //Remove machines from group defintion if they are not assigned to the dashboard
                for (var groupdef_it in groupsDefinition) {
                    for (var mach_it in groupsDefinition[groupdef_it]) {
                        if (!(groupsDefinition[groupdef_it][mach_it] in machinesHashmap)) {
                            groupsDefinition[groupdef_it].splice(mach_it, 1);
                        }
                    }
                }

                //Retrieve the user configuration.
                initRSSFeed(JSONserverResponse.tbl_messages);

                //Retrieve all the weekly and monthly cycle percentages of each machine.
                setweeklyMonthlyMachinesStatusHashmap(JSONserverResponse.perf);


                //Retrieve the groups of machines that will be displayed in the "Current Performance" section.
                try {
                    currentPerfGroups = JSONserverResponse.tbl_device_machines_groups[0].groups.replace(/, /g, ",").split(",");
                    if (currentPerfGroups[0] === "" || groupsDefinition.length <= 0) {
                        console.log('No group defined for this dashboard');
                        groups = false;
                    } else {
                        for (var group_it in currentPerfGroups) {
                            if (currentPerfGroups[group_it] != 'All Machines') {
                                lsGroups.push(currentPerfGroups[group_it]);
                            }
                        }
                    }
                } catch (e) {
                    console.log('There was an error trying to parse the groups:' + e.message);
                    groups = false;
                }

                configok = true;
            }
        }
    };

    xhttp.open("POST", endUserConfigUrl, false);
    try {
        xhttp.send();
    } catch (e) {
        configok = false;
    }

    return configok;
}
