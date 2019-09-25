using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using DataModelLib;
using Newtonsoft.Json;

namespace QuestionAnswerViewModelLib
{
    public class QuestionAnswerViewModel
    {
        public const string BASE_URL = "http://localhost:52413/";       
      

        #region Initializers
        public QuestionAnswerViewModel()
        {
            this.NextQuestionCommand = new MVVMUtilityLib.DelegateCommand(
               (object obj) => { GetQuestion(); },
               (object obj) => { return true; });
        }
        #endregion

        #region Private Feilds
        OptionTable _optionTableRef = new OptionTable();
        #endregion

        #region Properties
        public int OptionId
        {
            get { return this._optionTableRef.OptionId; }
            set { this._optionTableRef.OptionId = value; }
        }

        public int QuestionId
        {
            get { return this._optionTableRef.QuestionId; }
            set { this._optionTableRef.QuestionId = value; }
        }

        public int LinkId
        {
            get { return this._optionTableRef.LinkId; }
            set { this._optionTableRef.LinkId = value; }
        }
        #endregion

        #region Commands
        public ICommand NextQuestionCommand { get; set; }
        #endregion

        #region ViewLogic
        public async Task<OptionTable> GetQuestion()
        {
            string url = string.Format(BASE_URL);
            OptionTable result = new OptionTable();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url+"Chatbot/Get");
                string json = await response.Content.ReadAsStringAsync();
               result = JsonConvert.DeserializeObject<OptionTable>(json);
            }
            // HttpResponseMessage responses = await client.GetAsync(BASE_URL);
            //string res = await responses.Content.ReadAsStringAsync();
            return result;
        }
        #endregion
    }
}
