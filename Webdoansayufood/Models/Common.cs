using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webdoansayufood.Models
{
    public abstract class Common
    {

        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}