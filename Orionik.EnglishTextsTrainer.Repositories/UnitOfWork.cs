using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orionik.EnglishTextsTrainer.Repositories
{
    public class UnitOfWork
    {
        public WordRepository WordRepository { get; set; }
        
        public UnitOfWork(string connection)
        {
            WordRepository = new WordRepository(connection);
        }
    }
}
