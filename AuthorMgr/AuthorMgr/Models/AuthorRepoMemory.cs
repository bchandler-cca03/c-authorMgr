using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorMgr.Models
{
    public class AuthorRepoMemory : IAuthorRepository
    {
        private static List<Author> _authors;
        private static int _nextId = 1;

        public AuthorRepoMemory()
        {
            if (_authors == null)
            {
                _authors = new List<Author>();  // if there isn't one, make one!
            }
        }

        public void AddAuthor(Author newAuthor)
        {
            newAuthor.Id = _nextId++;
            _authors.Add(newAuthor);
        }

        public void EditAuthor(Author editAuthor)
        {
            // TODO -- put something here so it blows up.
            return;
        }

        public Author GetById(int id)
        {
            return _authors.Find(a => a.Id == id);  // take each author from list, pass in the 
                                                    // specific objects, and compare a.Id to id
                                                    //  when match made, find has worked.

        }

        public List<Author> ListAll()
        {
            return _authors;
        }
    }
}
