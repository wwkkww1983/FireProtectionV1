<style lang="less">
.dataMonitorBox {
  box-sizing: border-box;
  height: 100%;
  padding-top: 78px;
  .bannerBox {
    width: 100%;
    height: 180px;

    img {
      width: 100%;
      height: 100%;
    }
  }
  .listBox {
    padding: 0 26px;
    box-sizing: border-box;
    .container {
      display: flex;
      align-items: center;
      margin-top: 70px;
      position: relative;
      .datas {
        position: absolute;
        color: #fff;
        left: 35%;
        top:50%;
        transform: translateY(-50%);
        font-size: 28px;
        display: flex;
        flex-direction: column;
        line-height: 55px;
        font-size: 32px;


      }
      .rightImg {
        width: 100%;
        height: 160px;
      }
    }
  }
}


@media screen and(min-width: 320px)and(max-width: 359px){ 
.dataMonitorBox .listBox .container{
  margin-top: 30px;
  background: pink;

 
}
} 
@media screen and(min-width: 360px)and(max-width: 374px)and(max-height:640px){ 
.dataMonitorBox .listBox .container{
  margin-top: 30px;
/*   background: fuchsia; */

}
} 
@media screen and(min-width: 375px)and(max-width: 385px){ 
.dataMonitorBox .listBox .container{ 
  margin-top: 50px;
/*   background: blue; */
  } 

}
@media screen and(min-width: 375px)and(min-height: 812px){ 
  .dataMonitorBox .listBox .container{ 
    margin-top: 120px;
/*     background: greenyellow; */


  } 
}
@media screen and(min-width: 386px)and(max-width: 392px) { 
  .dataMonitorBox .listBox .container{ 
    margin-top: 60px;
/*     background: green; */
  } 
} 
@media screen and(min-width: 401px)and(max-width: 414px)and(max-height:732px){ 
  .dataMonitorBox .listBox .container{ 
    margin-top: 40px;
/*     background: yellow; */
  } 
} 
@media screen and(min-width: 590px)and(max-width: 660px){ 
  .dataMonitorBox .listBox .container{ 
    margin-top: 76px;
/*     background: orange; */
  } 
} 
@media screen and(min-width: 750px)and(max-width: 799px){ 
  .dataMonitorBox .listBox .container{ 
    margin-top: 76px;
/*     background: red; */
  } 
} 


@media only screen and (-webkit-device-pixel-ratio:.75){/*低分辨率小尺寸的图片样式*/
.dataMonitorBox{
  background: pink;
}
}
@media only screen and (-webkit-device-pixel-ratio:1){/*普通分辨率普通尺寸的图片样式*/
.dataMonitorBox{
  background: blueviolet;
}
}
@media only screen and (-webkit-device-pixel-ratio:1.5){/*高分辨率大尺寸的图片样式*/
.dataMonitorBox{
  background: skyblue;
}
}


</style>
<template>
  <div class="dataMonitorBox">
    <topinfo></topinfo>
    <div class="bannerBox">
      <img src="../../assets/imgs/dataMonitor/hall_img.png" alt>
    </div>
    <div class="listBox">
      <div class="container" v-for="n in dataMonitorList" :key="n.img" @click="changePage(n.path)">
        <div class="datas">
          <span>{{n.name}}</span>
          <span>已接入:{{n.number}}</span>
        </div>
        <img class="rightImg" :src="n.img">
      </div>
    </div>

    <tabBar></tabBar>
  </div>
</template>
<script>
import tabBar from "../../components/tabBar/tabBar";
import topinfo from "../../components/topinfo/topinfo";
import fireUnits from "../../assets/imgs/dataMonitor/hall_btn_01.png";
import fireHouse from "../../assets/imgs/dataMonitor/hall_btn_02.png";
import fireCock from "../../assets/imgs/dataMonitor/hall_btn_03.png";
import fireDataReport from "../../assets/imgs/dataMonitor/hall_btn_04.png";
import { Toast,Indicator } from "mint-ui";
export default {
  components: {
    tabBar,
    topinfo
  },
  data() {
    return {
      dataMonitorList: [
        { img: fireUnits, path: "fireUnits", number: "暂无数据",name:'防火单位' },
        {
          img: fireHouse,
          path: "fireHouse",
          number: "暂无数据",
          name:'微型消防站',
        },
        {
          img: fireCock,
          path: "fireCock",
          number: "暂无数据",
          name:'市政消火栓'
        },
        {
          img: fireDataReport,
          path: "fireDataReport",
          number: "暂无数据",
          name:'综合数据报表'
        }
      ],
      consolidatedNumber: "暂无数据",
      count:0
    };
  },
  mounted() {
    this.count++
    console.log("打印第一进入",this.count)

    let that = this;
    this.getPosition();//初始化定位
    /* 初始化数据数量接口 */
    this.$http({
      method: "get",
      url: "/api/services/app/DataReport/GetDataMinotor"
    })
      .then(function(res) {
        if (res.status == 200) {
          console.log("首页数据请求成功")
          that.consolidatedNumber = res.data.result[3].joinNumber;
          for (let i = 0; i < res.data.result.length; i++) {
              if(i == 0){
             that.dataMonitorList[i].number = res.data.result[i].joinNumber;
              }else{
                  that.dataMonitorList[i].number = res.data.result[i].joinNumber;
              }
          }
        }
      })
      .catch(function(res) {
          
        if (res.status !== 200) {
          Toast({
            message: "网络连接超时",
            position: "center",
            duration: 3000
          });

          setTimeout(()=>{
            Indicator.close();
          },3000)
        }
      });

      
  },

  activated() {
    let that = this;
     this.$http({
      method: "get",
      url: "/api/services/app/DataReport/GetDataMinotor"
    })
      .then(function(res) {
        if (res.status == 200) {
          that.consolidatedNumber = res.data.result[3].joinNumber;
          for (let i = 0; i < res.data.result.length; i++) {
              if(i == 0){
             that.dataMonitorList[i].number = res.data.result[i].joinNumber;
              }else{
                  that.dataMonitorList[i-1].number = res.data.result[i-1].joinNumber;
              }
          }
        }
      })
      .catch(function(res) {
          
        if (res.status !== 200) {
          Toast({
            message: "网络连接超时",
            position: "center",
            duration: 3000
          });

          setTimeout(()=>{
            Indicator.close();
          },3000)
        }
      });
  },

  methods: {
    changePage(n) {
      console.log("数据监控跳转", n);
      this.$router.push({
        path: `/dataMonitor/${n}`
      });
    },
     getPosition() {
            let that = this;
            let mapObj = new AMap.Map("iCenter");
            mapObj.plugin("AMap.Geolocation", function() {
            let geolocation = new AMap.Geolocation({
            enableHighAccuracy: true, //是否使用高精度定位，默认:true
            timeout: 10000, //超过10秒后停止定位，默认：无穷大
            maximumAge: 0, //定位结果缓存0毫秒，默认：0
            convert: true, //自动偏移坐标，偏移后的坐标为高德坐标，默认：true
            showButton: true, //显示定位按钮，默认：true
            buttonPosition: "LB", //定位按钮停靠位置，默认：'LB'，左下角
            buttonOffset: new AMap.Pixel(10, 20), //定位按钮与设置的停靠位置的偏移量，默认：Pixel(10, 20)
            showMarker: true, //定位成功后在定位到的位置显示点标记，默认：true
            showCircle: true, //定位成功后用圆圈表示定位精度范围，默认：true
            panToLocation: true, //定位成功后将定位到的位置作为地图中心点，默认：true
            zoomToAccuracy: true //定位成功后调整地图视野范围使定位位置及精度范围视野内可见，默认：false
            });
            mapObj.addControl(geolocation);
            geolocation.getCurrentPosition();
            AMap.event.addListener(geolocation, "complete", onComplete); //返回定位信息
            AMap.event.addListener(geolocation, "error", onError); //返回定位出错信息
            });
            function onComplete(status, result) {
                console.log("定位成功",status, result);
                let { M, O} = status.position;
                console.log(" P, Q",  M, O)
                localStorage.setItem('lat',M)
                localStorage.setItem('lng',O)
                localStorage.setItem('locationAddress',status.formattedAddress)
                let navgationAddress = status.addressComponent.district + status.addressComponent.township +status.addressComponent.street+status.addressComponent.streetNumber
                localStorage.setItem('navgationAddress',navgationAddress)

            }
            function onError(status, result) {
              console.log("定位失败",status, result);
              localStorage.setItem('locationFail','定位失败')


            }
    }
  }
};
</script>