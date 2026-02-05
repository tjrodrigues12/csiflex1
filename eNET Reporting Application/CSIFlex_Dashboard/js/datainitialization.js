/*DATAINITIALIZING.JS*/
/*Script for retrieving once the data that will be required many times by others features of the dashboard.
 */

/*jslint browser: true, devel:true*/
//Declare global variables
/*global $, statusColorsHashmap, endUserConfigUrl, COFF, groups:true, lsGroups*/
//Declare global functions
/*global XMLHttpRequest, initRSSFeed, setweeklyMonthlyMachinesStatusHashmap*/


var userConfig; //This variable contains the user configuration.

var groupsDefinition; //machine list for each group

var currentPerfGroups;//list of the groups to display in the current performance

var machinesHashmap = {};//The list of machines names that appear in the machines records.
var groupsVSmachineHashmap = {};

function activTooltip() 
{
    $('.hasTooltip').tooltip({ html: true}); //hasTooltip is a class which added dynamically to the target.js file 
}

//Function for retrieving some initial data of the records.These data will be used for various operations.This function is called only one time
function initializeData() 
{
    var configok = false;
    var dashboardstatus = "ok";
    var xhttp = new XMLHttpRequest();   

    //Function for retrieve the currents status of all the machines and putting these status in an array
    xhttp.onreadystatechange = function () {
       console.log(xhttp.readyState + ' '  + xhttp.status);
        //console.log(xhttp.responseText);
        if (xhttp.readyState == 4 && xhttp.status == 200)  
        {
            var serverResponse = xhttp.responseText;
            var JSONserverResponse = JSON.parse(serverResponse);
            
            
            
            //check if dashboard is defined
            if (JSONserverResponse.config === undefined || JSONserverResponse.Additional_infos[0].Error === "Not Registered user/device" || JSONserverResponse.config.length <= 0 || JSONserverResponse.COLORS.length <= 0) 
            {
                //Dashboard is not configured
                configok = false; 
                dashboardstatus = "not configured";
            } 
            else 
            {
                //Retrieve the user configuration. 
                userConfig = JSONserverResponse.config;

                //Retrieve the groups of machines.
                groupsDefinition = JSONserverResponse.groups;


                //Retrieve all the status found in all of the records.
                var statusAndColors = JSONserverResponse.COLORS;
                //Fill the hashmap that does the mapping between status and colors.                
                for (var i = 0; i < statusAndColors.length; ++i) 
                {
                    statusColorsHashmap[statusAndColors[i].statut.toUpperCase()] = {
                        color: statusAndColors[i].color
                        };
                }

                var tmplist = JSONserverResponse.tbl_device_machines_groups[0].machines.split(",");  //make a list of all machines by group and split them with , 
                for (var str_it = 0; str_it < tmplist.length; str_it++) 
                { 
                    machinesHashmap[tmplist[str_it].trim()] = {
                        index: str_it,
                        status: COFF
                    };
                }

                //Remove machines from group defintion if they are not assigned to the dashboard (so those machines which are not selected in dashboard will not show to the CSIFlex Dashboard Page)
                for (var groupdef_it in groupsDefinition) // groupdef_it states groupdefinition number (means every group has a particular number assigned with it)
                {
                    for (var mach_it in groupsDefinition[groupdef_it])  //mach_it states machine number in a particular groupdefinition (means a machine in a group)
                    {
                        if (!(groupsDefinition[groupdef_it][mach_it] in machinesHashmap)) // This statement means if groupdefinition of particular machine in particular group not found  
                        { 
                            groupsDefinition[groupdef_it].splice(mach_it, 1);  // delete the duplicate group definition
                            // add a item in the hashmap
                            if (groupsDefinition[groupdef_it].toString() == '') // if there is no such groupdefinition found then we store groupsVSmachineHashmap to No Machines for a particular groupdefinition number
                            {
                                groupsVSmachineHashmap[groupdef_it] = 'No machines';
                            }
                            else
                            { //otherwise save the group definition 
                                groupsVSmachineHashmap[groupdef_it] = groupsDefinition[groupdef_it].toString().replace(',','\n');
                            }
                        }
                    }
                }

                //Retrieve the user configuration.
                initRSSFeed(JSONserverResponse.tbl_messages);
               // currentPerfGroups = JSONserverResponse.tbl_device_machines_groups[0].groups.replace(/, /g, ",").split(",");
                
                //Retrieve all the weekly and monthly cycle percentages of each machine.
                setweeklyMonthlyMachinesStatusHashmap(JSONserverResponse.perf);


                //Retrieve the groups of machines that will be displayed in the "Current Performance" section.
                try 
                {
                    currentPerfGroups = JSONserverResponse.tbl_device_machines_groups[0].groups.replace(/, /g, ",").split(",");
                    if (currentPerfGroups[0] === "" || groupsDefinition.length <= 0)
                    {
                        console.log('No group defined for this dashboard');
                        groups = false;
                    } 
                        else 
                    {
                        for (var group_it in currentPerfGroups) // group_it is an item in Current Performance Group
                        {
                            if (currentPerfGroups[group_it] != 'All Machines') 
                            {
                                lsGroups.push(currentPerfGroups[group_it]);
                            }
                        }
                    }
                } 
                catch (e) 
                {
                    console.log('There was an error trying to parse the groups:' + e.message);
                    groups = false;
                }

                configok = true;
            }
        }
        
        

    };

    xhttp.open("POST", endUserConfigUrl, false);  // endUserConfigUrl means Url that is generated by eNETDNC on the server 
    try 
    {
        xhttp.send();
    } 
    catch (e) 
    {
        console.log(e.message);
        configok = false;
        dashboardstatus = "no response";
    }

    return dashboardstatus;
    
}

