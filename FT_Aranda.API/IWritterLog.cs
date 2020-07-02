using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FT_Aranda.API
{
    public interface IWritterLog
    {
        void WriteLog(string source, string method, object data);
    }
}
