document.addEventListener('DOMContentLoaded', function () {
    // ============================ Donut Chart Start ==========================
    var options = {
        series: [65.2, 25, 9.8],
        chart: {
            height: 200,
            type: 'donut',
        },
        colors: ['#3D7FF9', '#27CFA7', '#FA902F'],
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

    var chart = new ApexCharts(document.querySelector("#activityDonutChart"), options);
    chart.render();
    // ============================ Donut Chart End ==========================
});
