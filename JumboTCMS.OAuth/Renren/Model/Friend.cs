using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JumboTCMS.OAuth.Renren.Model
{
    [Serializable]
    public class Friend
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Tinyurl { get; set; }
        public string Headurl { get; set; }
        public string Headurl_with_logo { get; set; }
        public string Tinyurl_with_logo { get; set; }
    }
}
