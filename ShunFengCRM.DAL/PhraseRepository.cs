using ShunFengCRM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DAL
{
    public class PhraseRepository
    {
        public ICollection<StaticInfo> GetPhrases()
        {
            var sqlStr = "SELECT * FROM [SF_CRM].[dbo].[T_Phrase]";
            return new Tools.SqlHelper().GetStaticInfo(sqlStr);
        }
    }
}
