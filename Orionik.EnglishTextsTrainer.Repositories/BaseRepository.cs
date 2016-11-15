using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orionik.EnglishTextsTrainer.Logger;

namespace Orionik.EnglishTextsTrainer.Repositories
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; }

        protected BaseRepository(string connection)
        {
            ConnectionString = connection;

            Logging.Instance.Write(typeof(BaseRepository), "Constructor");
        }
    }
}
