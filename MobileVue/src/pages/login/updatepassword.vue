<template>
  <div class="loginBox">
    <div class="topNavBg">忘记密码</div>
    <div class="fromBox">
      <div class="newpasswordBox">
        <img class="phoneIcon" src="../../assets/imgs/load_img_03.png" alt="手机图标">
        <mt-field placeholder="请设置新密码" type="password" v-model="newpassword"></mt-field>
      </div>
      <div class="passwordBox">
        <img class="passwordIcon" src="../../assets/imgs/load_img_05.png" alt>
        <mt-field id="password" placeholder="再次输入密码" type="password" v-model="renewpassword"></mt-field>
      </div>
      <div class="loginBtn">
        <img @click="login" src="../../assets/imgs/load_btn_qd.png" alt>
      </div>
    </div>
  </div>
</template>
<script>
import { Toast } from "mint-ui";
export default {
  data() {
    return {
      newpassword: "",
      renewpassword: "",
      toastInstance:null
    };
  },
  methods: {
    login() {
      if (this.newpassword === this.renewpassword) {
        this.$router.push({
          path: "/login"
        });
      } else {
        this.toastInstance = Toast({
          message: "两次输入的密码不一致",
          position: "center",
          duration: 3000
        });
      }
    }
  }
};
</script>
<style lang="less">
@fontcolor: #cfcfcf;
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
  .fromBox {
    width: 100%;
    padding: 0 20px;
    box-sizing: border-box;
    padding-top: 70px;
    input::-webkit-input-placeholder {
      color: @fontcolor;
      font-size: 28px;
    }
    .mint-field-core{
      height: 80px;
      font-size: 30px;
    }
    a {
      display: inline-block;
      width: 100%;
    }
    .newpasswordBox,s
    .passwordBox {
      height: 80px;
      display: flex;
      justify-content: flex-start;
      border-bottom: 1px solid #cacaca; /* no */
      width: 100%;
      padding: 0 20px;
      box-sizing: border-box;
      align-items: center;
      .phoneIcon {
        width: 26px;
        height: 32px;
        margin-right: 25px;
      }
      .passwordIcon {
        width: 28px;
        height: 30px;
        margin-right: 25px;
      }
    }
    .passwordBox {
      justify-content: space-between;
      margin-top: 60px;
      .yzm {
        width: 170px;
        height: 48px;
      }
    }
    .login_forgetPassword {
      display: flex;
      width: 100%;
      box-sizing: border-box;
      padding: 0 62px;
      padding-top: 52px;
      font-size: 26px;
      justify-content: space-between;
      .login_forgetPassword_lef {
        display: flex;
        align-items: center;

        .radio_on,
        .radio_off {
          width: 26px;
          height: 26px;
        }
        span {
          color: #9b9a9a;
          margin-left: 14px;
        }
      }
      .login_forgetPassword_right {
        a {
          color: #0398fe;
        }
      }
    }

    .loginBtn {
      width: 100%;
      height: 78px;
      margin-top: 76px;
      img {
        width: 100%;
        height: 100%;
      }
    }
  }
}
</style>