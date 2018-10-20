using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tender.Models
{
    public class TenderContext:DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Сategory> Categories { get; set; }
        public DbSet<Сurrency> Currencies { get; set; }
    }
}