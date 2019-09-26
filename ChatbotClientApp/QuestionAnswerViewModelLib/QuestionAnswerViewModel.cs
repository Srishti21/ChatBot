using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DataModelLib;
using Newtonsoft.Json;

namespace QuestionAnswerViewModelLib
{
    public class QuestionAnswerViewModel:INotifyPropertyChanged
    {
        #region Declarations

        

        
        private string _monitor;
        private HttpClient _client;
        public const string BASE_URL = "http://localhost:52413/";
        private int _next;
        private string _question;
        private ObservableCollection<string> _optionsCollection;
        private string _selectedOption;
        private int _cur;
        private Stack<int> _previousQuestions;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Initializers

        public QuestionAnswerViewModel()
        {
            _optionsCollection = new ObservableCollection<string>();
            _previousQuestions = new Stack<int>();

            this.NextQuestionCommand = new MVVMUtilityLib.DelegateCommand(
                (object obj) => { GetNextQuestion(); },
                (object obj) => { return true; });
            this.PrevQuestionCommand = new MVVMUtilityLib.DelegateCommand(
                (object obj) => { GetPrevQuestion(); },
                (object obj) => { return true; });
            FirstQuestion();

        }

        #endregion

        #region Private Fields

        public string Monitor
        {
            get { return _monitor; }
            set { _monitor = value; }
        }

        public int Current
        {
            get { return _cur; }
            set { _cur = value; }
        }

        OptionTable _optionTableRef = new OptionTable();

        #endregion

        #region Properties

        public string SelectedOption
        {
            get { return _selectedOption; }
            set { _selectedOption = value; }
        }

        public string Message { get; set; }

        public ObservableCollection<string> Options
        {
            get { return _optionsCollection; }
            set { _optionsCollection = value; }
        }

        public string Question
        {
            get { return _question; }
            set
            {
                if (_question!=value)
                {
                    _question = value;
                    OnPropertyChanged("Question");
                }
                
            }
        }

        public int OptionId
        {
            get { return this._optionTableRef.option_id; }
            set { this._optionTableRef.option_id = value; }
        }

        public int QuestionId
        {
            get { return this._optionTableRef.question_id; }
            set { this._optionTableRef.question_id = value; }
        }

        public int LinkId
        {
            get { return this._optionTableRef.link_id; }
            set { this._optionTableRef.link_id = value; }
        }

        public HttpClient Client
        {
            get { return _client; }
            set { _client = value; }
        }

        #endregion

        #region Commands
        public ICommand NextQuestionCommand { get; set; }
        public ICommand PrevQuestionCommand { get; set; }
        #endregion

        #region ViewLogic
        public void FirstQuestion()
        {
            GetClient();
            HttpResponseMessage response = Client.GetAsync("Chatbot").Result;  // Blocking call!    
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                SplitQuestionAndAnswer(products);


            }

            QuestionId = 1;
            LinkId = 1;
        
        }

        public void SplitQuestionAndAnswer(string products)
        {
            Options.Clear();
            Dictionary<int, string> options = JsonConvert.DeserializeObject<Dictionary<int, string>>(products);

            Question = options[0];

            for (int i = 1; i < options.Count; i++)
            {
                Options.Add(i + " " + options[i]);
            }
        }
        void OnPropertyChanged([CallerMemberName]String propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        public void GetClient()
        {
            Client = new HttpClient { BaseAddress = new Uri(BASE_URL) };
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void GetNextQuestion()
        { 
            GetClient();
            _previousQuestions.Push(QuestionId);
            if (LinkId != 0)
            {
               
               GetQuestionByLink();
            }
            else
            {
                ShowMonitor();
            }

        }

        public void GetQuestionByLink()
        {
            OptionId = int.Parse(SelectedOption[0].ToString());
            var responses = Client.PostAsJsonAsync("Chatbot/GetLink", _optionTableRef).Result;
            if (responses.IsSuccessStatusCode)
            {
                var op = responses.Content.ReadAsStringAsync().Result;
                LinkId = Int32.Parse(op);
                QuestionId = LinkId;
                GetQuestion();
            }
        }

        public void ShowMonitor()
        {
            QuestionId = _previousQuestions.Pop();
            QuestionId = _previousQuestions.Pop();
            var responseMessage = Client.PostAsJsonAsync("ChatBot/MonitorFetch", _optionTableRef).Result;
            Monitor = responseMessage.Content.ReadAsStringAsync().Result;
            if (Monitor != null)
            {
                Question = "SUGGESTED MONITOR ::" + Monitor;

            }
            _previousQuestions.Push(QuestionId);
        }


        public void GetPrevQuestion()
        {
            LinkId = _previousQuestions.Pop();
            GetClient();
            if(LinkId!=1) GetQuestion();
            else FirstQuestion();
        }

        public void GetQuestion()
        {
            var response = Client.PostAsJsonAsync<OptionTable>("Chatbot/FetchQuestion", _optionTableRef).Result;
            if (response.IsSuccessStatusCode)
            {
                var question = response.Content.ReadAsStringAsync().Result;
                SplitQuestionAndAnswer(question);
            }
        }
        #endregion
    }
}
