import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex);
//创建Vuex实例
const store = new Vuex.Store({
  state:{
      phone:''
  },
  mutations:{
     phoneUser(state,phone){
        state.phone = phone
     } 
  }
})

export default store // 导出store
