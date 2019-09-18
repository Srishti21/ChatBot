using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatbotrepo
{
    public interface IDataFetch<T>
    {
        T GetById(int id);
        T FindWhere(int id1,int id2);
        ICollection<T> DataFetch(int id);
        ICollection<T> DataFetch(int id1,int id2);
    }
}
