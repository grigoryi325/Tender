using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tender.Models.PageModel
{
    public class TenderListViewModel
    {
        public IEnumerable<Card> Cards { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}