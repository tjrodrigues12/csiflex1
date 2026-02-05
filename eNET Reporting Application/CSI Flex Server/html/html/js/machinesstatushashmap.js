/*MACHINESSTATUSHASHMAP.JS*/

/*jslint browser: true, devel: true */

//Declare global variable
/*global machinesPerf, machinesHashmap, userConfig*/
//Declare global functions
/*global updateWeeklyBestRSS, updateMonthlyBestRSS*/


//This function sets the "weeklyMonthlyMachinesStatusHashmap" variable.
function setweeklyMonthlyMachinesStatusHashmap(performance) {
    if (performance !== undefined || performance.length > 0) {
        //Fill the hashmap.Associate a corresponding weekly
        for (var perf_it = 0; perf_it < performance.length; ++perf_it) {
            var weeklyJsonVar;
            var monthlyJsonVar;

            try {
                weeklyJsonVar = JSON.parse(performance[perf_it].weekly_);
                monthlyJsonVar = JSON.parse(performance[perf_it].monthly_);


                //console.log(weeklyJsonVar);
                if (weeklyJsonVar.length > 0 && monthlyJsonVar.length > 0 && (performance[perf_it].machinename_ in machinesHashmap)) {
                    var obj = {
                        machine: performance[perf_it].machinename_,
                        WeeklyJson: weeklyJsonVar,
                        MonthlyJson: monthlyJsonVar
                    };
                    machinesPerf.push(obj);
                }
            } catch (err) {
                console.log("Error in machine data for machine " + performance[perf_it].machinename_);
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
