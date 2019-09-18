using chatbotrepo.DAL;
using System.Web.Mvc;

namespace chatbotrepo.Controllers
{
    public class CustomerController : Controller
    {

        #region Private Fields
        private IDataPost _customer;
        #endregion

        #region Default Constructor
        public CustomerController()
        {
            _customer = new CustomerRepository(new ChatbotEntities());
        }
        #endregion

        #region Methods
        [System.Web.Http.HttpPost]
        public void SaveCustomer(CustomersTbl customer)
        {
            if (customer.name != null)
            {
                _customer.SaveCustomer(customer);
            }
        }
        #endregion
    }
}
