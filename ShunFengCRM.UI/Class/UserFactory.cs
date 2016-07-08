using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShunFengCRM.UI.Class
{
    public class UserFactory
    {
        public static ICollection<UserModel> GetUserModels()
        {
            return new List<UserModel>()
            {
                new UserModel(){ LoginName="123456", Password="123456", UserId=1},
                new UserModel(){LoginName="223456", Password="223456", UserId=2},
                new UserModel(){LoginName="323456", Password="323456", UserId=3},
                new UserModel(){LoginName="423456", Password="423456", UserId=4},
            };
        }
    }
}