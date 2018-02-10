using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorMgr.Models
{
    public interface IAuthorRepository
    {
        void AddAuthor(Author newAuthor);

        Author GetById(int d);

        void EditAuthor(Author editAuthor);

        List<Author> ListAll();

    }
}
