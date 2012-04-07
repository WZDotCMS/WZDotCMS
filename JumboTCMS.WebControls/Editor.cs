using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
namespace JumbotCms.WebControls
{

    [DefaultProperty("Value")]
    [ValidationProperty("Value")]
    [ToolboxData("<{0}:Editor runat=server></{0}:Editor>")]
    public class Editor : WebControl, INamingContainer, IPostBackDataHandler
    {
        public Editor()
        { }
        [Category("Custom Parameters")]
        [Description("控件路件")]
        [DefaultValue("/editor/")]
        [Bindable(true)]
        [Browsable(true)]
        public string BasePath
        {
            get
            {
                object obj2 = this.ViewState["BasePath"];
                if (obj2 == null)
                {
                    obj2 = ConfigurationManager.AppSettings["JBTeditor:BasePath"];
                }
                return ((obj2 == null) ? "/editor/" : ((string)obj2));
            }
            set
            {
                this.ViewState["BasePath"] = value;
            }
        }
        [Category("Appearence")]
        [Description("控件宽度")]
        [Bindable(true)]
        [DefaultValue("455px")]
        [Browsable(true)]
        public override Unit Width
        {
            get
            {
                object obj2 = this.ViewState["Width"];
                return ((obj2 == null) ? Unit.Pixel(455) : ((Unit)obj2));
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }

        [Category("Appearence")]
        [DefaultValue("248px")]
        public override Unit Height
        {
            get { object o = ViewState["Height"]; return (o == null ? Unit.Pixel(248) : (Unit)o); }
            set { ViewState["Height"] = value; }
        }
        [DefaultValue("")]
        public string Value
        {
            get
            {
                object obj2 = this.ViewState["Value"];
                return ((obj2 == null) ? "" : ((string)obj2));
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }
        [Category("Custom Parameters")]
        [DefaultValue("Full")]
        public string Toolbar
        {
            get
            {
                object obj2 = this.ViewState["Toolbar"];
                return ((obj2 == null) ? "" : ((string)obj2));
            }
            set
            {
                this.ViewState["Toolbar"] = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(
                "<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />",
                this.ClientID,
                this.UniqueID,
                HttpUtility.HtmlEncode(this.Value)
                );
            writer.Write(
                "<iframe id=\"iframe{0}\" src=\"{1}editor.html?id={0}&amp;Toolbar={2}\" style=\"width:{3};height:{4};\" frameborder=\"no\" scrolling=\"no\"></iframe>",
                this.ClientID,
                this.BasePath,
                this.Toolbar,
                this.Width,
                this.Height
                );
        }
        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            if (postCollection[postDataKey] != this.Value)
            {
                this.Value = postCollection[postDataKey];
                return true;
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }
    }
}

