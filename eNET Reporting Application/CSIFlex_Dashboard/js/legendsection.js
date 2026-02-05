/*LEGENDSECTION.JS*/
/*
This function place the legends in the space that is between the CSI Flex logo and the ENETDNC logo.
*/

//Remove setTimeout undefined warning
/*jslint browser: true, devel:true*/

//Declare global variables
/*global $, legenddelay*/
//Declare global functions
/*global GetStatusColor*/

var legendarray = [];
var currentLegendPage = 0;
var legendmodulo = 8; //number of displayed legend per page

function updateLegend(mach_values) {
    for (var rec in mach_values) {
        try {
            //check that status is not in the array
            if (legendarray.indexOf(mach_values[rec].status) < 0) {
                legendarray.push(mach_values[rec].status);
            }
        } catch (err) {
            console.log("There was an error updating the legend:" + err);
        }
    }
}

var legendminheight = 0;

function nextLegend() {
    var legend_html = "";
    for (var legend_it = (currentLegendPage * legendmodulo); legend_it < ((currentLegendPage * legendmodulo) + legendmodulo); legend_it++) {
        if (legendarray[legend_it] === undefined) {
            break;
        }
        legend_html += '<div class="col-xs-6 col-sm-4 col-md-3 col-lg-3" style="margin-top:0.15em;margin-bottom:0.15em;"><div class="legendcircle" style="background:' + GetStatusColor(legendarray[legend_it]) + ';"></div><div class="single-line legendstatus"><span>' + legendarray[legend_it] + '</span></div></div>';
    }
    $("#LegendSection").html(legend_html);

    currentLegendPage += 1;
    if (currentLegendPage * legendmodulo > legendarray.length - 1) {
        currentLegendPage = 0;
    }

    //update min height
    var newh = $("#LegendSection").outerHeight();
    if (newh > legendminheight) {
        legendminheight = newh;
        $("#LegendSection").css("min-height", legendminheight);
    }

    setTimeout(nextLegend, legenddelay);
}
