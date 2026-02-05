/*TIMELINE.JS*/
/*
This script manages the timeline of each record in the "LiveStatus" table.
*/

/*jslint browser: true, devel: true*/
//Declare global variables
/*global $, timelineUrl, timelinesHashmap, timelineHeight:true, legendarray*/
//Declare global functions
/*global GetStatusColor, UpdateTimelineScale*/


var svgns = "http://www.w3.org/2000/svg";

function initTimelines() {
    timelineHeight = $("#hiddendiv").html("test").outerHeight() * 0.6; //60% of cell height
    $("#hiddendiv").html("");

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            var serverResponse = xhttp.responseText;
            var timelines = JSON.parse(serverResponse);

            var width = 1;
            var timeline_index = 0;

            for (var timeline in timelines) {
                //check for atleast one event
                if (timelines[timeline][0] !== undefined) {
                    //fill the timeline hashmap
                    timelinesHashmap[timeline] = {
                        index: timeline_index,
                        xpos: 0,
                        starttime: 0,
                        currentstatus: "",
                        shift: 0
                    };

                    var symbol = document.createElementNS(svgns, 'symbol');
                    symbol.setAttributeNS(null, 'id', "svg-" + timeline_index);
                    document.getElementById('hiddentimelines').appendChild(symbol);
                    var x = 0;

                    for (var event in timelines[timeline]) {
                        var rect = document.createElementNS(svgns, 'rect');
                        rect.setAttributeNS(null, 'x', x);
                        rect.setAttributeNS(null, 'y', 0);
                        rect.setAttributeNS(null, 'height', timelineHeight);
                        rect.setAttributeNS(null, 'width', timelines[timeline][event].cycletime); //cycletime in seconds
                        rect.setAttributeNS(null, 'fill', GetStatusColor(timelines[timeline][event].status));
                        document.getElementById('svg-' + timeline_index).appendChild(rect);
                        x += timelines[timeline][event].cycletime;
                        timelinesHashmap[timeline].currentstatus = timelines[timeline][event].status;

                        //Update legend
                        try {
                            //check that status is not in the array
                            if (legendarray.indexOf(timelines[timeline][event].status) < 0) {
                                legendarray.push(timelines[timeline][event].status);
                            }
                        } catch (err) {
                            console.log("There was an error updating the legend:" + err);
                        }
                    }

                    //SAVE DATA IN HASHMAP FOR UPDATE                
                    timelinesHashmap[timeline].xpos = x;
                    timelinesHashmap[timeline].starttime = timelines[timeline][0].time;
                    timelinesHashmap[timeline].shift = timelines[timeline][0].shift;

                    timeline_index += 1;
                }
            }
        }
    };
    xhttp.open("GET", timelineUrl, false);
    xhttp.send();
}


function updateTimeline(mach_values) {
    var d = new Date();
    var h = d.getHours();
    var m = d.getMinutes();
    var s = d.getSeconds();
    var currenttime = (h * 60 * 60) + (m * 60) + s;
    var elapsedtime = 0;

    for (var rec in mach_values) {
        //try to retrieve timeline from hashmap
        try {
            elapsedtime = currenttime - (timelinesHashmap[mach_values[rec].machine].xpos + timelinesHashmap[mach_values[rec].machine].starttime);

            //first check for same shift
            if (timelinesHashmap[mach_values[rec].machine].shift == mach_values[rec].Shift) {
                //first check if current status is still the same
                if (timelinesHashmap[mach_values[rec].machine].currentstatus == mach_values[rec].status) {
                    //get last rectangle element from the hidden timeline and update it
                    var lastrect = document.getElementById('svg-' + timelinesHashmap[mach_values[rec].machine].index).lastChild;
                    var lastwidth = parseInt(lastrect.getAttribute('width'), 10);
                    lastrect.setAttributeNS(null, 'width', (lastwidth + elapsedtime)); //cycletime in seconds
                    timelinesHashmap[mach_values[rec].machine].xpos += elapsedtime;
                } else {
                    //create a new rectangle element from new xpos
                    if (elapsedtime >= 0) {
                        var rect = document.createElementNS(svgns, 'rect');
                        rect.setAttributeNS(null, 'x', timelinesHashmap[mach_values[rec].machine].xpos);
                        rect.setAttributeNS(null, 'y', 0);
                        rect.setAttributeNS(null, 'height', timelineHeight);
                        rect.setAttributeNS(null, 'width', elapsedtime); //cycletime in seconds
                        rect.setAttributeNS(null, 'fill', GetStatusColor(mach_values[rec].status));
                        document.getElementById('svg-' + timelinesHashmap[mach_values[rec].machine].index).appendChild(rect);
                        timelinesHashmap[mach_values[rec].machine].xpos += elapsedtime;
                        timelinesHashmap[mach_values[rec].machine].currentstatus = mach_values[rec].status;
                    }
                }
            } else { //shift change                
                clearTimelines();
                initTimelines();
                UpdateTimelineScale();
                break;
            }
        } catch (err) {
            //No need to flood console with error since any undefined timeline will be catch in the init
            //console.log("There was an error updating the timeline for " + mach_values[rec].machine + ":" + err);
        }
    }
}

function clearTimelines() {
    var timelinenode = document.getElementById("hiddentimelines");
    while (timelinenode.firstChild) {
        timelinenode.removeChild(timelinenode.firstChild);
    }
}
