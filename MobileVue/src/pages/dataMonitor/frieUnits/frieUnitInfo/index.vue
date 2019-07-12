<style lang="less">
@fontColor: #262626;
@ulBorder: #dcdcdc;
.frieUnitsInfo {
  padding-top: 80px;
  box-sizing: border-box;
  width: 100%;
  .mint-header {
    width: 100%;
    height: 80px;
    background: url("../../../../assets/imgs/load_up_img_bg.png") repeat-x;
  }
  .mint-header-title {
    font-size: 36px;
  }
  .mintui-back:before {
    color: transparent;
  }
  .return_btn {
    width: 20px;
    height: 34px;
  }

  .tabNavBox {
    height: 100%;
    width: 100%;
    
    .mint-navbar .mint-tab-item.is-selected {
      width: 160px;
      height: 54px;
      background-color: #039cfe;
      border: 1px solid #dcdcdc;/* no */
      border-radius: 8px;
      color: white;
      margin: 0;
      line-height: 54px;
      font-size: 30px;
    }
    .mint-navbar .mint-tab-item {
      padding: 0;
    }
    .mint-tab-item-label {
      line-height: 54px;
      max-width: 222px !important;
      font-size: 30px;
    }
    .mint-tab-item {
      padding: 0;
      flex: none;
      color: #989898;
      font-size: 30px;
    }
    .mint-navbar {
      height: 78px;
      display: flex;
      align-items: center;
      justify-content: space-around;
      border-bottom: solid 1px #dcdcdc;  /*no */
      .lawyLog{
        display: inline-block;
        width: 210px !important;
        .mint-tab-item-label{
          width: 100%;
        }
      }
      .mint-tab-item-label {
        font-size: 30px;
        width: 160px;
        height: 54px;
      }
    }
  }
/* 基本信息 */

  .baseInfoBox{
    padding: 0px 20px;
    box-sizing: border-box;
    table {
      font-size: 26px;
      color: #262626;
      position: relative;
      left: 50%;
      transform: translateX(-50%);
      td {
        height: 100px;
        &.leftTd{
          width: 180px;
        }
        .phoneNumBox {
          display: flex;
          align-items: center;
        }
        .callIcon {
          width: 60px;
          height: 60px;
          margin-left: 66px;
        }
      }
    }
    .placedTop{
      background: url("../../../../assets/imgs/fireUnits/btn_bg.png") repeat-x;
      height: 80px;
      border-radius: 10px;
      width: 100%;
      text-align: center;
      line-height: 80px;
      color: white;
      font-size: 30px;
      margin-top: 40px;
    }
    .cnacelTop{
      height: 80px;
      border-radius: 10px;
      width: 100%;
      text-align: center;
      line-height: 80px;
      color: #0392fe;
      font-size: 30px;
      margin-top: 40px;
      background-color: white;
      border: 1px solid #0392fe; /* no */
    }
  }
  
  /* 消防数据 */
  .frieDataBox {
    padding: 26px;
    padding-top: 0px;
    .safeElectricity,
    .fireAlarm,.equipmentFailure,.patrolRecord,.onDutyRecord{
      font-size: 24px;
      color: @fontColor;
     border-bottom: 1px solid @ulBorder;  /* no */
      padding-top: 26px;
      padding-bottom: 1px;

      li {
        list-style: none;
        margin-bottom: 34px;
        display: flex;
        align-items: center;
        &.fristLi {
          font-size: 28px;
        }
        .rightText {
          margin-left: 20px;
        }

        .ElectricityIcon {
          width: 28px;
          height: 34px;
          margin-right: 18px;
        }
        .fireIcon {
          width: 35px;
          height: 35px;
        }
        .viewIcon {
          width: 36px;
          height: 36px;
          margin-left: 16px;
        }
        .FailureIcon{
          width: 34px;
          height: 34px;
        }
        .RecordIcon{
          width: 32px;
          height: 32px;
        }
        .DutyIcon{
          width: 32px;
          height: 32px;
        }
      }
    }
  }
  /* 执法日志 */
  .lawyLogBox{
    padding: 0 20px;
    li{
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 32px 0;
      font-size: 26px;
      color: #262626;
      border-bottom: 1px solid #dcdcdc;/*  no */
      .leftBox{
        display: flex;
        align-items: center;
        .time{
          margin-right: 64px;
        }
        .yellow{
          color: #ff8400;
        }
        .orange{
          color: #ff0000;
        }
        .kk{
          color: seagreen;
        }
      }
      .rightIcon{
         width: 18px;
         height: 30px;
      }

    }
  }
  /* 执法日志无数据 */
  .noDataBox{
    width: 100%;
    height: 300px;
    position: relative;
    display: flex;
    justify-content: center;
    align-items: center;
    .noDataIcon{
      width: 170px;
      height: 30px;
    }
  }


}
</style>
<template>
  <div class="frieUnitsInfo">
  <!--   <mt-header fixed :title="$route.query.name">
      <router-link to="" slot="left">
        <mt-button icon="back"  @click.native="goBack">
          <img class="return_btn" src="../../../../assets/imgs/fireUnits/return_btn.png" alt>
        </mt-button>
      </router-link>
    </mt-header> -->
   <tabBack :title="$route.query.name" mark="TofireUnits">
    
   </tabBack>
    <topHome></topHome>
    <div class="tabNavBox">
        <mt-navbar  v-model="selected">
          <mt-tab-item id="baseInfo">基本信息</mt-tab-item>
          <mt-tab-item id="frieData">消防数据</mt-tab-item>
          <mt-tab-item class="lawyLog" id="lawyLog">监管检查日志</mt-tab-item>
        </mt-navbar>
      <!-- tab-container -->

      <mt-tab-container v-model="selected">
        <!-- 基本信息 -->
        <mt-tab-container-item id="baseInfo">
          <div class="baseInfoBox">
            <table>
              <tr>
                <td class="leftTd">区域：</td>
                <td>{{fireUnitInfo.area}}</td>
              </tr>
              <tr>
                <td class="leftTd">类型：</td>
                <td>{{fireUnitInfo.type}}</td>
              </tr>
              <tr>
                <td class="leftTd">联系人：</td>
                <td>{{fireUnitInfo.contractName}}</td>
              </tr>
              <tr>
                <td class="leftTd">联系电话：</td>
                <td>
                  <div class="phoneNumBox">
                    <span>{{fireUnitInfo.contractPhone}}</span>
                    <a :href="'tel:'+fireUnitInfo.contractPhone ">
                      <img
                        class="callIcon"
                        src="../../../../assets/imgs/fireUnits/call_btn.png"
                        alt
                      >
                    </a>
                  </div>
                </td>
              </tr>
              <tr>
                <td class="leftTd">维保单位：</td>
                <td>{{fireUnitInfo.safeUnit}}</td>
              </tr>
              <tr>
                <td class="leftTd">地址：</td>
                <td>{{fireUnitInfo.address}}</td>
              </tr>
            </table>
            <div class="cnacelTop"  v-if="fireUnitInfo.isAttention" @click="cancelAttention">
                取消关注
            </div>
            <div class="placedTop" v-else @click="Attention">
                关注
            </div>
          </div>
        </mt-tab-container-item>
        <!-- 消防数据 -->
        <mt-tab-container-item id="frieData">
          <div class="frieDataBox">
            <!-- 安全用电 -->
            <ul class="safeElectricity ">
              <li class="fristLi">
                <img
                  class="ElectricityIcon"
                  src="../../../../assets/imgs/fireUnits/xfsj_img_01.png"
                  alt
                >
                <span class="rightText">安全用电</span>
              </li>
              <li>
                <span class="leftText">电缆温度探测器：</span>
                <span class="rightText">{{elecTCount}}个</span>
              </li>
              <li>
                <span class="leftText">剩余电流探测器：</span>
                <span class="rightText">{{elecECount}}个</span>
              </li>
              <li>
                <span class="leftText">最近30天报警:</span>
                <span class="rightText">{{elec30DayCount}}次</span>
                <img @click="goToRecordList('最近30天报警记录','安全用电')" class="viewIcon" src="../../../../assets/imgs/fireUnits/view_btn.png" alt>
              </li>
              <li>
                <span class="leftText">高频报警部件:</span>
                <span class="rightText">{{elecHighCount}}个</span>
                <img @click="goToRecordList('高频报警部件','安全用电')" class="viewIcon" src="../../../../assets/imgs/fireUnits/view_btn.png" alt>
              </li>
            </ul>
            <!-- 火警预警 -->
            <ul class="fireAlarm ">
              <li class="fristLi">
                <img class="fireIcon" src="../../../../assets/imgs/fireUnits/xfsj_img_02.png" alt>
                <span class="rightText">火警预警</span>
              </li>
              <li>
                <span class="leftText">物联网数据终端：</span>
                <span class="rightText">{{firePointsCount}}个</span>
              </li>
              <li>
                <span class="leftText">最近30天报警:</span>
                <span class="rightText">{{fire30DayCount}}次</span>
                <img class="viewIcon" @click="goToRecordList('最近30天报警记录','火警预警')" src="../../../../assets/imgs/fireUnits/view_btn.png" alt>
              </li>
              <li>
                <span class="leftText">高频报警部件:</span>
                <span class="rightText">{{fireHighCount}}个</span>
                <img class="viewIcon" @click="goToRecordList('高频报警部件','火警预警')" src="../../../../assets/imgs/fireUnits/view_btn.png" alt>
              </li>
            </ul>
            <!-- 设备设施故障 -->
            <ul class="equipmentFailure ">
              <li class="fristLi">
                <img class="FailureIcon" src="../../../../assets/imgs/fireUnits/xfsj_img_03.png" alt>
                <span class="rightText">设备设施故障</span>
              </li>
              <li>
                <span class="leftText">发生故障数量：</span>
                <span class="rightText">{{faultCount}}个</span>
              </li>
              <li>
                <span class="leftText">已处理故障数量：</span>
                <span class="rightText off_line">{{faultProcessedCount}} 个</span>
              </li>
              <li>
                <span class="leftText">待处理故障数量：</span>
                <span class="rightText">{{faultPendingCount}}个</span>
                <img class="viewIcon" @click="goToRecordList('待处理故障','设施故障')" src="../../../../assets/imgs/fireUnits/view_btn.png" alt>
              </li>
            </ul>
            <!-- 巡查记录 -->
            <ul class="patrolRecord ">
              <li class="fristLi">
                <img class="RecordIcon" src="../../../../assets/imgs/fireUnits/xfsj_img_04.png" alt>
                <span class="rightText">巡查记录</span>
              </li>
              <li>
                <span class="leftText">最近提交时间：</span>
                <span class="rightText">{{patrolLastTime}}</span>
              </li>
              <li>
                <span class="leftText">最近30天提交记录数量：</span>
                <span class="rightText off_line">{{patrol30DayCount}}次</span>
              </li>
            </ul>
            <!-- 值班记录 -->
            <ul class="onDutyRecord">
              <li class="fristLi">
                <img class="DutyIcon" src="../../../../assets/imgs/fireUnits/xfsj_img_05.png" alt>
                <span class="rightText">值班记录</span>
              </li>
              <li>
                <span class="leftText">最近提交时间：</span>
                <span class="rightText">{{dutyLastTime}}</span>
              </li>
              <li>
                <span class="leftText">最近30天提交记录数量：</span>
                <span class="rightText off_line">{{duty30DayCount}}次</span>
              </li>
            </ul>
          </div>
        </mt-tab-container-item>
        <!-- 执法日志 -->
        <mt-tab-container-item id="lawyLog">
         <ul class="lawyLogBox " v-if="Supervision.length>0">
          <li  @click="goToLawyLog(n.id)" v-for="(n) in Supervision" :key="n.time">
            <div class="leftBox">
              <span class="time">{{n.creationTime}}</span>
              <span v-if="n.checkResult == 1">合格</span>
              <span v-if="n.checkResult == -1">现场改正</span>
              <span class="yellow" v-if="n.checkResult == -2">限期整改</span>
              <span class="orange" v-if="n.checkResult == -3">停业整顿</span>
            </div>
            <img class="rightIcon" src="../../../../assets/imgs/fireUnits/next_btn.png" alt="">
          </li> 
         </ul>
         <div class="noDataBox" v-else>
            <img class="noDataIcon" src="../../../../assets/imgs/nodata.png" alt="">
         </div>
        </mt-tab-container-item>
      </mt-tab-container>
    </div>
  </div>
</template>
<script>
import { Toast,Indicator } from "mint-ui";
import tabBack from '../../../../components/topBack/index'
import topHome from '../../../../components/topHome/index'
export default {
  components: {
    tabBack,
    topHome
  },
  inject:['reload'],
  data() {
    return {
      selected: "baseInfo",
      fireUnitInfoId:'',
      FireUnitName:'',
      fireUnitInfo:'',
      yellow:'yellow',
      orange:'orange',
      elecECount:'',//	剩余电流探测器数量
      elecTCount:'',//	电缆温度探测器数量
      duty30DayCount :'', 
      elec30DayCount:'',
      elecHighCount:'',
      elecPointsCount:'',
      elecStateName:'',
      elecStateValue :'',
      faultCount:'',
      faultPendingCount:'',
      faultProcessedCount:'',
      fire30DayCount :'',
      fireHighCount:'',
      firePointsCount:'',
      fireStateName:'',
      fireStateValue :'',
      fireUnitName :'',
      patrolLastTime:'',
      dutyLastTime:'',
      patrol30DayCount:'',
      Supervision:[],//监管检查日志

    };
  },

  methods:{
    goBack(){
      console.log("fireInfo打印当前的路由位置",this.$route.path)
      this.$router.back(-1)
    },
    /* 关注 */
    Attention(){
      let that = this;
      this.$http({
         method: "post",
          url: "/api/services/app/FireUnit/AttentionFireUnit",
          data: {
           userId: localStorage.getItem('userId'),
           fireUnitId:that.fireUnitInfoId
          }
      }).then(res=>{
        console.log("关注成功",res)
        Toast('关注成功')
        that.reload();
      }).catch(res=>{
        console.log("关注失败")
      })
    },
    /* 取消关注 */
    cancelAttention(){
      let that = this;
      this.$http({
         method: "post",
          url: "/api/services/app/FireUnit/AttentionFireUnitCancel",
          data: {
           userId: localStorage.getItem('userId'),
           fireUnitId:that.fireUnitInfoId
          }
      }).then(res=>{
        console.log("取消关注成功",res)
        Toast('取消关注成功')
        that.reload();
      }).catch(res=>{
        console.log("关注失败")
         Toast('取消关注失败')
      })
    },
    goToRecordList(n,m,){
      console.log("n,m,address",n,m)
      let that = this;
      this.$router.push({
        path:'/dataMonitor/fireUnits/fireUnitinfos/recordList',
        query:{
          name:n,
          classify:m,
          address:that.$route.query.name,
          id:that.fireUnitInfoId
        }
      })
    },
    goToLawyLog(id){
      this.$router.push({
        path:"/dataMonitor/fireUnits/fireUnitinfos/lawLog",
        query:{
          id
        }
      })
    }
  },
  created(){
    this.fireUnitInfoId = this.$route.query.id;
    this.FireUnitName = this.$route.query.name
    if(this.$route.query.symbol){
      this.selected = this.$route.query.symbol
    }
  },
  mounted(){
    let that = this;
    Indicator.open({
        text: "加载中...",
        spinnerType: "fading-circle"
    });
    /* 基本信息 */
    this.$http({
      method: "get",
      url: "/api/services/app/FireUnit/GetFireUnitInfo",
      params: {
        Id:that.fireUnitInfoId,
        UserId:parseInt(localStorage.getItem('userId'))      
      }
    }).then(function(res) {
        console.log("防火单位信息成功", res);
        if(res.status == 200){
          that.fireUnitInfo = res.data.result;
          Indicator.close();
        }
    }).catch(function(res) {
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

    /*消防数据初始化数据 */
    this.$http({
        method: "get",
        url: "/api/services/app/FireUnit/GetFireUnitAlarm",
        params: {
         Id:that.fireUnitInfoId
       /*  Id:67, */

        }
    }).then(function(res) {
            console.log("防火单位消防数据成功", res);
            if(res.status == 200){


              that.duty30DayCount = res.data.result.duty30DayCount // 
              that.elecECount = res.data.result.elecECount // 
              that.elecTCount = res.data.result.elecTCount // 
              that.elec30DayCount = res.data.result.elec30DayCount
              that.elecHighCount = res.data.result.elecHighCount
              that.elecPointsCount = res.data.result.elecPointsCount
              that.elecStateName = res.data.result.elecStateName
              that.elecStateValue = res.data.result.elecStateValue
              that.faultCount = res.data.result.faultCount
              that.faultPendingCount = res.data.result.faultPendingCount
              that.faultProcessedCount = res.data.result.faultProcessedCount
              that.fire30DayCount = res.data.result.fire30DayCount
              that.fireHighCount = res.data.result.fireHighCount
              that.firePointsCount = res.data.result.firePointsCount
              that.fireStateName = res.data.result.fireStateName
              that.fireStateValue = res.data.result.fireStateValue
              that.fireUnitName = res.data.result.fireUnitName
              that.patrol30DayCount = res.data.result.patrol30DayCount
              that.patrolLastTime = res.data.result.patrolLastTime
              that.dutyLastTime = res.data.result.dutyLastTime
            }
    }).catch(function(res) {
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

    /* 监管检查日志 */
      this.$http({
         method: "get",
          url: "/api/services/app/Supervision/GetList",
          params: {
              FireUnitId:that.fireUnitInfoId,
              FireUnitName:that.FireUnitName
          }
      }).then(res=>{
        console.log("请求防火单位的监管检查日志成功",res)
        if(res.status == 200){
          that.Supervision = res.data.result.items
        }
      }).catch(res=>{
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
      })

      
  }
};
</script>