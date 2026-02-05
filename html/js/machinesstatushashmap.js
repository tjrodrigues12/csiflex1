/*MACHINESSTATUSHASHMAP.JS*/

/*jslint browser: true, devel: true */

//Declare global variable
/*global machinesPerf, machinesHashmap, userConfig*/
//Declare global functions
/*global updateWeeklyBestRSS, updateMonthlyBestRSS*/


// OLd Code This function sets the "weeklyMonthlyMachinesStatusHashmap" variable.
// function setweeklyMonthlyMachinesStatusHashmap(performance) {
//     if (performance !== undefined || performance.length > 0) {
//         //Fill the hashmap.Associate a corresponding weekly
//         for (var perf_it = 0; perf_it < performance.length; ++perf_it) {
//             var weeklyJsonVar;
//             var monthlyJsonVar;

//             try {
//                 weeklyJsonVar = JSON.parse(performance[perf_it].weekly_);
//                 monthlyJsonVar = JSON.parse(performance[perf_it].monthly_);


//                 //console.log(weeklyJsonVar);
//                 if (weeklyJsonVar.length > 0 && monthlyJsonVar.length > 0 && (performance[perf_it].machinename_ in machinesHashmap)) {
//                     var obj = {
//                         machine: performance[perf_it].machinename_,
//                         WeeklyJson: weeklyJsonVar,
//                         MonthlyJson: monthlyJsonVar
//                     };
//                     machinesPerf.push(obj);
//                 }
//             } catch (err) {
//                 console.log("Error in machine data for machine " + performance[perf_it].machinename_);
//             }
//         }
//         UpdateWeeklyMonthlyBest();
//     } else {
//         userConfig[0].piechart = 'off';
//     }
// }

// NEW CODE
function setweeklyMonthlyMachinesStatusHashmap(performance, groupdata) {

    var currentPerfGroups = groupdata;
    if (performance !== undefined || performance.length > 0) {
        //Fill the hashmap.Associate a corresponding weekly
        for (var perf_it = 0; perf_it < performance.length; ++perf_it) {
            var weeklyJsonVar;
            var monthlyJsonVar;

            try {
                // currentPerfGroups = JSONserverResponse.tbl_device_machines_groups[0].groups.replace(/, /g, ",").split(",");
                if (performance[perf_it].machinename_.startsWith('TitleTarget_Grp_')) {
                    var grp_name = performance[perf_it].machinename_.replace('TitleTarget_', '');

                    var title = {
                        machine: performance[perf_it].machinename_.replace('TitleTarget_Grp_', ''),
                        WeeklyTitle: performance[perf_it].weekly_.replace(grp_name, ''),
                        MonthlyTitle: performance[perf_it].monthly_.replace(grp_name, '')

                    };

                    for (var group_it in currentPerfGroups) {
                        if (currentPerfGroups[group_it] != 'All Machines') {
                            if (title.machine === currentPerfGroups[group_it]) {
                                targetTitle.push(title);
                            }
                        }
                    }
                    // if (title.machine!="")
                    // {
                    //     targetTitle.push(title);
                    // }
                }
                else {

                    weeklyJsonVar = JSON.parse(performance[perf_it].weekly_);
                    monthlyJsonVar = JSON.parse(performance[perf_it].monthly_);

                    if ((weeklyJsonVar.length > 0 && monthlyJsonVar.length > 0 && (performance[perf_it].machinename_ in machinesHashmap)) || performance[perf_it].machinename_.startsWith('Sec_Grp_') || performance[perf_it].machinename_.startsWith('Grp_')) {

                        if (performance[perf_it].machinename_.startsWith('Grp_')) {
                            var machinename1 = performance[perf_it].machinename_.replace('Grp_', '');
                            var obj1 = {
                                machine: machinename1,
                                WeeklyJson: weeklyJsonVar,
                                MonthlyJson: monthlyJsonVar

                            };

                            for (var group_it in currentPerfGroups) {
                                if (currentPerfGroups[group_it] != 'All Machines') {
                                    if (machinename1 === currentPerfGroups[group_it]) {
                                        machinesPerf.push(obj1);
                                    }
                                }
                            }
                        }
                        else if (performance[perf_it].machinename_.startsWith('Sec_Grp_')) {
                            var machinename1 = performance[perf_it].machinename_.replace('Sec_Grp_', '');
                            var mach = {
                                machine: machinename1,
                                WeeklyJson: weeklyJsonVar,
                                MonthlyJson: monthlyJsonVar

                            };
                            if (currentPerfGroups[0] === "" || groupsDefinition.length <= 0 || currentPerfGroups === null) {
                                console.log('No group defined for this dashboard');
                                groups = false;
                            } else {
                                for (var group_it in currentPerfGroups) {
                                    if (currentPerfGroups[group_it] != 'All Machines') {
                                        if (machinename1 === currentPerfGroups[group_it]) {
                                            targetMach.push(mach);
                                        }
                                    }
                                }
                                // if(mach.machine !="")
                                // {
                                //     targetMach.push(mach);
                                // }
                            }


                        }
                        else{
                            var obj = {
                                machine: performance[perf_it].machinename_,
                                WeeklyJson: weeklyJsonVar,
                                MonthlyJson: monthlyJsonVar

                            };

                            machinesPerf.push(obj);

                        }
                            
                    }
                }

            } catch (err) {
                console.log("Error in machine data for machine " + performance[perf_it].machinename_ + 'ERROR: ' + err.message);
            }
        }
        UpdateWeeklyMonthlyBest();
    } else {
        userConfig[0].piechart = 'off';
    }
}
function UpdateWeeklyMonthlyBest() {
    var tmpWCON = 0,
        tmpWMachine,
        tmpMCON = 0,
        tmpMMachine,
        machine_it = 0;
    while (machine_it < machinesPerf.length) {
        try {

            if (machinesPerf[machine_it].WeeklyJson[1].cycletime > tmpWCON) {
                tmpWCON = machinesPerf[machine_it].WeeklyJson[1].cycletime;
                tmpWMachine = machinesPerf[machine_it].machine;
            }

            if (machinesPerf[machine_it].MonthlyJson[1].cycletime > tmpMCON) {
                tmpMCON = machinesPerf[machine_it].MonthlyJson[1].cycletime;
                tmpMMachine = machinesPerf[machine_it].machine;
            }
        } catch (err) {
            console.log("Error updating Weekly/Monthly best:" + err);
        }
        machine_it += 1;
    }

    updateWeeklyBestRSS(tmpWMachine, tmpWCON);
    updateMonthlyBestRSS(tmpMMachine, tmpMCON);
}
