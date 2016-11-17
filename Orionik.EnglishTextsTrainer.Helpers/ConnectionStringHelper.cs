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
        public readonly List<string> Connection;
        private static ConnectionStringHelper _instance;

        private ConnectionStringHelper()
        {
            Connection = new List<string>();

            for (int i = 1; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                Connection.Add(ConfigurationManager.ConnectionStrings[i].ConnectionString);
            }
        }

        public static ConnectionStringHelper Instance => _instance ?? (_instance = new ConnectionStringHelper());

    }
}
