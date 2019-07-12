<style lang="less">
.adviceBox{
    
    padding-top: 110px;
    box-sizing: border-box;
    .adviceFrom{
        padding:0px 20px;
        .title{
            font-size: 30px;
            color: #262626;
        }
        .adviceTextarea{
            width: 100%;
            font-size: 26px;
            border-radius: 8px;
            border: solid 1px #dcdcdc;/* no */
            height: 450px;
            margin: 30px 0px;
            outline: none;
            text-indent: 10px;
            padding:20px;
            box-sizing: border-box;
            &::placeholder{
            color:#d2d2d2;
            }
    
        }

    .submitBtn{
        border-radius: 10px;
        height:  78px;
        width: 100%;
        background: url("../../assets/Btn_bg.png") repeat-x;
        text-align: center;
        line-height: 78px;
        color: white;
        font-size: 32px;
    
    }
    }

}

    
</style>
<template>
    <div class="adviceBox">
        <topBack :title="$route.name"></topBack>
        <topHome></topHome>
        <div class="adviceFrom">
            <span class="title">感谢您一直以来的支持，请在下方输入您的宝贵意见：</span>
            <textarea v-model="textarea" placeholder="请在这里输入您的建议内容" class="adviceTextarea"></textarea>
            <div @click="submit" class="submitBtn">
                提交
            </div>
        </div>
    </div>
</template>
<script>
import topBack from '../../components/topBack/index'
import topHome from '../../components/topHome/index'
import { Toast } from 'mint-ui'
export default {
    components:{
        topBack,
        topHome
    },
    data() {
        return {
            textarea:''
        }
    },
    methods: {
        submit(){
            let that =this;
            if(this.textarea.length>0){
                this.$http({
                    method: "post",
                    url: "/api/services/app/Version/Add",
                    data:{
                        suggest:that.textarea
                    }
                }).then(res=>{
                   console.log("添加数据成功",res)
                   if(res.data.success){
                      Toast({
                        message: '提交成功',
                        iconClass: 'icon icon-success'
                        });
                        that.$router.push({
                            path:'/mySet/technicalSupport'
                        })
                   }
                }).catch(res=>{
                    console.log("添加数据失败")
                })
            }else{
                Toast('请输入信息后再提交');
            }
        }
    },
}
</script>