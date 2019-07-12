const API = {
    //    todo 登录模块
    UserLoginForMobile: "/api/services/app/FireDeptUser/UserLoginForMobile", // 用户登录(移动端)
    //数据监控
    GetDataMinotor: "/api/services/app/DataReport/GetDataMinotor",//数据监控页
    //数据监控->  防火单位模块
    GetFireUnitListForMobile:"/api/services/app/FireUnit/GetFireUnitListForMobile", // 防火单位分页列表(手机端)
    GetFireUnitInfo: "/api/services/app/FireUnit/GetFireUnitInfo", //防火单位详情
    GetFireUnitAlarm: "/api/services/app/FireUnit/GetFireUnitAlarm", // 防火单位消防数据
    GetFireUnit30DayAlarmEle:"/api/services/app/FireUnit/GetFireUnit30DayAlarmEle", // 安全用电最近30天报警记录查询
    GetFireUnit30DayAlarmFire:"/api/services/app/FireUnit/GetFireUnit30DayAlarmFire", // 火警预警最近30天报警记录查询
    GetFireUnitHighFreqAlarmEle:"/api/services/app/FireUnit/GetFireUnitHighFreqAlarmEle", //安全用电高频报警部件查询
    GetFireUnitHighFreqAlarmFire:"/api/services/app/FireUnit/GetFireUnitHighFreqAlarmFire", //火警预警高频报警部件查询
    GetFireUnitPendingFault: "/api/services/app/FireUnit/GetFireUnitPendingFault", //设备设施故障待处理故障查询
   //数据监控->  微型消防站
   GetMiniFireStationList:"/api/services/app/MiniFireStation/GetList",//微型消防站数据列表
   GetMiniFireStationById:"/api/services/app/MiniFireStation/GetById",//获取单个微型消防站信息
   GetNearbyStation:"/api/services/app/MiniFireStation/GetNearbyStation",//根据坐标点获取附近1KM直线距离内的微型消防站
  };
  export default API;
  