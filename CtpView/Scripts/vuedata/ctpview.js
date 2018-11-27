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
                message: null
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
        }


    },
    created: function () {
        this.fetchData();
    },
    template: '#templ'
});