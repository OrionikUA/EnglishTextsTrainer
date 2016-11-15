using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orionik.EnglishTextsTrainer.Helpers
{
    public class ConnectionStringHelper
    {
        public readonly String Connection;
        private static ConnectionStringHelper _instance;

        private ConnectionStringHelper() { Connection = ConfigurationManager.ConnectionStrings["EnglishTextsTrainerConnectionString"].ConnectionString; }

        public static ConnectionStringHelper Instance => _instance ?? (_instance = new ConnectionStringHelper());

    }
}
