//GLOBAL.JS
//This file contains global functions and variables
/*jslint browser: true, devel:true */

//CONSTANTS
var CON = "CYCLE ON";
var COFF = "CYCLE OFF";
var SETUP = "SETUP";
var endUserConfigUrl ="http://10.0.10.186:8008/enduserconfig";
var newestMachinesRecordsUrl ="http://10.0.10.186:8008/refresh";
var timelineUrl = "http://10.0.10.186:8008/timeline";
var refreshrate = 1000; //milliseconds
var livestatusdelay = 6000;
var iframedelay = 6000;
var legenddelay = 3000;
var piechartdelay = 3500;
var currentperfdelay = 4000;
var temperaturedelay = 60000; //1 minute
var timelineHeight = 0; //set in timeline init
var timelineHeightAjust = 1
var timeModulo = 24;
var timelineEnabled = true;
var trendEnabled = true;
var dateformat = 'dd-MM-yyyy hh:mm:ss';
var lowanimation = false;
var groups = true;
var interactive = true;
var lsByGroup = false;

//This map does the mapping between a status and a color.
var statusColorsHashmap = {};
//array for current machine performance
var machinesPerf = [];
var targetMach = [];
var targetTitle = [];
//Timeline hashmap contains the index to reference the hidden svg element and additional information needed to update the timeline on refresh
var timelinesHashmap = {};

function GetStatusColor(colorstatus) {
    try {
        return statusColorsHashmap[colorstatus.toUpperCase()].color;
    } catch (e) {
        console.log('No color defined for status ' + colorstatus);
    }
    return statusColorsHashmap[COFF].color;
}

function ParseServerIP() {
    var url = window.location.href; // Returns path only
    endUserConfigUrl = url + '/enduserconfig';
    newestMachinesRecordsUrl = url + '/refresh';
    timelineUrl = url + '/timeline';
}

function TestConnection() {
    var connok = false;
    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            connok = true;
        }
    };
    xhttp.open("POST", newestMachinesRecordsUrl, false);
    try {
        xhttp.send();
    } catch (e) {
        console.log('Connection test failed');
    }

    if (connok) {
        window.location.reload(true);
    }
}
