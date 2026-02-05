/*jslint browser: true, devel:true */
//Declare global variables
/*global userConfig*/

function initRSSFeed(rssFeed) {

    if (rssFeed.length <= 0) {
        userConfig[0].messages = 'off';
    }
    var rssFeed_html = "";
    for (var msg_it = 0; msg_it < rssFeed.length; msg_it++) {
        var con = rssFeed[msg_it].messages.substr(0, 5);
        if (con == "_COND")
            rssFeed_html += '<span class="myfeed">' + rssFeed[msg_it].messages.substr(5) + ' : <span id="dailybest">MACHINEX with 00.0%</span></span>';
        else if (con == "_CONW")
            rssFeed_html += '<span class="myfeed">' + rssFeed[msg_it].messages.substr(5) + ' : <span id="weeklybest">MACHINEX with 00.0%</span></span>';
        else if (con == "_CONM")
            rssFeed_html += '<span class="myfeed">' + rssFeed[msg_it].messages.substr(5) + ' : <span id="monthlybest">MACHINEX with 00.0%</span></span>';
        else
            rssFeed_html += '<span class="myfeed">' + rssFeed[msg_it].messages + "</span>";
    }
    document.getElementById("myRSSFeed").innerHTML = rssFeed_html;
}

function updateDailyBestRSS(machine, cond) {
    if (document.getElementById("dailybest")) {
        document.getElementById("dailybest").innerHTML = machine + " with " + Math.round(cond) + "%";
    }
}

function updateWeeklyBestRSS(machine, conw) {
    if (document.getElementById("weeklybest")) {
        document.getElementById("weeklybest").innerHTML = machine + " with " + Math.round(conw) + "%";
    }
}

function updateMonthlyBestRSS(machine, conm) {
    if (document.getElementById("monthlybest")) {
        document.getElementById("monthlybest").innerHTML = machine + " with " + Math.round(conm) + "%";
    }
}
