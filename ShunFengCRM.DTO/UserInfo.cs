using ShunFengCRM.DTO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DTO
{
    public class UserInfo
    {
        public int UserId { get; set; }

        public string LoginName { get; set; }

        public UserType UserType { get; set; }
    }
}
