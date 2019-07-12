<template>
  <div class="forgetpasswordBox">
    <div class="topNavBg">忘记密码</div>
    <div class="fromBox">
      <div class="phoneBox">
        <img class="phoneIcon" src="../../assets/imgs/load_img_02.png" alt="手机图标">
        <mt-field placeholder="请输入手机号" type="tel" v-model="phone"></mt-field>
      </div>
      <div class="yzmBox">
        <img class="passwordIcon" src="../../assets/imgs/load_img_04.png" alt>
        <mt-field id="password" placeholder="请输入验证码" type="text" v-model="yzm"></mt-field>
        <img  class="yzm" src="../../assets/imgs/load_btn_yzm.png" alt>
      </div>
      <div class="loginBtn" @click="updatepassword">
        <img src="../../assets/imgs/load_btn_xyb.png" alt>
      </div>
    </div>
  </div>
</template>
<script>

export default {
  data() {
    return {
      phone: "",
      yzm: ""
    };
  },
  methods: {
      updatepassword(){
          this.$router.push("/login/updatepassword")
      }
  }
};
</script>
<style lang="less">
@fontcolor:#cfcfcf;
.forgetpasswordBox {
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
    .mint-field-core{
      height: 80px;
      font-size: 30px;
    }
    a {
      display: inline-block;
      width: 100%;
    }
    .phoneBox,.yzmBox {
      height: 80px;
      display: flex;
      justify-content: flex-start;
      border-bottom: 1px solid #cacaca; /* no */
      width: 100%;
      padding: 0 20px;
      box-sizing: border-box;
      align-items: center;
       input::-webkit-input-placeholder{
        color:@fontcolor;
        font-size: 28px;
      }
      .phoneIcon {
        width: 24px;
        height: 36px;
        margin-right: 25px;
      }
      .passwordIcon {
        width: 20px;
        height: 36px;
        margin-right: 25px;
      }
    }
    .yzmBox {
      justify-content: space-between;
      margin-top: 60px;
      .yzm {
        width: 170px;
        height: 48px;
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