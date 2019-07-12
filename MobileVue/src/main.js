// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import MintUI from 'mint-ui'
import 'mint-ui/lib/style.css'
import 'lib-flexible/flexible.js'
import axios from 'axios'
import store from './store'
import echarts from 'echarts'
/* import VConsole from 'vconsole'
let vConsole = new VConsole();//初始化
 Vue.use(vConsole);//设为全局 */
Vue.config.productionTip = false
Vue.use(MintUI);//定义全局MintUI
Vue.prototype.$http = axios
Vue.prototype.$echarts = echarts
/* axios.defaults.baseURL = '/api' */
/*  axios.defaults.baseURL = 'http://fd.sctsjkj.com:5080'  */
 axios.defaults.baseURL = 'http://47.98.179.238:5080' 
/*   axios.defaults.baseURL = 'http://192.168.0.67:5080'   */

/* eslint-disable no-new */
//创建全局bus
const bus = new Vue({});
Vue.prototype.$bus = bus;

new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
