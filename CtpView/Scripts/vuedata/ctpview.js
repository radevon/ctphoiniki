Vue.component("ctp", {
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

        fetchData: function () {
            this.parameters = [
                {
                id: 1,
                bindingid: 2,
                recvdate: new Date(),
                temp1: 12.35,
                temp2: 11.34,
                temp3: 22,
                pressure1: 23.56,
                pressure2: 33.546645,
                pressure3: 2.344,
                pressure4: 21.2,
                pumpstatus: true,
                valvestatus: 0,
                message: 'Ошибка по датчику давления!'
                },
                {
                    id: 2,
                    bindingid: 2,
                    recvdate: new Date(),
                    temp1: 72.035,
                    temp2: 1.2,
                    temp3: 23.76,
                    pressure1: 3.506,
                    pressure2: 2.6645,
                    pressure3: 32.344,
                    pressure4: 221.2,
                    pumpstatus: true,
                    valvestatus: 0,
                    message: 'dfgfg'
                }
            ];

            this.currentSec = 0;
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
        pumpStatusText: function () {
            
            var status=this.lastData.pumpstatus;
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

        valveStatusText: function () {

            var status = this.lastData.valvestatus;
            if (status == null) {
                return "<span class='label label-default'>неизв</span>"
            }
            if (status>0) {
                return "<span class='label label-success'>откр</span>";
            } else if(status<0){
                return "<span class='label label-danger'>закр</span>"
            }else
            {
                return "<span class='label label-info'>покой</span>"
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