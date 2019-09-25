using ChatbotClientApp.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

namespace ChatbotClientApp
{
    /// <summary>
    /// Interaction logic for ChatbotClient.xaml
    /// </summary>
    public partial class ChatbotClient : Window
    {
        public ChatbotClient()
        {
            InitializeComponent();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http:" + "//localhost:52413/");

            // Add an Accept header for JSON format.    
            client.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("Chatbot/Get").Result;  // Blocking call!    
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(products);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            OptionTable p = new OptionTable();
            p.QuestionId = 1;
            p.Linkid = 1;

            int temp = 1;
            var myContent = JsonConvert.SerializeObject(p);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            while (p.Linkid != 0)
            {

                //option
                Console.WriteLine("Enter an option");
                p.OptionId = Int32.Parse(Console.ReadLine());


                temp = p.Linkid;

                var responses = client.PostAsync("Chatbot/GetLink", byteContent).Result;

                if (responses.IsSuccessStatusCode)
                {
                    var op = responses.Content.ReadAsStringAsync().Result;
                    p.Linkid = Int32.Parse(op);
                    p.QuestionId = p.Linkid;
                    var response2 = client.PostAsync("Chatbot/FetchQuestion", byteContent).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        var question = response2.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(question);
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response2.StatusCode, response2.ReasonPhrase);
                    }

                }
                else
                    Console.Write("Error");
            }


            p.QuestionId = temp;
            var x = client.PostAsync("ChatBot/MonitorFetch", byteContent).Result;
            string monitor = x.Content.ReadAsStringAsync().Result;

            if (monitor != null)
            {
                Console.WriteLine("According to your preferences the suggested patient monitor is: " + monitor);
            }

            Console.WriteLine("\n\nPlease provide us some details to contact you later");

            Customer customer = new Customer();
            customer.monitor = monitor;
            CustomerDetails details = new CustomerDetails();
            details.SaveDetails(customer);
        }
    }
}
