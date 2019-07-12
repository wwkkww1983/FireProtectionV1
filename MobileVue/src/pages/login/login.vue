<template>
  <div class="loginBox">
    <div class="topNavBg">登录</div>
    <div class="logoBox">
      <img class="logo" src="../../assets/imgs/load_img_01.png" alt>
      <p class="logoText">手持执法终端</p>
    </div>

    <div class="fromBox">
      <div class="phoneBox">
        <img class="phoneIcon" src="../../assets/imgs/load_img_02.png" alt="手机图标">
         <mt-field  placeholder="请输入手机号" type="tel" v-model="phone"></mt-field>
      </div>
      <div class="passwordBox">
        <img  class="passwordIcon" src="../../assets/imgs/load_img_03.png" alt="">
        <mt-field id="password"  placeholder="请输入密码" :type="passwordStatus ? 'password':'text'" v-model="password"></mt-field>
        <img @click="passwordChange" class="secretPassword" src="../../assets/imgs/load_img_07.png" v-if="passwordStatus" >
        <img @click="passwordChange" class="secretPassword" src="../../assets/imgs/load_img_06.png" v-else >
      </div>
     <div class="login_forgetPassword">
        <div class="login_forgetPassword_lef">
          <img @click="radioChange" class="radio_off" src="../../assets/imgs/load__radio_on.png" v-if="radioStatus">
          <img @click="radioChange" class="radio_on" src="../../assets/imgs/load__radio_off .png" v-else>
          <span>自动登录</span>
        </div>

        <div class="login_forgetPassword_right">
          <router-link to="/login/forgetpassword">忘记密码</router-link>
        </div>
     </div>
      <div class="loginBtn" @click="loginFrom">
        <img src="../../assets/imgs/load_btn_login.png" alt="">
      </div>

    <!--   <div>
        phone 的值 ：{{this.$store.state.phone}}
      </div> -->
    
    </div>
  </div>
</template>
<script>
import { Toast } from "mint-ui";
import Cookies from 'js-cookie'
export default {
    data() {
    return {
      phone:'',
      password:'',
      passwordStatus:true,
      radioStatus:false,
      toastInstance:null
    };
  },
  methods:{
    passwordChange(){
     this.passwordStatus = ! this.passwordStatus;
    },
    radioChange(){
       this.radioStatus = ! this.radioStatus;

       console.log(" this.radioStatus", this.radioStatus)
    },
    loginFrom(){
      let account = this.phone;
      let password = this.password;
      let phone = this.phone;
      const that = this;

      if(phone && password && !this.radioStatus ){
        this.$http({
          method:'post',
          url:'/api/services/app/FireDeptUser/UserLoginForMobile',
          data:{
            account,
            password,
            isPersistent:false
          }
      }).then(function(res){
        if(res.data.result.success){
          localStorage.setItem('phoneUser',res.data.result.name);
          localStorage.setItem('userName',phone);
          localStorage.setItem('userId',res.data.result.userId);
          that.$router.push({
            path:'/dataMonitor'
          })
        }else{
            this.toastInstance = Toast({
            message: res.data.result.failCause,
            position: "center",
            duration: 3000
          });
        }
      }).catch(function (res){
        Toast({
            message: '网络连接超时,链接不上',
            position: "center",
            duration: 3000
        });
      })
      }else if(phone && password && this.radioStatus){
        this.$http({
          method:'post',
          url:'/api/services/app/FireDeptUser/UserLoginForMobile',
          data:{
            account,
            password,
            isPersistent:true
          }
          }).then(function(res){
            console.log("登录成功",res)
            if(res.data.result.success){
              localStorage.setItem('phoneUser',res.data.result.name);
              localStorage.setItem('userName',phone);
              localStorage.setItem('userId',res.data.result.userId);
              if(that.radioStatus){
                localStorage.setItem('userPassword',password);
              }
              that.$router.push({
                path:'/dataMonitor'
              })
            }else{
                this.toastInstance = Toast({
                message: res.data.result.failCause,
                position: "center",
                duration: 3000
              });
            }
          }).catch(function (res){
            Toast({
                message: '网络连接超时,链接不上',
                position: "center",
                duration: 3000
            });
          })

      }else{
        Toast({
            message: '请输入正确的账号密码',
            position: "center",
            duration: 3000
        });
      }
     
    }
  },
   mounted() {
    if (localStorage.getItem("phoneUser")) {
        this.$router.replace("/dataMonitor");
    }
  }
};
</script>
<style lang="less">
@fontColor :#cfcfcf;
.loginBox {
  .topNavBg {
    width: 100%;
    height: 80px;
    background: url("../../assets/imgs/load_up_img_bg.png") repeat-x;
    color: white;
    font-size: 36px;
    text-align: center;
    line-height: 80px;
  }
  .logoBox {
    width: 100%;
    height: 230px;
    .logo {
      width: 395px;
      height: 80px;
      position: absolute;
      left: 50%;
      transform: translateX(-50%);
      margin-top: 100px;
    }
    .logoText {
      font-size: 36px;
      color: #262626;
      position: absolute;
      left: 50%;
      transform: translateX(-50%);
      margin-top: 189px;
    }
  }
  .fromBox{
    width: 100%;
    padding: 0 20px;
    box-sizing: border-box;
    padding-top: 70px;
    .mint-field-core{
      height: 80px;
      font-size: 30px;
    }
    a{
      display: inline-block;
      width: 100%;
    }
    .phoneBox,.passwordBox{
      height: 100px;
      display: flex;
      justify-content: flex-start;
      border-bottom: 1px solid  #cacaca;/* no */
      width: 100%;
      padding: 0 20px;
      box-sizing: border-box;
      align-items: center;
      .phoneIcon{
        width: 24px;
        height: 36px;
        margin-right: 25px;
      }
      .passwordIcon{
        width: 26px;
        height: 32px;
        margin-right: 25px;
      }
      input::-webkit-input-placeholder{
        color: @fontColor;
        font-size: 28px;
      }
    }
    .passwordBox{
      margin-top: 60px;
      i::before{
        color: transparent;
      }
      .secretPassword{
        width: 36px;
        height: 20px;
      }
    }
    .login_forgetPassword{
      display: flex;
      width:100%;
      box-sizing: border-box;
      padding: 0 62px;
      padding-top: 52px;
      font-size: 26px;
      justify-content: space-between;
      .login_forgetPassword_lef{
        display: flex;
        align-items: center;
      
        .radio_on,.radio_off{
          width: 26px;
          height: 26px;
        }
        span{
          color: #9b9a9a;
          margin-left: 14px;
        }
      }
      .login_forgetPassword_right{
       a{
         color: #0398fe;
       }
      }
    }
    .loginBtn{
      width: 100%;
      height: 78px;
      margin-top: 76px;
      img{
        width: 100%;
        height: 100%;
      }
    }
  }
}
</style>