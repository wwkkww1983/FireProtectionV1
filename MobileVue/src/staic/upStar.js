export default{
     swapItems:function(arr, index1, index2,direction) {//参数：要排序的数组，数组的下标，数组的
        if(direction=='up'){//置顶
        arr.unshift(arr[index1]);//把当前数组位置的值，加入数组的最前部
        arr.splice(index1+1,1);//删除当前位置的数值
        return arr; //返回一个新的数组
        }
        if(direction=='down'){//置底
        arr.push(arr[index1]);
        arr.splice(index1,1);
        return arr;
        }
    
        arr[index1] = arr.splice(index2, 1, arr[index1])[0];
        return arr;
        }
}
