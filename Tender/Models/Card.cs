using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tender.Models
{
    public class Card
    {
        public int Id { get; set; }
        //предмет тендера
        public string Subject { get; set; }
        //описание тендера
        public string Description { get; set; }
        //организатор тренда
        public string Sponsor { get; set; }
        //начальная ставка
        public decimal Rate { get; set; }
        //дата публикации
        public DateTime PublicationDate { get; set; }
        //прием предложений с-по
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int? TypeId { get; set; }
        public Type Type { get; set; }

        public int? СategoryId { get; set; }
        public Сategory Сategory { get; set; }

        public int? СurrencyId { get; set; }
        public Сurrency Сurrency { get; set; }
    }
}