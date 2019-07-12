<template>
  <div class="frieUnitsBox">
    <tabBack :title="$route.name" mark="TodataMonitor"></tabBack>
    <div class="searchBox" @click="goToSearch($route.name)">
      <div class="search  border-1px">
        <input type="text" disabled placeholder="输入名称进行模糊查询">
        <img src="../../../assets/imgs/fireUnits//search_btn.png" alt>
      </div>
    </div>
    <!--主要内容-->
    <div class="page-loadmore" ref="pages">
      <div class="page-loadmore-wrapper" ref="wrapper" :style="{ height: wrapperHeight + 'px' }">
        <mt-loadmore
          :bottom-method="loadBottom"
          @bottom-status-change="handleBottomChange"
          :bottom-all-loaded="allLoaded"
          ref="loadmore"
          :autoFill="autoFill"
        >
          <ul class="ListBox page-loadmore-list">
            <li
              class="listContainer page-loadmore-listitem"
              @click="goToFrieUnintInfo(index)"
              v-for="(n,index) in fireUnits"
              :key="n.id"
            >
              <div class="leftBox">
                <img
                  @click.stop="setUp(n.id,n.isAttention,index)"
                  class="signIcon"
                  :src="n.isAttention?star:nostar"
                  alt
                >
                <div class="middleInfo">
                  <p class="unit">{{n.name}}</p>
                  <p class="unitAddress">{{n.address}}</p>
                </div>
              </div>

              <img class="nextIcon" src="../../../assets/imgs/fireUnits/next_btn.png" alt>
            </li>
          </ul>
          <div slot="bottom" class="mint-loadmore-bottom">
            <span
              v-show="bottomStatus !== 'loading'"
              :class="{ 'is-rotate': bottomStatus === 'drop' }"
            >↑</span>
            <span v-show="bottomStatus === 'loading'">
              <mt-spinner type="snake" color="#0390fe"></mt-spinner>
            </span>
          </div>
        </mt-loadmore>
      </div>
    </div>

    <div class="toTop" @click="backTop">
      <img src="../../../assets/imgs/fireUnits/returnTop_btn.png" alt>
    </div>
  </div>
</template>
 
<script>
import tabBack from "../../../components/topBack/index";
import nostar from "../../../assets/imgs/fireUnits/topping_btn_02.png";
import star from "../../../assets/imgs/fireUnits/topping_btn_01.png";
import { Indicator } from "mint-ui";
import { Toast } from "mint-ui";
export default {
  components: {
    tabBack
  },
  data() {
    return {
      allLoaded: false,
      autoFill: false,
      bottomStatus: "",
      wrapperHeight: 0,
      fireUnits: [],
      fireUnitsUp: [],
      upstar: [false],
      nostar,
      star,
      count: 0,
      SkipCount: 0,
      MaxResultCount: 8,
      arr: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
    };
  },
  methods: {
    //跳转到搜索页面
    goToSearch(path) {
      console.log("打印参数", path);
      this.$router.push({
        path: "/search",
        query: {
          mark: path
        }
      });
    },
    //设置置顶
    setUp(n, isAttention, index) {
      let userId = localStorage.getItem("userId");
      let that = this;
      console.log("this.fireUnits[index]", index);
      if (this.fireUnits[index].isAttention) {
        this.$http({
          method: "post",
          url: "/api/services/app/FireUnit/AttentionFireUnitCancel",
          data: {
            UserId: userId,
            fireUnitId: n
          }
        })
          .then(res => {
            console.log("取消置顶成功", res);
            if (res.data.result.success) {
              //取消置顶成功功后再次调用获取所有数据接口
              that
                .$http({
                  method: "get",
                  url: "/api/services/app/FireUnit/GetFireUnitListForMobile",
                  params: {
                    SkipCount: 0,
                    MaxResultCount: that.MaxResultCount,
                    UserId: userId
                  }
                })
                .then(res => {
                  console.log("设置置顶成功后的数据", res);
                  if (res.status == 200) {
                    that.fireUnits = res.data.result.items;
                  }
                })
                .catch(res => {
                  console.log("设置置顶成功后的数据失败", res);
                });
            }
          })
          .catch(res => {
            console.log("取消置顶失败", res);
          });
      } else {
        this.$http({
          method: "post",
          url: "/api/services/app/FireUnit/AttentionFireUnit",
          data: {
            UserId: userId,
            fireUnitId: n
          }
        })
          .then(res => {
            console.log("设置置顶成功", res);
            if (res.data.result.success) {
              //设置成功后再次调用获取所有数据接口
              that
                .$http({
                  method: "get",
                  url: "/api/services/app/FireUnit/GetFireUnitListForMobile",
                  params: {
                    SkipCount: 0,
                    MaxResultCount: that.MaxResultCount,
                    UserId: userId
                  }
                })
                .then(res => {
                  console.log("设置置顶成功后的数据", res);
                  if (res.status == 200) {
                    that.fireUnits = res.data.result.items;
                  }
                })
                .catch(res => {
                  console.log("设置置顶成功后的数据失败", res);
                });
            }
          })
          .catch(res => {
            console.log("设置置顶失败", res);
          });
      }
    },
    //页面跳转
    goToFrieUnintInfo(n) {
      let that = this;
      this.$router.push({
        path: "/dataMonitor/fireUnits/fireUnitinfos",
        query: {
          id: that.fireUnits[n].id,
          name: that.fireUnits[n].name
        }
      });
    },

    handleBottomChange(status) {
      this.bottomStatus = status;
    },

    loadBottom() {
      console.log("触发滚动事件");
      let that = this;
      let userId = localStorage.getItem("userId");
      that.count++;
      this.$http({
        method: "get",
        url: "/api/services/app/FireUnit/GetFireUnitListForMobile",
        params: {
          SkipCount: that.count * that.MaxResultCount,
          MaxResultCount: that.MaxResultCount,
          UserId: userId
        }
      })
        .then(function(res) {
          console.log("成功", that.count, res);
          that.handleBottomChange(" "); //上拉时 改变状态码
          if (res.status == 200) {
            if (res.data.result.items.length < 8) {
              this.allLoaded = true;
            }
            for (let i = 0; i < res.data.result.items.length; i++) {
              that.fireUnits.push(res.data.result.items[i]);
            }

            that.$refs.loadmore.onBottomLoaded();
          }
        })
        .catch(function(res) {});
    },
    backTop() {
      console.log("触发回到顶部");
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
    }
  },
  mounted() {
    //document.documentElement.clientHeight 可见区域的高度
    if (660 <= window.screen.height && window.screen.height <= 760) {
      this.MaxResultCount = 8;
    } else if (760 <= window.screen.height && window.screen.height <= 860) {
      this.MaxResultCount = 10;
    }
    this.wrapperHeight = window.screen.height - this.$refs.wrapper.getBoundingClientRect().top -20;
    //数据初始化请求
    Indicator.open({
      text: "加载中...",
      spinnerType: "fading-circle"
    });

    //页面加载完毕就请求数据
    let that = this;
    let userId = localStorage.getItem("userId");
    this.$http({
      method: "get",
      url: "/api/services/app/FireUnit/GetFireUnitListForMobile",
      params: {
        SkipCount: that.SkipCount,
        MaxResultCount: that.MaxResultCount,
        UserId: userId
      }
    })
      .then(function(res) {
        console.log("成功", res);
        if (res.status === 200) {
          that.fireUnits = res.data.result.items;
          Indicator.close();
        }
      })
      .catch(function(res) {
        console.log("错误",res)
        if (res.status !== 200) {
          Toast({
            message: "网络连接超时",
            position: "center",
            duration: 3000
          });

          setTimeout(() => {
            Indicator.close();
          }, 3000);
        }
      });
  }
};
</script>
<style lang='less'>
@bgColor: #fff;
.bordered {
  border-bottom: solid 1px #dcdcdc; /* no */
}
.frieUnitsBox {
  position: relative;
  padding-top: 184px;
  .searchBox {
    width: 100%;
    padding: 20px;
    box-sizing: border-box;
    position: fixed;
    top: 80px;
    background: white;
   border-bottom: 1px solid #dcdcdc;/* no */
    .search {
      width: 680px;
      height: 58px;
      border: 1px solid #dcdcdc; /* no */
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

  .page-loadmore {
    box-sizing: border-box;
    touch-action: none;
    .page-loadmore-wrapper {
      overflow: scroll;
    }
    .ListBox {
      width: 100%;
      .listContainer {
        padding: 20px 26px;
        box-sizing: border-box;
        .bordered();
        display: flex;
        align-items: center;
        justify-content: space-between;
        .leftBox {
          display: flex;
          align-items: center;
          width: 94%;
          .signIcon {
            width: 38px;
            height: 38px;
            margin-right: 26px;
          }
          .middleInfo {
            width: 90%;
            p {
              overflow: hidden;
              text-overflow: ellipsis;
              white-space: nowrap;
            }
            .unit {
              font-size: 30px;
              color: #262626;
              margin-bottom: 20px;
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
    }

    .mint-loadmore-bottom {
      span {
        display: inline-block;
        transition: 0.2s linear;
        vertical-align: middle;
      }
      .mint-spinner {
        display: inline-block;
        vertical-align: middle;
      }
    }
  }
  .toTop {
    position: fixed;
    width: 78px;
    height: 78px;
    bottom: 40px;
    right: 40px;
    img {
      width: 100%;
      height: 100%;
    }
  }
}
</style>