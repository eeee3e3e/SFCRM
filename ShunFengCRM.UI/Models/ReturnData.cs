using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShunFengCRM.UI.Models
{
    public class ReturnData<T>
    {
        public ReturnType ReturnType { get; set; }

        public string WarnMessage { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }

        public string ReturnTypeStr { get { return ReturnType.ToString(); } }
    }
}