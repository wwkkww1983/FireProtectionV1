<style lang="less">
.border-bottom {
  border: 1px solid #dcdcdc; /* no */
  border-left: none;
  border-right: none;
}
.safetyElectricityBox {
  padding-top: 80px;
  width: 100%;
  box-sizing: border-box;
  padding-bottom: 20px;
  .topTitle {
    height: 70px;
    width: 100%;
    box-sizing: border-box;
    display: flex;
    justify-content: space-between;
    padding: 0 26px;
    align-items: center;
    .border-bottom();
    .left {
      display: flex;
      color: #262626;
      font-size: 30px;
      align-items: center;
      img {
        width: 10px;
        height: 10px;
        margin-right: 20px;
      }
    }
    .totalNumber {
      color: #989898;
      font-size: 26px;
    }
  }
  /* 暂无数据 */
  .noData{
    height: 108px;
    width: 100%;
    line-height: 108px;
    text-align: center;
    font-size: 30px;
    color: #262626;
  }
  /* 消防巡查累计记录 */
  .firePatrol {
    #myChartfirePatrol {
      width: 100%;
      height: 500px;
    }
  }
  /* 巡查记录缺失7天以上单位 */
  .lackOfRecord{
    ul{
      list-style: none;
      li:last-of-type{
         border:none;
      }
      li{
        padding: 20px 28px;
      border-bottom: 1px solid #dcdcdc;   /* no */
        span{
          display: inline-block;
          margin-bottom: 20px;
            font-size: 26px;
            color: #262626;
        }
        .offlineTime{
          font-size: 24px;
          color: #afafaf; 
        }
      }
    }
  }
  /* 消防值班累计记录 */
  .fireWatch{
      #myChartFireWatch{
          width: 100%;
          height: 500px;
      }
  }
  /* 值班记录缺失1天以上单位 */
  .absenceOfDuty{
    .absenceOfDutyList{
      list-style: none;
      li{
        padding: 20px 26px;
         border-bottom: 1px solid #dcdcdc;/* 1px */
        .title{
          font-size: 26px;
          color: #262626;
          margin-bottom: 20px;
          display: inline-block;
        }
        .spanBox{
          font-size: 24px;
          color: #afafaf;
          .left{
            display: inline-block;
          }
        }
      }
    }

  }
}
</style>
<template>
  <div class="safetyElectricityBox">
    <topBack :title="$route.name"></topBack>
    <topHome></topHome>
    <!-- 消防巡查累计记录 -->
    <div class="firePatrol">
      <div class="topTitle">
        <div class="left">
          <img src="../../../assets/imgs/fireDataReport/zhsj_img_01.png" alt>
          <span>消防巡查累计记录</span>
        </div>
        <span class="totalNumber">{{patrolCount}}条</span>
      </div>
      <!-- 图形 -->
      <div id="myChartfirePatrol"></div>
    </div>
    <!-- 巡查记录缺失7天以上单位 -->
    <div class="lackOfRecord">
        <div class="topTitle">
          <div class="left">
            <img src="../../../assets/imgs/fireDataReport/zhsj_img_01.png" alt>
            <span>巡查记录缺失7天以上单位</span>
          </div>
          <span class="totalNumber">{{noWork7DayCount}}个</span>
        </div>
        <div class="noData" v-if="noWork7DayCount == 0 ">
          暂无数据
        </div>
        <ul v-else class="offlineUnitList">
          <li  v-for="(arr,index) in patrolFireUnitManualOuputs" :key="index">
            <span>{{arr.fireUnitName}}</span>
            <div class="offlineTime">巡查记录最后提交日期：{{arr.lastTime}}</div>
          </li>
        </ul> 
    </div>
    <!-- 消防值班累计记录 -->
    <div class="fireWatch">
        <div class="topTitle">
            <div class="left">
            <img src="../../../assets/imgs/fireDataReport/zhsj_img_01.png" alt>
            <span>消防值班累计记录</span>
            </div>
            <span class="totalNumber">{{dutyCount}}次</span>
        </div>
      <!-- 图形 -->
       <div id="myChartFireWatch"></div>
    </div>
    <!--值班记录缺失1天以上单位  -->
    <div class="absenceOfDuty">
          <div class="topTitle">
            <div class="left">
              <img src="../../../assets/imgs/fireDataReport/zhsj_img_01.png" alt>
              <span>值班记录缺失1天以上单位</span>
            </div>
            <span class="totalNumber">{{noWork1DayCount}}家</span>
          </div>
          <ul class="absenceOfDutyList">
              <li  v-for="(arr,index) in dutyFireUnitManualOuputs" :key="index">
                 <span class="title">{{arr.fireUnitName}}</span>
                 <div class="spanBox">
                   <span class="left">值班记录最后提交日期：{{arr.lastTime}}</span>
                 </div>
              </li>
          </ul>
    </div>
  </div>
</template>
<script>
import topBack from "../../../components/topBack/index";
import topHome from "../../../components/topHome/index";
export default {
  components: {
    topBack,
    topHome
  },
  data(){
    return{

      patrolCount:0,//消防巡查累计记录
      noWork7DayCount:0,//超过7天没有巡查记录的单位数量
      patrolFireUnitManualOuputs:'',//超过7天没有巡查记录的单位前10位
      dutyCount:0,//消防值班累计记录
      noWork1DayCount:0,//超过1天没有值班记录的单位数量
      dutyFireUnitManualOuputs:''


    }
  },
  methods: {

  },
  mounted() {

    let that =this;
    this.$http({
      method: "get",
      url: "/api/services/app/DataReport/GetAreasPatrolDuty",
      params:{
        UserId:localStorage.getItem('userId')
      }
    }).then(res=>{
      console.log("安全用电数据分析成功",res)
      if(res.status ==200){
        /* 消防巡查累计记录 */
        let myChartfirePatrol = this.$echarts.init(document.getElementById("myChartfirePatrol"));
        let patrolMonthCountsName= [];//柱状图的X轴标识
        let patrolMonthCounts = [];//柱状图的数据
        that.patrolCount = res.data.result.patrolCount//联网防火单位的数量
        //处理数据
        for(var i = 0; i < res.data.result.patrolMonthCounts.length; i++){
          patrolMonthCountsName.push(res.data.result.patrolMonthCounts[i].month)
          patrolMonthCounts.push(res.data.result.patrolMonthCounts[i].count);
        }
        // 绘制图表
       myChartfirePatrol.setOption({
          color:["#0db2ff"],
          grid: {
            top:'10%',
            height:180,
            left:'3%',
            bottom:'0%',
            containLabel: true
          },
          xAxis: {
                type: 'category',
                boundaryGap: false,
                data: patrolMonthCountsName
            },
          yAxis: {
              type: 'value'
          },
          series: [{
              data: patrolMonthCounts,
              type: 'line',
              areaStyle: {}
          }]
      });


       
        /* 巡查记录缺失7天以上单位 */
       that.noWork7DayCount = res.data.result.noWork7DayCount//超过7天没有巡查记录的单位数量
       that.patrolFireUnitManualOuputs = res.data.result.patrolFireUnitManualOuputs;//



        /* 消防值班累计记录 */
        let myChartFireWatch = this.$echarts.init(document.getElementById("myChartFireWatch"));
        that.dutyCount = res.data.result.dutyCount;//消防值班累计记录
        let dutyMonthCountsName = [];//柱状图数据x轴的标识
        let dutyMonthCounts = [];//柱状图数据
       
        for(var i = 0; i < res.data.result.dutyMonthCounts.length; i++){
          dutyMonthCountsName.push(res.data.result.dutyMonthCounts[i].month)
          dutyMonthCounts.push(res.data.result.dutyMonthCounts[i].count);
        }
        
        // 绘制图表
        myChartFireWatch.setOption({
          color:["#0db2ff"],
          grid: {
            top:'10%',
            height:180,
            left:'3%',
            bottom:'0%',
            containLabel: true
          },
          xAxis: {
                type: 'category',
                boundaryGap: false,
                data: dutyMonthCountsName
            },
          yAxis: {
              type: 'value'
          },
          series: [{
               data: dutyMonthCounts,
              type: 'line',
              areaStyle: {}
          }]
      });

       /* 值班记录缺失1天以上单位 */
       that.noWork1DayCount =  res.data.result.noWork1DayCount
       that.dutyFireUnitManualOuputs =  res.data.result.dutyFireUnitManualOuputs

       
      }
    }).catch(res=>{
      console.log("安全用电数据分析失败",res)
    })
  }
};
</script>