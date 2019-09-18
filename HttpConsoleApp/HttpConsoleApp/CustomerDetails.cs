using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpConsoleApp
{
    class CustomerDetails
    {
        public void SaveDetails(Customer customer) {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http:" + "//localhost:52413/");

            Console.WriteLine("\nMay I know your name");
            customer.name = Console.ReadLine();

            Console.WriteLine("\nYour contact");
            customer.contact = Console.ReadLine();

            Console.WriteLine("\nYour hospital and address");
            customer.address = Console.ReadLine();
            Console.WriteLine("\nThank you " + customer.name + "!");

            var response3 = client.PostAsJsonAsync("Customer/SaveCustomer", customer).Result;
            if (response3.IsSuccessStatusCode)
            {
                Console.WriteLine("We'll contact you asap.");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response3.StatusCode, response3.ReasonPhrase);
            }
        }
    }
}
