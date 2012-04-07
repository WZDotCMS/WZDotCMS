/*
 * 程序中文名称: 将博内容管理系统通用版
 * 
 * 程序英文名称: JumboTCMS
 * 
 * 程序版本: 5.2.X
 * 
 * 程序编写: 随风缘 (定制开发请联系：jumbot114#126.com,不接受免费的技术答疑,请见谅)
 * 
 * 官方网站: http://www.jumbotcms.net/
 * 
 * 商业服务: http://www.jumbotcms.net/about/service.html
 * 
 */

using System;
using System.Data;
using System.Web;
using System.IO;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _payment_api : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            if (!Page.IsPostBack)
            {
                string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/payment_alipay.config");
                JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
                this.txt1Seller_Email.Text = XmlTool1.GetText("Root/seller_email");
                this.txt1Partner.Text = XmlTool1.GetText("Root/partner");
                this.txt1Key.Text = XmlTool1.GetText("Root/key");
                XmlTool1.Dispose();

                string strXmlFile2 = HttpContext.Current.Server.MapPath("~/_data/config/payment_tenpay.config");
                JumboTCMS.DBUtility.XmlControl XmlTool2 = new JumboTCMS.DBUtility.XmlControl(strXmlFile2);
                this.txt2Bargainor_Id.Text = XmlTool2.GetText("Root/bargainor_id");
                this.txt2Key.Text = XmlTool2.GetText("Root/key");
                XmlTool2.Dispose();

                string strXmlFile3 = HttpContext.Current.Server.MapPath("~/_data/config/payment_99bill.config");
                JumboTCMS.DBUtility.XmlControl XmlTool3 = new JumboTCMS.DBUtility.XmlControl(strXmlFile3);
                this.txt3Email.Text = XmlTool3.GetText("Root/email");
                this.txt3MerchantAcctId.Text = XmlTool3.GetText("Root/merchantAcctId");
                this.txt3Key.Text = XmlTool3.GetText("Root/key");
                XmlTool3.Dispose();

                string strXmlFile4 = HttpContext.Current.Server.MapPath("~/_data/config/payment_chinabank.config");
                JumboTCMS.DBUtility.XmlControl XmlTool4 = new JumboTCMS.DBUtility.XmlControl(strXmlFile4);
                this.txt4V_Mid.Text = XmlTool4.GetText("Root/v_mid");
                this.txt4Key.Text = XmlTool4.GetText("Root/key");
                XmlTool4.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/payment_alipay.config");
            JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
            XmlTool1.Update("Root/seller_email", this.txt1Seller_Email.Text);
            XmlTool1.Update("Root/partner", this.txt1Partner.Text);
            XmlTool1.Update("Root/key", this.txt1Key.Text);
            XmlTool1.Save();
            XmlTool1.Dispose();
            FinalMessage("保存成功!", "payment_api.aspx", 0);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string strXmlFile2 = HttpContext.Current.Server.MapPath("~/_data/config/payment_tenpay.config");
            JumboTCMS.DBUtility.XmlControl XmlTool2 = new JumboTCMS.DBUtility.XmlControl(strXmlFile2);
            XmlTool2.Update("Root/bargainor_id", this.txt2Bargainor_Id.Text);
            XmlTool2.Update("Root/key", this.txt2Key.Text);
            XmlTool2.Save();
            XmlTool2.Dispose();
            FinalMessage("保存成功!", "payment_api.aspx", 0);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string strXmlFile3 = HttpContext.Current.Server.MapPath("~/_data/config/payment_99bill.config");
            JumboTCMS.DBUtility.XmlControl XmlTool3 = new JumboTCMS.DBUtility.XmlControl(strXmlFile3);
            XmlTool3.Update("Root/email", this.txt3Email.Text);
            XmlTool3.Update("Root/merchantAcctId", this.txt3MerchantAcctId.Text);
            XmlTool3.Update("Root/key", this.txt3Key.Text);
            XmlTool3.Save();
            XmlTool3.Dispose();
            FinalMessage("保存成功!", "payment_api.aspx", 0);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string strXmlFile4 = HttpContext.Current.Server.MapPath("~/_data/config/payment_chinabank.config");
            JumboTCMS.DBUtility.XmlControl XmlTool4 = new JumboTCMS.DBUtility.XmlControl(strXmlFile4);
            XmlTool4.Update("Root/v_mid", this.txt4V_Mid.Text);
            XmlTool4.Update("Root/key", this.txt4Key.Text);
            XmlTool4.Save();
            XmlTool4.Dispose();
            FinalMessage("保存成功!", "payment_api.aspx", 0);
        }
    }
}
