﻿using ShunFengCRM.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DAL
{
    public class VisitKindRepository
    {
        public ICollection<StaticInfo> Get()
        {
            var sqlStr = "SELECT * FROM [SF_CRM].[dbo].[T_VisitKind]";
            return new Tools.SqlHelper().GetStaticInfo(sqlStr);
        }
    }
}
