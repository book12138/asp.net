//从类似 **px 的数据中截取px前面的部分
function subStringAs_px(_object) {
    if (_object.indexOf('px') >= 0 && _object.substring(_object.indexOf('px')) == 'px')
        return _object.substring(0, _object.length - 2);
    else
        return _object;//不符合格式条件的数据直接退回去，不处理
}