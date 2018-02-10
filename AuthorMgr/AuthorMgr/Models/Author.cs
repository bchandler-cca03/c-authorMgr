using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorMgr.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string School { get; set; }

        public string Email { get; set; }
    }
}
