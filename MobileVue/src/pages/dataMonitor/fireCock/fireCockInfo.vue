<style lang="less">
.border{
    border-bottom: 1px solid #dcdcdc; /* no */
}
    .fireInfoBox{
        width: 100%;
        padding-top: 80px;
        box-sizing: border-box;
        .fireInfoContainer{
            width: 100%;
           padding: 0px 26px;
            box-sizing: border-box;
            .totalBox{
                padding: 30px 0px;
                font-size: 26px;
                color:#262626;

                .address{
                  margin: 20px 0;
                }
                .waterPressure{
                    margin-top: 80px;
                    .pressure{
                       
                        color: #ff8a00;
                    }
                     .title{
                            color: gray !important;
                        }
                }
                .title{
                    color: #989898;
                }
               .state{
                    font-size: 30px;
                }
                .content{
                    display: flex;
                    justify-content: space-between;
                    font-size: 36px;
                    color: #039cef;
                    line-height: 2;
                    .location{
                        display: flex;
                        align-items: center;
                        justify-content: flex-start;
                        font-size: 30px;
                        img{
                            width:44px;
                            height: 44px;
                            margin-right: 14px;
                        }
                    }
                }
                .offline{
                    color: #ff8a00;
                }
               .abnormal{
                    color: #ff8a00;
                }

            }
            .canvas{
                width: 100%;
                 #myChart{
                    width: 100%;
                    height: 400px;
                    }
            }

            .near30List{
                list-style: none;
                width: 100%;
                padding: 26px;
                box-sizing: border-box;
                .title{
                    font-size: 26px;
                    font-weight: normal;
                    font-stretch: normal;
                    line-height: 48px;
                    letter-spacing: 0px;
                    color: #989898;
                    display: flex;
                    justify-content: space-between;
                }
                li{
                    padding: 20px 0px;
                    font-size: 24px;
                    color: #262626;
                    border-top: 1px solid #dcdcdc; /* no */
                    display: flex;
                    justify-content: space-between;
                }
            }
        }

        .mint-popup{
            width: 70%;
            .chooseMap{
                width: 100%;
                height: 300px;
                .top{
                    height: 60px;
                    background: #039cef;
                    color: white;
                    font-size: 26px;
                    display: flex;
                    justify-content: space-between;
                    align-items: center;
                    padding: 0 20px;
                    span{
                        display: inline-block;
                        width: 94%;
                        text-align: center;
                    }
                    img{
                        width: 36px;
                        height: 36px;
                    }
                }
                ul{
                    list-style: none;
                    padding: 20px 20px;
                    li{
                     height: 80px;
                     margin-bottom: 20px;
                     display: flex;
                     font-size: 38px;
                     color: #989898;
                     align-items: center;
                     padding: 10px;
                     .godeA{
                        display: flex;
                        font-size: 38px;
                        color: #989898;
                        align-items: center;
                        text-decoration:none;
                        &:visited{
                            text-decoration:none;
                        }
                     }
                     .baiduA{
                        display: flex;
                        font-size: 38px;
                        color: #989898;
                        align-items: center;
                        text-decoration:none;
                        &:visited{
                            text-decoration:none;
                        }
                     }
                     img{
                         width: 70px;
                         height: 70px;
                         margin-right: 50px;
                     }
                    } 
                }
            }
        }
      
       
    }
     
</style>
<template>
    <div class="fireInfoBox">
       <tabBack :title="$route.name" mark="TofireCock"></tabBack>
       <topHome></topHome>
        <div class="fireInfoContainer">
            <div class="totalBox">
                <p class="nmuber">
                    <span class="title">编号：</span>
                    <span class="content">{{sn}}</span>
                </p>
                <div class="address">
                    <span class="title">地址：</span>
                    <div class="content">
                        <span>{{address}}</span>
                        <div class="location" @click="chooseMap">
                            <img src="../../../assets/imgs/frieHouse/location.png" alt="">
                            <span>导航</span>
                        </div>
                        
                    </div>
                </div>
                <p class="address">
                    <span class="title">状态：</span>
                    <span class="state content"  v-if="status == 0">未指定</span>
                    <span class="state normal content"  v-if="status == 1">正常</span>
                    <span class="state offline content"  v-if="status == -1">离线</span>
                    <span class="state abnormal content"  v-if="status == -2">异常</span>
                </p>
                <p class="waterPressure">
                    <span class="title">水压：</span>
                   
                    <span class="content" v-if="status == -1">——</span>
                     <span class="content" v-else>{{pressure}}Kpa</span>
                </p>
            </div>
            <div class="canvas">
                <div id="myChart"></div>
            </div>
            <ul v-if="near30Length>0" class="near30List">
                <div class="title">
                    <span class="left">最近30天报警记录</span>
                    <span class="left">总次数：{{near30Length}}</span>
                </div>
                <li v-for="(arr,index) in near30List" :key="index" class="near30Li">
                    <span class="timeBox">{{arr.creationTime}}</span>
                    <span class="describe">{{arr.title}}</span>
                </li>
            </ul>




        </div>




        <mt-popup v-model="popupVisible" position="center">
            <div class="chooseMap">
                <div class="top">
                <span>请选择选择地图</span>  
                <img @click="clooseMap" class="closeBtn" src="../../../assets/imgs/frieHouse/close.png" alt=""> 
                </div>
                <ul class="content">
                    <li class="gode" @click="gotoGaode">

                        <a class="godeA">
                            <img src="../../../assets/imgs/frieHouse/gode.png" alt="">
                            <span>高德地图</span>
                        </a>
                        
                    </li>
                    <li class="baidu" @click="gotoBaidu">
                        <a class="baiduA">
                            <img src="../../../assets/imgs/frieHouse/baidu.png" alt="">
                            <span>百度地图</span>
                        </a>
                        
                    </li>
                </ul>
            </div>
        </mt-popup>
    </div>
</template>
<script>
import tabBack from '../../../components/topBack/index'
import topHome from '../../../components/topHome/index'
import { Popup,Toast  } from 'mint-ui';

export default {
    components:{
        tabBack,
        topHome
    },
    data(){
        return{ 
            sn:'',
            address:'',
            status:'',
            pressure:'',
            lat:0,
            lon:0,
            hydrantRecord:[],
            RecordCount:0,
            popupVisible:false,
            near30Length:0,//
            near30List:[],//

        }
        
    },
    mounted(){
        let id = this.$route.query.id;
        let  that = this;
        /* 正常记录 */
        this.$http({
            method: "get",
            url: "/api/services/app/Hydrant/GetInfoById",
            params: {   
                id,
            }
        }).then(res=>{
            console.log("打印具体信息成功",res)
            if(res.status == 200){
                that.sn = res.data.result.sn
                that.address = res.data.result.address
                that.status = res.data.result.status
                that.pressure = res.data.result.pressure
                that.lat = res.data.result.lat
                that.lon = res.data.result.lng
            }
        }).catch(res=>{
            console.log("打印具体信息失败",res)
        })
        /* 图形数据 */
        this.$http({
            method: "get",
            url: "/api/services/app/BigScreen/GetHydrantPressHistory",
            params: {   
                id,
            }
        }).then(res=>{
             console.log("图形数据打印成功",res)
              let myChart = this.$echarts.init(document.getElementById("myChart"));
              let dataTime =[]
              let dataSeries =[];
              for(let i = 0; i< res.data.length;i++){
                  let index = res.data[i].x.indexOf(' ');
                  let time = res.data[i].x.slice(0,-index+1);
                  index = time.indexOf('/');
                   time =time.slice(index+1,10);
                    dataTime.push(time);
                    dataSeries.push(res.data[i].y);

              }

              myChart.setOption({
                color:'#039cfe',
                xAxis: {
                    type: 'category',
                    boundaryGap: false,
                    data: dataTime,
                    axisLabel:{
                        interval:0,
                        align:'right'
                    }
                },
                yAxis: {
                    type: 'value'
                },
                grid: {
                    left: '0%',
                    right: '0%',
                    
                    top:'10%',
                    containLabel: true
                },
                series: [{
                    data: dataSeries,
                    type: 'line',
                    areaStyle: {}
                }]
            })
        }).catch(res=>{
            console.log("报错")
        })

        /* 接收最近30天报警记录 */
        this.$http({
            method: "get",
            url: "/api/services/app/Hydrant/GetNearbyAlarmById",
            params: {   
                id,
                SkipCount:0,
                MaxResultCount:8

            }
        }).then(res=>{
            console.log("打印出最近30天的报警激励",res)
            that.near30Length = res.data.result.totalCount;
            that.near30List = res.data.result.items;
       }).catch(res=>{

        })
       
    },
    methods:{
        chooseMap(){
           this.popupVisible = true;
        },
        clooseMap(){
            this.popupVisible = false;
        },
        /*  高德地图*/
        gotoGaode(){
           var appstore, ua = navigator.userAgent;
             if(ua.match(/Android/i)){ 
                appstore = 'market://search?q=com.singtel.travelbuddy.android';
            }
            let sname = localStorage.getItem('navgationAddress');
            let slat = localStorage.getItem('lat')
            let slon = localStorage.getItem('lng')
            let dlat = this.lat;//终点的纬度
            let dlon = this.lon;//终点的经度
            let dname = this.address;//终点的名称
            var a = document.getElementsByClassName('godeA')[0]
            a.href = `amapuri://route/plan/?did=BGVIS2&dlat=${dlat}&dlon=${dlon}&dname=${dname}&dev=0&t=0`
            a.onclick = this.applink(appstore);
   },
    applink(fail){  
        this.popupVisible = false;
        Toast({  message: "你没有安装地图软件，安装后再使用", //提示内容分
            position: "center", //提示框位置
            duration: 3000 , //持续时间（毫秒），若为 -1 则不会自动关闭
            className:"addClass"   //Toast 的类名。可以为其添加样式
        })
        console.log("没有APP")
    }, 
    /* 百度地图 */
    gotoBaidu(){
        var appstore, ua = navigator.userAgent;
        if(ua.match(/Android/i)){ 
            appstore = 'market://search?q=com.singtel.travelbuddy.android';
        }
        let name = localStorage.getItem('navgationAddress');
        let slat = localStorage.getItem('lat')
        let slon = localStorage.getItem('lng')
        let destination = this.address;//终点的名称
        var a = document.getElementsByClassName('baiduA')[0]
        a.href = `baidumap://map/direction?destination=${destination}&mode=driving&sy=0&index=0&target=1&src=andr.baidu.openAPIdemo`
        a.onclick = this.applink(appstore);
    }

       
    }
}
</script>