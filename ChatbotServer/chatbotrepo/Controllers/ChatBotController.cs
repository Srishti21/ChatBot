using chatbotrepo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace chatbotrepo.Controllers
{
    public class ChatbotController : Controller
    {
        #region Private Fields
        private readonly IDataFetch<QuestionsTbl> _question;
        private readonly IDataFetch<MonitorsTbl> _monitor;
        private readonly IDataFetch<OptionsTbl> _option;
        #endregion

        #region Default Constructor
        public ChatbotController()
        {
            _question = new QuestionRepository(new ChatbotEntities());
            _option = new OptionsRepository(new ChatbotEntities());
            _monitor = new MonitorRepository(new ChatbotEntities());
        }
        #endregion

        #region Methods
        public string Get()
        {
            string ques1;
            var QuesRow = _question.GetById(1);
            ques1 = QuesRow.question + Environment.NewLine;
            var list = QuesRow.OptionsTbls.ToList();
            foreach (OptionsTbl opt in list)
            {
                ques1 += opt.option_id + ".  " + opt.option + "\n";
            }
            return ques1;
        }

        [System.Web.Mvc.HttpPost]
        public int GetLink(OptionsTbl options)
        {
            var OptRow = _option.FindWhere(options.question_id, options.option_id);
            return OptRow.link_id;
        }

        public string FetchQuestion(OptionsTbl o)
        {
            string fetchQues;
            var QuesRow = _question.GetById(o.link_id);
            fetchQues = QuesRow.question + Environment.NewLine;
            var list = QuesRow.OptionsTbls.ToList();
            foreach (OptionsTbl opt in list)
            {
                fetchQues += opt.option_id + ".  " + opt.option + "\n";
            }
            return fetchQues;
        }

        [System.Web.Http.HttpPost]
        public string MonitorFetch(OptionsTbl o)
        {
            var m = _option.FindWhere(o.question_id, o.option_id);
            var monitor = _monitor.GetById(m.monitor_id);
            return monitor.monitor_name;
        }


        #endregion
    }
}