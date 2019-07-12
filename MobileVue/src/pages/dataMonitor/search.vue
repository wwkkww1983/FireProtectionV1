<style lang="less">
.padding{
    padding: 0 28px;
}
.border{
   /* border-bottom: 1px solid  #dcdcdc;  no */
}
.searchBox {
    position: relative;
    .headerSearch {
      width: 100%;
      height: 80px;
      display: flex;
      align-items: center;
      padding: 0 22px;
      background: url('../../assets/imgs/load_up_img_bg.png') repeat-x;
      box-sizing: border-box;
      .nextBtn{
          width: 20px;
          height: 34px;
          margin-right: 20px;
      }
      .inputBox{
          width: 94%;
          height: 58px;
          border-radius: 8px;
          background: #fff;
          display: flex;
          align-items: center;
          input{
            height: 100%;
            width: 92%;
            border-radius: 8px;
            text-indent: 10px;
            outline: none;
            border: none;
            &::placeholder{
                color: #cfcece;
            }
          }
          .searchIcons{
              width: 32px;
              height: 36px;
          }

      }

    }
    .hsitoryListBox{
          .historyTitle{
              font-size: 30px;
              color: #262626;
              height: 80px;
              line-height: 80px;

             .padding();
             /* .border(); */
          }
          .hsitoryList{
              list-style: none;
            .listContainer {
                padding: 20px 26px;
                box-sizing: border-box;
               /*  .border(); */
                display: flex;
                align-items: center;
                justify-content: space-between;
                .leftBox {
                  width: 94%;
                  .middleInfo {
                    width: 90%;
                    p {
                      overflow: hidden;
                      text-overflow: ellipsis;
                      white-space: nowrap;
                      line-height: normal;
                    }
                    .unit {
                      font-size: 30px;
                      color: #262626;
                      margin-bottom: 16px;
                      width: 100%;
                    }
                    .unitAddress {
                      width: 100%;
                      font-size: 26px;
                      color: #b2b1b1;
                    }
                  }
                }
                .nextIcon {
                  width: 18px;
                  height: 30px;
                }
            }
            .listContainer2{
                padding: 20px;
                display: flex;
                justify-content: space-between;
                align-items: center;
              /*   .border(); */
                .rightContainer {
                .frieHouseName {
                    font-size: 30px;
                    color: #262626;
                    margin-bottom: 18px;
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
    .searchList{
      width: 100%;
      background: #dfdfdf;
      padding-top: 20px;
      position: absolute;
      top: 80px;

      ul{
          list-style: none;
          background: white;
          .noData{
            height:180px;
            text-align:center;
            font-size:30px;
            color:#dfdfdf;
            line-height:180px;
          }
           .listContainer {
                padding: 20px 26px;
                box-sizing: border-box;
               /*  .border(); */
                display: flex;
                align-items: center;
                justify-content: space-between;
                .leftBox {
                  width: 94%;
                  .middleInfo {
                    width: 90%;
                    p {
                      overflow: hidden;
                      text-overflow: ellipsis;
                      white-space: nowrap;
                      line-height: normal;
                    }
                    .unit {
                      font-size: 30px;
                      color: #262626;
                      margin-bottom: 16px;
                      width: 100%;
                    }
                    .unitAddress {
                      width: 100%;
                      font-size: 26px;
                      color: #b2b1b1;
                    }
                  }
                }
                .nextIcon {
                  width: 18px;
                  height: 30px;
                }
           }
          .listContainer2{
            padding: 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            /* .border(); */
            .rightContainer {
              .frieHouseName {
                display:inline-block;
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
  }
</style>
<template>
  <div class="searchBox">
    <!-- 顶部搜索 -->
    <div class="headerSearch">
        <img @click="previousPage" class="nextBtn" src="../../assets/imgs/fireUnits/return_btn.png" alt="">
        <div class="inputBox">
            <input type="text" :placeholder="placeholder" v-model="search">
            <img @click="searchMethod" class="searchIcons" src="../../assets/imgs/fireUnits/search_btn.png" alt="">
        </div>
    </div>
    <!-- 搜索历史 -->
    <div class="hsitoryListBox">
        <div class="historyTitle border-bottom-1px">搜索历史</div>
        <ul class="hsitoryList">
            <div v-if="this.$route.query.mark == '防火单位'">
                 <li class="listContainer border-bottom-1px" @click="h_goToFrieUnintInfo(n.id,n.name)" v-for="(n) in GetFireUnitListForMobileHistory" :key="n.id">
                    <div class="leftBox">
                        <div class="middleInfo">
                            <p class="unit">{{n.name}}</p>
                            <p class="unitAddress">{{n.address}}</p>
                        </div>
                    </div>
                   <img class="nextIcon" src="../../assets/imgs/fireUnits/next_btn.png" alt>
                </li>
            </div>

            <div v-if="this.$route.query.mark == '微型消防站'">
                <li class="listContainer2 border-bottom-1px" @click="h_goToFrieUnintInfo(arr.id,arr.name)" v-for="(arr,index) in GetMiniFireStationHistory" :key="index"  >
                        <div class="rightContainer">
                        <span class="frieHouseName">{{arr.name}}</span>
                        <p class="frieHouseAddress">{{arr.address}}</p>
                      </div>
                      <img src="../../assets/imgs/fireUnits/next_btn.png" alt>
                </li>
            </div>

            <div v-if="this.$route.query.mark == '市政消火栓'">
                <li class="listContainer2 border-bottom-1px" @click="h_goToFrieUnintInfo(arr.id,arr.name)" v-for="(arr,index) in GetHydrantHistory" :key="index"  >
                      <div class="rightContainer">
                          <span class="frieHouseName">{{arr.sn}}</span>
                          <p class="frieHouseAddress">{{arr.address}}</p>
                      </div>
                      <img src="../../assets/imgs/fireUnits/next_btn.png" alt>
                </li>
            </div>
           
        </ul>
    </div>
    <!-- 内容模块 -->
    <div v-show="searchStatus" class="searchList" :style="{ height: Height + 'px' }">
        <ul>
            <div v-if="$route.query.mark == '防火单位'">
                   <li class="listContainer border-bottom-1px" @click="goToFrieUnintInfo(n.id,n.name,index)" v-for="(n,index) in searchListFireUnits" :key="n.id">
                        <div class="leftBox">
                            <div class="middleInfo">
                                <p class="unit">{{n.name}}</p>
                                <p class="unitAddress">{{n.address}}</p>
                            </div>
                        </div>
                        <img class="nextIcon" src="../../assets/imgs/fireUnits/next_btn.png" alt>
                    </li>

                    <div class="noData" v-if="searchListFireUnits.length<=0">
                      你搜索的关键字暂无结果
                    </div>
            </div>

            <div v-if="$route.query.mark == '微型消防站'  ">
                <li class="listContainer2 border-bottom-1px" v-for="(arr,index) in searchListMiniFireStation" :key="index" @click="goToFrieHosueInfo(arr.id,index)" >
                        <div class="rightContainer">
                        <span class="frieHouseName">{{arr.name}}</span>
                        <p class="frieHouseAddress">{{arr.address}}</p>
                      </div>
                      <img src="../../assets/imgs/fireUnits/next_btn.png" alt>
                </li>

                <div class="noData" v-if="searchListMiniFireStation.length<=0">
                  你搜索的关键字暂无结果
                </div>
            </div>


            <div v-if="$route.query.mark == '监管检查日志'">
                   <li class="listContainer border-bottom-1px" @click="goToAddRegulatory(n.id,n.name,index)" v-for="(n,index) in searchListFireUnits" :key="n.id">
                        <div class="leftBox">
                            <div class="middleInfo">
                                <p class="unit">{{n.name}}</p>
                                <p class="unitAddress">{{n.address}}</p>
                            </div>
                        </div>
                        <img class="nextIcon" src="../../assets/imgs/fireUnits/next_btn.png" alt>
                    </li>
                  <div class="noData" v-if="searchListFireUnits.length<=0">
                      你搜索的关键字暂无结果
                    </div>
            </div>

            <div v-if="$route.query.mark == '市政消火栓'">
                   <li class="listContainer listContainer" @click="goToHydrant(n.id,index)" v-for="(n,index) in searchListHydrant" :key="n.id">
                        <div class="leftBox">
                            <div class="middleInfo">
                                <p class="unit">{{n.sn}}</p>
                                <p class="unitAddress">{{n.address}}</p>
                            </div>
                        </div>
                        <img class="nextIcon" src="../../assets/imgs/fireUnits/next_btn.png" alt>
                    </li>
                    <div class="noData" v-if="searchListHydrant.length<=0">
                      你搜索的关键字暂无结果
                    </div>
            </div>
         
        </ul>
    </div>
  </div>
</template>
<script>

 let GetFireUnitListForMobileHistory = [];
 let GetMiniFireStationHistory = [];
 let GetHydrantHistory  = [];
export default {
    data(){
        return{
            placeholder:'',
            search:'',
            Height:'',
            searchStatus:false,
            searchListFireUnits:[],
            searchListMiniFireStation:[],//微型消防站搜索列表
            searchListHydrant:[],//消火栓搜索列表
            GetFireUnitListForMobileHistory:[],
            GetMiniFireStationHistory:[],//微型消防站的历史搜索列表
            GetHydrantHistory:[],//微型消防站的历史搜索列表
            
        }
    },
    methods:{
      previousPage(){
         this.$router.back(-1) ;
         console.log("ssssss")
      } ,
      /* 输入框搜索 */
      searchMethod(){

        console.log("触发搜索");
          let that = this;
          this.searchStatus = true
          if(this.$route.query.mark == '防火单位' || this.$route.query.mark == '监管检查日志'){
               this.$http({
                    method: "get",
                    url: "/api/services/app/FireUnit/GetFireUnitListForMobile",
                    params: {
                      Name: that.search
                    }
                }).then(function(res) {
                    console.log("模糊查询防火单位成功数据", res);
                    if(res.status = 200){
                        that.searchListFireUnits = res.data.result.items
                    }
                }).catch(function(res) {
                    console.log("模糊查询防火单位失败数据", res);
                });
          }else if(this.$route.query.mark == '微型消防站'){
              this.$http({
                    method: "get",
                    url: "/api/services/app/MiniFireStation/GetList",
                    params: {
                    Name: that.search
                    }
                }).then(function(res) {
                    console.log("模糊查询微型消防站成功数据", res);
                    if(res.status == 200 ){
                        that.searchListMiniFireStation = res.data.result.items
                    }
                }).catch(function(res) {
                    console.log("模糊查询微型消防站失败数据", res);
                });
          }else if(this.$route.query.mark == '市政消火栓'){
              this.$http({
                    method: "get",
                    url: "/api/services/app/Hydrant/GetListForApp",
                    params: {
                     Sn: that.search
                    }
                }).then(function(res) {
                    console.log("模糊查询消火栓成功数据", res);
                    if(res.status == 200 ){
                        that.searchListHydrant = res.data.result.items
                    }
                }).catch(function(res) {
                    console.log("模糊查询消火栓失败数据", res);
                });
          } 
        
      },
       /*  跳转到具体的消防单位*/
       goToFrieUnintInfo(id,name,index){
          let that =this;

          /* 点击之后存入本地历史记录 */
          GetFireUnitListForMobileHistory = JSON.parse(localStorage.getItem('GetFireUnitListForMobileHistory')) ;
          if(GetFireUnitListForMobileHistory.length <=0){
              GetFireUnitListForMobileHistory.push(that.searchListFireUnits[index]);
              localStorage.setItem('GetFireUnitListForMobileHistory',JSON.stringify(GetFireUnitListForMobileHistory))
          }else{
              for(let arr of GetFireUnitListForMobileHistory){
                  if(id != arr.id ){
                      GetFireUnitListForMobileHistory.push(that.searchListFireUnits[index]);
                      localStorage.setItem('GetFireUnitListForMobileHistory',JSON.stringify(GetFireUnitListForMobileHistory))
                      }
              }
          }

          /* 点击之后跳转 */
            this.$router.push({
                  path: "/dataMonitor/fireUnits/fireUnitinfos",
                  query: {
                    id:id,
                    name:name
                  }
            })
       },
      /* 跳转到具体消防站信息 */
        goToFrieHosueInfo(id,index) {
            let that = this;
            /* 点击之后存入本地历史记录 */
            GetMiniFireStationHistory = JSON.parse(localStorage.getItem('GetMiniFireStationHistory')) ;
            if(GetMiniFireStationHistory.length <=0){
                GetMiniFireStationHistory.push(that.searchListMiniFireStation[index]);
                localStorage.setItem('GetMiniFireStationHistory',JSON.stringify(GetMiniFireStationHistory))
            }else{
                for(let arr of GetMiniFireStationHistory){
                    if(id != arr.id ){
                        GetMiniFireStationHistory.push(that.searchListMiniFireStation[index]);
                        localStorage.setItem('GetMiniFireStationHistory',JSON.stringify(GetMiniFireStationHistory))
                        }
                }
            }

            this.$router.push({
                path: "/dataMonitor/fireHouseInfo",
                query: {
                id
                }
            });
        },
      /* 跳转到新增监管执法界面 */
        goToAddRegulatory(id,name){
          this.$router.push({
            path:'/regulatory/addregulatory',
            query:{
              fireunitId:id,
              fireUnitName:name
            }
          })
        },
      /* 跳转到具体的消火栓界面 */
        goToHydrant(id,index){
           let that = this;
            /* 点击之后存入本地历史记录 */
            GetHydrantHistory = JSON.parse(localStorage.getItem('GetHydrantHistory')) ;
            if(GetHydrantHistory.length <=0){
                GetHydrantHistory.push(that.searchListHydrant[index]);
                localStorage.setItem('GetHydrantHistory',JSON.stringify(GetHydrantHistory))
            }else{
                for(let arr of GetHydrantHistory){
                    if(id != arr.id ){
                        GetHydrantHistory.push(that.searchListHydrant[index]);
                        localStorage.setItem('GetHydrantHistory',JSON.stringify(GetHydrantHistory))
                    }
                }
            }


          this.$router.push({
            path:'/dataMonitor/fireCock/fireCockInfo',
            query:{
                id:id
            }
          })
        },

       /* 历史记录中的跳转 */
        h_goToFrieUnintInfo(id,name){
            if(this.$route.query.mark == '防火单位'){
                this.$router.push({
                  path: "/dataMonitor/fireUnits/fireUnitinfos",
                  query: {
                    id:id,
                    name:name
                  }
            })
            }else if(this.$route.query.mark == '微型消防站'){
                  this.$router.push({
                      path: "/dataMonitor/fireHouseInfo",
                      query:{
                      id
                      }
                  });
            }else if(this.$route.query.mark == '市政消火栓'){
              this.$router.push({
                      path: "/dataMonitor/fireCock/fireCockInfo",
                      query:{
                      id
                      }
                  });
            }
        },
      },
    mounted(){
        let that =this;
        this.Height = document.body.clientHeight-60;

      
        console.log("可用高度",this.Height)
        if(this.$route.query.mark == '防火单位' || this.$route.query.mark == '微型消防站' ||this.$route.query.mark == '监管检查日志'){
             this.placeholder = '请输入名称进行模糊查询'
        }else if(this.$route.query.mark == '市政消火栓' ){
            this.placeholder = '输入编号进行模糊查询'
        }

        localStorage.setItem('GetFireUnitListForMobileHistory',JSON.stringify(GetFireUnitListForMobileHistory))
        localStorage.setItem('GetMiniFireStationHistory',JSON.stringify(GetMiniFireStationHistory))
        localStorage.setItem('GetHydrantHistory',JSON.stringify(GetHydrantHistory))
    
        if(this.$route.query.mark == '防火单位'){
         GetFireUnitListForMobileHistory=  JSON.parse(localStorage.getItem("GetFireUnitListForMobileHistory")) 
         if(GetFireUnitListForMobileHistory.length <=0){
             console.log("暂无")
         }else{
           that.GetFireUnitListForMobileHistory = GetFireUnitListForMobileHistory
         }
        }else if(this.$route.query.mark == '微型消防站'){
          GetMiniFireStationHistory=  JSON.parse(localStorage.getItem("GetMiniFireStationHistory")) 
         if(GetMiniFireStationHistory.length <=0){
             console.log("暂无")
         }else{
           that.GetMiniFireStationHistory = GetMiniFireStationHistory
         }
        }else if(this.$route.query.mark == '市政消火栓'){
          GetHydrantHistory  = JSON.parse(localStorage.getItem("GetHydrantHistory"))  
          if(GetHydrantHistory.length <=0){
             console.log("暂无")
         }else{
           that.GetHydrantHistory = GetHydrantHistory
         }
        }
    }
};
</script>