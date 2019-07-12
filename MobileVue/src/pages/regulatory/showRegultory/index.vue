<style lang="less">
.border-bottom{
    border-bottom: solid 1px #dcdcdc; /* no */
}
.display {
  display: flex;
  align-items: center;
}
.editReBox{
    padding-top: 80px;
    box-sizing: border-box;
    width: 100%;
    .topNav{
         .border-bottom();
            .mint-navbar .mint-tab-item.is-selected {
                width: 160px;
                height: 54px;
                background-color: #039cfe;
                border-radius: 8px;
                color: white;
                margin: 0;
                line-height: 54px;
                font-size: 30px;
                 margin-right: 100px !important;
            }
            .mint-navbar .mint-tab-item {
                padding: 0;
                color: #989898;
                font-size: 30px;
                
            }
            .mint-tab-item-label {
                line-height: 54px;
                margin-right: 100px !important;
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
                padding: 0 40px;
                justify-content: flex-start;
                font-size: 26px;
                .mint-tab-item-label {
                width: 160px;
                height: 54px;
                font-size: 30px;
                }
            }
    }
    .comprehensiveBox{
        padding: 34px;
        box-sizing: border-box;
        .topFill{
            span{
                font-size: 26px;
            }
            .fireUnits,.checkPersons{
                span{
                    font-size: 26px;
                }
            }
            .checkTime{
                margin: 50px 0px;
            }
            .checkResult{
                margin-top: 50px;
            }
            .checkResult{
                display: flex;
                align-items: center; 
                .selectResult{
                    .inputWrapper{
                        border-radius: 8px;
                        border: solid 1px #dcdcdc; /* no */
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
                            height: 50px;
                            font-size: 26px;
                            color: #262626;
                        }
                    }
                }
            }
            label{
                width: 112px;
                color: #262626;
                font-size: 26px;
                margin-right: 30px;
            }
            input{
                border: none;
                border-bottom: 1px solid #dcdcdc; /* no */
                width: 310px;

            }
        }
        .issueLegal{
            margin-top: 66px;
            div{
                display: flex;
                align-items: center;
                justify-content: space-between;
                font-size: 24px;
                color: #989898;
                &.middle{
                    margin: 50px 0px;
                }
            }
            input{
                width: 140px;
                border: none; 
                border-bottom: 1px solid #dcdcdc;/* no */
            }
            label{
                width: 160px;
                text-align: right;
            }
            .topTitle{
                font-size: 26px;
                color: #262626;
                 justify-content: flex-start;
                 margin-bottom: 50px;
                img{
                    width: 34px;
                    height: 30px;
                    margin-right: 14px;
                }
            }
        }
        /* 现场取照 */
        .onSitePhotography{
            width: 100%;
            width: 100%;
            margin-top: 68px;
            .topTitle {
                display: flex;
                img {
                    width: 36px;
                    height: 30px;
                }
                span {
                    color: #262626;
                    font-size: 26px;
                    margin-left: 12px;
                }
            }
            .photos{
                display: flex;
                display: flex;
                justify-content:flex-start;
                flex-flow:row wrap;
                align-content: flex-start;
                width: 100%;
                margin-top: 20px;
                img{
                    margin-right: 10px;
                    margin-bottom: 20px;
                    flex:  0 0 0 30%;
                    width: 30%;
                    height: 130px;
                }
            }

        }
    }
    .projectDetailsBox {
        padding: 0 26px;
        padding-bottom: 40px;
        width: 100%;
        box-sizing: border-box;
        /* 共同title样式 */
        .liTitle {
            .display();
            .liTitleTxt {
                font-size: 29px;
                color: #262626;
                margin-left: 16px;
            }
        }
        ul {
            padding-top: 30px;
            list-style: none;
            font-size: 26px;
            color: #262626;
            border-bottom: 1px solid #dcdcdc; /* no */
            padding-bottom: 30px;
            &:nth-child(1){
                .Incon02 {
                    width: 34px;
                    height: 26px;
                }
            }
        }
        li {
            position: relative;
            border-bottom: dotted 2px #dcdcdc;
            padding: 41px 0 5px 0;
            width: 95%;
            margin-bottom: 30px;

            .leftText {
                position: absolute;
                left: 0;
                bottom: -0.5em;
                background: #fff;
                padding-right: 10px;
                display: inline-block;
            }
            .statusText {
                position: absolute;
                right: 0;
                bottom: -0.5em;
                background: #fff;
                padding-left: 10px;
                color: #17e017;
            }
            .statusTextFalse{
                position: absolute;
                right: 0;
                bottom: -0.5em;
                background: #fff;
                padding-left: 10px;
                color: #ff8a00;
            }
           .isTips{
               position: absolute;
               right:-50px;
               width: 30px;
               height: 32px;
               top: 34px;

           }
        }
        .otherTips{
           width: 100%;
           padding-top: 30px;
            .Incon06 {
                width: 32px;
                height: 32px;
            }
            textarea{
                margin-top: 24px;
                width: 100%;
                height: 140px;
                border-radius: 8px;
                border: solid 1px #dcdcdc; /* no */
                outline: none;
                padding:20px;
                box-sizing:border-box;
                font-size:28px;
                color:#262626;

            }
        }
        ul:nth-of-type(1) .Incon{ 
            width: 34px;
            height: 26px;
        }
        ul:nth-of-type(2) .Incon{
            width: 30px;
            height: 32px;
        }
       ul:nth-of-type(3) .Incon{
            width: 24px;
            height: 34px;
        }
       ul:nth-of-type(4) .Incon{
            width: 36px;
            height: 36px;
        }
         /* 其他情况 */
        .itemDetailsList05{
           width: 100%;
           padding-top: 30px;
            .Incon06 {
                width: 32px;
                height: 32px;
            }
            textarea{
                margin-top: 24px;
                width: 99%;
                height: 140px;
                margin-left: 8px;
                border-radius: 8px;
                border: solid 1px #dcdcdc; /* no */
                outline: none;
                
            }
        }
    }
    .mint-popup {
        width: 750px;
        height: 500px;
        .popupBox {
            width: 100%;
            .top {
                width: 100%;
                background-color: #03aefe;
                height: 70px;
                color: white;
                font-size: 30px;
                text-align: center;
                line-height: 70px;
                position: relative;
                img {
                    position: absolute;
                    right: 20px;
                    top: 20px;
                    width: 34px;
                    height: 34px;
                }
            }
        .popupContainer{
            .isTrue{
              display: flex;  
              height: 106px;
              align-items: center;
              font-size: 26px;
              color: #262626;
              padding-left: 27px;
              .mint-switch-core::before{
                  background: #989898;
              }
            }
            .showtextarea{
                width: 88%;
                border: 1px solid #dcdcdc; /* no */
                outline: none;
                position: fixed;
                left: 50%;
                transform: translateX(-50%);
                height: 210px;
                text-indent: 10px;
                padding-top: 20px;
            }
        }
        }
    }
}
    
</style>
<template>
    <div class="editReBox">
           <topBack :title="$route.name"></topBack> 
           <!--  -->
           <div class="topNav">
                <mt-navbar v-model="selected">
                    <mt-tab-item id="comprehensive">综合信息</mt-tab-item>
                    <mt-tab-item id="projectDetails">项目明细</mt-tab-item>
                </mt-navbar>
           </div>
           <!--  -->
           <div class="container">
                <mt-tab-container v-model="selected">
                    <!-- 综合信息 -->
                     <mt-tab-container-item id="comprehensive">
                         <div class="comprehensiveBox">
                            <div class="topFill">
                                <div class="fireUnits">
                                    <label for="">防火单位:</label>
                                    <span>{{fireUnitName}}</span>
                                </div>
                                <div class="checkTime">
                                    <label for="">检查时间:</label>
                                    <span>{{creationTime}}</span>
                                </div>
                                <div class="checkPersons">
                                    <label for="">检查人员:</label>
                                    <span>{{checkUser}}</span>
                                </div>
                                <div class="checkResult">
                                    <label for="">检查结论:</label>
                                    <div class="selectResult">
                                      <span v-if="checkResult == 1">合格</span>
                                      <span v-if="checkResult == -1">现场改正</span>
                                      <span v-if="checkResult == -2">限期整改</span>
                                      <span v-if="checkResult == -3">现场改正</span>
                                    </div>
                                </div>
                            </div>
                            <!-- 下发法律文书 -->
                            <div class="issueLegal">
                                <div class="topTitle">
                                    <img src="../../../assets/imgs/regulatory/zfrz_img_01.png" alt="">
                                    <span>下发法律文书</span>
                                </div>
                                <div class="">
                                    <div class="left">
                                        <label for="">当场：</label>
                                        <input type="text" readonly :value="documentSite" id="onTheSpot" name="onTheSpot">
                                    </div>
                                     <div class="right">
                                        <label for="">期限：</label>
                                        <input type="text" readonly  :value="documentDeadline" id="timeLimit" name="timeLimit">
                                    </div>
                                </div>
                                 <div class="middle">
                                    <div class="left">
                                        <label for="">重大：</label>
                                        <input type="text" readonly :value="documentMajor" id="important" name="important">
                                    </div>
                                     <div class="right">
                                        <label for="">复查：</label>
                                        <input type="text" readonly :value="documentReview" id="review" name="review">
                                    </div>
                                </div>
                                 <div>
                                    <div class="left">
                                        <label for="">检查意见书：</label>
                                        <input type="text" readonly :value="documentInspection" id="checkSubmissions" name="checkSubmissions">
                                    </div>
                                     <div class="right">
                                        <label for="">处罚决定书：</label>
                                        <input type="text" readonly  :value="documentPunish" id="penaltyDecision" name="penaltyDecision">
                                    </div>
                                </div>
                            </div>
                            <!--照片取证显示 -->
                            <div class="onSitePhotography" v-if="photoPath.length>0">
                                <div class="topTitle">
                                    <img src="../../../assets/imgs/regulatory/pz_img01.png" alt>
                                    <span>现场拍照</span>
                                </div>
                                <div class="photos">
                                  <img @click="previewPicture(index)"  v-for="(arr,index) in photoPath" :key="index" :src="`http://47.98.179.238:5080${arr}`" alt="">  
                                </div>

                            </div>

                         </div>
                     </mt-tab-container-item>
                     <!-- 项目明细 -->
                     <mt-tab-container-item id="projectDetails">
                        <div class="projectDetailsBox">
                              <!--  消防安全管理 -->
                            <ul v-for="(parent,index) in lawList" :key="index">
                                <div class="liTitle">
                                    <img class="Incon" :src="lawIcon[index]" alt>
                                    <span class="liTitleTxt">{{parent.supervisionItemName}}</span>
                                </div>
                                <li v-for="(child,cindex) in parent.sonList" :key="cindex">
                                    <span class="leftText">{{child.supervisionItemName}}</span>
                                    <span class="statusText" v-if="child.isOK">合格</span>
                                    <span class="statusTextFalse" v-else>不合格</span>
                                    <img  @click="editStatus(parent,child,cindex,index)" v-if="child.remark.length>0" class="isTips" src="../../../assets/imgs/regulatory/zfrz_img_btn.png" alt="">
                                </li>
                                
                            </ul>
                            <div class="otherTips" v-if="remark.length>0">
                                <div class="liTitle">
                                    <img class="Incon06" src="../../../assets/imgs/regulatory/zfrz_img_06.png" alt>
                                    <span class="liTitleTxt">其他情况</span>
                                </div>
                                <textarea readonly :value="remark" name="otherSituations" id="otherSituations"></textarea>
                            </div>
                        </div>
                
                     </mt-tab-container-item>

                </mt-tab-container>
           </div>

            <!--  -->
            <mt-popup v-model="messageVisible" position="bottom" pop-transition="popup-fade">
                <div class="popupBox">
                    <div class="top">
                        <P>项目：{{messageText}}</P>
                        <img @click="closeModal" class="close" src="../../../assets/imgs/regulatory/close.png" alt>
                    </div>
                    <div class="popupContainer">
                        <div class="isTrue">
                            <span>是否合格：</span>
                          <span v-if="value == true">合格</span>
                          <span v-if="value == false">不合格</span>
                        </div>
                        <textarea class="showtextarea" readonly  v-model="lawyTextarea" placeholder="在这里输入存在的问题" name="isTrueTextarea" id="" cols="30" rows="10"></textarea>
                    </div>
                </div>
            </mt-popup>
           
    </div>
</template>
<script>
import topBack from '../../../components/topBack/index'
import icon02 from '../../../assets/imgs/regulatory/zfrz_img_02.png'
import icon03 from '../../../assets/imgs/regulatory/zfrz_img_03.png'
import icon04 from '../../../assets/imgs/regulatory/zfrz_img_04.png'
import icon05 from '../../../assets/imgs/regulatory/zfrz_img_05.png'
export default {
    components:{
        topBack
    },
    data(){
        return{
            id:'',
            selected:'comprehensive',
            selecte:'',
            showOptions:false,
            messageVisible: false,
            value:true,
            fireUnitName:'',//防火单位名称
            creationTime:'',//检查时间
            checkUser:'',//检查人员
            checkResult:'',//检查结果
            documentSite:'',
            documentDeadline:'',//限期
            documentMajor:'',//重大
            documentReview:'',//复查
            documentInspection:'',//检查意见书
            documentPunish:'',//处罚决定书
            messageText:'',//是否合格的备注信息
            lawyTextarea:'',
            remark:'',
            photoPath:'',//现场拍照取证
            options: [
                {
                value: '限期整改'
                },
                {
                value: '停业整顿'
                },
                {
                value: '正常'
                }
            ],

            lawList:[],//执法记录明细项目信息
            lawIcon:[icon02,icon03,icon04,icon05]
        }
    },
    methods:{
        choose(){

        },
        editStatus(parent,child,cindex,index){
            this.messageText = child.supervisionItemName; //把蒙层的top标题设置为相对应的名字
            this.value = child.isOK; //把蒙层的开关状态设置为该列条的是否合格状态
            this.lawyTextarea = child.remark; //把蒙层的备注
            this.messageVisible = true;
        },
        closeModal(){
             this.messageVisible = false;
        },
        turn: function(){
          console.log(this.value)
      },
      /* 展示图片 */
        /* 点击缩小图，展示图片 */
        previewPicture(index){
            let urls = [];
            for(let arr of this.photoPath){
               arr =`http://47.98.179.238:5080${arr}`;
               urls.push(arr);
            }
        console.log("this.photoPath",this.photoPath,urls)
       
        plus.nativeUI.previewImage(urls,{current:index});
        }
    },
    mounted() {
        let that =this;
        this.id = this.$route.query.id

        //初始化数据请求
        this.$http({
           method: "get",
           url: "/api/services/app/Supervision/GetSingleSupervisionMain",
           params:{
               supervisionId:that.id
           }
        }).then(res=>{
            console.log("获取单条记录主信息成功",res)
            if(res.status == 200){
                that.fireUnitName = res.data.result.fireUnitName;
                that.creationTime = res.data.result.creationTime;
                that.checkUser = res.data.result.checkUser;
                that.checkResult = res.data.result.checkResult;
                that.documentSite = res.data.result.documentSite;
                that.documentDeadline = res.data.result.documentDeadline;
                that.documentMajor = res.data.result.documentMajor;
                that.documentReview = res.data.result.documentReview;
                that.documentInspection = res.data.result.documentInspection;
                that.documentPunish = res.data.result.documentPunish;
                that.remark = res.data.result.remark;
                that.photoPath = res.data.result.photoPath;
            }

        }).catch(res=>{
            console.log("获取单条记录主信息失败",res)
        })

        //获取单条执法记录明细项目信息
        this.$http({
           method: "get",
           url: "/api/services/app/Supervision/GetSingleSupervisionDetail",
           params:{
               supervisionId:that.id
           } 
        }).then(res=>{
            console.log("获取单条执法记录明细项目信息成功",res)
            if(res.status == 200){
                that.lawList = res.data.result
            }
        }).catch(res=>{
            console.log("获取单条执法记录明细项目信息成功失败",res)
        })
    },

}
</script>