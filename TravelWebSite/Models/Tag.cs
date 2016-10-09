using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWebSite.Models
{
    /// <summary>
    /// This class contains a number of input fields in the document
    /// </summary>
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
    }
}