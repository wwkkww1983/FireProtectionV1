<style lang="less">
    .addressSearchBox{
        .headerSearch {
            width: 100%;
            height: 80px;
            display: flex;
            align-items: center;
            padding: 0 22px;
            background: url('../../../assets/imgs/load_up_img_bg.png') repeat-x;
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
        .searchResultList{
            list-style: none;
            li{
                height: 100px;
                line-height: 100px;
                padding: 0px 20px;
                color: #262626;
                font-size: 30px;
                border-bottom: 1px solid #dcdcdc; /* no */
                display: flex;
                justify-content: space-between;
                align-items: center;
                .next_btn{
                    width: 18px;
	                height: 30px;
                }
            }
        }
    }
</style>
<template>
    <div class="addressSearchBox">
        <div class="headerSearch">
            <img @click="previousPage"  class="nextBtn" src="../../../assets/imgs/fireUnits/return_btn.png" alt="">
            <div class="inputBox">
                <input type="text" @input="inputFun" placeholder="输入地址查询" v-model="searchkeyword">
                <img  class="searchIcons" src="../../../assets/imgs/fireUnits/search_btn.png" alt="">
            </div>
        </div>

        <!-- 查询出来的地址列表 -->
        <ul class="searchResultList">
            <li v-for="(arr,index) in searchResult"  @click="selectAddress(arr.location.M,arr.location.O,arr.name)" :key="index">
                <span>{{arr.name}}</span>
                <img class="next_btn" src="../../../assets/imgs/frieHouse/next_btn.png" alt="">
            </li>
        </ul>

    </div>
</template>
<script>
import topBack from '../../../components/topBack'
export default {
    components:{
        topBack
    },
    data() {
        return {
            searchkeyword:'',
            searchResult:'',
            timer:''


        }
    },
    methods: {
        /* 返回上一页 */
        previousPage(){
            this.$router.back(-1) ;

        } ,
        inputFun(val){
            let that =this;
           if(this.searchkeyword){
             
                    AMap.plugin("AMap.PlaceSearch", function() {
                    let autoOptions = {
                        city: "成都",
                        pageSize:12
                    };
                    let autoComplete = new AMap.PlaceSearch(autoOptions);
                    autoComplete.search(that.searchkeyword, function(status, result) {
                         console.log("关键字查询的结果", status, result);
                         if(result.poiList.pois){
                             that.searchResult = result.poiList.pois
                            console.log("oooooooo")
                         }
                    });
                });
             
           }
        },
        /* 点击 */
        selectAddress(lng,lat,address){
               this.$router.push({
                    path:'/dataMonitor/fireCock',
                    query:{
                       lat,
                       lng,
                       selected:'near',
                       address
                    }
               }) 
        }
    },
}
</script>