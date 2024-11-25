// ============================ Donut Chart Script ===========================
var donutChartOptions = {
    series: [65.2, 25, 9.8],
    chart: {
        height: 200,
        type: 'donut',
    },
    colors: ['#3D7FF9', '#27CFA7', '#FA902F'],
    enabled: true, // Enable data labels
    formatter: function (val, opts) {
        return opts.w.config.series[opts.seriesIndex] + '%';
    },
    dropShadow: {
        enabled: false
    },
    plotOptions: {
        pie: {
            donut: {
                size: '55%' // Fixed slice width
            }
        }
    },
    responsive: [{
        breakpoint: 480,
        options: {
            chart: {
                width: 200
            },
            legend: {
                show: false
            }
        }
    }],
    legend: {
        position: 'right',
        offsetY: 0,
        height: 230,
        show: false
    }
};

var donutChart = new ApexCharts(document.querySelector("#activityDonutChart"), donutChartOptions);
donutChart.render();