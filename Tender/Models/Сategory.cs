using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tender.Models
{
    public class Сategory
    {
        public int Id { get; set; }
        public string NameCategory { get; set; }

        public ICollection<Card> Cards { get; set; }
        public Сategory()
        {
            Cards = new List<Card>();
        }
    }
}