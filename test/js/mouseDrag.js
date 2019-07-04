/*！  1.使用方法：onmousedown="AtMouseDown(this)" onmousemove="drag(event)" onmouseup="AtMouseUp()"  
 *    2.使用方法解释：在HTML标签元素内，加入以上代码即可，然后引用此js文件 (改进：最好在from或者body中添加onmousemove="drag(event)" onmouseup="AtMouseUp()")
 *    3.使用示例：<div id="model" onmousedown="AtMouseDown(this)" onmousemove="drag(event)" onmouseup="AtMouseUp()"></div>
 *    4.参数解释：this是当前对象，event是对象在发生事件的时候，系统会采集一些参数信息，然后存放在里面
 *    5.场合区分：仅限于用来控制单个元素的拖动，无法用于多元素操作  
 *    6.为了使用起来更加完美，建议设置body的CSS属性（禁止文字被选中，也可以仅限于小范围的限制，但一定要包含操作对象）：
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
 *     禁止页面（或对象）下的图片被拖拽（浏览器自身对于图片是有鼠标拖动效果的），以免产生拖动混乱，在body，或对象元素标签里，写入ondragstart="return false;"
 */

var dragObject = null;      //存储操作对象
var isDrag = false;         //是否产生拖动效果

//在鼠标按下时
function AtMouseDown(_object) { 
    dragObject = _object;   //存储操作对象
    beginDrag();
}

//开始模拟鼠标拖动效果
function beginDrag() {
    isDrag = true;
    $(dragObject).css({ 'transform': 'scale(0.7,0.7)', 'opacity': 0.3 });
}

//模拟鼠标拖动
function drag(e) {      //参数为对象的event
    if (isDrag) {
        $(dragObject).css(
            {   //鼠标指针总是指向元素体中间
                'top': (e.clientY - parseInt(subStringAt_px($(dragObject).css('height'))) / 2) + 'px',
                'left': (e.clientX - parseInt(subStringAt_px($(dragObject).css('width'))) / 2) + 'px'
            });
    }
}

//在鼠标弹起的时候
function AtMouseUp() {
    isDrag = false;     //停止模拟鼠标拖动
    $(dragObject).css({ 'transform': 'scale(1,1)', 'opacity': 1 });  //拖动结束时的对象样式
    dragObject = null;      //清空对象
}

//从类似 **px 的数据中截取px前面的部分
function subStringAt_px(_object) {
    if (_object.indexOf('px') >= 0 && _object.substring(_object.indexOf('px')) == 'px')
        return _object.substring(0, _object.length - 2);
    else
        return _object;//不符合格式条件的数据直接退回去，不处理
}