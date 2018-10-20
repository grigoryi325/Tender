using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tender.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string NameType { get; set; }
        //в одном виде тендера может быть много тендеров(связь один ко многим) 
        public ICollection<Card> Cards { get; set; }
        public Type()
        {
            Cards = new List<Card>();
        }
    }
}