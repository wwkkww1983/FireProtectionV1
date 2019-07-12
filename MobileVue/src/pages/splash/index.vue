<template>
  <div class="splash">
    <img src="../../assets/imgs/guidePages_img.png" alt="引导页">
  </div>
</template>
<script>
import { setTimeout } from "timers";

export default {
  mounted() {
    const _this = this;
    //未登录，跳转到登录页，已登录跳转到首页
    if (localStorage.getItem("phoneUser")) {
        setTimeout(()=>{
            _this.$router.replace("/dataMonitor");
        },3000)
    } else {
      setTimeout(() => {
        _this.$router.replace("/login");
      }, 3000);
    }
  }
};
</script>
<style lang="less">
.splash {
  width: 100%;
  height: 100%;
  img {
    width: 100%;
    height: 100%;
  }
}
</style>
