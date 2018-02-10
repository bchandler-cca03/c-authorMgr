using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorMgr.Models
{
    public class AuthorRepoADO : IAuthorRepository
    {
       private readonly ILogger<AuthorRepoADO> _log;

        private string connStr = "Server=(localdb)\\mssqllocaldb;Database=aspnet-AuthorMgr-838AA152-102F-4D9A-A3C7-83CA67A40567;Trusted_Connection=True;MultipleActiveResultSets=true";

        private const string selectQuery = "SELECT Id, FName, LName, School, Email\n"
            + "FROM Authors\n";

        private const string selectById = "WHERE Id = @id\n";

        private const string orderByName = "ORDER BY LName, FName\n";

        private const string insertAuthorQuery = "INSERT INTO Authors\n"
           + "(FName, LName, School, Email)\n"
           +  "values (@fname, @lname, @school, @email)\n";

        // return to
        private const string editAuthorQuery = "UPDATE Authors\n"
           + "set FName = @fname, LName = @lname, School = @school, email = @email\n"
           + "where Id = @id\n";

        public AuthorRepoADO(ILogger<AuthorRepoADO> log)
        {
            _log = log;
        }

        public void AddAuthor(Author newAuthor)
        {
            using(var conn = new SqlConnection(connStr))
            {
                try
                {
                    var command = new SqlCommand(insertAuthorQuery, conn);

                    command.Parameters.AddWithValue("@fname", newAuthor.LName);
                    command.Parameters.AddWithValue("@lname", newAuthor.FName);
                    command.Parameters.AddWithValue("@school", newAuthor.School);
                    command.Parameters.AddWithValue("@email", newAuthor.Email);

                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Todo:  add error logging
                    _log.LogError(ex, "Error Adding Author {fname}{lname}", newAuthor.FName, newAuthor.LName);
                }
            }
        }
        public void EditAuthor(Author editAuthor)
        {
            using (var conn = new SqlConnection(connStr))
            {
                try
                {
                    var command = new SqlCommand(editAuthorQuery, conn);

                    command.Parameters.AddWithValue("@fname", editAuthor.LName);
                    command.Parameters.AddWithValue("@lname", editAuthor.FName);
                    command.Parameters.AddWithValue("@school", editAuthor.School);
                    command.Parameters.AddWithValue("@email", editAuthor.Email);
                    command.Parameters.AddWithValue("@id", editAuthor.Id);

                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Todo:  add error logging
                    _log.LogError(ex, "Error Editing Author {fname}{lname}", editAuthor.FName, editAuthor.LName);
                }
            }
        }

        public Author GetById(int id)
        {
            Author author = new Author();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(selectQuery + selectById, conn);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        author.Id = int.Parse(reader[0].ToString());
                        author.LName = reader[1].ToString();
                        author.FName = reader[2].ToString();
                        author.School = reader[3].ToString();
                        author.Email = reader[4].ToString();
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, "Error Adding Author by Id: {id}", author.Id);
                   
                }
            }
            return author;
        }
        




        public List<Author> ListAll()
        {
            List<Author> authors = new List<Author>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(selectQuery + orderByName, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // var foo = reader.GetInt64(0);
                        Author newAuthor = new Author
                        { 
                        // incoming as an int from the database
                        // reader[0].ToString converts to a string
                        // Parse converts from a string to an int
                        // it's not an int from the database
                        // the reader a list which does not know the type of data enclosed
                        // reader must be able to take anything from the database
                        // the reader does not know what the data is until it receives it
                        // the reader[1] object and all objects have a ToString method
                        // 

                        // Id = int.Parse(reader[0].ToString()),
                            Id = reader.GetInt32(0),
                            LName = reader[1].ToString(),
                            FName = reader[2].ToString(),
                            School = reader[3].ToString(),
                            Email = reader.GetString(4)
                        };       
                        authors.Add(newAuthor);
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, "Error Loading List of Authors");
                }

            }
            return authors;
        }
    }
}
