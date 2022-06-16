using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.Repositories
{
    internal class BlogRepository : DatabaseConnector,
        IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base
            (connectionString) { }

        public void Delete(int id)
        {
           using (SqlConnection conn = Connection) 
            { 
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Blog 
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);  

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Blog Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT b.Id AS BlogId,
b.Title,
b.Url,
t.Id AS TagId,
t.Name
FROM Blog b
LEFT JOIN BlogTag bt on b.Id = bt.BlogId
LEFT JOIN Tag t on t.Id = bt.TagId
WHERE b.id = @id;";

                    cmd.Parameters.AddWithValue("@id", id);

                    Blog blog = null;

                    SqlDataReader reader = cmd.ExecuteReader();
                    try 
                    {
                        reader.Read();

                        blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("Url")),

                        };
                    }

                    catch (SqlException)
                    {
                        Console.WriteLine("Invalid");
                    }
                

                    reader.Close();

                    return blog;
                }
            }
        }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                        Id, Title, Url
                                       From Blog";

                    List<Blog> blogs = new List<Blog>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("URL")),
                            
                        };
                        blogs.Add(blog);

                    }
                    reader.Close();
                    return blogs;
                }
            }
        }

        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog
                                       (Title, Url) 
                                        VALUES
                                       (@title, @url)";
                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);


                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void Update(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog 
                                           SET Title = @title,
                                               Url = @url
                                         WHERE id = @id";

                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);
                    cmd.Parameters.AddWithValue("@id", blog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }



}
