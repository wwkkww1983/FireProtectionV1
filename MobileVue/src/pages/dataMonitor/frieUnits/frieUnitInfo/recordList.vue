<style lang="less">
.recordLsitBox {
  width: 100%;
  padding-top: 80px;
  box-sizing: border-box;
  position: relative;
  .border {
   border-bottom: 1px solid #dcdcdc;  /* no */
  }

  .topNav {
      color: #989898;
      display: flex;
      justify-content: space-between;
      align-items: center;
      position: fixed;
      top: 80px;
      .border();
      padding: 20px 20px;
      font-size: 24px;
      width: 100%;
      box-sizing: border-box;
      background: white;
  }
    .page-loadmore {
      margin-top: 80px;
      box-sizing: border-box;
      .page-loadmore-wrapper{
              overflow: scroll;
      }
      ul {
        list-style: none;
        padding: 0 20px;
        li {
          padding: 20px 0;
          font-size: 24px;
          color: #262626;
          line-height: 42px;
          .border();
          .theFaultTime,
          .theFault {
            display: flex;
            justify-content: flex-start;
            .right {
              width: 150px;
            }
          }
          .theFault {
            .theFaultDescribe {
              width: 600px;
            }
          }
        }
      }
      /* 没有数据容错界面 */
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

      .mint-loadmore-bottom {
          span {
              display: inline-block;
              transition: .2s linear;
              vertical-align: middle;

          }
          .mint-spinner {
              display: inline-block;
              vertical-align: middle;
          }
      }
    }
  
}
</style>
<template>
  <div class="recordLsitBox">
    <topBack :title="name" symbol="frieData" :from="fromPath"></topBack>
    <topHome></topHome>
    <!--  -->
    <div class="topNav">
      <span>[{{address}}] {{classify}}</span>
      <span>总数量：{{totalCount}}</span>
    </div>

    <div class="page-loadmore" ref="pages">
      <div class="page-loadmore-wrapper" ref="wrapper" :style="{ height: wrapperHeight + 'px' }">
        <mt-loadmore :bottom-method="loadBottom"  @bottom-status-change="handleBottomChange" :bottom-all-loaded="allLoaded" :autoFill="autoFill" ref="loadmore">
          <ul class="recordLsitContainer">
            <!-- 安全用电的高频报警部件-->
            <div v-if="name == '高频报警部件' && classify == '安全用电' && GetFireUnitHighFreqAlarmEle.length>0 ">
                <li  v-for="(item,index) in GetFireUnitHighFreqAlarmEle" :key="index">
                  <div>
                    <span>报警部件：{{item.name}}</span>
                  </div>
                  <div>
                    <span>报警时间：{{item.time}}</span>
                  </div>
                  <div>
                    <span>最近30天报警次数：{{item.count}}次</span>
                  </div>
                </li>
            </div>

            <!-- 安全用电的最近30天报警记录 -->
            <div v-else-if="name == '最近30天报警记录' && classify == '安全用电' && GetFireUnit30DayAlarmEle.length>0 ">
              <li   v-for="(item ,index) in GetFireUnit30DayAlarmEle" :key="index">
                <div>
                  <span>报警时间：{{item.time}}</span>
                </div>
                <div>
                  <span>报警事件：{{item.content}}</span>
                </div>
                <div>
                  <span>报警地点：{{item.location}}</span>
                </div>
              </li>
            </div>
            <!--火警预警的高频报警部件  -->
            <div v-else-if="name == '高频报警部件' && classify == '火警预警' && GetFireUnitHighFreqAlarmFire.length>0">
                <li   v-for="(item,index) in GetFireUnitHighFreqAlarmFire" :key="index">
                  <div>
                    <span>报警部件：{{item.name}}</span>
                  </div>
                  <div>
                    <span>最近报警时间：{{item.time}}</span>
                  </div>
                  <div>
                    <span>最近30天总计报警次数：{{item.count}}次</span>
                  </div>
                </li>
            </div>
            <!--火警预警的最近30天报警记录  -->
            <div v-else-if="name == '最近30天报警记录' && classify == '火警预警' && GetFireUnit30DayAlarmFire.length>0">
              <li   v-for="(item ,index) in GetFireUnit30DayAlarmFire" :key="index">
                <div>
                  <span>报警时间：{{item.time}}</span>
                </div>
                <div>
                  <span>报警事件：{{item.content}}</span>
                </div>
                <div>
                  <span>报警地点：{{item.location}}</span>
                </div>
              </li>
            </div>
            <div v-else-if="name == '待处理故障' && GetFireUnitFaultList.length>0">
                <li   v-for="(item,index) in GetFireUnitFaultList" :key="index">
                  <div class="theFaultTime">
                    <span class="right">故障时间：</span>
                    <span>{{item.time}}</span>
                  </div>
                  <div class="theFault">
                    <span class="right">故障事件：</span>
                    <p class="theFaultDescribe">{{item.content}}</p>
                  </div>
                </li>
            </div>
            <div class="noDataBox" v-else>
                <img class="noDataIcon" src="../../../../assets/imgs/nodata.png" alt="">
            </div>
            
          </ul>
          <div slot="bottom" class="mint-loadmore-bottom">
            <span v-show="bottomStatus !== 'loading'"  :class="{ 'is-rotate': bottomStatus === 'drop' }">↑</span>
            <span v-show="bottomStatus === 'loading'">
              <mt-spinner type="snake" color="#0390fe"></mt-spinner>
            </span>
          </div>
        </mt-loadmore>
      
      </div>
    </div>
  </div>
</template>
<script>
import { Indicator } from 'mint-ui'
import topBack from '../../../../components/topBack/index'
import topHome from '../../../../components/topHome/index'
export default {
  components:{
    topBack,
    topHome
  },
  data() {
    return {
      fireUnitId:'',
      name: "",
      address: "",
      classify: "",
      allLoaded: false,
      autoFill:false,
      bottomStatus: '',
      wrapperHeight: 0,
      GetFireUnit30DayAlarmEle:[],//安全用电最近30天报警记录查询
      GetFireUnitHighFreqAlarmEle:[],//安全用电得高频报警部件
      GetFireUnit30DayAlarmFire:[],//火警预警最近30天报警记录查询
      GetFireUnitHighFreqAlarmFire:[],//火警预警高频报警部件
      GetFireUnitFaultList:[],//设备设施故障
      totalCount:0,
      MaxResultCount:8,
      count:0,
      list:[],
      fromPath:''
    };
  },
  methods:{
    handleBottomChange(status) {
        this.bottomStatus = status;
    },
    loadBottom() {
      let that = this;
      that.count++;
      if(this.classify == '安全用电'){
          if(this.name == '最近30天报警记录'){
            this.$http({
              method: "get",
              url: "/api/services/app/FireUnit/GetFireUnit30DayAlarmEle",
              params: {
                  Id:67,
                  SkipCount:that.count*that.MaxResultCount,
                  MaxResultCount:that.MaxResultCount
                }
            }).then(res=>{
              console.log("安全用电最近30天报警记录数据成功",that.count,res)
              if(res.status == 200){
                if(res.data.result.items.length >0){
                  for(let index in res.data.result.items){
                    that.GetFireUnit30DayAlarmEle.push(res.data.result.items[index])
                  }
                }else if(res.data.result.items.length<that.MaxResultCount){
                  that.allLoaded = true;
                }
                that.$refs.loadmore.onBottomLoaded();
              }

            }) .catch(res=>{
              console.log("安全用电最近30天报警记录数据失败",res)
            });
          }else if(this.name == '高频报警部件'){
            this.$http({
              method: "get",
              url: "/api/services/app/FireUnit/GetFireUnitHighFreqAlarmEle",
              params: {
                  Id:67,
                  SkipCount:that.count*that.MaxResultCount,
                  MaxResultCount:that.MaxResultCount
                }
            }).then(res=>{
              console.log("安全用电高频报警数据成功",that.count,res)
              if(res.status == 200){
                if(res.data.result.items.length >0){
                  for(let index in res.data.result.items){
                    that.GetFireUnitHighFreqAlarmEle.push(res.data.result.items[index])
                  }
                }else if(res.data.result.items.length == 0){
                  that.allLoaded = true;
                }
                that.$refs.loadmore.onBottomLoaded();
              }

            }) .catch(res=>{
              console.log("安全用电最近30天报警记录数据失败",res)
            });
          }
      }else if(this.classify == '火警预警'){
        if(this.name == '最近30天报警记录'){
            this.$http({
              method: "get",
              url: "/api/services/app/FireUnit/GetFireUnit30DayAlarmFire",
              params: {
                  Id:67,
                  SkipCount:that.count*that.MaxResultCount,
                  MaxResultCount:that.MaxResultCount
                }
            }).then(res=>{
              console.log("火警预警最近30天报警记录数据成功",that.count,res)
              if(res.status == 200){
                if(res.data.result.items.length >0){
                  for(let index in res.data.result.items){
                    that.GetFireUnit30DayAlarmFire.push(res.data.result.items[index])
                  }
                }else if(res.data.result.items.length == 0){
                  that.allLoaded = true;
                }
                that.$refs.loadmore.onBottomLoaded();
              }

            }) .catch(res=>{
              console.log("火警预警最近30天报警记录数据失败",res)
            });
        }else if(this.name == '高频报警部件'){
          this.$http({
              method: "get",
              url: "/api/services/app/FireUnit/GetFireUnitHighFreqAlarmFire",
              params: {
                  Id:67,
                  SkipCount:that.count*that.MaxResultCount,
                  MaxResultCount:that.MaxResultCount
                }
            }).then(res=>{
              console.log("火警预警高频报警数据成功",that.count,res)
              if(res.status == 200){
                if(res.data.result.items.length >0){
                  for(let index in res.data.result.items){
                    that.GetFireUnitHighFreqAlarmEle.push(res.data.result.items[index])
                  }
                }else if(res.data.result.items.length == 0){
                  that.allLoaded = true;
                }
                that.$refs.loadmore.onBottomLoaded();
              }

            }).catch(res=>{
              console.log("火警预警高频报警数据失败",res)
            });
        }
      }

    }
  },
  created() {
    this.name = this.$route.query.name;
    this.classify = this.$route.query.classify;
    this.address = this.$route.query.address;
    this.fireUnitId = this.$route.query.id
  },
  mounted(){
  
    Indicator.open({
      text: "加载中...",
      spinnerType: "fading-circle"
    });

     this.wrapperHeight = document.documentElement.clientHeight - this.$refs.wrapper.getBoundingClientRect().top-20;
  
    let that = this;
    if(this.classify == '安全用电'){
      if(this.name == '最近30天报警记录'){
        this.$http({
           method: "get",
           url: "/api/services/app/FireUnit/GetFireUnit30DayAlarmEle",
           params: {
              Id:that.fireUnitId,
              SkipCount:0,
              MaxResultCount:that.MaxResultCount
            }
        }).then(res=>{
          console.log("安全用电最近30天报警记录数据成功",res)
          if(res.status == 200){
            that.GetFireUnit30DayAlarmEle = res.data.result.items
            that.totalCount = res.data.result.totalCount
             Indicator.close();
          }
        }) .catch(res=>{
          console.log("安全用电最近30天报警记录数据失败",res)
        });
      }else if(this.name == '高频报警部件'){
         this.$http({
           method: "get",
           url: "/api/services/app/FireUnit/GetFireUnitHighFreqAlarmEle",
           params: {
              Id:that.fireUnitId,
              SkipCount:0,
              MaxResultCount:that.MaxResultCount
            }
        }).then(res=>{
          console.log("安全用电高频报警部件查询",res)
          if(res.status == 200){
            that.GetFireUnitHighFreqAlarmEle = res.data.result.items
            that.totalCount = res.data.result.totalCount

            console.log("安全用电高频报警部件查询00000",  that.GetFireUnitHighFreqAlarmEle,that.totalCount)
             Indicator.close();
          }
        }) .catch(res=>{
          console.log("安全用电最近30天报警记录数据失败",res)
        });
      }
    }else if(this.classify == '火警预警'){
      if(this.name == '最近30天报警记录'){
        this.$http({
           method: "get",
           url: "/api/services/app/FireUnit/GetFireUnit30DayAlarmFire",
           params: {
              Id:that.fireUnitId,
              SkipCount:0,
              MaxResultCount:that.MaxResultCount
            }
        }).then(res=>{
          console.log("火警预警最近30天报警记录数据成功",res)
          if(res.status == 200){
            that.GetFireUnit30DayAlarmFire = res.data.result.items
            that.totalCount = res.data.result.totalCount
             Indicator.close();
          }
        }) .catch(res=>{
          console.log("火警预警最近30天报警记录数据失败",res)
        });
      }else if(this.name == '高频报警部件'){
        this.$http({
           method: "get",
           url: "/api/services/app/FireUnit/GetFireUnitHighFreqAlarmFire",
           params: {
              Id:that.fireUnitId,
              SkipCount:0,
              MaxResultCount:that.MaxResultCount
            }
        }).then(res=>{
          console.log("火警预警高频报警部件查询",res)
          if(res.status == 200){
            that.GetFireUnitHighFreqAlarmFire = res.data.result.items
            that.totalCount = res.data.result.totalCount
             Indicator.close();
          }
        }) .catch(res=>{
          console.log("安全用电最近30天报警记录数据失败",res)
        });
      }
    }else if(this.classify == '设施故障'){
        this.$http({
           method: "get",
           url: "/api/services/app/FireUnit/GetFireUnitPendingFault",
           params: {
              Id:that.fireUnitId,
              SkipCount:0,
              MaxResultCount:that.MaxResultCount
            }
        }).then(res=>{
          console.log("设备设施故障监控成功",res)
          if(res.status == 200){
            that.GetFireUnitFaultList = res.data.result.items
            that.totalCount = res.data.result.totalCount
             Indicator.close();
          }
        }) .catch(res=>{
          console.log("设备设施故障监控失败",res)
        });
    }
   
  },
  beforeRouteEnter(to, from, next) {
   next(vm=>{          //  这里的vm指的就是vue实例，可以用来当做this使用
   console.log("sssssddddfff",from);
      vm.fromPath = from.fullPath
    })
  }
};
</script>