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

        public List<Post> GetAllPosts()
        {
            List<Post> posts = new List<Post>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand($"{_getAllPostQuery} Order By p.PostDate DESC", connection);
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

                    SqlCommand sqlCommand = new SqlCommand($"{_getAllPostQuery} WHERE PostId = @postId", connection);
                    sqlCommand.Parameters.AddWithValue("@postId", postId);
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

        public List<Post> GetPostsByUserId(int userId)
        {
            List<Post> posts = new List<Post>();

            using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand($"{_getAllPostQuery} Where u.UserId = @userId Order By p.PostDate DESC", connection);
                sqlCommand.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    posts.Add(MapRowToPost(reader));
                }
            }

                return posts;
        }

        private Post MapRowToPost(SqlDataReader reader)
        {
            Post post = new Post()
            {
                PostId = Convert.ToInt32(reader["PostId"]),
                PostTitle = Convert.ToString(reader["PostTitle"]),
                PostDate = DateTime.Parse(Convert.ToString(reader["PostDate"])),
                PostContent = Convert.ToString(reader["PostContent"]),
                ApplicationUserRefId = Convert.ToInt32(reader["UserId"]),
                Users = new ApplicationUser()
                {
                    UserEmailAddress = Convert.ToString(reader["UserEmailAddress"]),
                    UserScreenName = Convert.ToString(reader["UserScreenName"])
                }
            };

            return post;
        }

        private readonly string _getAllPostQuery = @"SELECT p.PostId
, p.PostTitle
, p.PostDate
, p.PostContent
, u.UserId
, u.UserEmailAddress
, u.UserScreenName
FROM Posts p
Inner Join Users u ON u.UserId = p.ApplicationUserRefId";
    }
}
