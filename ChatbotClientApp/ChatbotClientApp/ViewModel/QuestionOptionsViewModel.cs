using ChatbotClientApp.Model;
using System.Net.Http;
using System.Windows.Controls;

namespace ChatbotClientApp.ViewModel
{
    public class QuestionOptionsViewModel : UserControl
    {
        public const string BASE_URL = "http://localhost:52413/";
       
        public async void GetQuestion(int link_id)
        {
            OptionTable optionTable = new OptionTable();
            optionTable.QuestionId = 1;
            optionTable.Linkid = 1;

            string url = string.Format(BASE_URL);


            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
            }

            // return QuestionOptionsViewModel;
        }
    }
}
