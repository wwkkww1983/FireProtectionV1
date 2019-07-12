import Vue from 'vue'
import {
  Indicator,
  Toast
} from 'mint-ui'
let o = {
  stopBack() {
    let that = this;
    plus.key.removeEventListener('backbutton',function(){});
    plus.key.addEventListener("backbutton", function() {
      Indicator.close();
      that.$confirm('确定要退出程序吗？','操作提醒',{
        confirmButtonText: '退出',
        cancelButtonText: '取消'
      }).then(() => {
        plus.runtime.quit()
      }).catch(() => {
        return
      })
    }, false)
  },
  back(beforeBack) { // 点击手机`返回`按钮,非主页面返回上级;如果有返回前处理执行,如果前处理没有返回,或返回!true不再执行返回事件
    plus.key.removeEventListener('backbutton',function(){});
    plus.key.addEventListener("backbutton", function() {
      let cross = false;
      Indicator.close();
      if(beforeBack){
        cross = beforeBack()
        if(cross){
          history.go(-1);
        }
      }else{
        history.go(-1);
      }      
    }, false)
  }
}

window.phone = o; // 挂载到window上
Vue.prototype.phone = o;
export default o;