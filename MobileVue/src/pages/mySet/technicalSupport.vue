<style lang="less">
.border-bottom{
    border-bottom: 1px solid #dcdcdc; /* no */
}
.technicalSupportBox{
    padding-top: 80px;
    box-sizing: border-box;
    width: 100%;
    .banner{
      width: 100%;
      height: 350px;  
    }
    .list{
        height: 130px;
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0 26px;
        .border-bottom();
        .left{
            display: flex;
            align-items: center;
            img{
                margin-right: 20px;
                &.icon01{
                    width: 40px;
                    height: 34px;
                }
                &.icon02{
                    width: 40px;
                    height: 40px;
                }
            }
            span{
                color: #262626;
                font-size: 30px;
            }
        }
        .callHref{
           display:inline-block;

        }
        .callBtn{
            width: 60px;
            height: 60px;
        }
        .nextBtn{
            width: 18px;
            height: 30px;
        }
    }
}
    
</style>
<template>
    <div class="technicalSupportBox">
        <topBack :title="$route.name"></topBack>
        <img class="banner" src="../../assets/imgs/mySet/sist_img_06.png" alt="">
         <div class="callBox list">
             <div class="left">
                <img class="icon01" src="../../assets/imgs/mySet/sist_img_04.png" alt="">
                <span>技术支持电话：18349900415</span>
             </div>
              <a class="callHref" href="tel:18349900415">
                <img class="callBtn" src="../../assets/imgs/mySet/call_btn.png" alt="">
              </a>
         </div>
         <!-- 我要建议 -->
         <div class="adviceBox list" @click="goToAdvice">
            <div class="left ">
                <img class="icon02" src="../../assets/imgs/mySet/sist_img_05.png" alt="">
                <span>我要提建议</span>
            </div>
            <img class="nextBtn" src="../../assets/imgs/mySet/next_btn.png" alt="">
         </div>
    </div>

</template>
<script>
import topBack from '../../components/topBack/index'
export default {
    components:{
        topBack
    },
    methods: {
       goToAdvice(){
           this.$router.push({
               path:'/mySet/technicalSupport/advice'
           })
       }
    },
}
</script>