using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DTO.Enum
{
    public enum UserType
    {
        /// <summary>
        /// 分部经理
        /// </summary>
        DivisionManager = 1,
        /// <summary>
        /// 业务主管
        /// </summary>
        Executive = 2,
        /// <summary>
        /// 点部主管
        /// </summary>
        Director = 3,
        /// <summary>
        /// 收派员
        /// </summary>
        Courier = 4,
        /// <summary>
        /// 销售人员
        /// </summary>
        Salesperson = 5,
    }
}
