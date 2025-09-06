using System;
using System.Collections.Generic;
using System.Linq;

namespace User.Api.Core
{
    public abstract class Database
    {
        protected Database() { }

    public abstract void ExecuteNonQuery(User user);

    public abstract String GetJson(String username);

    public static Database Instance { get; set; }


    }
}