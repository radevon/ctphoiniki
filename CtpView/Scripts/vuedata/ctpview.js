﻿Vue.component("ctp", {
    data: function(){
        return {
            parameters: [],
            updateTime: null,
            currentSec: 0
        };
    },
    props: {
        bindingid:{
            type: Number,
            required: true
        },
        datainterval: {
            type: Number,
            required: true
        },
        updatetimeout: {
            type: Number,
            required: true
        }

    },
    methods: {
        getDefaultObject: function(){
            return {
                id: null,
                bindingid: null,
                recvdate: null,
                temp1: null,
                temp2: null,
                temp3: null,
                pressure1: null,
                pressure2: null,
                pressure3: null,
                pressure4: null,
                pumpstatus: null,
                valvestatus: null,
                message:null
                };
        },

        getIntervalDate: function () {
            var curr = new Date(), prev;

            if (this.datainterval != undefined) {
                prev = new Date(curr - this.datainterval * 60 * 60 * 1000);
            }
            else {
                prev = new Date(curr - 0.5 * 60 * 60 * 1000);
            }
            return {
                start: prev,
                end: curr
            }
        },


        fetchData: function () {
            var self=this;
            var interv = this.getIntervalDate();
            $.ajax({
                url: ajaxBasePath + '/Ctp/CtpData',
                    type: 'GET',
                    data: { BindingId: this.bindingid, from: moment(interv.start).tz("Europe/Minsk").format("YYYY-MM-DDTHH:mm:ss"), to: moment(interv.end).tz("Europe/Minsk").format("YYYY-MM-DDTHH:mm:ss") },
                    success: function (data, status) {
                        self.updateTime = new Date();

                        self.parameters = data.map(function (e, i) {
                            return {
                                id: e.Id,
                                bindingid: e.BindingId,
                                recvdate: new Date(parseInt(e.RecvDate.substr(6))),
                                temp1: e.Temp1,
                                temp2: e.Temp2,
                                temp3: e.Temp3,
                                pressure1: e.Pressure1,
                                pressure2: e.Pressure2,
                                pressure3: e.Pressure3,
                                pressure4: e.Pressure4,
                                pumpstatus: e.PumpStatus,
                                valvestatus: e.ValveStatus,
                                message: e.Message
                            };
                        
                        });
                        self.currentSec = 0;
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        self.updateTime = new Date();
                        self.currentSec = 0;
                        console.error("Ошибка при запросе данных по показаниям");

                    }
            }); // ajax close
      
        },

        pumpStatusText: function (val) {
            
                var status=val;
                if (status == null) {
                    return "<span class='label label-default'>неизв</span>"
                }
                if (status) {
                    return "<span class='label label-success'>вкл</span>";
                }else
                {
                    return "<span class='label label-danger'>выкл</span>"
                }
        },

        valveStatusText: function (val) {

            var status = val;
            if (status == null) {
                return "<span class='label label-default'>неизв</span>"
            }
            if (status > 0) {
                return "<span class='label label-success'>откр</span>";
            } else if (status < 0) {
                return "<span class='label label-danger'>закр</span>"
            } else {
                return "<span class='label label-info'>покой</span>"
            }
        },

        points: function (dataKey) {
            var x = [], y = [];
            if (this.parameters.length > 0) {
                for (var i = this.parameters.length - 1 ; i >= 0; i--) {
                    x.push(this.parameters[i].recvdate);
                    y.push(this.parameters[i][dataKey]);

                }
            }

            return {
                x: x,
                y: y
            }


        }

    },
    computed:{
        lastData: function () {
            if (this.parameters.length > 0) {
                return this.parameters[0];
            } else {
                return this.getDefaultObject();
            }
        },
        dataLength: function () {
            return this.parameters.length;
        },

        pointsTemp: function () {
            var x = [], y1 = [], y2 = [];
            if (this.parameters.length > 0) {
                for (var i = this.parameters.length - 1 ; i >= 0; i--) {
                    x.push(this.parameters[i].recvdate);
                    y1.push(this.parameters[i].temp1);
                    y2.push(this.parameters[i].temp2);
                }
            }

            return {
                x: x,
                y1: y1,
                y2: y2
            }


        }



    },
    created: function () {
        this.currentSec = 0;

        var self = this;
        
        self.fetchData();
        // таймер времени до обновления
        setInterval(function () {
            if (self.currentSec < self.updatetimeout / 1000)
                self.currentSec++;
        }, 1000);


        setInterval(function () {
            self.fetchData();
        }, self.updatetimeout);
        
    },
    template: '#templ'
});


Vue.filter("fixed", function (value, decimal) {
    if (value !== null && value !== undefined && !isNaN(value)) {
        if (decimal && !isNaN(decimal)) {
            return value.toFixed(decimal);
        } else {
            return value;
        }
    } else
        return null;

});

Vue.filter('dateformat', function (value, format) {
    if (moment(value).isValid()) {
        return moment(value).tz("Europe/Minsk").format(format);
    }
    else
        return value;

});




Vue.component("graphDoubleLine", {
    data: function () {
        chart: null
    },
    props: ["points", "label1", "label2"],
    template: '<canvas></canvas>',

    watch: {
        points: function (val) {
            this.chart.data.labels = val.x;
            this.chart.data.datasets[0].data = val.y1;
            this.chart.data.datasets[1].data = val.y2;
            this.chart.update();
        }
    },

    mounted: function () {

        this.chart = new Chart(this.$el, {
            type: 'line',
            data: {
                labels: this.points.x,
                datasets: [
                    {
                        label: this.label1,
                        data: this.points.y1,
                        fill: true,
                        borderWidth: 2,
                        pointRadius: 1,
                        borderColor: "rgba(255,11,11,0.4)",
                        backgroundColor: "rgba(255,11,11,0.02)",
                        lineTension: 0.1
                        /*cubicInterpolationMode: 'linear'*/
                    },
                    {
                        label: this.label2,
                        data: this.points.y2,
                        fill: true,
                        borderWidth: 2,
                        pointRadius: 1,
                        borderColor: "rgba(11,11,255,0.4)",
                        backgroundColor: "rgba(11,11,255,0.02)",
                        lineTension: 0.1
                        /*cubicInterpolationMode: 'linear'*/
                    }
                ]
            },
            options: {
                scales: {
                    yAxes: [
                        {
                            ticks: {
                                beginAtZero: true
                            }
                        }
                    ],
                    xAxes: [{
                        type: 'time',
                        time: {
                                displayFormats: { day: 'DD MMM, HH:mm' },
                        }

                    }]
                }
            }
        });
    }
});


Vue.component("graphLine", {
    data: function () {
        chart: null
    },
    props: ["points", "label", "color", "borderColor"],
    template: '<canvas></canvas>',

    watch: {
        points: function (val) {
            this.chart.data.labels = val.x;
            this.chart.data.datasets[0].data = val.y;
            this.chart.update();
        }
    },

    mounted: function () {

        this.chart = new Chart(this.$el, {
            type: 'line',
            data: {
                labels: this.points.x,
                datasets: [
                    {
                        label: this.label,
                        data: this.points.y,
                        fill: true,
                        borderWidth: 1,
                        pointRadius: 3,
                        backgroundColor: this.color,
                        borderColor: this.borderColor,
                        lineTension: 0.1,
                        cubicInterpolationMode: 'linear'
                    }
                ]
            },
            options: {
                scales: {
                    yAxes: [
                        {
                            ticks: {
                                beginAtZero: false
                            }
                        }
                    ],
                    xAxes: [{
                        type: 'time',
                        time: {

                            displayFormats: { day: 'DD MMM, HH:mm' },

                        }

                    }]
                }
            }
        });
    }
});
