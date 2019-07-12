<style lang="less">
.border-bottom{
    border-bottom: 1px solid #dcdcdc; /* no */
}
.regulatoryBox {
  width: 100%;
  box-sizing: border-box;
  padding-top: 160px;
  .add_classifyBox{
      width: 100%;
      height: 80px;
      padding:0 28px;
      box-sizing: border-box;
      .border-bottom();
      display: flex;
      justify-content: space-between;
      align-items: center;
      position: fixed;
      top: 80px;
      z-index: 1000000;
      .select{
          
          .inner{
              .inputWrapper{
                 border-radius: 8px;
               border: solid 1px #dcdcdc;  /*no */
                width: 166px;
                height: 50px;
                display: flex;
                align-items: center;
                padding: 0px 14px;
                input{
                    border: none;
                    width: 100%;
                    line-height: 50px;
                    font-size: 26px;
                     color: #989898;
                    &::placeholder{
                        color: #989898;
                    }
                }
                .icon{
                    width: 17px;
                    height: 12px;
                }
              }
            ul{
              margin-top: 10px;
              list-style: none;
              width: 166px;
              padding: 14px 14px;
              background: white;
              border: 1px solid #dcdcdc; /* no */
              border-radius: 8px;
              position: absolute;
              li{
                  height: 70px;
                  font-size: 26px;
                  color: #262626;
                  border-bottom: 1px solid #dcdcdc;/* no */
                  line-height: 70px;
              }
            }
          }
          
      }
      .addBtn{
          img{
        width: 92px;
          height: 28px;
          }
         
      }

  }
  .regulatorListyBox{
    overflow: scroll;
    box-sizing: border-box;
    position: relative;
    box-sizing: border-box;

    .page-loadmore-wrapper{
        overflow: scroll;
    }
    ul{
          list-style: none;
          background: white;
          li{
              .border-bottom();
              padding: 20px 24px;
              display: flex;
              justify-content: space-between;
              align-items: center;
              .left{
                  width: 76%;
                   .addressName{
                        font-size: 26px;
                        color: #262626;
                        width: 100%;
                        display: inline-block;
                        text-overflow: ellipsis;
                        overflow: hidden;
                        white-space: nowrap;
                      
                    }
                    .time_status{
                        display: flex;
                        justify-content: space-between;
                        margin: 20px 0px;
                        .time{
                            font-size: 24px;
                            color: #989898;
                        }
                    }
              }
             
            .right{
                display: flex;
                align-items: center;
                  
                    .status{
                        font-size: 24px;
                    }
                    .ff0000{
                        color: #ff0000;
                    }
                    .ff8a00{
                        color: #ff8a00;
                    }
                    .green{
                        color: #17e017;
                    }
                
                .next_bg{
                    margin-left: 20px;
                    width: 18px;
                    height: 30px;
                }
            }
          
              
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
  <div class="regulatoryBox">
    <!-- header头部 -->
    <topinfo></topinfo>
    <!-- 新增 -->
    <div class="add_classifyBox ">
      <div class="select">
            <div class="inner">
                <div class="inputWrapper" @click="showOptions = !showOptions">
                    <input type="text" readonly :value="selected" placeholder="全部记录">
                   <img class="icon" src="../../assets/imgs/regulatory/jgjc_btn_dropDown.png" alt="">
                </div>
                <ul class="options"  v-show="showOptions">
                    <li  :style="active" @click="choose(item.value,item.id)"   v-for="(item, index) in options" :key="index">{{item.value}}</li>
                </ul>
            </div>
        </div>
      <!--  -->
      <div @click="addNew" class="addBtn">
          <img src="../../assets/imgs/regulatory/new_btn.png" alt="">
      </div>
    </div>

    <div class="page-loadmore regulatorListyBox">
        <div class="page-loadmore-wrapper" ref="wrapper" :style="{ height: wrapperHeight + 'px' }">
            <mt-loadmore :bottom-method="loadBottom" @bottom-status-change="handleBottomChange" :autoFill="autoFill" :bottom-all-loaded="allLoaded" ref="loadmore">
                <ul>
                    <li @click="showRegultory(arr.id)" class="regulatoryList " v-for="(arr,index) in regulatory" :key="index">
                        <div class="left">
                               <span class="addressName">{{arr.fireUnitName}}</span>
                               <div class="time_status">
                                    <span class="time">最近执法检查：{{arr.creationTime}}</span>
                                
                                </div>
                                
                        </div>
                        <div class="right">
                            <span class="status green" v-if="arr.checkResult == 1">合格</span>
                            <span class="status" v-if="arr.checkResult == -1">现场改正</span>
                            <span class="status ff8a00" v-if="arr.checkResult == -2" >限期整改</span>
                            <span class="status ff0000" v-if="arr.checkResult == -3">停业整顿</span>
                            <img class="next_bg" src="../../assets/imgs/regulatory/next_btn.png" alt="">
                        </div>
                        
                    </li>
                </ul>
                 <div slot="bottom" class="mint-loadmore-bottom">
                    <span v-show="bottomStatus !== 'loading'" :class="{ 'is-rotate': bottomStatus === 'drop' }">↑</span>
                    <span v-show="bottomStatus === 'loading'">
                    <mt-spinner type="snake" color="#0390fe"></mt-spinner>
                    </span>
                </div>
            </mt-loadmore>
        </div>
    </div>
    <tabBar></tabBar>
  
  </div>
</template>
<script>
import tabBar from "../../components/tabBar/tabBar";
import topinfo from "../../components/topinfo/topinfo";
export default {
  components: {
    tabBar,
    topinfo
  },
  data() {
    return {
        showOptions:false,
        selected:'全部记录',
        active:'',
        regulatory:[],
        options: [
            {
            id:0,
            value: '全部记录'
            },
            {
            id:-1,
            value: '现场改正'
            },
            {
            id:1,
            value: '合格'
            },
            {
            id:-2,
            value: '限期整改'
            },
            {
            id:-3,
            value: '停业整顿'
            }
        ],
         allLoaded: false,
        autoFill:false,
        bottomStatus: '',
        wrapperHeight: 0,
        count:0,
        SkipCount:0,
        MaxResultCount:7,
        CheckResult:0
    }
  },
  methods:{
    indexSelect(){
　　      console.log(this.indexId);//在这里可以正确输出每个下拉框对应的下标值，当然输出值都是可以的
    },
    choose(value,id) {
        let that = this;
        this.count =0;
        this.SkipCount =0;
        this.MaxResultCount =7;
        this.showOptions = false;
          this.allLoaded = false;
        if (value !== this.selected) {
        this.selected = value
            switch(id)
                {
                case 0:
                    this.CheckResult = 0;
                    this.$http({
                        method: "get",
                        url: "/api/services/app/Supervision/GetList",
                        params:{
                            SkipCount:0,
                            MaxResultCount:that.MaxResultCount,
                            CheckResult:0
                        }
                    }).then(res=>{
                        console.log("监管执法的初始化数据成功",res)
                        if(res.status == 200){
                            that.regulatory = res.data.result.items
                        }
                    }).catch(res=>{
                        console.log("监管执法的初始化数据失败",res)
                    })
                    break;
                case 1:
                 this.CheckResult = 1;
                   this.$http({
                        method: "get",
                        url: "/api/services/app/Supervision/GetList",
                        params:{
                            SkipCount:0,
                            MaxResultCount:that.MaxResultCount,
                            CheckResult:1
                        }
                    }).then(res=>{
                        console.log("监管执法的初始化数据成功",res)
                        if(res.status == 200){
                            that.regulatory = res.data.result.items
                        }
                    }).catch(res=>{
                        console.log("监管执法的初始化数据失败",res)
                    })
                    break;
                case -1:
                    this.CheckResult = -1;
                    this.$http({
                        method: "get",
                        url: "/api/services/app/Supervision/GetList",
                        params:{
                            SkipCount:0,
                            MaxResultCount:that.MaxResultCount,
                            CheckResult:-1
                        }
                    }).then(res=>{
                        console.log("监管执法的初始化数据成功",res)
                        if(res.status == 200){
                            that.regulatory = res.data.result.items
                        }
                    }).catch(res=>{
                        console.log("监管执法的初始化数据失败",res)
                    })
                    break;
                case -2:
                     this.CheckResult = -2;
                    this.$http({
                        method: "get",
                        url: "/api/services/app/Supervision/GetList",
                        params:{
                            SkipCount:0,
                            MaxResultCount:that.MaxResultCount,
                            CheckResult:-2
                        }
                    }).then(res=>{
                        console.log("监管执法的初始化数据成功",res)
                        if(res.status == 200){
                            that.regulatory = res.data.result.items
                        }
                    }).catch(res=>{
                        console.log("监管执法的初始化数据失败",res)
                    })
                    break;
                case -3:
                     this.CheckResult = -3;
                     this.$http({
                        method: "get",
                        url: "/api/services/app/Supervision/GetList",
                        params:{
                            SkipCount:0,
                            MaxResultCount:that.MaxResultCount,
                            CheckResult:-3
                        }
                    }).then(res=>{
                        console.log("监管执法的初始化数据成功",res)
                        if(res.status == 200){
                            that.regulatory = res.data.result.items
                        }
                    }).catch(res=>{
                        console.log("监管执法的初始化数据失败",res)
                    })
                    break;
                default:
                    this.$http({
                        method: "get",
                        url: "/api/services/app/Supervision/GetList",
                        params:{
                            SkipCount:0,
                            MaxResultCount:that.MaxResultCount,
                            CheckResult:0
                        }
                    }).then(res=>{
                        console.log("监管执法的初始化数据成功",res)
                        if(res.status == 200){
                            that.regulatory = res.data.result.items
                        }
                    }).catch(res=>{
                        console.log("监管执法的初始化数据失败",res)
                    })
                }
        }
    },
    addNew(){
        this.$router.push({
            path:'/regulatory/addregulatory'
        })
    },
    /* 查看记录 */
    showRegultory(id){
        this.$router.push({
            path:'/regulatory/showregulatory',
            query:{
                id
            }
        })
    },
    /* 修改记录 */
    editStatus(id){
        this.$router.push({
            path:'/regulatory/editRegulatory',
            query:{
                id
            }
        })
    },
    /*  */
    handleBottomChange(status) {
            this.bottomStatus = status;
    },
    //向下滑动加载更多
    loadBottom() {
        let that = this;
        that.count++;
        that.$http({
        method: "get",
        url: "/api/services/app/Supervision/GetList",
        params:{
            SkipCount:that.count*that.MaxResultCount,
            MaxResultCount:that.MaxResultCount,
            CheckResult:that.CheckResult
        }
        }).then(res=>{
            console.log("监管执法的初始化数据成功",res)
            if(res.status == 200){
                if(res.data.result.items.length<that.MaxResultCount){
                         this.allLoaded = true;
                }
               for(let arr of res.data.result.items) {
                   that.regulatory.push(arr)
               }
            }
            that.$refs.loadmore.onBottomLoaded();
        }).catch(res=>{
            console.log("监管执法的初始化数据失败",res)
        })

         
    }
  },
  mounted(){
      this.wrapperHeight = document.documentElement.clientHeight - this.$refs.wrapper.getBoundingClientRect().top-60;

    let that = this;
    that.$http({
      method: "get",
      url: "/api/services/app/Supervision/GetList",
      params:{
          SkipCount:that.SkipCount,
          MaxResultCount:that.MaxResultCount
      }
    }).then(res=>{
        console.log("监管执法的初始化数据成功",res)
        if(res.status == 200){
            that.regulatory = res.data.result.items
        }
    }).catch(res=>{
        console.log("监管执法的初始化数据失败",res)
    })
  }
};
</script>