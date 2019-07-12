<style lang="less">

@import url("../../assets/css/border.css");
    .border-bottom{
        border-bottom: 1px solid #dcdcdc; /* no */
    }
    .mySetBox{
        width: 100%;
        box-sizing: border-box;
        padding-top: 80px;
    }
    .mySetList{
        list-style: none;
        li{
            height: 110px;
            padding: 0 26px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            .border-bottom();
            .left{
                display: flex;
                align-items: center;
                img{
                    margin-right: 24px;
                }
                .icon01{
                    width: 32px;
                    height: 34px;
                }
                .icon02{
                    width: 32px;
                    height: 36px;
                }
                .icon03{
                    width: 32px;
                    height: 34px;
                }
                span{
                    font-size: 30px;
                    color: #262626;
                    &.tip01{
                        font-size: 26px;
                       color: #b2b1b1;
                       margin-left: 44px;
                    }
                }

            }
            .nextBtn{
                width: 18px;
                height: 30px;
            }

        }


        
    }
    .pictrue{
      width:200px;
      height: 200px;
      border: 1px solid darkblue;
    }
    .mint-msgbox{
        height:300px;
        .mint-msgbox-header{
            .mint-msgbox-title{
                font-size:36px;
            }
            
        }
        .mint-msgbox-content{
            min-height:1.4rem;
            border:none;
            .mint-msgbox-message{
                font-size:34px;
                line-height: 1.4rem;
            }
        }
        .mint-msgbox-btns{
           border-top:1px solid #ddd;/* no */
           height:none;
           .mint-msgbox-btn{
               font-size:30px;
               height:110px;
           }
           .mint-msgbox-cancel{
                border-right: 1px solid #ddd !important; /* no */
           }
        }
    }
/* .test{
  width: 200px;
  height: 300px;

} */



</style>
<template>
  <div class="mySetBox">
    <topinfo></topinfo>
    <ul class="mySetList">
      <!-- 修改密码 -->
      <li class="updatePassword" @click="updatePassword">
        <div class="left">
          <img class="icon01" src="../../assets/imgs/mySet/sist_img_01.png" alt>
          <span>修改密码</span>
        </div>
        <img class="nextBtn" src="../../assets/imgs/mySet/next_btn.png" alt>
      </li>
      <!-- 技术支持 -->
      <li class="technical" @click="goTotechnical">
        <div class="left">
          <img class="icon02" src="../../assets/imgs/mySet/sist_img_02.png" alt>
          <span>技术支持</span>

          <span class="tip01">天树聚城市智慧消防</span>
        </div>
        <img class="nextBtn" src="../../assets/imgs/mySet/next_btn.png" alt>
      </li>
      <!-- 注销登录 -->
      <li class="logOut" @click="logOut">
        <div class="left">
          <img class="icon03" src="../../assets/imgs/mySet/sist_img_03.png" alt>
          <span>注销登录</span>
        </div>
        <img class="nextBtn" src="../../assets/imgs/mySet/next_btn.png" alt>
      </li>
    </ul>

<!--     <div class="test border-1px">

    </div> -->

    <tabBar></tabBar>
  </div>
</template>
<script>
import tabBar from "../../components/tabBar/tabBar";
import topinfo from "../../components/topinfo/topinfo";
import { MessageBox,Indicator,Toast } from "mint-ui";
import upload_img from '../../components/photo/index'
export default {
  components: {
    tabBar,
    topinfo,
    upload_img
  },
  data(){
    return{
      zipTo:1024,
      imgList:[]
    }
  },
    computed: {
        imgLists() {
            return this.imgList || []
        }
    },
  methods: {

     pickPic(rest){ /*选择照片成功后回调函数*/
          setTimeout(function(){
            Indicator.close();
          },200)
          let that = this;
          let fickedFile = rest.file; // 选中的文件对象
          rest && that.imgList.push(rest.path)
          console.log("上传图片逻辑",rest);
          //... 上传图片逻辑
        },
        deletePic(rest) {
          let that = this;
          let index = that.imgList.indexOf(rest);
          that.imgList.splice(rest, 1)
        },


    goTotechnical() {
      this.$router.push({
        path: "/mySet/technicalSupport"
      });
    },
    updatePassword() {
      this.$router.push({
        path: "/mySet/updatePassword"
      });
    },
    logOut() {
      MessageBox.confirm("", {
        title: "注销登录",
        message: "确定注销吗？",
        confirmButtonText: "确认",
        cancelButtonText: "取消"
      })
        .then(action => {
          if (action == "confirm") {
            this.$http({
              method: "post",
              url: "/api/services/app/FireDeptUser/UserLogout"
            })
              .then(res => {
                console.log("注销成功", res);
                localStorage.clear();
                that.$router.push({
                  path: "/login"
                });
              })
              .catch(res => {
                console.log("注销失败", res);
              });
          }
        })
        .catch(error => {
          if (error == "cancel") {
            console.log("点击取消");
          }
        });

      let that = this;
    }
  }
};
</script>