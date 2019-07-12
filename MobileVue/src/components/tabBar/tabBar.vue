<style lang="less">
  .navbar{
    width: 100%;
    height: 100px;
    position: fixed;
    bottom: 0;
    left: 0;
    box-shadow: 0px -1px 15px 0px 
		rgba(0, 0, 0, 0.3);
    ul{
      width: 100%;
      height: 100%;
      display: flex;
      flex-direction: row;
      align-items: center;
      justify-content: space-around;
      li{
        display: flex;
        justify-content: center;
        img{
          width: 96px;
          height: 76px;
        }
      }
    }
  }
</style>
<template>
  <div class="navbar" >
    <ul>
      <li  @click="$router.push('/dataMonitor')">
        <img :src="($route.name=='dataMonitor' )?blue_sjjk:gray_sjjk" alt="">
      </li>
       <li  @click="$router.push('/regulatory')">
        <img :src="$route.name==='regulatory'?blue_jgzf:gray_jgzf" alt="">
      </li>
       <li  @click="$router.push('/mySet')">
        <img :src="$route.name=='mySet'?blue_wdsz:gray_wdsz" alt="">
      </li>
    </ul>
  </div>
</template>
<script>
import gray_sjjk from "../../assets/imgs/lobby_btn_gray_sjjk.png"
import blue_sjjk from "../../assets/imgs/lobby_btn_blue_sjjk.png"
import gray_jgzf from "../../assets/imgs/lobby_btn_gray_jgzf.png"
import blue_jgzf from "../../assets/imgs/lobby_btn_blue_jgzf.png"
import gray_wdsz from "../../assets/imgs/lobby_btn_gray_wdsz.png"
import blue_wdsz from "../../assets/imgs/lobby_btn_blue_wdsz.png"
export default {
  components: {},
  data() {
    return {
      gray_sjjk,
      blue_sjjk,
      gray_jgzf,
      blue_jgzf,
      gray_wdsz,
      blue_wdsz
      };
  },
  watch: {}, //监听属性
  created() {
  },

  computed: {}, //计算属性

  methods: {
  }, //方法

  mounted: function() {
    
  } //钩子函数
};
</script>
