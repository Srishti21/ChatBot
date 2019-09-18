using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chatbotrepo.DAL;

namespace chatbotrepo
{
    public interface IDataPost
    {
        void SaveCustomer(CustomersTbl customer);
    }
}
