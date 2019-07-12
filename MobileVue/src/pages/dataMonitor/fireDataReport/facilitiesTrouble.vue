<style lang="less">
.border-bottom {
  border: 1px solid #dcdcdc; /* no */
  border-left: none;
  border-right: none;
}
.facilitiesTroubleBox {
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
  .fireUnit {

    .numberBox{
       font-size: 30px;
       color: #262626;
       padding: 30px 26px;
       p{
           display: flex;
           justify-content: space-between;
           margin-bottom: 28px;
           .number{
            color: #989898;
            font-size: 26px;
           }
       }
    }
    #myChart{
      width: 100%;
      height: 690px;
    }
  }
  /* 最近30天预警次数 */
  .top10FireUnits{
    .top10Icon{
      width: 88px !important;
      height: 24px !important;
      margin-left: 10px;
    }
    .top10FireUnitsList{
      list-style: none;
      li{
        padding: 20px 26px;
        border-bottom: 1px solid #dcdcdc; /* no */
        .title{
          font-size: 26px;
          color: #262626;
          margin-bottom: 20px;
          display: inline-block;
        }
        .spanBox{
          font-size: 24px;
          margin-bottom: 20px;
          color: #afafaf;
          .left{
            display: inline-block;
          }
        }
         .spanBox:last-of-type{
         
          margin-bottom: 0px;
        
        }
      }
    }

  }
}
</style>
<template>
  <div class="facilitiesTroubleBox">
    <topBack :title="$route.name"></topBack>
    <topHome></topHome>
    <!-- 联网防火单位 -->
    <div class="fireUnit">
      <div class="numberBox">
            <p class="one">
              <span>消防设施故障累计发现数量</span>
              <span class="number">{{faultCount}}条</span>
            </p>
            <p class="two">
              <span>消防设施故障待处理数量</span>
              <span class="number">{{faultPendingCount}}条</span>
            </p>
      </div>
      <!-- 图形 -->
      <div id="myChart"></div>
    </div>
 
    <!--最近30天预警次数  -->
    <div class="top10FireUnits">
          <div class="topTitle">
            <div class="left">
              <img src="../../../assets/imgs/fireDataReport/zhsj_img_01.png" alt>
              <span>消防设施故障待处理数量</span>
              <img class="top10Icon" src="../../../assets/imgs/fireDataReport/zhsj_img_02.png" alt="">
            </div>
          </div>
          <ul class="top10FireUnitsList">
              <li  v-for="(arr,index) in pendingFaultFireUnits" :key="index">
                 <span class="title">{{arr.fireUnitName}}</span>
                 <div class="spanBox">
                   <span class="left">设施故障发现数量：{{arr.count}}</span>
                 </div>
                 <div class="spanBox">
                   <span class="left">设施故障待处理数量：{{arr.pendingCount}}</span>
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
      faultCount:0,
      faultPendingCount:0,
      pendingFaultFireUnits:[]


    }
  },
  methods: {

    /*  */
  },
  mounted() {
    let that =this;
    this.$http({
      methods:'get',
      url: "/api/services/app/DataReport/GetAreasFault",
      params:{
          UserId:localStorage.getItem('userId')
       }
    }).then(res=>{
      console.log("数据初始化陈工",res)
      let myChart = this.$echarts.init(document.getElementById("myChart"));
      that.faultCount = res.data.result.faultCount;//累计发现数量
      that.faultPendingCount = res.data.result.faultPendingCount//设施故障待处理累计数量

      let faultCount= [];//累计发现数量柱状图
      let faultPendingCount = [];//待处理累计数量柱状图
      let xtips = [];
        //处理数据
        for(var i = 0; i < res.data.result.monthFaultCounts.length; i++){
          faultCount.push(res.data.result.monthFaultCounts[i].count)
          faultPendingCount.push(res.data.result.monthFaultCounts[i].pendingCount)
          xtips.push(res.data.result.monthFaultCounts[i].month)
        }

      myChart.setOption({
          color:['orange','#37da04'],
          tooltip: {
              trigger: 'axis',
              axisPointer: {
                  type: 'shadow'
              }
          },
          legend: {
              orient: "horizontal",
              bottom: 10,
              padding: [5, 50],
              textStyle: {
                color: "#262626"
              },
              itemWidth :30,
              data: ['发现数量', '已处理数量']
          },
          grid: {
              left: '3%',
              right: '4%',
              bottom: '18%',
              top:'2%',
              containLabel: true
          },
          xAxis: {
              boundaryGap: [0, 0.01],
              data: xtips
          },
          yAxis: {
              type: 'value',
          
          },
          series: [
              {
                  name: '发现数量',
                  type: 'bar',
                  data: faultCount
              },
              {
                  name: '已处理数量',
                  type: 'bar',
                  data: faultPendingCount
              }
          ]
      })
      that.pendingFaultFireUnits = res.data.result.pendingFaultFireUnits

    }).catch(res=>{
      console.log("数据初始化失败",res)
    })

   
    

   
  }
};
</script>