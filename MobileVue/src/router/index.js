import Vue from 'vue'
import Router from 'vue-router'

/* 在此引入页面路径 */
const Splash = () => import ('../pages/splash/index')
const Login = () => import ('../pages/login/login')
const dataMonitor = () => import ('../pages/dataMonitor/index')
const fireCock = () => import ('../pages/dataMonitor/fireCock/index')
const fireCockInfo = () => import ('../pages/dataMonitor/fireCock/fireCockInfo')
const fireCockSearch = () => import ('../pages/dataMonitor/fireCock/addressSearch')
const fireUnits = () => import ('../pages/dataMonitor/frieUnits/index')
const fireUnitinfos = () => import ('../pages/dataMonitor/frieUnits/frieUnitInfo/index')
const recordList = () => import ('../pages/dataMonitor/frieUnits/frieUnitInfo/recordList')
const lawLog = () => import ('../pages/dataMonitor/frieUnits/frieUnitInfo/lawLog')
const fireHouse = () => import ('../pages/dataMonitor/frieHouse/index')
const fireHouseInfo = () => import ('../pages/dataMonitor/frieHouse/frieHouseInfo')
const addressSearch = () => import ('../pages/dataMonitor/frieHouse/addressSearch')
const fireDataReport= () => import ('../pages/dataMonitor/fireDataReport/index')
const safetyElectricity= () => import ('../pages/dataMonitor/fireDataReport/safetyElectricity')
const fireAlarmData= () => import ('../pages/dataMonitor/fireDataReport/fireAlarmData')
const facilitiesTrouble= () => import ('../pages/dataMonitor/fireDataReport/facilitiesTrouble')
const onDutyPatrol= () => import ('../pages/dataMonitor/fireDataReport/onDutyPatrol')
const regulatory = () => import ('../pages/regulatory/index')
const addregulatory = () => import ('../pages/regulatory/addRegulatory/index')
const showregulatory = () => import ('../pages/regulatory/showRegultory/index')
const mySet = () => import ('../pages/mySet/index')
const technicalSupport = () => import ('../pages/mySet/technicalSupport')
const advice = () => import ('../pages/mySet/advice')
const updatePassword1 = () => import ('../pages/mySet/updatePassword')
const forgetpassword = () => import ('../pages/login/forgetpassword')
const updatepassword = () => import ('../pages/login/updatepassword')
const search = () => import ('../pages/dataMonitor/search')



const appRouter = {
  routes: [{
    path: '',
    redirect: '/login',
    name: 'login'
  },
  /* {
    path: '/splash',
    name: 'splash',
    component: Splash
    
}, */
{
  path: '/login',
  name: 'login',
  component: Login
  
},
{
  path: '/dataMonitor',
  name: 'dataMonitor',
  component: dataMonitor
  
},

{
  path: '/regulatory',
  name: 'regulatory',
  component: regulatory
  
},
{
  path: '/regulatory/addregulatory',
  name: '监管检查日志',
  component: addregulatory
  
},
{
  path: '/regulatory/showregulatory',
  name: '监管检查日志',
  component: showregulatory
  
},
{
  path: '/mySet',
  name: 'mySet',
  component: mySet
  
},
{
  path: '/mySet/technicalSupport',
  name: '技术支持',
  component: technicalSupport
},
{
  path: '/mySet/technicalSupport/advice',
  name: '我要提建议',
  component: advice,
  },
{
  path: '/mySet/updatePassword',
  name: '修改密码',
  component: updatePassword1
  },
{
  path: '/dataMonitor/fireCock',
  name: '市政消火栓',
  component: fireCock
  
},
{
  path: '/dataMonitor/fireCock/fireCockInfo',
  name: '市政消火栓详情',
  component: fireCockInfo
  
},
{
  path: '/dataMonitor/fireCock/fireCockInfo/fireCockSearch',
  name: '市政消火栓附近搜索',
  component: fireCockSearch
  
},
{
  path: '/dataMonitor/fireHouse',
  name: '微型消防站',
  component: fireHouse
  
},
{
  path: '/dataMonitor/fireHouseInfo',
  name:  '微型消防站信息',
  component: fireHouseInfo
  
},
{
  path: '/dataMonitor/fireHouseInfo/addressSearch',
  name:  '微型消防站',
  component: addressSearch,
  
},
{
  path: '/dataMonitor/fireUnits',
  name: '防火单位',
  component: fireUnits
  
},
{
  path: '/search',
  name: 'search',
  component: search
  
},
{
  path: '/dataMonitor/fireUnits/fireUnitinfos',
  name:'fireUnitinfos',
  component: fireUnitinfos
  
},
{
  path: '/dataMonitor/fireUnits/fireUnitinfos/recordList',
  name:'recordList',
  component: recordList
  
},
{
  path: '/dataMonitor/fireUnits/fireUnitinfos/lawLog',
  name: '执法日志',
  component: lawLog
  
},
{
  path: '/dataMonitor/fireDataReport',
  name: '综合数据报表',
  component: fireDataReport
  
},
{
  path: '/dataMonitor/fireDataReport/safetyElectricity',
  name: '安全用电数据分析',
  component: safetyElectricity

},
{
  path: '/dataMonitor/fireDataReport/fireAlarmData',
  name: '火警预警数据分析',
  component: fireAlarmData
  
},
{
  path: '/dataMonitor/fireDataReport/facilitiesTrouble',
  name: '设施故障数据分析',
  component: facilitiesTrouble
  
},
{
  path: '/dataMonitor/fireDataReport/onDutyPatrol',
  name: '值班巡查数据分析',
  component: onDutyPatrol
  
},
{
  path: '/login/forgetpassword',
  name: 'forgetpassword',
  component: forgetpassword
  
},
{
  path: '/login/updatepassword',
  name: 'updatepassword',
  component: updatepassword
  
},

]
}
Vue.use(Router)
export default new Router(appRouter);
