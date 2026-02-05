/*DATETIMETEMP.JS*/
/*
Script for managing the date, the time and the
temperature.
*/

//Remove setTimeout undefined warning
/*jslint browser: true, devel:true */

//Declare global variables
/*global $, userConfig, temperaturedelay, dateformat*/

//Function for setting the temperature
function loadWeather(location, units, woeid) {
    $.simpleWeather({
        location: location,
        woeid: woeid,
        unit: units,
        success: function (weather) {
            var weatherspan = '<img class="weathericon" src="' + weather.thumbnail + '" height="' + $('#temperatureSection').outerHeight() + 'px" type="image/svg+xml"></img><span>' + weather.temp + '&#176;' + weather.units.temp + '</span>';
            $("#temperatureSection").html(weatherspan);
        },
        error: function (error) {
            $("#temperatureSection").html('');
        }
    });
}

function refreshTemperature() {
    var unit = 'c';
    var city = userConfig[0].detail_temperature;
    //if (userConfig[0].degree == "fahrenheit" || userConfig[0].degree == "Fahrenheit" || userConfig[0].degree == "fahrenheit " || userConfig[0].degree == "Fahrenheit ") {
		 if (userConfig[0].degree == "Fahrenheit") {
        unit = 'f';
    }

    loadWeather(city, unit, '');
}

function startTemperature() {
    refreshTemperature();
    setTimeout(startTemperature, temperaturedelay); //update every minute
}

// function to manage the clock. 
function startTime() {
    var today = new Date();
    document.getElementById('time').innerHTML = today.toString(dateformat);
    var t = setTimeout(function () {
        startTime();
    }, 500);
}
