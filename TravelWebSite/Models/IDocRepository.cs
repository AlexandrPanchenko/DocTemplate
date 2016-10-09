using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebSite.Models
{
    /// <summary>
    /// ////This interface conteins the functionality to communicate with the database
    /// </summary>
    public interface IDocRepository:IDisposable
    {
        List<Document> GetDocumentsList();
        Document GetDocumentById(int ?id);
        void Create(Document item);
        void Update(Document item);
        void Delete(int ?id);
        void Save();
    }
}
