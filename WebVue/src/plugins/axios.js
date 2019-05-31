import axios from "axios";
import { Message } from "element-ui";
import store from "../store";
import NProgress from "nprogress"; // 顶部进度条
import "nprogress/nprogress.css";

const service = axios.create({
  timeout: 30000
});

service.interceptors.request.use(
  function(config) {
    NProgress.start();
    if (config.params) {
      // config.headers["Cookie"] = store.state.token;
      config.params.UserId = store.state.userInfo.userId; // 给所有请求加上用户id
    }
    return config;
  },
  function(error) {
    return Promise.reject(error);
  }
);

service.interceptors.response.use(
  function(response) {
    NProgress.done();
    return Promise.resolve(response.data);
  },
  function(error) {
    NProgress.done();
    //  todo 打印错误
    if (error.response) {
      Message.error(JSON.stringify(error.response.data.error));
    }
    return Promise.reject(error);
  }
);

export default service;
