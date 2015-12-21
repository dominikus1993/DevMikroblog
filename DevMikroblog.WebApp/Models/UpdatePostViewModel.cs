using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevMikroblog.WebApp.Models
{
    public class UpdatePostViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}