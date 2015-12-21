using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Security;

namespace DevMikroblog.WebApp.Models
{
    [DataContract]
    public class CreatePostViewModel
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}