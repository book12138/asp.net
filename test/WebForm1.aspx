<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="test.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery-3.3.1.min.js"></script>
    <script src="js/login.js"></script>
    <link href="css/head.css" rel="stylesheet" />
    <link href="css/login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <%--顶部--%>
         <div class="head">
            <img class="fl_left" src="backImg/logo.gif" />
            <div class="userInfor">
                <div style="height:30px;display:block;cursor:pointer;">                        
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UP_userImageAndText" OnPreRender="UP_userImageAndText_PreRender" runat="server">
                        <ContentTemplate>
                            <%--头像--%>
                            <asp:Image ID="userHeadImage" ImageUrl="~/image/default-profile-img.png" Visible="false" CssClass="fl_left" style="border-radius:50%;line-height:30px;width:20px;height:20px;margin-top:1px;" runat="server" />
                            <%--为用户显示的文本--%>
                            <asp:Label CssClass="fl_right" onclick="showLogin()" uh="0" style="font-size:12px;color:#8d8d8d;line-height:30px;margin-left:5px;" Text="加入/登录账号" ID="userText" runat="server" />
                            <%--上面label中tabindex为0时表示用户未登录，为1表示用户登录--%>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="refreshUserImageAndText" />
                        </Triggers>
                    </asp:UpdatePanel>                       
                </div>
                <asp:Button ID="refreshUserImageAndText" style="display:none" runat="server" />
            </div>
            <div class="clr"></div>
        </div>

        <%--遮罩层--%>
        <div class="shade"></div>
        <%--登录窗口--%>
        <div class="login">
            <div class="hideLogin"><a href="javascript:void(0)" onclick="reset()">×</a></div>
            <div class="clr"></div>
            <div class="loginContent">
                <img style="transform:scale(1.2);margin-left:auto;margin-bottom:20px;" src="image/mark.png" /><br />

                <%--登录账号--%>
                <div class="loginAccount">
                    <span class="loginTitle">登录<span>NIKE</span>账户</span>
                    <br />

                    <%--用户输入域--%>
                    <div class="userDataInput">
                        <div>
                            <img src="image/t01bc21ceecc09bea38.png"     />
                            <asp:TextBox CssClass="userName" oninput="checkUserNameInAccountLogin($(this).val())" runat="server" ToolTip="用户名"></asp:TextBox>
                            <div class="clr"></div>
                        </div>
                        <div class="nameErrorInfor"></div>
                        <div style="margin-top:20px;">
                            <img src="image/FFFFFFFF.jpg" />
                            <asp:TextBox CssClass="userPwd" oninput="checkPwdInputIsEmpty($(this).val())" TextMode="Password" runat="server" ToolTip="密码" />
                            <div class="clr"></div>
                        </div>
                        <div class="pwdErrorInfor"></div>

                        <%--是否保持登录状态 以及 忘记密码--%>
                        <div class="saveLoginDataAndForgetPwd_div">
                            <div class="fl_left">
                                <input type="checkbox" checked="checked" class="saveLoginDataCheckBox" />
                                <span style="font-size:13px;">保持登录状态</span>
                            </div>
                            <a href="javascript:void(0)">忘记密码？</a>
                            <div class="clr"></div>
                        </div>
                    </div>

                    <span class="hint">一旦登录，即表明你同意 Nike 的<a href="javascript:void(0)">隐私政策</a> 和<a href="javascript:void(0)">使用条款</a>。</span>

                    <%--登录按钮--%>
                    <div>
                        <input type="button" onclick="LoginAccountBtnClick()" class="loginContentBtn" value="登录" />
                        <span style="font-size:12px;">还没有账号？<a href="javascript:void(0)" onclick="goToCreateAccount()" class="goToCreateUserAccount">立即注册</a></span>
                    </div>
                </div>
                
                <%--创建新账号--%>
                <div class="createAccount">
                    <span class="loginTitle">创建您的<span>NIKE</span>账户</span>
                    <br />
                    <span style="font-size:14px;color:#808080;margin-top:10px;">我们会严格保障您的私人信息安全，但也请您认真填写以下内容</span>

                    <%--用户输入域--%>
                    <div class="userDataInput">
                        <div>
                            <img src="image/t01bc21ceecc09bea38.png"/>
                            <asp:TextBox CssClass="createUserName" oninput="checkUserNameInAccountCreate($(this).val())" runat="server" ToolTip="用户名"></asp:TextBox>
                            <div class="clr"></div>
                        </div>
                        <div class="createNameErrorInfor"></div>
                        <div style="margin-top:20px;">
                            <img src="image/FFFFFFFF.jpg" />
                            <asp:TextBox CssClass="createUserPwd" oninput="checkPwdInput($(this).val())" TextMode="Password" runat="server" ToolTip="密码" />
                            <div class="clr"></div>
                        </div>
                        <div class="createPwdErrorInfor"></div>
                        <div style="margin-top:20px;">
                            <img src="image/FFFFFFFF.jpg" />
                            <asp:TextBox CssClass="createAgainUserPwd" oninput="checkPwdInputIsSame($(this).val())" TextMode="Password" runat="server" ToolTip="再次输入密码" />
                            <div class="clr"></div>
                        </div>                        
                        <div class="createPwdAgainErrorInfor"></div>
                    </div>

                    <%--前往 实施密码安全措施--%>
                    <div>
                        <input type="button" class="loginContentBtn" onclick="checkInputNextStep()" value="下一步" />
                        <span style="font-size:12px;">已有账号？<a href="javascript:void(0)" onclick="goToLoginExistingAccount()" class="goToCreateUserAccount">立即登录</a></span>
                    </div>                    
                </div>

                <%--账户安全措施--%>
                <div class="securityMeasures">
                    <span class="loginTitle">账户密码安全措施</span>
                    <br />
                    <span style="font-size:14px;color:#808080;margin-top:10px;">当您忘记自己账户的密码时，我们即可通过您填写的问题和答案进行身份验证，以帮助您的密码找回</span>

                    <%--用户输入域--%>
                    <div class="userDataInput">
                        <div>
                            <img src="image/questions.jpg" />
                            <asp:TextBox CssClass="questions" oninput="checkEmept(this,$('.questionsErrorInfor'))" runat="server" ToolTip="问题"></asp:TextBox>
                            <div class="clr"></div>
                        </div>
                        <div class="questionsErrorInfor">*不可为空</div>
                        <div style="margin-top:20px;">
                            <img src="image/answer.jpg" />
                            <asp:TextBox CssClass="answer" oninput="checkEmept(this,$('.answerErrorInfor'))" runat="server" ToolTip="答案" />
                            <div class="clr"></div>
                        </div>
                        <div class="answerErrorInfor">*不可为空</div>
                        <div style="margin-top:20px;">
                            <img src="image/hint.jpg" />
                            <asp:TextBox CssClass="answerHint" runat="server" ToolTip="答案提示" />
                            <div class="clr"></div>
                        </div>
                        <div class="answerHintErrorInfor"></div>
                    </div>

                    <div>
                        <input type="button" class="loginContentBtn" onclick="registerNewAccount($('.createUserName').val().trim(),$('.createUserPwd').val().trim(),$('.questions').val().trim(),$('.answer').val().trim(),$('.answerHint').val().trim())" value="注册" />
                        <span style="font-size:12px;"><a href="javascript:void(0)" onclick="goBackInputNewAccount()" class="goToCreateUserAccount">返回上一步</a></span>
                    </div>
                </div>

                <%--密码找回之 确认所需找回需密码的账户账户名--%>
                <div class="PasswordRecovery_OfAccountName">
                    <span class="loginTitle">指定找回密码的账户</span>
                    <br />
                    <span style="font-size:14px;color:#808080;margin-top:10px;">请输入您所需找回密码的账户账户名</span>

                    <%--用户输入域--%>
                    <div class="userDataInput">
                        <div>
                            <img src="image/t01bc21ceecc09bea38.png"     />
                            <asp:TextBox CssClass="PR_userName" oninput="checkUserNameInAccountLogin($(this).val())" runat="server" ToolTip="用户名"></asp:TextBox>
                            <div class="clr"></div>
                        </div>
                        <div class="PR_nameErrorInfor"></div>
                    </div>

                    <%--下一步按钮--%>
                    <div>
                        <input type="button" onclick="goToAuthentication($('.PR_userName').val().trim(),$('.PR_nameErrorInfor'))" class="loginContentBtn" value="下一步" />
                    </div>
                </div>

                <%--密码找回之 【身份验证】--%>
                <div class="authentication">
                    <span class="loginTitle">身份验证</span>
                    <br />
                    <span style="font-size:14px;color:#808080;margin-top:10px;">在进行密码找回前，我们需要验证您的身份信息，请您回答下列问题</span>

                    <%--用户输入域--%>
                    <div class="userDataInput">
                        <div>
                            <span style="color:red;font-family:华文行楷;">请问:</span>你的名字是啥？
                        </div>
                        <div class="questionsErrorInfor">*不可为空</div>
                        <div style="margin-top:20px;">
                            <img src="image/answer.jpg" />
                            <asp:TextBox CssClass="answer" oninput="checkEmept(this,$('.answerErrorInfor'))" runat="server" ToolTip="答案" />
                            <div class="clr"></div>
                        </div>
                        <div class="answerErrorInfor">*不可为空</div>
                        <div style="margin-top:20px;">
                            <img src="image/hint.jpg" />
                            <asp:TextBox CssClass="answerHint" runat="server" ToolTip="答案提示" />
                            <div class="clr"></div>
                        </div>
                        <div class="answerHintErrorInfor"></div>
                    </div>

                    <div>
                        <input type="button" class="loginContentBtn" onclick="registerNewAccount($('.createUserName').val().trim(),$('.createUserPwd').val().trim(),$('.questions').val().trim(),$('.answer').val().trim(),$('.answerHint').val().trim())" value="注册" />
                        <span style="font-size:12px;"><a href="javascript:void(0)" onclick="goBackInputNewAccount()" class="goToCreateUserAccount">返回上一步</a></span>
                    </div>
                </div>

                <%--下巴空白--%>
                <div style="height:30px;"></div>
            </div>
        </div>
    </form>
</body>
</html>
