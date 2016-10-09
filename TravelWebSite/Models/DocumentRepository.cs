using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TravelWebSite.Models
{
    /// <summary>
    /// /////This class contains a repository for interaction with entities
    /// </summary>
    public class DocumentRepository : IDocRepository
    {
        private EFDbContext _db;
        public DocumentRepository()
        {
            _db = new EFDbContext();
        }
        // <summary>
        /// Method create a new document in the database
        /// </summary>
        public void Create(Document item)
        {
            _db.Documents.Add(item);
            _db.SaveChanges();
        }
        // <summary>
        /// Method deletes the document in the database
        /// </summary>
        public void Delete(int ?id)
        {
            Document document = _db.Documents.Find(id);
            _db.Documents.Remove(document);
            _db.SaveChanges();
        }
        /// <summary>
        /// The method saves all changes to the database
        /// </summary>

        public void Save()
        {
            _db.SaveChanges();
        }
        // <summary>
        /// Method updates the document in the database
        /// </summary>
        public void Update(Document item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
       
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                disposedValue = true;
            }
        }


        public void Dispose()
        {
          
            Dispose(true); 
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// This method returns document by id
        /// </summary>
        public Document GetDocumentById(int ?id)
        {
            return _db.Documents.Find(id);
        }
        /// <summary>
        /// This method returns all the documents from the database
        /// </summary>
        public List<Document> GetDocumentsList()
        {
            return _db.Documents.ToList();
        }


        #endregion
    }
}