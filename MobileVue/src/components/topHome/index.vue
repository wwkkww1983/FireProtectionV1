<style lang="less">
.topHome{
    width: 40px;
    height: 80px;
    position: fixed;
    right: 20px;
    top: 0px;
    z-index: 2001;
    img{
      width: 100%;
      height: 40px;
      position: absolute;
      top: 50%;
      transform: translateY(-50%);
     
    }
   
}

</style>
<template>
  <div class="topHome">
    <img   @click="goHome" src="../../assets/imgs/home.png" alt="">
  </div>
</template>
<script>
export default {
methods: {
  goHome(){

   this.$router.push({
     path:'/dataMonitor'
   })
  }
},
};
</script>