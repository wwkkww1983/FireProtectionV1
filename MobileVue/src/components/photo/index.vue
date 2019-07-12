<style lang="less">
  #uploadImg{
    .upload_image_box{
        padding: 34px;
        width: 100%;
        box-sizing: border-box;
        background: #fff;
      .upload_image{
          background: #f2f2f2;
          width: 100%;
          border: 1px dashed #e6e6e6;/*no*/
          border-radius: 10px;
          padding: 110px 0;
            .photo_box{
                width: 100%;
                .icon_photo{
                    width: 44px;
                    height: 40px;
                    position: relative;
                    left: 50%;
                    margin-left: -22px;
                }
                p{
                    font-size: 24px;/* px */
                    color: #999999;
                    text-align: center;
                    margin: 0;
                }
            }
      }
    }
    .uploaded{padding: 34px;width: 100%;box-sizing: border-box;background: #fff;
      .imgItem{width: 214px;height: 214px;display: inline-block;border: 2px dashed #e5e5e5;border-radius: 20px;position: relative;vertical-align: top;margin-right: 20px;
        .delete{width: 30px;height: 30px;position: absolute;right: 0;}
        .img{width: 100%;height: 100%;border-radius: 20px;}
      }
      .imgUpload{width: 214px;height: 214px;display: inline-block;background: #f2f2f2;padding: 66px 0;box-sizing: border-box;border: 2px dashed #e5e5e5;border-radius: 20px;
        .icon_photo{width: 44px;height: 40px;position: relative;left: 50%;margin-left: -22px;}
        p{font-size: 24px;/* px */color: #999999;text-align: center;margin: 0;}
      }
    }
  }
</style>
<template>
    <div id="uploadImg">
        <div class="upload_image_box" v-show="show1">
            <div class="upload_image">
                <div class="photo_box" @click="actionSheet()">

                <p><span>上传图片</span></p>
                </div>
            </div>
            </div>
        <div class="uploaded" v-show="show2">
            <div v-for="(item,index) in imgLists" :key="index" class="imgItem">
                <div @click="deleteImg(index)" class="delete" ></div>
                <img :src="item" class="img" @click="previewPicture(index)">
            </div>
            <div class="imgUpload" @click="actionSheet()">
               
                <p><span>上传图片</span></p>
            </div>
        </div>
    </div>
</template>

<script>
import {Toast, Indicator} from "mint-ui"
export default {
    props: [
      'imgList',
      'showUpload',
      'showUploaded',
      'maxPic',//用户传的最大的照片数目
      'zipTo' // 压缩到多少KB(可选,默认到1024K)
    ],
     data() {
        return {
        show1: true,
        show2: false
        }
    },
    computed: {
        imgLists() {
            return this.imgList || []
        },
      getMaxPic() {
        return this.maxPic*1 || 1
      }
    },
    watch: {
        imgLists(newValue, oldValue) {
            if(newValue.length > 0){
              this.show1 = false;
              this.show2 = true;
            } else {
              this.show1 = true;
              this.show2 = false;
            }
        }
    },
    methods: {
        actionSheet() {
            let that = this
            plus.nativeUI.actionSheet({
                title: "获取照片",
                cancel: "取消",
                buttons: [
                    {
                    title: "拍照"
                    },
                    {
                    title: "相册"
                    },
                ]
                },
                function(e) { //点击回调
                switch(e.index) {
                case 0:
                    break;
                case 1:
                    that.cameraImg(); /*拍照*/
                    break;
                case 2:
                    that.galleryImg(); /*打开相册*/
                    break;
                default:
                    break;
                }
                }
            );
        },
        galleryImg() {
          let that = this;
          plus.gallery.pick(
            function(e) {
              for (var i in e.files) {
                var path = e.files[i]; // file:///storage/emulated/0/截屏、超级截屏_20180803_112739.png 
                if (that.imgLists.length < that.getMaxPic) {
                plus.io.resolveLocalFileSystemURL(path, function(entry) { //entry 请求到的目录或文件对象
                  var fileSystem = entry.fileSystem
                  var fileInfo = {};
                  entry.file(function(file){ // 获取文件信息
                    fileInfo = file;
                    let zipTo = that.zipTo; // KB
                    that.zipPicture(path, fileInfo, zipTo)
                    .then(function(zipObj){
                      if(zipObj && zipObj.target){
                        Indicator.open();
                        console.log('107:', that.imgLists, zipObj.target,!that.imgLists.includes(zipObj.target))
                        if(!that.imgLists.includes(zipObj.target)){
                          that.getBase64(zipObj.target) //zipObj.target -压缩后的文件所在的路径
                          .then(function(dataURL){ //dataUrl -压缩后的文件的base64编码
                            that.dataURLtoFile(dataURL, fileInfo.name)
                            .then(function(f){ // f - base64转成文件后的文件对象
                              let fileBigObj = {
                                path: zipObj.target,
                                file: f
                              }
                              console.log("fileBigObj",fileBigObj);
                              that.$emit('pickPic',fileBigObj)
                              return fileBigObj;
                            })
                          })
                        }else{
                          Toast({message: '同一张图片不能重复提交'})
                        }
                      }
                    })
                    .catch(function(zipErr){
                      that.zipErrHandle(zipErr, fileInfo)
                    })
                  })
                })
              } else {
                  that.$alert(i18n.t('m.msg_0005')+`${that.getMaxPic}`+i18n.t('m.msg_0007'));
                break;
                }
              }
            },
            function(error) {
            Indicator.close();
            if(error.code == 12){
              Toast({message: '用户取消'})
            }else{
              that.galleryPermission(error)
            }
          }, 
          {
              filter: "image",
              multiple: true, // 设置了此项,选择成功返回的是一个包含键名为files的路径数组的对象,否则为单一路径;
            maximum: that.getMaxPic - that.imgLists.length
            });
        },
        cameraImg() { // 调用相机
          let that = this;
          let c = plus.camera.getCamera();//调用相机方法
          let hasCamera  = that.cameraPermission()// 相机权限
         if(!hasCamera) return
          c.captureImage(function(e) {
            //获取操作文件或目录对象
            plus.io.resolveLocalFileSystemURL(e, function(entry) { //entry 请求到的目录或文件对象
            
              var path = entry.toLocalURL() //toLocalURL: 获取目录路径转换为本地路径URL地址
            //压缩图片
            plus.io.resolveLocalFileSystemURL(path, function(entry) { //entry 请求到的目录或文件对象
              var fileSystem = entry.fileSystem//
              var fileInfo = {};
              entry.file(function(file){ // 获取文件信息
                fileInfo = file;
                let zipTo = that.zipTo; // KB
               /*  that.zipPicture(path, fileInfo, zipTo)
                .then(function(zipObj){
                    console.log("压缩成功",zipObj)
                  if(zipObj && zipObj.target){
                    Indicator.open();
                    if(!that.imgLists.includes(zipObj.target)){
                      that.getBase64(zipObj.target) //zipObj.target -压缩后的文件所在的路径
                      .then(function(dataURL){ //dataUrl -压缩后的文件的base64编码
                        that.dataURLtoFile(dataURL, fileInfo.name)
                        .then(function(f){ // f - base64转成文件后的文件对象
                          let fileBigObj = {
                            path: zipObj.target,
                            file: f
                          }
                          console.log(fileBigObj);
                          that.$emit('pickPic',fileBigObj)
                          return fileBigObj;
                        })
                      })
                    } else {
                      Toast({message: '同一张图片不能重复提交'})
                    }
                  }
                })
                .catch(function(zipErr){

                    console.log("压缩失败",zipErr)
                  that.zipErrHandle(zipErr, fileInfo)
                }) */
              })
            })
          
            }, function(e) {
              console.log("读取拍照文件错误：" + e.message);
            });
          }, function(error) {
            if(error.code == 11){
              Toast({message: '用户取消'})
            }
          }, {
            filename: "_doc/" //拍照文件存放的路径
          })
        },
        deleteImg(index) {
            let deletePic = this.imgLists[index]
            this.$emit('deletePic', deletePic)
        },
      /**
       * @description 打开相册失败，请求系统权限
       * @param {Error} e
       */
      galleryPermission(e) {
        var galleryPermission = plus.navigator.checkPermission("GALLERY");
        if (plus.os.name == "iOS") {
            if (e.code == 8) {
            plus.nativeUI.alert("您的相册权限未打开，请在当前应用设置-隐私-相册来开打次权限", function(event){
              plus.runtime.openURL('prefs:root=Privacy');
            })
            }
        } else if (plus.os.name == "Android") {
            if (e.code != 12) {
                plus.nativeUI.alert("您的相册权限未打开，请在应用列表中将您的权限打开", function() {
                    // var android = plus.android.importClass('com.android.settings');
                    var main = plus.android.runtimeMainActivity();
                    var Intent = plus.android.importClass("android.content.Intent");
                    var mIntent = new Intent('android.settings.APPLICATION_SETTINGS');
                    main.startActivity(mIntent);
                });
            }
        }
      },
      cameraPermission() {
        var cameraPermission = plus.navigator.checkPermission("CAMERA");
        if( cameraPermission == "denied" ){// 阻塞
          if (plus.os.name == "iOS") {
            plus.nativeUI.alert("您的相机权限没有打开，请在当前应用设置-隐私-相机来开打次权限", function(event){
              plus.runtime.openURL('prefs:root=Privacy');
            })
          } else if (plus.os.name == "Android") {
            plus.nativeUI.alert("您的相机权限没有打开，请在应用列表中将权限打开", function() {
                // var android = plus.android.importClass('com.android.settings');
                var main = plus.android.runtimeMainActivity();
                var Intent = plus.android.importClass("android.content.Intent");
                var mIntent = new Intent('android.settings.APPLICATION_SETTINGS');
                main.startActivity(mIntent);
            });
          }
          return false
        }else{
          return true
        }
      },
//       zipPicture(pth, cb) {
//         return new Promise(function(resolve, reject){
//           let conf = {
//             src: pth,
//             dst:pth,
//             overwrite:true,
//             quality: 50
//           };
//           plus.zip.compressImage(conf, function(data) {
//             resolve(data)
//           }, function(err) {
//             Toast({message:'图片压缩失败,错误编码:'+err.code})
//             reject(err);
//           });
//         })
//       },
      previewPicture(index) {
        plus.nativeUI.previewImage(this.imgLists,{current:index});
      },
      zipPicture(pth, fileInfo, zipTo) {
        return new Promise(function(resolve, reject){
          let zipName = fileInfo.name,
            quality = 0.50;
          typeof zipTo === 'number' ? zipTo>0 ? zipTo = zipTo :zipTo = 1024 : zipTo = 1024; //默认压缩到1M
          zipTo < fileInfo.size ? quality = zipTo / fileInfo.size : quality = 1;
          let conf = {
            src: pth,
            dst: '_doc/'+zipName,//pth, // 转换目标图片的路径
            overwrite:false, //true, // 是否覆盖
            quality: quality * 100 // 取值(0,100]
          };
          plus.zip.compressImage(conf, function(data) {
            resolve(data)
          }, function(err) {
            // plus.nativeUI.alert('图片压缩失败,错误编码:'+err.code)
            reject(err);
          });
        })
      },
      dataURLtoFile(dataurl, filename) {//将base64转换为文件
        //atob()- window内置的函数;ASCII码转binary ;不支持非ASCII码转换,
        //btoa()- window内置函数;binary转ASCII;不支持非ASCII码转换,
        return new Promise(function(resolve, reject){
          var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
              bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
          while(n--){
              u8arr[n] = bstr.charCodeAt(n);
          }
          var file = new File([u8arr], filename, {type:mime})
          resolve(file);
        })
      },
      getBase64(url){
        return new Promise(function(resolve, reject){
          var canvas = document.createElement('canvas'),//创建canvas元素
            dataURL= '',
            ctx = canvas.getContext('2d'),
            img = new Image; //通过构造函数来创建的 img 实例，在赋予 src 值后就会立刻下载图片，相比 createElement() 创建 <img> 省去了 append()，也就避免了文档冗余和污染
          img.crossOrigin = 'Anonymous';
          img.onload = function(){
            canvas.height = img.height;
            canvas.width = img.width;
            ctx.drawImage(img,0,0,);
            dataURL = canvas.toDataURL('image/jpeg', 1);
            resolve(dataURL)
            // callback?callback(dataURL):null; //调用回调函数
            canvas = null;
          };
          img.src = url; // 图片预览
        })        
      },
      zipErrHandle(zipErr, fileInfo) { // 压缩失败
        let that = this;
        if(zipErr.code == -5){ // 错误编码为-5--该路径已经存在该文件;跳过压缩,直接获取原来存在的文件
          var zipRelatePath = "_doc/"+ fileInfo.name;
          plus.io.resolveLocalFileSystemURL( zipRelatePath, function(entry){
            var zipLocalURL = entry.toLocalURL()
            console.log('336:', that.imgLists, zipLocalURL, !that.imgLists.includes(zipLocalURL))
            if( !that.imgLists.includes(zipLocalURL)){
              that.getBase64(zipLocalURL) //zipLocalURL -压缩后的文件所在的'绝对路径'
              .then(function(dataURL){ //dataUrl -压缩后的文件的base64编码
                  that.dataURLtoFile(dataURL, fileInfo.name)
                  .then(function(f){ // f - base64转成文件后的文件对象
                    let fileBigObj = {
                      path: zipLocalURL,
                      file: f
                    }
                    console.log(fileBigObj);
                    that.$emit('pickPic',fileBigObj)
                    return f;
                  })
              })
            } else {
              Toast({message: '同一张图片不能重复提交'})
            }
          }, function(){
            
          })
        }
      }
    }
}
</script>

