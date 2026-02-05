//Remove JSLint warning about JQuery
/*jslint browser: true, devel:true*/
//Declare global variables
/*global $, refreshrate, userConfig*/
//Declare global functions
/*global startTemperature, startTime, initializeData, initTimelines, initializeDoughnutChart, initializeStatusTable, initCurrentPerf, updateDoughnutChart, ReloadData, nextPage, dashboardUnavailable, updateUIOptions, InitLayout, nextLegend, nextGroups*/

var reloadtimer;

$(function () {
    //First get the enduserconfig    
    if (initializeData()) {
        if (userConfig[0].datetime === "on") {
            startTime();
        }
        if (userConfig[0].temperature === "on") {
            startTemperature();
        }

        updateUIOptions();

        //then retrieve and create the timelines
        initTimelines();
        initCurrentPerf();
        initializeStatusTable();
        initializeDoughnutChart();

        InitLayout();
        //Display multiple pages of information
        updateDoughnutChart();
        nextLegend();
        nextGroups();
        nextPage();
        reloadtimer = setInterval(ReloadData, refreshrate);
    } else {
        dashboardUnavailable("Server is unreachable or your dashboard is not configured.");
        setTimeout(window.location.reload.bind(window.location), (1000 * 60 * 5)); //Refresh after 5 minutes
    }
});
