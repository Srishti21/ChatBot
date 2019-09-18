using chatbotrepo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatbotrepo.Repository
{
    public class MonitorIdFetch
    {
        #region Private Fields
        private readonly ChatbotEntities _context;
        #endregion

        #region Constructors
        public MonitorIdFetch()
        {
            _context = new ChatbotEntities();
        }

        public MonitorIdFetch(ChatbotEntities context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public int DataFetch(int id, int qid)
        {
            var c = (from m in _context.OptionsTbls
                     where (m.option_id == id && m.question_id == qid)
                     select m.monitor_id).SingleOrDefault();
            return c;
        }
        #endregion

    }
}