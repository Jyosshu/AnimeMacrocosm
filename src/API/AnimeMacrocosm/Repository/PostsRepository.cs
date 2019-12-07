using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using AnimeMacrocosm.Interface;
using AnimeMacrocosm.Models;
using AnimeMacrocosm.Settings;
using Microsoft.Extensions.Options;
using Npgsql;
using Dapper;
using System.Runtime.InteropServices;

namespace AnimeMacrocosm.Repository
{
    public class PostsRepository : IPostRepository
    {
        private readonly AppSettings _appSettings;
        private bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public PostsRepository(IOptionsSnapshot<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public IDbConnection OpenConnection()
        {
            IDbConnection connection;

            if (isWindows == true)
            {
                connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection);
            }
            else
            {
                connection = new NpgsqlConnection(_appSettings.ConnectionStrings.PostgresConnection);
            }

            connection.Open();

            return connection;
        }

        public List<Post> GetAllPosts()
        {
            List<Post> posts = new List<Post>();

            try
            {
                using (var connection = OpenConnection())
                {
                    posts = connection.Query<Post, User, Post>(GET_ALL_POST_SELECT_QUERY + " Order By p.PostDate DESC",
                        map: (post, user) =>
                        {
                            post.User = user;
                            return post;
                        },
                        splitOn: "UserId").AsList();
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
            Post userPost = new Post();

            try
            {
                using (var connection = OpenConnection())
                {
                    //userPost = connection.Query<Post, User, Post>(GET_ALL_POST_SELECT_QUERY + " WHERE PostId = @PostId",
                    //    map: (post, user) =>
                    //    {
                    //        post.User = user;
                    //        return post;
                    //    },
                    //    param: new { PostId = postId },
                    //     splitOn: "UserId");

                    //SqlCommand sqlCommand = new SqlCommand($"{GET_ALL_POST_SELECT_QUERY} WHERE PostId = @postId", connection);
                    //sqlCommand.Parameters.Add("@postId", SqlDbType.Int);
                    //sqlCommand.Parameters["@postId"].Value = postId;

                    //using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    //{
                    //    if (reader.HasRows)
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            post = MapRowToPost(reader);
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a general exception retrieving post: {postId}. {ex.Message}");
            }
            return userPost;
        }

        public List<Post> GetPostsByUserId(int userId)
        {
            List<Post> posts = new List<Post>();

            using (var connection = OpenConnection())
            {
                posts = connection.Query<Post>(GET_ALL_POST_SELECT_QUERY + " WHERE u.UserId = @UserId Order By p.PostDate DESC", new { UserId = userId }).AsList();

                //SqlCommand sqlCommand = new SqlCommand($"{GET_ALL_POST_SELECT_QUERY} Where u.UserId = @userId Order By p.PostDate DESC", connection);
                //sqlCommand.Parameters.AddWithValue("@userId", userId);

                //using (SqlDataReader reader = sqlCommand.ExecuteReader())
                //{
                //    if (reader.HasRows)
                //    {
                //        while (reader.Read())
                //        {
                //            posts.Add(MapRowToPost(reader));
                //        }
                //    }
                //}
            }

                return posts;
        }

        private readonly string GET_ALL_POST_SELECT_QUERY = @"SELECT p.PostId
, p.PostTitle
, p.PostDate
, p.PostContent
, u.UserId
, u.UserEmailAddress
, u.UserScreenName
FROM Posts p
INNER JOIN Users u ON u.UserId = p.UserId";
    }
}
