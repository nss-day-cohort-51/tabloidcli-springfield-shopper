using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;
using TabloidCLI.Models;
using Microsoft.Data.SqlClient;

namespace TabloidCLI.Repositories
{
    class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, Title, Url FROM Blog";

                    List<Blog> blogs = new List<Blog>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Blog blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                            };
                            blogs.Add(blog);
                        }

                        return blogs;
                    }
                }
            }
        }

        public Blog Get(int id)
        {
            throw new NotImplementedException();
        }
        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, Url  )
                                                     VALUES (@title, @url)";
                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(Blog entry)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE from Blog WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
