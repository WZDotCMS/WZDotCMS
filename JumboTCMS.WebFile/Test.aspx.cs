using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using JumboTCMS.Entity;
namespace JumboTCMS.WebFile
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Language lng = new JumboTCMS.DAL.LanguageDAL().GetEntity("cn");
            this.TextBox1.Text = lng.Home;
            this.TextBox2.Text = lng.More;
        }
    }
}
