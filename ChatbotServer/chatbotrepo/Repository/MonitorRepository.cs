using chatbotrepo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatbotrepo
{
    public class MonitorRepository : IDataFetch<MonitorsTbl>
    {
        #region Private Fields
        private readonly ChatbotEntities _context;
        #endregion

        #region Constructors
        public MonitorRepository()
        {
            _context = new ChatbotEntities();
        }

        public MonitorRepository(ChatbotEntities context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public MonitorsTbl GetById(int id)
        {
            var monitor = (from m in _context.MonitorsTbls
                          where m.monitor_id == id
                          select m).SingleOrDefault();
            return monitor;
        }

        public MonitorsTbl FindWhere(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public ICollection<MonitorsTbl> DataFetch(int id)
        {
            var monitors = (from m in _context.MonitorsTbls
                            where m.monitor_id == id
                            select m).ToList();
            return monitors;
        }

        public ICollection<MonitorsTbl> DataFetch(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}