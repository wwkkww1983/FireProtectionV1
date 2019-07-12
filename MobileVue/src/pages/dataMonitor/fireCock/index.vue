<style lang="less">
.border {
  border-bottom: 1px solid #dcdcdc; /* no */
}
    .fireCockBox{
        width: 100%;
        padding-top: 80px;
        /* topNav */
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
                border: 1px solid #dcdcdc;  /*no */
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
        /*  */
        .page-loadmore {
            overflow: scroll;
            box-sizing: border-box;
            position: relative;
            .page-loadmore-wrapper{
                overflow: scroll;
            }
            .ListBox{
                 width: 100%;
                 list-style: none;
               .listContainer{
                   box-sizing: border-box;
                   padding: 20px 26px;
                    border-bottom: 1px solid #dcdcdc;/*  no */
                    display: flex;
                    justify-content: space-between;
                    align-items:center;
                    width: 100%;
                    .leftBox{
                        width: 90%;
                        .numberState{
                            margin-bottom: 16px;
                            span{
                                display: inline-block;
                                &.number{
                                    width: 146px;
                                    border-right: 1px solid #262626; /* no */
                                    text-align: left;
                                    font-size: 30px;
                                    color: #262626;
                                }
                                &.state{
                                    font-size: 30px;
                                    margin-left: 20px;
                                }
                                &.normal{
                                   color: #34e21d;
                                }
                                &.offline{
                                    color: #aaaaaa;
                                }
                                &.abnormal{
                                    color: #ff8a00;
                                }
                            }
                        }
                        .address{
                            font-size: 26px;
                            color: #b2b1b1;
                        }
                    }
                    .nextIcon{
                        width: 18px;
	                    height: 30px;
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
            .backTop{
                position: fixed;
                width: 78px;
                height: 78px;
                bottom: 40px;
                right: 40px;
            }
        }

        /* 附近 */
        .near{
            .listContainer{
                box-sizing: border-box;
                padding: 20px 26px;
                border-bottom: 1px solid #dcdcdc; /* no */
                display: flex;
                justify-content: space-between;
                align-items:center;
                width: 100%;
                .leftBox{
                    width: 90%;
                    .numberState{
                        margin-bottom: 20px;
                        span{
                            display: inline-block;
                            &.number{
                                width: 146px;
                                border-right: 1px solid #262626; /* no */
                                text-align: left;
                                font-size: 30px;
                                color: #262626;
                            }
                            &.state{
                                font-size: 30px;
                                margin-left: 20px;
                                 color: #34e21d;
                            }
                            &.normal{
                                   color: #34e21d;
                            }
                            &.offline{
                                color: #aaaaaa;
                            }
                            &.abnormal{
                                color: #ff8a00;
                            }


                           
                        }
                    }
                    .distance{
                        margin-bottom: 20px;
                        font-size: 26px;
                        color: #b2b1b1;

                    }
                    .address{
                        font-size: 26px;
                        color: #b2b1b1;
                    }
                }
                .nextIcon{
                    width: 18px;
                    height: 30px;
                }
            }
            .noData{
                font-size: 30px;
                color: #aaaaaa;
                height: 80px;
                line-height: 80px;
                text-align: center;
               
            }
        }

    }
</style>
<template>
    <div class="fireCockBox">
        <!-- 返回导航 -->
       <tabBack :title="$route.name" mark="TodataMonitor"></tabBack>
       <topHome></topHome>
         <!-- topNav -->
        <div class="topNav ">
            <mt-navbar v-model="selected">
                <mt-tab-item id="comprehensive">综合</mt-tab-item>
                <mt-tab-item id="near">附近</mt-tab-item>
            </mt-navbar>
            <div class="searchBox" @click="goToSearch($route.name)">
            <div class="search border-1px">
                <input type="text" disabled :placeholder="(selected == 'comprehensive')?'请输入编号进行模糊查询':'输入地址查询'">
                <img src="../../../assets/imgs/fireUnits//search_btn.png" alt>
            </div>
            </div>
            <p  class="addressBox" v-if="selected == 'near'">
            查找“{{address}}”附近结果
            </p>
        </div>
        <!--  -->
        <mt-tab-container v-model="selected">
            <!-- 综合 -->
            <mt-tab-container-item id="comprehensive">
                 <div class="page-loadmore">
                    <div class="page-loadmore-wrapper" ref="wrapper" :style="{ height: wrapperHeight + 'px' }">
                        <mt-loadmore :bottom-method="loadBottom" @bottom-status-change="handleBottomChange" :autoFill="autoFill" :bottom-all-loaded="allLoaded" ref="loadmore">
                            <ul  class="ListBox page-loadmore-list">
                                <li v-for="(arr,index) in Hydrant" :key="index" class="listContainer  page-loadmore-listitem" @click="goToInfo(arr.id)">
                                    <div class="leftBox">
                                    <div class="numberState">
                                        <span class="number">{{arr.sn}}</span>
                                        <span class="state"  v-if="arr.status == 0">未指定</span>
                                        <span class="state normal"  v-if="arr.status == 1">正常</span>
                                        <span class="state offline"  v-if="arr.status == -1">离线</span>
                                        <span class="state abnormal"  v-if="arr.status == -2">异常</span>
                                    </div>
                                    <p class="address">{{arr.address}}</p>
                                    </div>

                                    <img class="nextIcon" src="../../../assets/imgs/fireUnits/next_btn.png" alt>
                                </li>
                            </ul>
                            <div slot="bottom" class="mint-loadmore-bottom">
                                <span v-show="bottomStatus !== 'loading'" :class="{ 'is-rotate': bottomStatus === 'drop' }">↑</span>
                                <span v-show="bottomStatus === 'loading'">
                                <mt-spinner type="snake"  color="#0390fe"></mt-spinner>
                                </span>
                            </div>
                        </mt-loadmore>
                    </div>

                    <div class="test">

                    </div>

                    <img @click="backToTop" class="backTop" src="../../../assets/imgs/fireUnits/returnTop_btn.png" alt="">
                </div>
            </mt-tab-container-item>
            <!-- 附近 -->
            <mt-tab-container-item id="near">
                <div class="near">
                    <ul v-if="GetNearbyHydrant.length>0">
                        <li @click="goToInfo(arr.id)" class="listContainer " v-for="(arr,index) in GetNearbyHydrant" :key="index">
                            <div class="leftBox">
                                <div class="numberState">
                                    <span class="number">{{arr.sn}}</span>
                                    <span class="state"  v-if="arr.status == 0">未指定</span>
                                    <span class="state normal"  v-if="arr.status == 1">正常</span>
                                    <span class="state offline"  v-if="arr.status == -1">离线</span>
                                    <span class="state abnormal"  v-if="arr.status == -2">异常</span>
                                </div>
                                <p class="distance">距离当前位置{{arr.distance}}米</p>
                                <p class="address">{{arr.address}}</p>
                            </div>
                            <img class="nextIcon" src="../../../assets/imgs/fireUnits/next_btn.png" alt>
                        </li>
                    </ul>
                    <div class="noData" v-else>
                        该地理位置附近暂无数据
                    </div>
                </div>
            </mt-tab-container-item>

        </mt-tab-container>
        <!--  -->
       

    </div>
</template>
<script>
import tabBack from '../../../components/topBack/index'
import topHome from '../../../components/topHome/index'
import { Indicator } from "mint-ui";
export default {
    components:{
        tabBack,
        topHome
    },
    data(){
        return{
            address:"正在定位中",
            selected: "comprehensive",
            allLoaded: false,
            autoFill:false,
            bottomStatus: '',
            wrapperHeight: 0,
            Hydrant:[],//综合消防栓数据
            count:0,
            MaxResultCount:8,
            GetNearbyHydrant:''//附近消火栓数据

        }
    },
    methods:{
         //跳转到搜索页面
        goToSearch() {
            if(this.selected == 'near'){
                this.$router.push({
                    path: "/dataMonitor/fireCock/fireCockInfo/fireCockSearch",
                    query:{
                        mark:'市政消火栓'
                    }
                });
            }else{
                this.$router.push({
                    path: "/search",
                    query:{
                        mark:'市政消火栓'
                    }
                });
            }
           
        },
        /* 跳转到详情页面 */
        goToInfo(id){
            this.$router.push({
                path:'/dataMonitor/fireCock/fireCockInfo',
                query:{
                    id:id
                }
            })
        },
        //回到顶部
        backToTop(){
            let that = this;
            let timer = setInterval(function() {
            let curHeight = that.$refs.wrapper.scrollTop; // 得到当前高度
            var now = curHeight;
            var speed = (0 - now) / 7; // 随着高度减速
            speed = speed > 0 ? Math.ceil(speed) : Math.floor(speed);
            if (curHeight == 0) {
                clearInterval(timer);
            }
            that.$refs.wrapper.scrollTop = curHeight + speed; //直接给高度赋值,会调用needtotop方法
            }, 30);
        },
        //
        handleBottomChange(status) {
                this.bottomStatus = status;
        },
        //向下滑动加载更多
        loadBottom() {
            let that = this;
            that.count++;
            this.$http({
                method: "get",
                url: "/api/services/app/Hydrant/GetListForApp",
                params: {   
                SkipCount: that.count*that.MaxResultCount,
                MaxResultCount: that.MaxResultCount
                }
            }).then((res)=> {
                console.log("消火栓数据加载更多成功",res)
                if(res.status == 200){
                    if(res.data.result.items.length<that.MaxResultCount){
                         this.allLoaded = true;
                      }
                    for(let arr of  res.data.result.items ){
                        that.Hydrant.push(arr)
                    }
                     that.$refs.loadmore.onBottomLoaded();
                }
            }).catch(res=>{
                console.log("消火栓数据加载更多失败",res)
            })
             
        },

        //
        getPosition() {
            if(localStorage.getItem('lat') && localStorage.getItem('lng')){

                let that = this;
                that.address = localStorage.getItem('locationAddress')
                this.$http({
                    methods:'get',
                    url:'/api/services/app/Hydrant/GetNearbyHydrant',
                    params:{
                        lng:localStorage.getItem('lng'),
                        lat:localStorage.getItem('lat')
                    }
                }).then(res=>{
                    console.log("市政消火栓附近数加载成功",res)
                    that.GetNearbyHydrant = res.data.result;
                }).catch(res=>{
                    console.log('市政消火栓附近数加载失败',res)
                })
            }else if(localStorage.getItem('locationFail')){
                this.address = localStorage.getItem('locationFail')
            }
        },
    },
    mounted(){
        //接收参数
        let selected = this.$route.query.selected;
        let lat = this.$route.query.lat;
        let lng = this.$route.query.lng;
        let address = this.$route.query.address
        let that = this;

            if(selected && lat && lng){
                this.selected = selected
                this.lat = lat
                this.lng = lng
                this.address = address
                console.log("选择地址后附近消火栓数据成功",lat,lng,address)

                this.$http({
                    method: "get",
                    url: "/api/services/app/Hydrant/GetNearbyHydrant",
                    params:{
                        lng:104.145627,
                        lat:30.649302
                    }

                }).then(res=>{
                    console.log("选择地址后附近消火栓数据成功",res)
                    if(res.data.result.length>0){
                        that.GetNearbyHydrant = res.data.result
                        console.log("ssssssssss",that.GetNearbyHydrant)
                    }
                }).catch(res=>{
                    console.log("选择地址后附近消火栓数据失败",res)
                })
            }else{
            this.getPosition(); // 调用获取地理位置
            }
        
        this.wrapperHeight = window.screen.height - this.$refs.wrapper.getBoundingClientRect().top-4;
         //消火栓综合数据初始化
                //数据初始化请求
                Indicator.open({
                text: "加载中...",
                spinnerType: "fading-circle"
                });
             this.$http({
                method: "get",
                url: "/api/services/app/Hydrant/GetListForApp",
                params: {   
                SkipCount: that.count*that.MaxResultCount,
                MaxResultCount: that.MaxResultCount
                }
            }).then((res)=> {
                console.log("消火栓数据初始化成功",res)
                if(res.status == 200){
                    that.Hydrant = res.data.result.items
                }
                Indicator.close();
            }).catch(res=>{
                console.log("消火栓数据初始化失败",res)
            })
         
         
    }
}
</script>