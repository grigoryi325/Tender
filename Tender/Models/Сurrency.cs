using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tender.Models
{
    public class Сurrency
    {
        public int Id { get; set; }
        public string NameСurrency { get; set; }

        public ICollection<Card> Cards { get; set; }
        public Сurrency()
        {
            Cards = new List<Card>();
        }
    }
}