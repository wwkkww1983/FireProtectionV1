<style lang="less">
    .fireDateReport{
        padding-top: 80px;
        width: 100%;
        box-sizing: border-box;
        .menuIcon{
            padding: 60px;
            display: flex;
            flex-direction: row;
            flex-wrap: wrap;
            align-content: space-between;
            width:100%;
            box-sizing:border-box;
            .column{
                display:flex;
                justify-content:space-between;
                width:100%;
                .iconBox{
                    width: 210px;
                    height: 216px;
                    margin-bottom: 124px;
                    img{
                        width: 100%;
                        height: 100%;
                    }
                } 
            }
           
        }

    }
</style>
<template>
    <div class="fireDateReport">
        <topBack :title="$route.name"></topBack>
        <div class="menuIcon">
            <div class="column">
                <div class="iconBox" @click="safetyElectricity">
                    <img src="../../../assets/imgs/fireDataReport/zhsj_btn_01.png" alt="">
                </div>
                <div class="iconBox" @click="fireAlarmData">
                    <img src="../../../assets/imgs/fireDataReport/zhsj_btn_02.png" alt="">
                </div>
            </div>
             <div class="column">
                <div class="iconBox" @click="onDutyPatrol">
                    <img src="../../../assets/imgs/fireDataReport/zhsj_btn_03.png" alt="">
                </div>
                <div class="iconBox" @click="facilitiesTrouble">
                    <img src="../../../assets/imgs/fireDataReport/zhsj_btn_04.png" alt="">
                </div>
             </div>
        </div>


    </div>
</template>
<script>
import topBack from '../../../components/topBack'

export default {
    components:{
        topBack
    },
    methods: {
        safetyElectricity(){
            this.$router.push({
                path:'/dataMonitor/fireDataReport/safetyElectricity'
            })
        },
        fireAlarmData(){
            this.$router.push({
                path:'/dataMonitor/fireDataReport/fireAlarmData'
            })
        },
        facilitiesTrouble(){
            this.$router.push({
                path:'/dataMonitor/fireDataReport/facilitiesTrouble'
            })
        },
        onDutyPatrol(){
            this.$router.push({
                path:'/dataMonitor/fireDataReport/onDutyPatrol'
            })
        },
       
    },
}
</script>