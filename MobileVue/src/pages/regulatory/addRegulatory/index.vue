<style lang="less">
.display {
  display: flex;
  align-items: center;
}
.addRegulatoryBox {
  padding-top: 80px;
  box-sizing: border-box;
  width: 100%;
  .topNav {
    border-bottom: solid 1px #dcdcdc;  /*no */
    .mint-navbar .mint-tab-item.is-selected {
      width: 160px;
      height: 54px;
      background-color: #039cfe;
      border-radius: 8px;
      color: white;
      margin: 0;
      line-height: 54px;
      font-size: 30px !important;
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
      }
    }
  }
  /* 主要条目 */
  .comprehensiveBox {
    padding: 34px;
    box-sizing: border-box;
    position: relative;
    .topFill {
      .checkTime {
        margin: 50px 0px;
      }
      .checkResult {
        margin-top: 50px;
      }
      .checkResult {
        display: flex;
        align-items: center;
        .selectResult {
          .inputWrapper {
            border-radius: 8px;
            border: solid 1px #dcdcdc; /* no */
            width: 166px;
            height: 50px;
            display: flex;
            align-items: center;
            padding: 0px 14px;
            input {
              border: none;
              width: 100%;
              line-height: 50px;
              font-size: 26px;
              color: #989898;
              &::placeholder {
                color: #989898;
              }
            }
            .icon {
              width: 17px;
              height: 12px;
            }
          }
          ul {
            margin-top: 10px;
            list-style: none;
            width: 166px;
            padding: 14px 14px;
            background: white;
            border: 1px solid #dcdcdc; /* no */
            border-radius: 8px;
            position: absolute;
            li {
              height: 70px;
              font-size: 26px;
              color: #262626;
              border-bottom: 1px solid #dcdcdc;
              line-height: 70px;
            }
          }
        }
      }
      label {
        width: 112px;
        color: #262626;
        font-size: 26px;
        margin-right: 30px;
      }
      span {
        font-size: 26px;
      }
      input {
        border: none;
        border-bottom: 1px solid #dcdcdc; /* no */
        max-width: 500px;
        &.fireUnit {
          white-space: nowrap;
          overflow: hidden;
          text-overflow: ellipsis;
          font-size: 26px;
        }
        &.checkUser {
          font-size: 26px;
        }
      }
    }
    .issueLegal {
      margin-top: 66px;
      div {
        display: flex;
        align-items: center;
        justify-content: space-between;
        font-size: 24px;
        color: #989898;
        &.middle {
          margin: 50px 0px;
        }
      }
      input {
        width: 140px;
        border: none;
        border-bottom: 1px solid #dcdcdc; /* no */
      }
      label {
        width: 160px;
        text-align: right;
      }
      .topTitle {
        font-size: 26px;
        color: #262626;
        justify-content: flex-start;
        margin-bottom: 50px;
        img {
          width: 34px;
          height: 30px;
          margin-right: 14px;
        }
      }
    }
      /* 现场拍照 */
    .onSitePhotography {
      width: 100%;
      margin-top: 68px;
      margin-bottom: 80px;
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
      .photos {
        width: 100%;
        display: flex;
        margin-top: 26px;
      
        .photoList{
          list-style: none;
          display: flex;
          justify-content:flex-start;
          flex-flow:row wrap;
          align-content: flex-start;
          width: 100%;
          img {
            width: 30%;
            height: 130px;
            margin-right: 10px;
          }
          li{
            margin-right: 10px;
            margin-bottom: 20px;
            flex:  0 0 0 30%;
          
            width: 30%;
          }
            .did{
              position: relative;
              width: 100%;
              height: 130px;
            
              img{
                
                height: 130px;
                width: 100%;
              }
              .close{
                position: absolute;
                width: 30px;
                height:30px;
                top: 0px;
                right: 0px;
              }

            }
          
          }
        }
      .photoss {
        background: transparent;
        border: none;
      }
    }
    /* 提交按钮 */
    .submit {
      width: 96%;
      left: 50%;
      transform: translateX(-50%);
      height: 76px;
      background: url("../../../assets/imgs/regulatory/btn_bg.png") repeat-x;
      border-radius: 10px;
      font-size: 30px;
      color: #fff;
      line-height: 76px;
      text-align: center;
      position: absolute;
      bottom: 20px;
      z-index: 2000;
    }
  }
  /*各项明细  */
  .projectDetailsBox {
    padding: 0 26px;
    padding-bottom: 40px;
    width: 100%;
    box-sizing: border-box;
    position: relative;
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
      &:nth-child(1) {
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
      .statusTextFalse {
        position: absolute;
        right: 0;
        bottom: -0.5em;
        background: #fff;
        padding-left: 10px;
        color: #ff8a00;
      }
      .isTips {
        position: absolute;
        right: -50px;
        width: 30px;
        height: 32px;
        top: 34px;
      }
    }
    ul:nth-of-type(1) .Incon {
      width: 34px;
      height: 26px;
    }
    ul:nth-of-type(2) .Incon {
      width: 30px;
      height: 32px;
    }
    ul:nth-of-type(3) .Incon {
      width: 24px;
      height: 34px;
    }
    ul:nth-of-type(4) .Incon {
      width: 36px;
      height: 36px;
    }
    /* 其他情况 */
    .itemDetailsList05 {
      width: 100%;
      padding-top: 30px;
      .Incon06 {
        width: 32px;
        height: 32px;
      }
      textarea {
        margin-top: 24px;
        width: 100%;
        height: 140px;
        border-radius: 8px;
        border: solid 1px #dcdcdc; /* no */
        outline: none;
        margin-bottom: 80px;
      }
    }

    /* 提交按钮 */
    .submit {
      width: 96%;
      left: 50%;
      transform: translateX(-50%);
      height: 76px;
      background: url("../../../assets/imgs/regulatory/btn_bg.png") repeat-x;
      border-radius: 10px;
      font-size: 30px;
      color: #fff;
      line-height: 76px;
      text-align: center;
      position: fixed;
      bottom: 20px;
      z-index: 2000;
    }
  }

}

  .bg {
    width: 100%;
    position: fixed;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    background: rgba(0, 0, 0, 0.3);
  }
  .popup {
    width: 750px;
    height: 40%;
    padding-bottom: 20px;
    background: #fff;
    position: fixed;
    bottom: 0px;
    left: 0px;
    z-index: 2002;
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
      .popupContainer {
        .isTrue {
          display: flex;
          height: 106px;
          align-items: center;
          font-size: 26px;
          color: #262626;
          padding-left: 27px;
          .mint-switch-core::before {
            background: #989898;
          }
        }
        textarea {
          width: 90%;
          border: 1px solid #dcdcdc; /* no */
          outline: none;
          position: fixed;
          left: 50%;
          transform: translateX(-50%);
          height: 210px;
          margin-bottom: 20px;
        }
        .sendBtn {
          width: 90%;
          left: 50%;
          transform: translateX(-50%);
          height: 76px;
          background: url("../../../assets/imgs/regulatory/btn_bg.png") repeat-x;
          line-height: 76px;
          text-align: center;
          color: white;
          font-size: 32px;
          border-radius: 8px;
          position: fixed;
          bottom: 20px;
        }
      }
    }
  }
.mint-msgbox {
  height: 300px;
  .mint-msgbox-header {
    .mint-msgbox-title {
      font-size: 36px;
    }
  }
  .mint-msgbox-content {
    min-height: 1.4rem;
    border: none;
    .mint-msgbox-message {
      font-size: 34px;
      line-height: 1.4rem;
    }
  }
  .mint-msgbox-btns {
    border-top: 1px solid #ddd; /* no */
    height: none;
    .mint-msgbox-btn {
      font-size: 30px;
      height: 110px;
    }
    .mint-msgbox-cancel {
      border-right: 1px solid #dcdcdc; /* no */
    }
  }
}

.mint-toast-text{
  font-size: 28px;
}

</style>
<template>
  <div class="addRegulatoryBox">
    <topBack :title="$route.name" mark="Toregulatory"></topBack>
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
                <label for>防火单位:</label>
                <input class="fireUnit" @focus="goToSearch" :value="fireUnitName" type="text" name="fireUnit"
                  id="fireUnit"
                >
              </div>
              <div class="checkTime">
                <label for>检查时间:</label>
                <span>{{nowTime}}</span>
              </div>
              <div class="checkPersons">
                <label for>检查人员:</label>

                <input
                  type="text"
                  class="checkUser"
                  v-model="checkUser"
                  name="checkPerson"
                  id="checkPerson"
                >
              </div>
              <div class="checkResult">
                <label for>检查结论:</label>
                <div class="selectResult">
                  <div class="inputWrapper" @click="showOptions = !showOptions">
                    <input type="text" readonly :value="selecte" placeholder="全部记录">
                    <img
                      class="icon"
                      src="../../../assets/imgs/regulatory/jgjc_btn_dropDown.png"
                      alt
                    >
                  </div>
                  <ul class="options" v-show="showOptions">
                    <li
                      @click="choose(item.value,item.id)"
                      v-for="(item, index) in options"
                      :key="index"
                    >{{item.value}}</li>
                  </ul>
                </div>
              </div>
            </div>
            <!-- 下发法律文书 -->
            <div class="issueLegal">
              <div class="topTitle">
                <img src="../../../assets/imgs/regulatory/zfrz_img_01.png" alt>
                <span>下发法律文书</span>
              </div>
              <div class>
                <div class="left">
                  <label for>当场：</label>
                  <input type="number" v-model="documentSite" id="onTheSpot" name="onTheSpot">
                </div>
                <div class="right">
                  <label for>期限：</label>
                  <input type="number" v-model="documentDeadline" id="timeLimit" name="timeLimit">
                </div>
              </div>
              <div class="middle">
                <div class="left">
                  <label for>重大：</label>
                  <input type="number" v-model="documentMajor" id="important" name="important">
                </div>
                <div class="right">
                  <label for>复查：</label>
                  <input type="number" v-model="documentReview" id="review" name="review">
                </div>
              </div>
              <div>
                <div class="left">
                  <label for>检查意见书：</label>
                  <input
                    type="number"
                    v-model="documentInspection"
                    id="checkSubmissions"
                    name="checkSubmissions"
                  >
                </div>
                <div class="right">
                  <label for>处罚决定书：</label>
                  <input
                    type="number"
                    v-model="documentPunish"
                    id="penaltyDecision"
                    name="penaltyDecision"
                  >
                </div>
              </div>
            </div>

            <!-- 现场拍照 -->
            <div class="onSitePhotography">
              <div class="topTitle">
                <img src="../../../assets/imgs/regulatory/pz_img01.png" alt>
                <span>现场拍照</span>
              </div>
              <div class="photos">
                <ul class="photoList">
                   <img @click="showActionSheet" id="ww" src="../../../assets/imgs/regulatory/pz_btn_click.png" alt>
                  <li v-for="(arr,index) in Imgs" :key="index">
                     <div class="did">
                      <img class="close" @click.stop="closePic(index)" src="../../../assets/imgs/regulatory/pz_btn_delete.png" alt="">
                      <img @click="previewPicture(index)"  id="ww" :src="arr" alt >
                    </div>
                  </li>
                </ul>
              </div> 
            </div>
             <!-- 提交按钮 -->
            <div @click="submit" class="submit">提交</div>
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
              <li @click="editStatus(parent,child,cindex,index)"  v-for="(child,cindex) in parent.sonList" :key="cindex" >
                <span class="leftText">{{child.supervisionItemName}}</span>
                <span class="statusText" v-if="child.isOK">合格</span>
                <span class="statusTextFalse" v-else>不合格</span>
                <img v-if="child.remark.length>0" class="isTips" src="../../../assets/imgs/regulatory/zfrz_img_btn.png" alt>
              </li>
            </ul>

            <!-- 其他情况 -->
            <div class="itemDetailsList05">
              <div class="liTitle">
                <img class="Incon06" src="../../../assets/imgs/regulatory/zfrz_img_06.png" alt>
                <span class="liTitleTxt">其他情况</span>
              </div>
              <textarea v-model="remarkOther" name="otherSituations" id="otherSituations"></textarea>
            </div>
             <!-- 提交按钮 -->
            <div @click="submit" class="submit">提交</div>
          </div>

           
        </mt-tab-container-item>
      </mt-tab-container>
    </div>
  
    <!--  -->
    <div class="bg" @click="closeModal"  @touchmove.prevent v-if="messageVisible" ref="bg" style="{ height: bgHeight + 'px' }"></div>
    <!-- 备注蒙层 -->
    <div class="popup" v-if="messageVisible">
      <div class="popupBox">
        <div class="top">
          <P>项目：{{messageText}}</P>
          <img  @click="closeModal"  class="close" src="../../../assets/imgs/regulatory/close.png" alt>
        </div>
        <div class="popupContainer">
          <div class="isTrue">
            <span>是否合格：</span>
            <mt-switch @change="turn" v-model="value"></mt-switch>
          </div>
          <textarea v-model="lawyTextarea" placeholder="在这里输入存在的问题" name="isTrueTextarea" id cols="30" rows="10"></textarea>
          <div @click="submitisTrue" class="sendBtn">确定</div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
let parentIndex, childIndex;
import topBack from "../../../components/topBack/index";
import icon02 from "../../../assets/imgs/regulatory/zfrz_img_01.png";
import icon03 from "../../../assets/imgs/regulatory/zfrz_img_03.png";
import icon04 from "../../../assets/imgs/regulatory/zfrz_img_04.png";
import icon05 from "../../../assets/imgs/regulatory/zfrz_img_05.png";
import { Toast, MessageBox,Indicator } from "mint-ui";
export default {
  components: {
    topBack
  },
  data() {
    return {
      bgHeight: 0,
      selected: "comprehensive",
      selecte: "",
      showOptions: false,
      messageVisible: false,
      value: true,
      messageText: "", //是否合格的备注信息
      lawyTextarea: "",
      nowTime: "", //当前时间
      fireUnitId: "", //防火单位id
      fireUnitName: "", //防火单位名称
      checkUser: "", //检查人员
      checkResult: -10, //检查结果
      documentSite: 0, //法律文书-当场
      documentDeadline: 0, //法律文书-限期
      documentMajor: 0, //法律文书-重大
      documentReview: 0, //法律文书-复查
      documentInspection: 0, //法律文书-检查意见书
      documentPunish: 0, //法律文书-处罚决定书
      remarkOther: "", //其他情况
      Imgs:[],//拍照取证的照片集合
      MaxPic:9,//照片取证的最大数
      code64Img:[],//照片取证的最大数
      options: [
        {
          value: "限期整改",
          id: -2
        },
        {
          value: "现场改正",
          id: -1
        },
        {
          value: "停业整顿",
          id: -3
        },
        {
          value: "合格",
          id: 1
        }
      ],
      lawList: [
        {
          supervisionItemName: "消防安全管理",
          parentId: 0,
          parentName: "",
          remark: "",
          sonList: [
            {
              supervisionItemName: "消防安全制度、消防安全操作规程",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 2,
              isOK: true
            },
            {
              supervisionItemName: "确定单位、部门、岗位消防安全责任人",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 3,
              isOK: true
            },
            {
              supervisionItemName: "对员工经常的消防安全教育",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 4,
              isOK: true
            },
            {
              supervisionItemName: "定期防火检查",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 5,
              isOK: true
            },
            {
              supervisionItemName: "整改火灾隐患",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 6,
              isOK: true
            },
            {
              supervisionItemName: "消防设施定期检测、维修制度",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 7,
              isOK: true
            },
            {
              supervisionItemName: "疏散通道、安全出口管理",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 8,
              isOK: true
            },
            {
              supervisionItemName: "单位建立防火档案",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 9,
              isOK: true
            },
            {
              supervisionItemName: "每日防火巡查",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 10,
              isOK: true
            },
            {
              supervisionItemName: "职工定期消防培训",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 11,
              isOK: true
            },
            {
              supervisionItemName: "重点工种人员持证上岗",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 12,
              isOK: true
            },
            {
              supervisionItemName: "灭火疏散预案",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 13,
              isOK: true
            },
            {
              supervisionItemName: "消防中心值班",
              parentId: 1,
              parentName: "消防安全管理",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 14,
              isOK: true
            }
          ]
        },
        {
          supervisionItemName: "建筑防火",
          parentId: 0,
          parentName: "",
          remark: "",
          sonList: [
            {
              supervisionItemName: "防火间距",
              parentId: 15,
              parentName: "建筑防火",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 16,
              isOK: true
            },
            {
              supervisionItemName: "消防车通道",
              parentId: 15,
              parentName: "建筑防火",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 17,
              isOK: true
            },
            {
              supervisionItemName: "建筑耐火等级与用途",
              parentId: 15,
              parentName: "建筑防火",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 18,
              isOK: true
            },
            {
              supervisionItemName: "安全出口",
              parentId: 15,
              parentName: "建筑防火",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 19,
              isOK: true
            },
            {
              supervisionItemName: "楼梯",
              parentId: 15,
              parentName: "建筑防火",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 20,
              isOK: true
            },
            {
              supervisionItemName: "防火门",
              parentId: 15,
              parentName: "建筑防火",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 21,
              isOK: true
            },
            {
              supervisionItemName: "防火卷帘",
              parentId: 15,
              parentName: "建筑防火",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 22,
              isOK: true
            },
            {
              supervisionItemName: "自然排烟窗口",
              parentId: 15,
              parentName: "建筑防火",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 23,
              isOK: true
            }
          ]
        },
        {
          supervisionItemName: "消防设施",
          parentId: 0,
          parentName: "",
          remark: "",
          sonList: [
            {
              supervisionItemName: "消防水箱",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 25,
              isOK: true
            },
            {
              supervisionItemName: "消防电话插孔",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 26,
              isOK: true
            },
            {
              supervisionItemName: "应急广播",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 27,
              isOK: true
            },
            {
              supervisionItemName: "应急照明",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 28,
              isOK: true
            },
            {
              supervisionItemName: "疏散指示标志",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 29,
              isOK: true
            },
            {
              supervisionItemName: "消防电梯",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 30,
              isOK: true
            },
            {
              supervisionItemName: "自备发电机",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 31,
              isOK: true
            },
            {
              supervisionItemName: "消防电源",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 32,
              isOK: true
            },
            {
              supervisionItemName: "灭火器",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 33,
              isOK: true
            },
            {
              supervisionItemName: "防排烟系统",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 34,
              isOK: true
            },
            {
              supervisionItemName: "火灾自动报警系统",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 35,
              isOK: true
            },
            {
              supervisionItemName: "泡沫灭火系统",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 36,
              isOK: true
            },
            {
              supervisionItemName: "自动喷水及水雾灭火系统",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 37,
              isOK: true
            },
            {
              supervisionItemName: "水泵接合器",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 38,
              isOK: true
            },
            {
              supervisionItemName: "消防水泵",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 39,
              isOK: true
            },
            {
              supervisionItemName: "消防卷盘",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 40,
              isOK: true
            },
            {
              supervisionItemName: "室内消火栓",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 41,
              isOK: true
            },
            {
              supervisionItemName: "室外消火栓",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 42,
              isOK: true
            },
            {
              supervisionItemName: "消防水池",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 43,
              isOK: true
            },
            {
              supervisionItemName: "气体灭火系统",
              parentId: 24,
              parentName: "消防设施",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 44,
              isOK: true
            }
          ]
        },
        {
          supervisionItemName: "火灾爆炸危险场所",
          parentId: 0,
          parentName: "",
          remark: "",
          sonList: [
            {
              supervisionItemName: "防爆泄压面积",
              parentId: 45,
              parentName: "火灾爆炸危险场所",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 46,
              isOK: true
            },
            {
              supervisionItemName: "不发火花地面",
              parentId: 45,
              parentName: "火灾爆炸危险场所",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 47,
              isOK: true
            },
            {
              supervisionItemName: "防止液体流散设施",
              parentId: 45,
              parentName: "火灾爆炸危险场所",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 48,
              isOK: true
            },
            {
              supervisionItemName: "通风窗、洞口",
              parentId: 45,
              parentName: "火灾爆炸危险场所",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 49,
              isOK: true
            },
            {
              supervisionItemName: "防静电",
              parentId: 45,
              parentName: "火灾爆炸危险场所",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 50,
              isOK: true
            },
            {
              supervisionItemName: "防雷",
              parentId: 45,
              parentName: "火灾爆炸危险场所",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 51,
              isOK: true
            },
            {
              supervisionItemName: "防爆电气",
              parentId: 45,
              parentName: "火灾爆炸危险场所",
              remark: "",
              sonList: null,
              supervisionId: 0,
              supervisionItemId: 52,
              isOK: true
            }
          ]
        }
      ],
      lawIcon: [icon02, icon03, icon04, icon05]
    };
  },
  methods: {
    /* 弹出口选择上传图片的格式 */
    showActionSheet() {
      let that = this;
      let bts = [
        {
          title: "拍照"
        },
        {
          title: "从相册选择"
        }
      ];
      plus.nativeUI.actionSheet(
        {
          cancel: "取消",
          buttons: bts
        },
        function(e) {
          if (e.index == 1) {
            that.getImage();
          } else if (e.index == 2) {
            that.galleryImgs();
          }
        }
      );
    },
    //从相册选择照片
    galleryImgs() {
      let that = this;
      plus.gallery.pick(
        function(e) {
           if (that.Imgs.length < that.MaxPic) {
             for(var i in e.files){
              var path = e.files[i];
              console.log("从相册选择照片3", e.files);
                Indicator.open();
              that.uploadImg(path);//转码图片
              that.Imgs.push(path)
            }  
           

           }else{
            console.log('超过已有的长度')
           }

        },
        function(e) {},
        {
          filter: "image",
          multiple: true // 设置了此项,选择成功返回的是一个包含键名为files的路径数组的对象,否则为单一路径;
        }
      );
    },
    /* 点击获取相机 */
    getImage() {
      let that = this;
      const cmr = window.plus.camera.getCamera();
      var newd ;
      cmr.captureImage( p => {
          window.plus.io.resolveLocalFileSystemURL(p,entry => {

            console.log("ppc",p,entry)
            var imgSrc = entry.toLocalURL();
            Indicator.open();
            that.uploadImg(imgSrc);//转码图片
            that.Imgs.push(imgSrc);
            console.log('Imgs', that.Imgs)

            },e => {
              window.plus.navtiveUI.toast("读取拍照文件错误：" + e.message);
            }
          );
        },e => {
          console.log("打印相机反应",e);
        }, function(error) {
            if(error.code == 11){
              Toast({message: '用户取消'})
            }
          },
        { filter: "image" 
          }
      );
    },
  /* 点击缩小图，展示图片 */
    previewPicture(index){
      var urls = this.Imgs;
       plus.nativeUI.previewImage(urls,{current:index});
    },
    /* 删除当前图片 */
    closePic(index){
      this.Imgs.splice(index, 1)
    },
    /* 图片转base64 */
    getBase64Image(img) {
        var canvas = document.createElement("canvas");
        var width ,height;
        if(img.width>2000 || img.height>2000){
            width = img.width/4;
            height = img.height/4;
        }else{
            width = img.width;
            height = img.height;
        }
        canvas.width = width; /*设置新的图片的宽度*/
        canvas.height = height; /*设置新的图片的长度*/
        var ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0, width, height); /*绘图*/
        var dataURL = canvas.toDataURL("image/png/jpg", 0.8);

        return dataURL.replace("data:image/png/jpg;base64,", "");
			
    },
    uploadImg(imgPath){
        console.log("imgPath = " + imgPath);
        let that = this;
        var image = new Image();
        image.onload = function() {
         var baseImg = that.getBase64Image(image);
         that.code64Img.push(baseImg);
         Indicator.close();
          console.log("image对象",image);
        }
        image.src = imgPath;
    },
    /*  */
    //选择检查结果
    choose(value, id) {
      this.showOptions = false;
      this.selecte = value;
      this.checkResult = id;
    },
    editStatus(parent, child, cindex, index) {
      this.messageText = child.supervisionItemName; //把蒙层的top标题设置为相对应的名字
      parentIndex = index;
      childIndex = cindex;
      this.value = child.isOK; //把蒙层的开关状态设置为该列条的是否合格状态
      this.lawyTextarea = child.remark; //把蒙层的备注
      this.messageVisible = true;
    },
    closeModal() {
      this.messageVisible = false;
    },
    turn: function() {
      console.log(this.value);
    },
    /* 提交修改单条项目明细 */
    submitisTrue() {
      let that = this;
      this.lawList[parentIndex].sonList[childIndex].isOK = this.value;
      this.lawList[parentIndex].sonList[childIndex].remark = this.lawyTextarea;
      this.messageVisible = false;
    },
    //获取当前时间
    // 获取当前时间函数
    timeFormate() {
      let year = new Date().getFullYear();
      let month = new Date().getMonth() + 1 < 10 ? "0" + (new Date().getMonth() + 1) : new Date().getMonth() + 1;
      let date = new Date().getDate() < 10 ? "0" + new Date().getDate() : new Date().getDate();
      this.nowTime = year + "年" + month + "月" + date + "日";
    },
    //搜索防火单位名称
    goToSearch() {
      this.$router.push({
        path: "/search",
        query: {
          mark: "监管检查日志"
        }
      });
    },
    /* 提交新增结果 */
    submit() {
      if (this.fireUnitId && this.checkResult !== -10) {
        MessageBox.confirm("", {
          title: "提交记录",
          message: "确定提交吗？",
          confirmButtonText: "确认",
          cancelButtonText: "取消"
        })
          .then(action => {
            console.log(23);
            if (action == "confirm") {
              console.log("点击确认");
              let supervision = {
                fireUnitId: this.fireUnitId,
                checkUser: this.checkUser,
                checkResult: this.checkResult,
                fireDeptUserId: localStorage.getItem("userId"),
                remark: this.remarkOther,
                documentSite: parseInt(this.documentSite),
                documentDeadline: parseInt(this.documentDeadline),
                documentMajor: parseInt(this.documentMajor),
                documentReview: parseInt(this.documentReview),
                documentInspection: parseInt(this.documentInspection),
                documentPunish: parseInt(this.documentPunish)
              };
              let supervisionDetailInputs = [];
              for (let i = 0; i < this.lawList.length; i++) {
                for (let j = 0; j < this.lawList[i].sonList.length; j++) {
                  if (
                    this.lawList[i].sonList[j].remark.length > 0 ||
                    this.lawList[i].sonList[j].isOK == false
                  ) {
                    supervisionDetailInputs.push(this.lawList[i].sonList[j]);
                  }
                }
              }
              let code64 = this.code64Img;
              this.$http({
                method: "post",
                url: "/api/services/app/Supervision/AddSupervision",
                data: {
                  supervision: supervision,
                  supervisionDetailInputs,
                  code64
                }
              })
                .then(res => {
                  console.log("添加数据成功", res);
                  if (res.data.success) {
                    this.$router.push({
                      path: "/regulatory"
                    });
                  }
                })
                .catch(res => {
                  console.log("添加数据失败", res);
                });
            }
          })
          .catch(error => {
            if (error == "cancel") {
              console.log("点击取消");
            }
          });
      } else if(!this.fireUnitId){
        Toast({
          message: "请选择正确的防火单位",
          position: "center",
          duration: 30000
        });
      }else if(this.checkResult == -10){
         Toast({
          message: "请选择检查结论",
          position: "center",
          duration: 3000
        });
      }
     
    }
  },
  mounted() {
    this.timeFormate();
    this.bgHeight = window.screen.height;

    //接收从搜索页面传来的防火单位的id和防火单位的名称
    this.fireUnitId = this.$route.query.fireunitId;
    this.fireUnitName = this.$route.query.fireUnitName;
  }
};
</script>