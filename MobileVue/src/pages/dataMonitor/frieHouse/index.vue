<style lang="less">
.border {
  border-bottom: 1px solid #dcdcdc; /* no */
}
.fireHouseBox {
  width: 100%;
  padding-top: 80px;
  box-sizing: border-box;
  .fireHousesContainer {
    .topNav {
      .border();
      .mint-navbar .mint-tab-item.is-selected {
        width: 160px;
        height: 54px;
        background-color: #039cfe;
        border-radius: 8px;
        color: white;
        margin: 0;
        line-height: 54px;
        font-size: 30px;
      }
      .mint-navbar .mint-tab-item {
        padding: 0;
        color: #989898;
        font-size: 30px;
      }
      .mint-tab-item-label {
        line-height: 54px;
        font-size: 30px;
      }
      .mint-tab-item {
        padding: 0;
        flex: none;
      }
      .mint-navbar {
        height: 78px;
        display: flex;
        align-items: center;
        padding: 0 20px;
        justify-content: flex-start;
        font-size: 26px;
        .mint-tab-item-label {
          width: 160px;
          height: 54px;
          font-size: 30px;
        }
      }
      .searchBox {
        width: 100%;
        padding: 20px;
        box-sizing: border-box;
        .search {
          width: 680px;
          height: 58px;
          border: 1px solid #dcdcdc;/*  no */
          overflow: hidden;
          border-radius: 8px;
          display: flex;
          align-items: center;
          padding: 0 16px;
          input {
            height: 100%;
            width: 100%;
            border: none;
            font-size: 24px;
            &::placeholder {
              color: #cfcece;
            }
            &:disabled {
              background: transparent;
            }
          }
          img {
            width: 30px;
            height: 36px;
          }
        }
      }
      .addressBox{
        font-size: 24px;
        color: #a0a0a0;
        padding: 20px;
      }
    }
    /*综合  */
    .comprehensive{
        ul {
          list-style: none;
          li {
            padding: 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            .border();
            .rightContainer {
              .frieHouseName {
                display: inline-block;
                font-size: 30px;
                color: #262626;
                margin-bottom: 20px;
              }
              .frieHouseAddress {
                font-size: 26px;
                color: #b2b1b1;
              }
            }
            img {
              width: 18px;
              height: 30px;
            }
          }
        }
     }
    /* 附近 */
    .near {
      ul {
        list-style: none;
        li {
          display: flex;
          padding: 20px;
          align-items: center;
          font-size: 26px;
          color: #b2b1b1;
          &.nearNoData{
            text-align: center;
            border: none;
          }
         /*  .border(); */
          .leftContainer {
            width: 86%;
            strong {
              font-weight: normal;
              color: #262626;
              font-size: 30px;
              font-weight: bold;
            }
            .contact {
              margin: 16px 0;
            }
          }
          img {
            width: 60px;
            height: 60px;
          }
        }
      }
      .noData{
       font-size: 30px;
       text-align: center;
       height: 80px;
       line-height: 80px;
       color: #a0a0a0;
      }
    }
/* ************************************************8 */
.page-loadmore{
  overflow:scroll;
   touch-action: none;
  
.page-loadmore-wrapper {
    overflow: scroll;
}
.mint-loadmore-bottom span {
    display: inline-block;
    -webkit-transition: .2s linear;
    transition: .2s linear;
    vertical-align: middle
}
}
/* ****************************** */
  }
  .messageIcon {
    width: 110px;
    height: 114px;
    position: fixed;
    bottom: 40px;
    right: 40px;
  }
  .mint-popup {
    width: 750px;
    height: 646px;
    .popupBox {
      width: 100%;
      .top {
        width: 100%;
        background-color: #03aefe;
        height: 70px;
        color: white;
        font-size: 30px;
        text-align: center;
        line-height: 70px;
        position: relative;
        img {
          position: absolute;
          right: 20px;
          top: 20px;
          width: 34px;
          height: 34px;
        }
      }
      .container {
        padding: 26px;
        font-size: 26px;
        width: 100%;
        box-sizing: border-box;
        .tips {
          color: #c5c5c5;
          line-height: 1.5;
          margin-bottom: 42px;
        }
        .messageTitle {
          color: #262626;
          display: inline-block;
          margin-bottom: 22px;
        }
        .name {
          color: #262626;
        }
        .address {
          width: 666px;
          height: 56px;
          border-radius: 8px;
         border: solid 1px #c2c2c2;  /* no */
          padding-left: 14px;
          line-height: 56px;
          margin: 24px 0;
          input{
            border:none;
            width: 100%;
          }
        }
        .note {
          color: #262626;
          margin-bottom: 22px;
        }
        .sended {
          width: 100%;
          text-align: right;
          color: #262626;
          margin-bottom: 36px;
        }
        .sendBtn {
          width: 100%;
          height: 76px;
          background: url("../../../assets/imgs/frieHouse/btn_bg.png") repeat-x;
          line-height: 76px;
          text-align: center;
          color: white;
          font-size: 32px;
          border-radius: 8px;
        }
      }
    }
  }
}
</style>
<template>
  <div class="fireHouseBox">
   <tabBack :title="$route.name" mark="TodataMonitor"></tabBack>
   <topHome></topHome>
    <div class="fireHousesContainer">
      <!-- topNav -->
      <div class="topNav ">
        <mt-navbar v-model="selected">
          <mt-tab-item id="comprehensive">综合</mt-tab-item>
          <mt-tab-item id="near">附近</mt-tab-item>
        </mt-navbar>
        <div class="searchBox" @click="goToSearch($route.name)">
          <div class="search border-1px">
            <input type="text" disabled :placeholder="(selected == 'comprehensive')?'输入名称进行模糊查询':'输入地址查询'">
            <img src="../../../assets/imgs/fireUnits//search_btn.png" alt>
          </div>
        </div>
        <p  class="addressBox" v-if="selected == 'near'">
          查找“{{address}}”附近结果
        </p>
      </div>

      <!-- container -->
      <mt-tab-container v-model="selected">
        <!-- 综合 -->
        <mt-tab-container-item id="comprehensive">
          <div class="page-loadmore comprehensive">
              <div class="page-loadmore-wrapper" ref="wrapper" :style="{ height: wrapperHeight + 'px' }">
                <mt-loadmore :bottom-method="loadBottom" @bottom-status-change="handleBottomChange" :bottom-all-loaded="allLoaded" :autoFill="autoFill" ref="loadmore">
                  <ul class="page-loadmore-list">
                    <li  v-for="(arr,index) in frieHouseArr" :key="index" @click="goToFrieHosueInfo(arr.id)" class=" page-loadmore-listitem">
                        <div class="rightContainer">
                          <span class="frieHouseName">{{arr.name}}</span>
                          <p class="frieHouseAddress">{{arr.address}}</p>
                        </div>
                      <img src="../../../assets/imgs/fireUnits/next_btn.png" alt>
                    </li>
                  </ul>
                  <div slot="bottom" class="mint-loadmore-bottom">
                    <span v-show="bottomStatus !== 'loading'" :class="{ 'is-rotate': bottomStatus === 'drop' }">↑</span>
                    <span v-show="bottomStatus === 'loading'">
                      <mt-spinner type="snake"></mt-spinner>
                    </span>
                  </div>
                </mt-loadmore>
              </div>
          </div>
        </mt-tab-container-item>
        <!-- 附近 -->
        <mt-tab-container-item id="near">
          <div class="near">
            <ul v-if="NearfrieHouseArr.length>0">
              <li  v-for="(arr,index) in NearfrieHouseArr" :key="index">
                <div class="leftContainer">
                  <strong>{{arr.name}}</strong>
                  <p class="contact">距离当前位置{{arr.distance}}米 | {{arr.contactName}}（{{arr.contactPhone}}）</p>
                  <p>{{arr.address}}</p>
                </div>
                <a :href="'tel:'+arr.contactPhone">
                 <img src="../../../assets/imgs/fireUnits/call_btn.png" alt>
                </a>
               
              </li>
              
              
            </ul>
            <div class="noData" v-else>
                  该地理位置附近暂无微型消防站
            </div>

            <img v-if="NearfrieHouseArr.length>0" @click="message" class="messageIcon" src="../../../assets/imgs/frieHouse/xfz_img_news.png" alt>
          </div>
        </mt-tab-container-item>
      </mt-tab-container>
    </div>
    <mt-popup v-model="messageVisible" position="bottom" pop-transition="popup-fade">
      <div class="popupBox">
        <div class="top">
          <P>紧急救援通知</P>
          <img @click="closeModal" class="close" src="../../../assets/imgs/frieHouse/close.png" alt>
        </div>
        <div class="container">
          <p class="tips">
            紧急短信通道，仅用于抢险救灾时向附近的各微型消防站
            管理人员群发紧急抢险短息通知
          </p>
          <span class="messageTitle">短信内容：</span>
          <div class="name">
            <span>【微型消防站】</span>
            ，您好！
          </div>
          <p class="address">
             <input type="text" v-model="messageAddress">
          </p>
          <p class="note">发生险情，请立即前往现场救援，谢谢！</p>
          <div class="sended">成都市成华区消防大队</div>
          <div class="sendBtn" @click="sendMessage" >发送</div>
        </div>
      </div>
    </mt-popup>
  </div>
</template>
<script>
import topHome from "../../../components/topHome/index";
import tabBack from "../../../components/topBack/index";
import { location } from "../../../staic/map.js";
import { Indicator } from 'mint-ui'


export default {
  components: {
    topHome,
    tabBack
  },
  data() {
    return {
      selected: "comprehensive",
      messageVisible: false,
      frieHouseArr: [],
      lat:1,
      lng:2,
      address:'正在定位中',
      NearfrieHouseArr: [],
      allLoaded:false,
      autoFill:false,
      bottomStatus: '',
      wrapperHeight: 0,
      SkipCount:0,
      MaxResultCount:9,
      count:0,
      selectedcount:0,
      messageAddress:''
    };
  },
  mounted() {
    console.log("高度",document.documentElement.offsetHeight,window.screen.height)
    this.wrapperHeight = window.screen.height- this.$refs.wrapper.getBoundingClientRect().top-20;

    //接收参数
    let selected = this.$route.query.selected;
    let lat = this.$route.query.lat;
    let lng = this.$route.query.lng;
    let address = this.$route.query.address
    if(selected && lat && lng){
      this.selected = selected
      this.lat = lat
      this.lng = lng
      this.address = address
      this.messageAddress = address

      this.$http({
          method: "get",
          url: "/api/services/app/MiniFireStation/GetNearbyStation",
          params:{
            lng,
            lat
          }

      }).then(res=>{
        console.log("选择地址后附近微型消防站的数据成功",res)
        if(res.data.result.length>0){
          that.NearfrieHouseArr = res.data.result
        }
      }).catch(res=>{
        console.log("选择地址后附近微型消防站的数据失败",res)
      })
    }else{
       this.getPosition(); // 调用获取地理位置
    }


    Indicator.open({
      text: "加载中...",
      spinnerType: "fading-circle"
    });
    
    let that = this;
   

    /* 综合初始化请求数据 */
    this.$http({
      method: "get",
      url: "/api/services/app/MiniFireStation/GetList",
      params: {
        SkipCount: that.SkipCount,
        MaxResultCount: that.MaxResultCount
      }
    })
      .then(function(res) {
        console.log("微型消防站成功", res);
        if (res.status == 200) {
          that.frieHouseArr = res.data.result.items;
          Indicator.close();
        }
      })
      .catch(function(res) {});
  },
  methods: {
    /* 定位 */
    getPosition() {
      if(localStorage.getItem('lat') && localStorage.getItem('lng')){
        this.address = localStorage.getItem('locationAddress')
        this.messageAddress = localStorage.getItem('locationAddress')
        let that = this;
            that.$http({
              method: "get",
              url: "/api/services/app/MiniFireStation/GetNearbyStation",
              params: {
                lng:localStorage.getItem('lng'),
                lat:localStorage.getItem('lat'),
              }
            }).then(function(res) {
              console.log("附近消防数据", res);
              Indicator.close();
              if(res.data.result.length>0){
                that.NearfrieHouseArr = res.data.result
              }
            }).catch(function(res) {
                console.log("附近消防失败", res);
            });
      }
            
    },
    /* 跳转搜索 */
    goToSearch(mark) {
      if(this.selected == 'near'){
         this.$router.push({
          path:'/dataMonitor/fireHouseInfo/addressSearch',
          query:{
            mark:mark
          }
        })
      }else{
         this.$router.push({
          path:'/search',
          query:{
            mark:mark
          }
        })
      }
     
    },
    /* 跳转到具体消防站信息 */
    goToFrieHosueInfo(id) {
      this.$router.push({
        path: "/dataMonitor/fireHouseInfo",
        query: {
          id
        }
      });
    },
    /* //message */
    message() {
      this.messageVisible = true;
    },
    /* 发送短信 */
    sendMessage(){
      this.messageVisible = false;
    },
    /* 关闭message蒙层 */
    closeModal() {
      this.messageVisible = false;
    },
    /*  */
    handleBottomChange(status) {
        this.bottomStatus = status;
      },
    /* 加载更多 */
    loadBottom() {
      let that = this;
        that.count++;
      that.$http({
          method: "get",
          url: "/api/services/app/MiniFireStation/GetList",
          params: {
            SkipCount: that.count*that.MaxResultCount,
            MaxResultCount: 9
          }
        }).then(function(res) {
            if (res.status == 200) {
              console.log("微型消防站成功",that.count, res);
              if(res.data.result.items.length<that.MaxResultCount){
                that.allLoaded = true;
              }

               for(let arr in res.data.result.items){
                    that.frieHouseArr.push(res.data.result.items[arr]);
                  }

              that.$refs.loadmore.onBottomLoaded();
            }
          })
          .catch(function(res) {});
    }
  }
};
</script>