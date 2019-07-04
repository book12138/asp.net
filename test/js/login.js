/**登录框布局设置 */
function loginLoad() {
    /* 手动居中登录框 */
    $('.login').css('left', (window.innerWidth - parseInt(subStringAs_px($('.login').css('width')))) / 2 + 'px');
    $('.login').css('top', (window.innerHeight - parseInt(subStringAs_px($('.login').css('height')))) / 2 + 'px');

    $(window).resize(function () {
        $('.login').css('left', (window.innerWidth - parseInt(subStringAs_px($('.login').css('width')))) / 2 + 'px');
        $('.login').css('top', (window.innerHeight - parseInt(subStringAs_px($('.login').css('height')))) / 2 + 'px');
    });
}

/**显示登录 */
function showLogin() {
    $('.shade').css('display', 'block');
    $('.login').css('display', 'block'); 
}

/**隐藏登录 */
function hideLogin() {
    $('.shade').css('display', 'none');
    $('.login').css('display', 'none'); 
}

/**
* 从类似 **px 的数据中截取px前面的部分
* @param _object 对象
*/
function subStringAs_px(_object) {
    if (_object.indexOf('px') >= 0 && _object.substring(_object.indexOf('px')) == 'px')
        return _object.substring(0, _object.length - 2);
    else
        return _object;//不符合格式条件的数据直接退回去，不处理
}

/**前往创建新账号 */
function goToCreateAccount() {
    $('.loginAccount').css('display', 'none');
    $('.createAccount').css('display', 'block');

    //清除在登录框中的残留
    $('.userName').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.nameErrorInfor').text('').css('color', 'red');

    $('.userPwd').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.pwdErrorInfor').text('').css('color', 'red');
}

/**前往登录已有账号 */
function goToLoginExistingAccount() {
    $('.createAccount').css('display', 'none');
    $('.loginAccount').css('display', 'block');

    /* 清除两个窗口的所有 */
    //清除 新账号创建痕迹
    $('.createUserName').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.createNameErrorInfor').text('').css('color', 'red');

    $('.createUserPwd').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.createPwdErrorInfor').text('').css('color', 'red');

    $('.createAgainUserPwd').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.createPwdAgainErrorInfor').text('').css('color', 'red');

    //清除密码安全措施实施痕迹
    $('.questions').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.questionsErrorInfor').text('*不可为空').css('color', 'red');

    $('.answer').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.answerErrorInfor').text('*不可为空').css('color', 'red');

    $('.answerHint').val('');
}

/**
 * 检测用户名 在 【账号登录】情况下
 * @param {any} content 待检测内容
 */
function checkUserNameInAccountLogin(content) {
    if (content == '') {
        $('.nameErrorInfor').text('*用户名不可为空').css('color', 'red');
        return;
    }

    $.post('userAccountProcessing.ashx',
        { 'name': content, 'play': '0' },
        function (data) {
            if (data.trim() == '1') {   //1表示存在，0表示不存在
                $('.nameErrorInfor').text('*输入正确').css('color', 'green');  //存在此用户
                $('.userName').parent().css('border-color', 'rgba(0,0,0,0.5)');
            } else {
                $('.nameErrorInfor').text('*不存在此用户').css('color', 'red');
            }
        });
}

/**
 * 检测密码输入框 是否为空
 * @param {any} content 待检测内容
 */
function checkPwdInputIsEmpty(content) {
    if (content.trim() == '') {
        $('.pwdErrorInfor').text('*密码不可为空').css('color', 'red');
    } else {
        $('.pwdErrorInfor').text('');
        $('.userPwd').parent().css('border-color', 'rgba(0,0,0,0.5)');
    }
}

/**登录账号按钮点击后 */
function LoginAccountBtnClick() {
    if ($('.nameErrorInfor').css('color') != 'rgb(0, 128, 0)') {
        if ($('.userName').val().trim() == '')
            $('.nameErrorInfor').text('*用户名不可为空').css('color', 'red');
        $('.userName').parent().css('border-color', 'red');
    }
    if ($('.userPwd').val().trim() == '') {
        $('.pwdErrorInfor').text('*密码不可为空').css('color', 'red');
        $('.userPwd').parent().css('border-color', 'red');
    }

    if ($('.nameErrorInfor').css('color') == 'rgb(0, 128, 0)' && $('.userPwd').val().trim() != '') {
        var whetherSave;
        var check = document.getElementsByClassName('saveLoginDataCheckBox');
        if (check.checked)
            whetherSave = 1;
        else
            whetherSave = 0;

        $.post('userAccountProcessing.ashx',
            { 'name': $('.userName').val().trim(), 'pwd': $('.userPwd').val().trim(), 'whetherSave': whetherSave ,'play': '1' },
            function (data) {
                if (data.trim() == '1') {   //1表示密码正确，0表示不正确
                    reset();                    
                } else {
                    $('.pwdErrorInfor').text('*密码错误').css('color', 'red');
                    $('.userPwd').parent().css('border-color', 'red');
                }
            });
    }
}

/**
 * 注册时 检测用户输入的用户名是否与数据库中的重复
 * @param {any} content 待检测内容
 */
function checkUserNameInAccountCreate(content) {
    if (content == '') {
        $('.createNameErrorInfor').text('*用户名不可为空').css('color', 'red');
        return;
    }

    $.post('userAccountProcessing.ashx',
        { 'name': content, 'play': '0' },
        function (data) {
            if (data.trim() == '1') {   //1表示存在，0表示不存在
                $('.createNameErrorInfor').text('*已存在此用户').css('color', 'red');                
            } else {
                $('.createNameErrorInfor').text('*输入正确').css('color', 'green');  //不存在此用户
                $('.createUserName').parent().css('border-color', 'rgba(0,0,0,0.5)');
            }
        });

    //$.ajax({
    //    url: 'userAccountProcessing.ashx',
    //    type: 'GET',
    //    async: false,
    //    //dataType: 'json',
    //    data: {
    //        'name': content, 'play': '0'
    //    },
    //    success: function (data) {
    //        if (data.trim() == '1') {   //1表示存在，0表示不存在
    //            $('.createNameErrorInfor').text('*已存在此用户').css('color', 'red');                
    //        } else {
    //            $('.createNameErrorInfor').text('*输入正确').css('color', 'green');  //不存在此用户
    //            $('.createUserName').parent().css('border-color', 'rgba(0,0,0,0.5)');
    //        }
    //    },
    //    error: function () {
    //        alert('提交数据失败');
    //    }
    //})
}

/**
 * 注册时 检查用户输入的密码是否超长
 * @param {any} content 待检测内容
 */
function checkPwdInput(content) {
    if (content.trim().length > 12) {
        $('.createPwdErrorInfor').text('*密码长度不得大于12').css('color', 'red');
    } else if (content.trim() == '') {
        $('.createPwdErrorInfor').text('*密码不可为空').css('color', 'red');
    }else {
        $('.createPwdErrorInfor').text('*密码格式正确').css('color', 'green');
        $('.createUserPwd').parent().css('border-color', 'rgba(0,0,0,0.5)');
    }
}

/**
 * 注册时 当用户再一次输入密码，核对用户的输入输入是否和上一次一致
 * @param {any} content 待检测内容
 */
function checkPwdInputIsSame(content) {
    if (content.trim() != $('.createUserPwd').val().trim()) {
        $('.createPwdAgainErrorInfor').text('*密码不一致').css('color', 'red');
    } else {
        $('.createPwdAgainErrorInfor').text('*密码一致').css('color', 'green');
        $('.createAgainUserPwd').parent().css('border-color', 'rgba(0,0,0,0.5)');
    }
}

/**注册时 用户输入中 检查是否进入下一步 */
function checkInputNextStep() {
    if ($('.createNameErrorInfor').css('color') != 'rgb(0, 128, 0)') {
        $('.createUserName').parent().css('border-color', 'red');
    }
    if ($('.createPwdErrorInfor').css('color') != 'rgb(0, 128, 0)') {
        $('.createUserPwd').parent().css('border-color', 'red');
    }
    if ($('.createPwdAgainErrorInfor').css('color') != 'rgb(0, 128, 0)') {
        $('.createAgainUserPwd').parent().css('border-color', 'red');
    }

    if ($('.createNameErrorInfor').css('color') == 'rgb(0, 128, 0)'
        && $('.createPwdErrorInfor').css('color') == 'rgb(0, 128, 0)'
        && $('.createPwdAgainErrorInfor').css('color') == 'rgb(0, 128, 0)') {
        $('.createAccount').css('display', 'none');
        $('.securityMeasures').css('display', 'block');
    }
}

/**回退到 填写新账号 */
function goBackInputNewAccount() {
    $('.securityMeasures').css('display', 'none');
    $('.createAccount').css('display', 'block');
}


/**
 * 检查输入框是否为空
 * @param {any} inputObject 输入框对象
 * @param {any} ErrorShowObject 错误显示 div对象
 */
function checkEmept(inputObject,ErrorShowObject) {
    if ($(inputObject).val().trim() == '') {
        $(ErrorShowObject).text('*不可为空').css('color', 'red');
    } else {
        $(ErrorShowObject).text('').css('color', 'green');
    }
}

/**
 * 注册新账号
 * @param {any} name 姓名
 * @param {any} pwd 密码
 * @param {any} question 问题
 * @param {any} answer 答案
 * @param {any} hint 提示
 */
function registerNewAccount(name,pwd,question,answer,hint) {
    $.post('userAccountProcessing.ashx',
        { 'name': name,'pwd':pwd,'question':question,'answer':answer,'hint':hint, 'play': '2' },
        function (data) {
            if (data.trim() == '1') {   //1表示注册成功，0表示不成功
                reset(); //重置所有
            } else {
                alert('注册失败，请尝试修改账户信息，重新注册');
            }
        });
}

/**关闭登录系统，重置所有 */
function reset() {
    //清除 【登录窗口】信息
    $('.userName').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.nameErrorInfor').text('').css('color', 'red');

    $('.userPwd').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.pwdErrorInfor').text('').css('color', 'red');

    //清除 【新账号创建窗口】痕迹
    $('.createUserName').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.createNameErrorInfor').text('').css('color', 'red');

    $('.createUserPwd').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.createPwdErrorInfor').text('').css('color', 'red');

    $('.createAgainUserPwd').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.createPwdAgainErrorInfor').text('').css('color', 'red');

    //清除 【密码安全措施】实施痕迹
    $('.questions').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.questionsErrorInfor').text('*不可为空').css('color', 'red');

    $('.answer').val('').parent().css('border-color', 'rgba(0,0,0,0.5)');
    $('.answerErrorInfor').text('*不可为空').css('color', 'red');

    $('.answerHint').val('');

    //重新【排版】 窗口顺序
    $('.createAccount').css('display', 'none');
    $('.loginAccount').css('display', 'block');


    //关闭总窗口
    $('.shade').css('display', 'none');
    $('.login').css('display', 'none');

    //主动刷新外部 登录状态
    $('#refreshUserImageAndText').click();
}

/**
 * 前往下一窗口 【密码找回之 【身份验证】
 * @param {any} inputObject 输入框对象
 * @param {any} errorShowObject 错误信息显示对象 div
 * @param {any} dataShowObject 在接下来的身份验证中 所需信息展示的对象
 */
function goToAuthentication(inputObject, errorShowObject, dataShowObject) {
    if ($(errorShowObject).css('border') != 'rgb(0, 128, 0)') {
        //设置身份验证处 展示的信息
        $.post('userAccountProcessing.ashx',
            { 'name': name,  'play': '4' },
            function (data) {
                if (data.trim() != '0') {   //0表示获取数据失败
                    dataShowObject.text(data); 
                } else {
                    alert('此账户没有设置密保问题，或者此账户不存在');
                }
            });
    } else {
        $(errorShowObject).parent().css('border-color', 'rgba(0,0,0,0.5)');
    }
}

/**注销账户 */
function cancelAccount() {
    $.post('userAccountProcessing.ashx',{  'play': '3' });
}

