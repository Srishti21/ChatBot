using chatbotrepo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatbotrepo.Repository
{
   
    public class LinkIdFetch 
    {
        #region Private Fields
        private readonly ChatbotEntities _context;
        #endregion

        #region Constructors
        public LinkIdFetch()
        {
            _context = new ChatbotEntities();
        }

        public LinkIdFetch(ChatbotEntities context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public int DataFetch(int id, int qid)
        {
            var c = (from m in _context.OptionsTbls
                     where (m.option_id == id && m.question_id == qid)
                     select m.link_id).SingleOrDefault();
            return c;
        }
        #endregion
    }
}