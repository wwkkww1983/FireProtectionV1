<style lang="less">
.frieHouseaInfoBox {
  width: 100%;
  padding-top: 80px;
  .baseInfoBox {
    table {
      font-size: 26px;
      color: #262626;
      position: relative;
      left: 8%;
      td {
        &.leftText {
          width: 150px;
        }
        &.rightText {
          width: 500px;
        }

        height: 100px;
        .phoneNumBox {
          display: flex;
          align-items: center;
        }
        .callIcon {
          width: 60px;
          height: 60px;
          margin-left: 66px;
        }
      }
    }
  }
}
</style>
<template>
  <div class="frieHouseaInfoBox">
   <tabBack :title="$route.name" mark="TofireHouse"></tabBack>
   <topHome></topHome>
    <div class="baseInfoBox">
      <table>
        <tr>
          <td class="leftText">站点名称：</td>
          <td>{{name}}</td>
        </tr>
        <tr>
          <td class="leftText">站点等级：</td>
          <td class="rightText">{{level}} 级</td>
        </tr>
        <tr>
          <td class="leftText">联系人：</td>
          <td class="rightText">{{contactName}}</td>
        </tr>
        <tr>
          <td class="leftText">联系电话：</td>
          <td class="rightText">
            <div class="phoneNumBox">
              <span>{{contactPhone}}</span>
              <a :href="'tel:'+ contactPhone">
                <img class="callIcon" src="../../../assets/imgs/fireUnits/call_btn.png" alt>
              </a>
            </div>
          </td>
        </tr>
        <tr>
          <td class="leftText">人员配备：</td>
          <td class="rightText">{{personNum}} 人</td>
        </tr>
        <tr>
          <td class="leftText">地址：</td>
          <td class="rightText">{{address}}</td>
        </tr>
      </table>
    </div>
  </div>
</template>
<script>
import tabBack from "../../../components/topBack/index";
import topHome from "../../../components/topHome/index";
export default {
  components: {
    tabBack,
    topHome
  },
  data() {
    return {
      id: "",
      address: "",
      contactName: "",
      contactPhone: "",
      createtionTime: "",
      level: "",
      name: "",
      personNum:""
    };
  },
  methods: {},
  created() {
    this.id = this.$route.query.id;
  },
  mounted() {
    let that = this;
    let id = this.id;
    this.$http({
      method: "get",
      url: "/api/services/app/MiniFireStation/GetById",
      params: {
        id
      }
    }).then(function(res) {
      if (res.status == 200) {
        that.address = res.data.result.address;
        that.contactName = res.data.result.contactName;
        that.contactPhone = res.data.result.contactPhone;
        that.createtionTime = res.data.result.createtionTime;
        that.level = res.data.result.level;
        that.name = res.data.result.name;
        that.personNum = res.data.result.personNum;
      }
    });


   
  }
};
</script>