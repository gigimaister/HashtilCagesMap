using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilCagesMap.DataBase
{
    public class DBHelperOld
    {
        private const string constring = "SERVER=sql186.main-hosting.eu;DATABASE=u319907874_hashtil_db;UID=u319907874_kobi_gigi;PASSWORD=K5$@F991Sw";

        public MySqlConnection con = new MySqlConnection(constring);

    }
}
