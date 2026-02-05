
//Remove JSLint warning about JQuery
/*jslint browser: true, devel:true*/
//Declare global variables
/*global $, refreshrate, userConfig*/
//Declare global functions
/*global startTemperature, startTime, initializeData, initTimelines, initializeDoughnutChart, initializeStatusTable, initCurrentPerf, updateDoughnutChart, ReloadData, nextPage, dashboardUnavailable, updateUIOptions, InitLayout, nextLegend, nextGroups*/

var reloadtimer;


$(function () {
    
    //First get the enduserconfig    
    if (initializeData() === "ok") 
    {
        console.log("ok");
        $("#loading").hide();
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
        initTarget();
        activTooltip();
        InitLayout();
        //Display multiple pages of information
        updateDoughnutChart();
        nextLegend();
        nextGroups();
        nextTargets();
        nextPage();
        reloadtimer = setInterval(ReloadData, refreshrate);
    } 
    else if (initializeData() === "not configured")
    {
        document.getElementById('loadingTitle').innerHTML = "This device is not configured in CSIFLEX to display the dashboard .";
        document.getElementById('loading-image').style.display = 'none';
        console.log("not configured");
        setInterval(initializeData, 3000);
    }
    else if( initializeData() === "no response")
    {
        console.log("no response");
        document.getElementById('loadingTitle').innerHTML = "Server is unreachable .";
        document.getElementById('loading-image').style.display = 'none';
        //setTimeout(window.location.reload.bind(window.location), 50000 );
        setInterval(initializeData, 3000);
    }
});

