<style lang="less">
    .topInfoBox{
        width: 100%;
        height: 80px;
        background: url("../../assets/imgs/load_up_img_bg.png") repeat-x;
        display: flex;
        justify-content: space-between;
        padding: 0 26px;
        align-items: center;
        position: fixed;
        top: 0;
        box-sizing: border-box;
        .rightBox{
            display: flex;
            align-items: center;
            img{
                width: 32px;
                height: 40px;
                margin-right: 18px;
            }
            span{
                font-size: 26px;
                color: #ffffff; 
            }
        }
        .leftBox{
            position: relative;
            img{
            width: 34px;
            height: 34px;
            }
            .redDot{
                position: absolute;
                width: 10px;
                height: 10px;
                top: 0px;
                right: 0px;
            }
           
        }
    }
</style>
<template>
    <div class="topInfoBox">
        <div class="rightBox">
            <img src="../../assets/imgs/lobby_img_01.png" alt="">
            <span class="userPhone">{{userName}}</span>
        </div>  
        <div class="leftBox">
            <img src="../../assets/imgs/lobby_btn_news.png" alt="">
        </div>  
    </div>
</template>
<script>
export default {
    data() {
        return {
            userName:''
        }
    },
    mounted() {
        this.userName = localStorage.getItem('phoneUser')
    },
}
</script>