/*DOUGHNUTCHART.JS*/
/*
Script for managing the state of the doughnut chart.
*/

/*jslint browser: true, devel:true */

//Declare global variables
/*global $, Chart, machinesHashmap, userConfig, machinesPerf, piechartdelay, CON, COFF, SETUP, lowanimation*/
//Declare global functions
/*global GetStatusColor*/

var myChart;

function initializeDoughnutChart() {

    var canvas, ctx, data;
    Chart.defaults.global.responsive = true;
    canvas = document.getElementById("myChart");
    ctx = canvas.getContext("2d");
    data = {
        labels: [
                "Cycle On",
                "Cycle Off",
                "Setup"
            ],
        datasets: [
            {
                data: [10, 40, 100],
                backgroundColor: [
                        GetStatusColor(CON),
                        GetStatusColor(COFF),
                        GetStatusColor(SETUP)
                    ],
                hoverBackgroundColor: [
                        "green",
                        "red",
                        "blue"
                    ]
                }
            ]
    };

    myChart = new Chart(ctx, {
        type: 'doughnut',
        data: data,
        options: {
            legend: {
                display: false //Set the "display" property to false to prevent the display of the legend.
            },
            animation: (lowanimation ? false : {
                animateScale: true,
                animateRotate: true
            }),
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, data) {
                        var allData = data.datasets[tooltipItem.datasetIndex].data;
                        var tooltipLabel = data.labels[tooltipItem.index];
                        var tooltipData = allData[tooltipItem.index];
                        var total = 0;
                        for (var i in allData) {
                            total += allData[i];
                        }
                        return tooltipLabel + ': ' + tooltipData.toFixed(2) + '%';
                    }
                }
            }
        }
    });

    Chart.pluginService.register({
        beforeDraw: function (chart) {
            var width = chart.chart.width,
                height = chart.chart.height,
                ctx = chart.chart.ctx;
            ctx.restore();
            var fontSize = (height / 220).toFixed(2);
            ctx.font = (fontSize < 0.9 ? fontSize : 0.9) + "em Arial";
            ctx.textBaseline = "middle";
            var cyclesValues = data.datasets[0].data;
            var totalOfValues = 0;
            $.each(cyclesValues, function (index, value) {
                totalOfValues += value;
            });
            var cycleOnString = "Cycle On";
            var cycleOntextX = Math.round((width - ctx.measureText(cycleOnString).width) / 2),
                cycleOntextY = height / 2;
            ctx.fillStyle = '#000000';
            ctx.fillText(cycleOnString, cycleOntextX, cycleOntextY - 10);
            ctx.fillText(((data.datasets[0].data[0] / totalOfValues) * 100).toFixed(2) + "%", cycleOntextX + 5, cycleOntextY + 10);
            ctx.save();
        }
    });
}

var weeklyMonthIndex = 0;

//This function update the doughnut chart with weekly or monthly data.
function updateDoughnutChart() {

    if (weeklyMonthIndex === machinesPerf.length) {
        weeklyMonthIndex = 0;
    }

    if (userConfig[0].piecharttime === "Weekly" && machinesPerf.length > 0) {

        var cycleOnWeeklyJson = machinesPerf[weeklyMonthIndex].WeeklyJson[1].cycletime;
        var cycleOffWeeklyJson = machinesPerf[weeklyMonthIndex].WeeklyJson[0].cycletime + machinesPerf[weeklyMonthIndex].WeeklyJson[3].cycletime;
        var setupWeeklyJson = machinesPerf[weeklyMonthIndex].WeeklyJson[2].cycletime;

        myChart.data.datasets[0].data[0] = cycleOnWeeklyJson;
        myChart.data.datasets[0].data[1] = cycleOffWeeklyJson;
        myChart.data.datasets[0].data[2] = setupWeeklyJson;
        myChart.update();
        $("#nameOfTheMachine").html(" - " + machinesPerf[weeklyMonthIndex].machine);

    } else if (userConfig[0].piecharttime === "Monthly" && machinesPerf.length > 0) {

        var cycleOnMonthlyJson = machinesPerf[weeklyMonthIndex].MonthlyJson[1].cycletime,
            cycleOffMonthlyJson = machinesPerf[weeklyMonthIndex].MonthlyJson[0].cycletime + machinesPerf[weeklyMonthIndex].MonthlyJson[3].cycletime,
            setupMonthlyJson = machinesPerf[weeklyMonthIndex].MonthlyJson[2].cycletime;

        myChart.data.datasets[0].data[0] = cycleOnMonthlyJson;
        myChart.data.datasets[0].data[1] = cycleOffMonthlyJson;
        myChart.data.datasets[0].data[2] = setupMonthlyJson;
        myChart.update();

        $("#nameOfTheMachine").html(" - " + machinesPerf[weeklyMonthIndex].machine);
    }

    weeklyMonthIndex += 1;
    setTimeout(updateDoughnutChart, piechartdelay);
}
