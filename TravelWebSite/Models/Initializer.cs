using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TravelWebSite.Models
{
    /// <summary>
    /// The class allows you to specify how the database initialization
    /// </summary>
    public class Initializer: DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            base.Seed(context);
        }
    }
}