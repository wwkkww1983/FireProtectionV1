<style lang="less">
.topbackBox{
    width: 100%;
    position: fixed;
    top: 0px;
    z-index: 1999;
    .minta{
        width: 100%;
        height: 80px;
        background: url("../../assets/imgs/load_up_img_bg.png") repeat-x;
        position: relative;
        padding:0px 27px;
        display: flex;
        justify-content: flex-start;
        -webkit-align-items: center;
        -moz-align-items: center;
        -ms-align-items: center;
        -o-align-items: center;
         align-items: center;
         .title{
          text-align: center;
          width: 400px;
          text-overflow: ellipsis;
          overflow: hidden;
          white-space: nowrap;
          font-size: 36px;
          font-weight: normal;
          color: #fff;
          position: relative;
          left: 40%;
          transform: translateX(-40%);
    }
    .return_btn{
        width: 20px;
        height: 34px;
    }
    }
   
}

</style>
<template>
  <div class="topbackBox">
    <div class="minta">
            <img  @click="back" class="return_btn" src="../../assets/imgs/fireUnits/return_btn.png" alt="">
            <div class="title">{{title}}</div>
    </div>
  </div>
</template>
<script>
export default {
  props:{ 
    title:String,
    symbol:String,
    from:String,
    mark:String,
    require:true
  },
data(){
      return{
        msg:''
      }
    },
mounted(){
   /* onst _this = this ;//把当前的有、this赋给_this，_this的作用域和this作用域一样
   this.$bus.$on('sendToTabBack',(data)=>{
        _this.msg = data
      }) */
},
methods: {
  back(){
    if (this.symbol) {
       this.$router.push({
         path:this.from,
         query:{
           symbol:this.symbol
         }
       })
    }else if(this.mark == 'TofireUnits'){
      this.$router.push({
         path:'/dataMonitor/fireUnits',
       })
    }else if(this.mark == 'TodataMonitor'){
       this.$router.push({
         path:'/dataMonitor',
       })
       
    }else if(this.mark == 'TofireHouse'){
       this.$router.push({
         path:'/dataMonitor/fireHouse',
       })
       
    }else if(this.mark == 'TofireCock'){
       this.$router.push({
         path:'/dataMonitor/fireCock',
       })
       
    }else if(this.mark == 'Toregulatory'){
       this.$router.push({
         path:'/regulatory',
       })
       
    }else{
       this.$router.go(-1)
    }
    
  }
}
 

};
</script>