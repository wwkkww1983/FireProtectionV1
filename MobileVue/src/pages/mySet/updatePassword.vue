<style lang="less">
.border-bottom {
  border-bottom: 1px solid #dcdcdc; /* no */
}
.updatePasswordBox {
  padding-top: 140px;
  box-sizing: border-box;
  .fromBox {
      padding: 0px 20px;
    .originalPassword,
    .newPassword,
    .reNewPassword {
      display: flex;
      padding: 20px;
      align-items: center;
      margin-bottom: 26px;
      .border-bottom();
      .mint-field-core {
        height: 80px;
        font-size: 30px;
      }
      a {
        display: inline-block;
        width: 100%;
      }

      img {
        margin-right: 24px;
        &.icon01 {
          width: 20px;
          height: 36px;
        }
        &.icon02 {
          width: 20px;
          height: 32px;
        }
        &.icon03 {
          width: 28px;
          height: 30px;
        }
      }
      input {
        &::placeholder {
          color: #cfcfcf;
          font-size: 28px;
        }
      }
    }
    .btn {
      background: url("../../assets/Btn_bg.png") repeat-x;
      height: 78px;
      width: 100%;
      border-radius: 10px;
      margin-top: 56px;
      color: white;
      font-size: 32px;
      line-height: 78px;
      text-align: center;
    }
  }
}
</style>
<template>
  <div class="updatePasswordBox">
    <topBack :title="$route.name"></topBack>
    <div class="fromBox">
      <div class="originalPassword">
        <img class="icon01" src="../../assets/imgs/mySet/sist_img_07.png" alt>
        <mt-field placeholder="请输入原始密码" type="password" v-model="originalPassword"></mt-field>
      </div>
      <div class="newPassword">
        <img class="icon02" src="../../assets/imgs/mySet/sist_img_08.png" alt>
        <mt-field placeholder="请设置新密码" type="password" v-model="newPassword"></mt-field>
      </div>
      <div class="reNewPassword">
        <img class="icon03" src="../../assets/imgs/mySet/sist_img_09.png" alt>
        <mt-field placeholder="请再次输入密码" type="password" v-model="reNewPassword"></mt-field>
      </div>

      <div class="btn" @click="submit">确定</div>
    </div>
  </div>
</template>
<script>
import topBack from "../../components/topBack/index";
import { Toast } from "mint-ui";
export default {
  components: {
    topBack
  },
  data() {
    return {
      originalPassword: "",
      newPassword: "",
      reNewPassword: ""
    };
  },
  methods: {
    submit() {
      let that = this;
      if (this.newPassword == this.reNewPassword) {
        let account = localStorage.getItem('userName')
        let oldPassword = this.originalPassword;
        let newPassword = this.newPassword
        this.$http({
            method: "post",
            url: "/api/services/app/FireDeptUser/ChangePassword",
            data:{
                account,
                oldPassword,
                newPassword
            }
        }).then(res=>{
          console.log("打印修改密码成功",res)
          if(res.data.result.success){
            localStorage.clear();
            that.$router.push({
              path:'/login'
            })
          }
        }).catch(res=>{
           console.log("打印修改密码失败",res)
        })
      } else {
        Toast("两次密码输入不一致");
      }
    }
  }
};
</script>