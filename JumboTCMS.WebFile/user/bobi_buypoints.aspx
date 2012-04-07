<%@ Page Language="C#" AutoEventWireup="True" Codebehind="bobi_buypoints.aspx.cs" Inherits="JumboTCMS.WebFile.User._bobi_buypoints" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>个人中心 - <%=site.Name%></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<script type="text/javascript" src="js/fore.common.js"></script>
<link   type="text/css" rel="stylesheet" href="../style/member/style.css" />
<script type="text/javascript">
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtPoints")
		.formValidator({tipid:"tipPoints",onshow:"如50、100、500！",onfocus:"如50、100、500！"})
		.RegexValidator({regexp:"^([1-9]{1}[0-9]{0,4})$",onerror:"请输入整数"});
});
</script>
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
  <div id="main">
    <!--#include file="include/left_menu.htm"-->
    <script type="text/javascript">$('#bar-bobi-head').addClass('currently');$('#bar-bobi li.small').show();</script>
    <div id="mainarea">
      <div class="nav_two">
        <ul>
          <li class="currently"><a>博币充值</a></li>
          <li><a href="bobi_card2points.aspx">激活充值卡</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <div>
        <table class="helptable">
          <tr>
            <td><ul>
                <li>如果你的银行卡具有网上支付功能，就可以使用银行卡进行在线支付充值。</li>
              </ul></td>
          </tr>
        </table>
        <form id="buypointsForm" name="form1" method="post" action="../api/sumbit.aspx" target="_blank" onsubmit="return $.formValidator.PageIsValid('1')">
          <input type="hidden" name="txtProductName" value="<%=site.Name %>博币充值">
          <input type="hidden" name="txtProductDesc" value="<%=site.Name %>博币充值">
          <table style="width:540px;" align="center" border="0" cellspacing="4" cellpadding="4" id="studio">
            <tr>
              <td colspan="3">您现在有博币： <span class="u_points">0</span></td>
            </tr>
            <tr>
              <td colspan="3" align="left"><img src="../style/common/icon_bank.jpg" alt="银行卡充值"/><font style="color:#000; font-size:14px;font-weight: bold;">银行卡充值</font></td>
            </tr>
            <tr>
              <td width="120" height="30" align="right">请输入充值金额：</td>
              <td width="190"><input type="text" class="inputss" style="width:60px;" name="txtPoints" id="txtPoints" value="100" /></td>
              <td width="*"><span id="tipPoints" style="width:200px;"> </span></td>
            </tr>
            <tr>
              <td align="right">请选择目标账户：</td>
              <td><select name='payway' id='payway'>
                  <option value="alipay">支付宝</option>
                  <option value="tenpay">财付通</option>
                  <option value="99bill">快钱</option>
                  <option value="chinabank">网银在线</option>
                </select></td>
              <td></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="bottom"><input type="submit" id="btnSave" value="确定充值" class="button" />
                <a href="index.aspx">取消</a></td>
            </tr>
          </table>
          <table class="helptable">
            <tr>
              <td><ul>
                  <li>您还可以通过向如下账户转帐或汇款的方式进行充值。</li>
                </ul></td>
            </tr>
          </table>
          <style>
table.t1{background:#fff;border-collapse:collapse;border:1px solid #D8EBFF;width:90%;margin:10px auto;}
table.t1 td,table.t1 th{border:1px solid #D8EBFF;padding:5px;}
table.t1 table,table.t1 table td{border:0px solid #D8EBFF;}
</style>
          <table class="t1">
            <tr>
              <td colspan="2">个人汇款（即时到帐）</td>
            </tr>
            <tr>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../style/common/yh4.gif" width="59" height="69" /></td>
                  </tr>
                  <tr>
                    <td><strong>开户行：</strong>招商银行北京慧忠里支行</td>
                  </tr>
                  <tr>
                    <td><strong>　账号：</strong>6225 8801 1055 4855</td>
                  </tr>
                  <tr>
                    <td><strong>　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../style/common/yh3.gif" width="59" height="69" /></td>
                  </tr>
                  <tr>
                    <td><strong>开户行：</strong>建设银行北京润德支行</td>
                  </tr>
                  <tr>
                    <td><strong>　账号：</strong>6227 0000 1201 0041 919</td>
                  </tr>
                  <tr>
                    <td><strong>　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
            </tr>
            <tr>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../style/common/yh1.gif" width="59" height="69" /></td>
                  </tr>
                  <tr>
                    <td><strong>开户行：</strong>农业银行吴江市支行八都分理处(江苏)</td>
                  </tr>
                  <tr>
                    <td><strong>　账号：</strong>6228 4804 0325 0808 017</td>
                  </tr>
                  <tr>
                    <td><strong>　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../style/common/yh5.gif" width="59" height="69" /></td>
                  </tr>
                  <tr>
                    <td><strong>开户行：</strong>交通银行北京亚北支行</td>
                  </tr>
                  <tr>
                    <td><strong>　账号：</strong>62225 80910 76519 06</td>
                  </tr>
                  <tr>
                    <td><strong>　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
            </tr>
            <tr>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../style/common/zfbt.gif"  /></td>
                  </tr>
                  <tr>
                    <td><strong>个人帐户：</strong><a href="https://me.alipay.com/jumbot" target="_blank">zhouguoqing114@126.com</a></td>
                  </tr>
                  <tr>
                    <td><strong>　　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
              <td></td>
            </tr>
          </table>
        </form>
        <div class="clear"></div>
      </div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
