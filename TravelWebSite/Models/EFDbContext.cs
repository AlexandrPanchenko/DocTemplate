using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TravelWebSite.Models
{
    /// <summary>
    /// ///Generating class tables in the database
    /// </summary>
    public class EFDbContext:DbContext
    {
        public EFDbContext() : base("DataBaseConnection")
        {
          Database.SetInitializer<EFDbContext>(new Initializer());
        }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}