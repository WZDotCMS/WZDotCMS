using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JumboTCMS.API.Discuz.Toolkit
{
    public class SessionWrapper
    {
        [XmlIgnore()]
        internal DiscuzSession Session;
    }
}
