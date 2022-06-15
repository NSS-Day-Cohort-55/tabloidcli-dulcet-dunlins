using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using Microsoft.Data.SqlClient;

namespace TabloidCLI.Repositories
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }
        
        public List<Journal> GetAll()
        {
            throw new NotImplementedException();
        }

        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Journal entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime)
                                        Values (@entryTitle, @entryContent, @entryCreateDateTime)";
                    cmd.Parameters.AddWithValue("@entryTitle", entry.Title);
                    cmd.Parameters.AddWithValue("@entryContent", entry.Content);
                    cmd.Parameters.AddWithValue("@entryCreateDateTime", entry.CreateDateTime);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Journal entry)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }



    }
}
