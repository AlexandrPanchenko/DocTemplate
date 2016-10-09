// <copyright company="WebChallenge 2016" file="Documents.cs">
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelWebSite.Models
{
    /// <summary>
    /// /////This class contain all information about document
    /// </summary>
    public class Document
    {
        public Document()
        {
            Tags = new List<Tag>();
        }
        public int DocumentID { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(20, ErrorMessage = "Длина данніх не может превышать 20 символов")]
        public string Title { get; set; }
        [Display(Name = "Кількість роздрукованих")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Text { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}