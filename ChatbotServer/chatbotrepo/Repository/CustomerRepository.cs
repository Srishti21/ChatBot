using chatbotrepo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace chatbotrepo
{
    public class CustomerRepository:IDataPost
    {
        #region Private Fields
        private readonly ChatbotEntities _context;
        #endregion

        #region Constructor
        public CustomerRepository()
        {
            _context = new ChatbotEntities();
        }
        public CustomerRepository(ChatbotEntities context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public void SaveCustomer(CustomersTbl customer)
        {
               _context.CustomersTbls.Add(customer);
               _context.SaveChanges();
        }
        #endregion
    }
}