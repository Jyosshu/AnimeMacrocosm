using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnimeMacrocosm.Interface;
using AnimeMacrocosm.Models;
using AnimeMacrocosm.Settings;
using Microsoft.Extensions.Options;

namespace AnimeMacrocosm.Repository
{
    public class PostsRepository : IPostRepository
    {
        private readonly AppSettings _appSettings;

        public PostsRepository(IOptionsSnapshot<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public List<Post> GetPosts()
        {
            List<Post> posts = new List<Post>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("Select * FROM Posts", connection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        posts.Add(MapRowToPost(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a general error getting posts.  {ex.Message}");
            }

            return posts;
        }

        public Post GetPostById(int postId)
        {
            Post post = new Post();

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Posts WHERE PostId = @postId");
                    sqlCommand.Parameters.AddWithValue("postId", postId);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        post = MapRowToPost(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a general exception retrieving post: {postId}. {ex.Message}");
            }
            return post;
        }

        private Post MapRowToPost(SqlDataReader reader)
        {
            Post post = new Post()
            {
                PostId = Convert.ToInt32(reader["postId"]),
                PostCreator = Convert.ToString(reader["PostCreator"]),
                PostTitle = Convert.ToString(reader["PostTitle"]),
                PostDate = DateTime.Parse(Convert.ToString(reader["PostDate"])),
                PostContent = Convert.ToString(reader["PostContent"])
            };

            return post;
        }
    }
}
