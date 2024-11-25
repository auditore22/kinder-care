document.addEventListener('DOMContentLoaded', function () {
    // ============================ Hour Spent Chart Start ==========================
    var options = {
        series: [{
            name: 'Study',
            data: [44, 55, 41, 50, 36, 43, 50, 44, 55, 41, 50, 36]
        }, {
            name: 'Exam',
            data: [26, 23, 20, 40, 32, 27, 30, 26, 23, 20, 40, 32]
        }],
        colors: ['#27CFA7', '#A9ECDC'],
        chart: {
            type: 'bar',
            height: 400,
            stacked: true,
            toolbar: {
                show: false
            },
            zoom: {
                enabled: true
            }
        },
        plotOptions: {
            bar: {
                columnWidth: "35%",
                horizontal: false,
                borderRadius: 10,
                dataLabels: {
                    total: {
                        enabled: false,
                        style: {
                            fontSize: '13px',
                            fontWeight: 900,
                        }
                    }
                }
            },
        },
        dataLabels: {
            enabled: false
        },
        grid: {
            show: true,
            borderColor: '#d5dbe7',
            strokeDashArray: 3,
            position: 'back',
        },
        xaxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        },
        yaxis: {
            labels: {
                formatter: function (value) {
                    return "$" + value + "Hr";
                },
                style: {
                    fontSize: "14px"
                }
            },
        },
        legend: {
            show: false,
            position: 'top',
            offsetY: 0,
            horizontalAlign: 'start',
            markers: {
                radius: 50,
            }
        },
        fill: {
            opacity: 1
        }
    };

    var chart = new ApexCharts(document.querySelector("#stackedColumnChart"), options);
    chart.render();
    // ============================ Hour Spent Chart End ==========================
});
