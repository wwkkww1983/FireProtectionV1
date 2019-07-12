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
  .fireUnit {
    #myChartFireUnit {
      width: 100%;
      height: 690px;
    }
  }
  .monitoringPoint{
      #myChartMonitoringPoint{
          width: 100%;
          height: 500px;

      }
  }
  /* 网关离线单位 */
  .offlineUnit{
    ul{
      list-style: none;
      li{
        padding: 20px 28px;
        /*border-bottom: 1px solid #dcdcdc; no */
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
  .safeElectricity{
      #myChartSafeElectricity{
          width: 100%;
          height: 500px;
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
        border-bottom: 1px solid #dcdcdc; /*no */
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
            width: 200px;
            display: inline-block;
            margin-right: 110px;
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
    <!-- 联网防火单位 -->
    <div class="fireUnit">
      <div class="topTitle">
        <div class="left">
          <img src="../../../assets/imgs/fireDataReport/zhsj_img_01.png" alt>
          <span>联网防火单位</span>
        </div>
        <span class="totalNumber">{{joinFireUnitCount}}家</span>
      </div>
      <!-- 图形 -->
      <div id="myChartFireUnit"></div>
    </div>
    <!-- 联网监控点位 -->
    <div class="monitoringPoint">
      <div class="topTitle">
        <div class="left">
          <img src="../../../assets/imgs/fireDataReport/zhsj_img_01.png" alt>
          <span>联网监控终端</span>
        </div>
        <span class="totalNumber">{{joinPointCount}}个</span>
      </div>
      <!-- 图形 -->
      <div id="myChartMonitoringPoint"></div>
    </div>
    <!-- 安全用电累计预警 -->
    <div class="safeElectricity ">
        <div class="topTitle">
            <div class="left">
            <img src="../../../assets/imgs/fireDataReport/zhsj_img_01.png" alt>
            <span>安全用电累计预警</span>
            </div>
            <span class="totalNumber">{{AlarmCounts}}次</span>
        </div>
      <!-- 图形 -->
       <div id="myChartSafeElectricity"></div>
    </div>
    <!--最近30天预警次数  -->
    <div class="top10FireUnits">
          <div class="topTitle">
            <div class="left">
              <img src="../../../assets/imgs/fireDataReport/zhsj_img_01.png" alt>
              <span>最近30天安全用电预警</span>
              <img class="top10Icon" src="../../../assets/imgs/fireDataReport/zhsj_img_02.png" alt="">
            </div>
          </div>
          <ul class="top10FireUnitsList">
              <li  v-for="(arr,index) in top10FireUnits" :key="index">
                 <span class="title">{{arr.name}}</span>
                 <div class="spanBox">
                   <span class="left">联网终端：{{arr.pointCount}}</span>
                   <span class="right">最近30天预警：{{arr.alarmCount}}次</span>
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
      joinFireUnitCount:'',//联网防火单位的数量
      joinPointCount:'',//联网监控点位数量
      offlineFireUnitsCount:'',//网管离线个数
      offlineUnit:'',//网关离线单位
      AlarmCounts:'',//安全用电累计预警次数
      top10FireUnits:''//最近30天预警次数top10

    }
  },
  methods: {
    /* 安全用电累计预警 */
    drawLine03() {

    }

    /*  */
  },
  mounted() {

    let that =this;
    this.$http({
      method: "get",
      url: "/api/services/app/DataReport/GetAreasAlarmElectric",
    }).then(res=>{
      console.log("安全用电数据分析成功",res)
      if(res.status ==200){
        /* 联网防火单位 */
        let myChartFireUnit = this.$echarts.init(document.getElementById("myChartFireUnit"));
        let joinTypeCountsName= [];//饼状图的标识图
        let joinTypeCounts = [];//饼状图的数据
        that.joinFireUnitCount = res.data.result.joinFireUnitCount//联网防火单位的数量
        //处理数据
        for(var i = 0; i < res.data.result.joinTypeCounts.length; i++){
          joinTypeCountsName.push(res.data.result.joinTypeCounts[i].type)
          let joinTypeCountsObj = {
              name: res.data.result.joinTypeCounts[i].type,
              value: res.data.result.joinTypeCounts[i].count,
          };
          joinTypeCounts.push(joinTypeCountsObj);
        }
        // 绘制图表
        myChartFireUnit.setOption({
          tooltip: {
            trigger: "item",
            formatter: "{a} <br/>{b} : {c} ({d}%)"
          },
          legend: {
            orient: "horizontal",
            padding: [
              0, // 上
              30, // 右
              10, // 下
              0 // 左
            ],
            bottom: 0,
            textStyle: {
              color: "#262626"
            },
            data: joinTypeCountsName
          },
          label: {
            normal: {
              show: false
            },
            emphasis: {
              show: true
            }
          },
          lableLine: {
            normal: {
              show: false
            },
            emphasis: {
              show: true
            }
          },
          series: [
            {
              name: "访问来源",
              type: "pie",
              radius: "70%",
              center: ["50%", "40%"],
              label: {
                normal: {
                  show: false
                },
                emphasis: {
                  show: false
                }
              },
              lableLine: {
                normal: {
                  show: false
                },
                emphasis: {
                  show: false
                }
              },
              data:joinTypeCounts,
              itemStyle: {
                emphasis: {
                  shadowBlur: 10,
                  shadowOffsetX: 0,
                  shadowColor: "rgba(0, 0, 0, 0.5)"
                }
              }
            }
          ],
          color: ["#ffde00","#2f82ff","#0dc9ff","#ff790d","#08da03","#749f83","#ca8622","#bda29a",]
        });

        /* 联网监控点位*/
        let myChartMonitoringPoint = this.$echarts.init(document.getElementById("myChartMonitoringPoint"));
        let joinTypePointCountsName =[];//联网监控点位的柱状图数据y轴
        let joinTypePointCounts =[];//联网监控点位的柱状图数据
        that.joinPointCount= res.data.result.joinPointCount//联网监控点位数量
        for(var i = 0; i < res.data.result.joinTypePointCounts.length; i++){
          joinTypePointCountsName.push(res.data.result.joinTypePointCounts[i].type)
          joinTypePointCounts.push(res.data.result.joinTypePointCounts[i].count);
        }

        /* 处理数据从大到小排序 */
          let max;
　　　　　　　//遍历数组，默认arr中的某一个元素为最大值，进行逐一比较
            for(var i=0; i<joinTypePointCounts.length; i++){
　　　　　　　　　　//外层循环一次，就拿arr[i] 和 内层循环arr.legend次的 arr[j] 做对比
                for(var j=i; j<joinTypePointCounts.length; j++){
                    if(joinTypePointCounts[i]>joinTypePointCounts[j]){
                        //如果arr[j]大就把此时的值赋值给最大值变量max
　　　　　　　　　　　　　max=joinTypePointCounts[j];
                        joinTypePointCounts[j]=joinTypePointCounts[i];
                        joinTypePointCounts[i]=max;
                      }
                }
            }

          console.log("joinTypePointCounts",joinTypePointCounts)
        // 绘制图表
        myChartMonitoringPoint.setOption({
          color: ["#0db2ff"],
          tooltip: {
            trigger: "axis",
            axisPointer: {
              // 坐标轴指示器，坐标轴触发有效
              type: "shadow" // 默认为直线，可选为：'line' | 'shadow'
            }
          },
          grid: {
            left: "3%",
            right: "4%",
            bottom: "10%",
            height:200,
            containLabel: true
          },
          xAxis: [
            {
              type: "value",
            }
          ],
          yAxis: [
            {
              type: "category",
              data:joinTypePointCountsName,
              axisTick: {
                alignWithLabel: true
              }
            }
          ],
          series: [
            {
              name: "直接访问",
              type: "bar",
              barWidth: "50%",
              data: joinTypePointCounts
            }
          ]
        });
        /* 网关离线单位 */
        that.offlineUnit = res.data.result.offlineFireUnits //网关离线单位
        that.offlineFireUnitsCount = res.data.result.offlineFireUnitsCount//网关离线单位的数量
        /* 安全用电累计预警 */
        let myChartSafeElectricity = this.$echarts.init(document.getElementById("myChartSafeElectricity"));
        let monthAlarmCounts = [];//安全用电累计预警柱状图数据
        let monthAlarm = [];//安全用电累计预警柱状图X轴数据
        let AlarmCounts = 0;
        for(var i = 0; i < res.data.result.monthAlarmCounts.length; i++){
          monthAlarm.push(res.data.result.monthAlarmCounts[i].month)
          monthAlarmCounts.push(res.data.result.monthAlarmCounts[i].count);
          AlarmCounts += res.data.result.monthAlarmCounts[i].count
        }
        that.AlarmCounts = AlarmCounts
        
        // 绘制图表
        myChartSafeElectricity.setOption({
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
                data:monthAlarm
            },
          yAxis: {
              type: 'value'
          },
          series: [{
              data: monthAlarmCounts,
              type: 'line',
              areaStyle: {}
          }]
      });

       /* 最近30天预警次数top10 */
       that.top10FireUnits =  res.data.result.top10FireUnits

       
      }
    }).catch(res=>{
      console.log("安全用电数据分析失败",res)
    })
    this.drawLine03();
  }
};
</script>