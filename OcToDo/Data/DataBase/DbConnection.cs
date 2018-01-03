using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcToDo.Data.DataBase
{
    public abstract class DbConnection
    {
        protected OcToDoDataContext DbContext { get; } = new OcToDoDataContext();
    }
}
