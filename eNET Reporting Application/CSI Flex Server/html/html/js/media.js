/*jslint browser: true, devel: true */

//Declare global variables
/*global $, lstimer, livestatusHashmap, lsByGroup, groups, rotationCompleted:true*/
//Declare global functions
/*global nextPage, displayPageAllMachines, displayPagePerGroup, UpdatePageAllMachines, UpdatePagePerGroup*/

var paused = false;
var timer;

$(window).on('mousemove', ShowMediaCtrl);

function ShowMediaCtrl() {
    if ($(window).width() >= 1200) {
        $('#mediamenu').css('opacity', 0.75);
        $('#mediamenu').addClass('show');
        try {
            clearTimeout(timer);
        } catch (e) {}
        if ($('#mediamenu:hover').length === 0) {
            HideMediaCtrl();
        }
    }
}

function HideMediaCtrl() {
    if ($(window).width() >= 1200) {
        timer = setTimeout(function () {
            $('#mediamenu').animate({
                opacity: 0
            }, 350, function () {
                // Animation complete.
                $('#mediamenu').removeClass('show');
            });
        }, 1000);
    }
}

function Previous() {
    Pause();

    if (!lsByGroup) {
        UpdatePageAllMachines(-1);
        displayPageAllMachines();
    } else if (groups) {
        UpdatePagePerGroup(-1);
        displayPagePerGroup();
    } else {
        console.log('Invalid livestatus configuration.');
    }
}

function Start() {
    if (paused) {
        nextPage();
    }
    paused = false;
}

function Pause() {
    if (!paused) {
        //Return page index to current
        if (!lsByGroup) {
            UpdatePageAllMachines(-1);
        } else {
            UpdatePagePerGroup(-1);
        }

        //prevent iframe rotation on Start
        rotationCompleted = false;
    }
    paused = true;
    clearTimeout(lstimer);
}

function Next() {
    Pause();
    if (!lsByGroup) {
        UpdatePageAllMachines(1);
        displayPageAllMachines();
    } else if (groups) {
        UpdatePagePerGroup(1);
        displayPagePerGroup();
    } else {
        console.log('Invalid livestatus configuration.');
    }
}
