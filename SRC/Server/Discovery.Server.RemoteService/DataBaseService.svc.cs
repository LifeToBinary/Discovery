using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Discovery.Core.Constants;
using Discovery.Core.Enums;
using Discovery.Core.Model;
using Discovery.Core.Models;

namespace Discovery.Server.RemoteService
{
    /// <summary>
    /// 用于与数据库交互
    /// </summary>
    public class DataBaseService : IDataBaseService
    {
        /// <summary>
        /// 添加一个帖子到数据库
        /// </summary>
        /// <param name="post">即将要被添加到数据库的Post</param>
        /// <returns>数据库为此Post分配的ID</returns>
        public int AddANewPost(Post post)
        {
            // AddANewPost 存储过程的参数名称和参数值的映射表
            var addANewPostParameterValues =
                new Dictionary<string, object>
                {
                    ["@title"] = post.Title,
                    ["@iconPath"] = post.IconPath,
                    ["@url"] = post.Url,
                    ["@creationTime"] = post.CreationTime,
                    ["@lastEditedTime"] = post.LastEditedTime,
                    ["@authorID"] = post.AuthorID,
                    ["@content"] = post.Content,
                    ["@postCategory"] = post.PostCategory
                };
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.AddANewPost;
                foreach (KeyValuePair<string, object> parameter in addANewPostParameterValues)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@newPostID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
                connection.Open();
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.Parameters[8].Value);
            }
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns>数据库连接字符串</returns>
        private string GetDataBaseConnectionString()
            => ConfigurationManager.ConnectionStrings["DiscoveryDataBase"]
                                   .ConnectionString;
        public void UploadFile(byte[] data, string path)
            => File.WriteAllBytes(@"C:\ToBinary\WebFiles\Discovery\DiscoveryWebFiles\Discoverer\Images/" + path, data);
        /// <summary>
        /// 取消关注一个用户
        /// </summary>
        /// <param name="funsID">关注者ID</param>
        /// <param name="idolID">被关注者ID</param>
        public void CancelConcern(int funsID, int idolID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.CancelConcern;
                command.Parameters.AddWithValue("@funsID", funsID);
                command.Parameters.AddWithValue("@idolID", idolID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 取消收藏一个帖子
        /// </summary>
        /// <param name="discovererID">收藏者ID</param>
        /// <param name="postId">帖子ID</param>
        public void CancelFavorite(int discovererID, int postId)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.CancelFavorite;
                command.Parameters.AddWithValue("@discovererID", discovererID);
                command.Parameters.AddWithValue("@postID", postId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 关注一个用户
        /// </summary>
        /// <param name="funsID">关注者ID</param>
        /// <param name="idolID">被关注者ID</param>
        public void ConcernADiscoverer(int funsID, int idolID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ConcernADiscoverer;
                command.Parameters.AddWithValue("@funsID", funsID);
                command.Parameters.AddWithValue("@idolID", idolID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 检查一个用户是否存在
        /// </summary>
        /// <param name="discovererSignInName">用户名</param>
        /// <returns>True: 已经存在 False: 不存在</returns>
        public bool DiscovererIsExists(string discovererSignInName)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.CheckUserNameIsExists;
                var parameters = new SqlParameter[]
                {
                    new SqlParameter
                    {
                        ParameterName = @"userName",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 16,
                        Value = discovererSignInName
                    },
                    new SqlParameter
                    {
                        ParameterName = "@isExists",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    }
                };
                command.Parameters.AddRange(parameters);
                connection.Open();
                command.ExecuteNonQuery();
                return Convert.ToBoolean(parameters[1].Value);
            }
        }

        /// <summary>
        /// 收藏一个帖子
        /// </summary>
        /// <param name="discovererID">收藏者ID</param>
        /// <param name="postID">帖子ID</param>
        public void FavoriteAPost(int discovererID, int postID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.FavoriteAPost;
                command.Parameters.AddWithValue("@discovererID", discovererID);
                command.Parameters.AddWithValue("@postID", postID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 搜索帖子
        /// </summary>
        /// <param name="postTitle">帖子标题</param>
        /// <returns>搜索到的帖子列表</returns>
        public IEnumerable<Post> FindPostFromAllDiscoverers(string postTitle)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.SearchPostsFromAllDiscoverers;
                command.Parameters.AddWithValue("@postTitle", postTitle);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return CreatePostFromSqlDataReader(reader);
                    }
                }
            }
        }

        /// <summary>
        /// 读取一个 SqlDataReader 的数据以创建Post对象
        /// </summary>
        /// <param name="reader">一个用于读取数据的 SqlDataReader 实例</param>
        /// <returns>一个新的 Post 对象</returns>
        private Post CreatePostFromSqlDataReader(SqlDataReader reader)
            => new Post
            {
                ID = reader.GetInt32(0),
                Title = reader.GetString(1),
                Url = reader.GetString(2),
                CreationTime = reader.GetDateTime(3),
                AuthorID = reader.GetInt32(4),
                Content = reader.GetString(5),
                PostCategory = (Field)reader.GetInt32(6),
                LastEditedTime = reader.GetDateTime(7),
                IconPath = reader.GetString(8)
            };
        /// <summary>
        /// 搜索一个用户的帖子
        /// </summary>
        /// <param name="discovererID">用户ID</param>
        /// <param name="postTitle">帖子标题</param>
        /// <returns>搜索到的帖子列表</returns>
        public IEnumerable<Post> FindPostsOfTheDiscoverer(int discovererID, string postTitle)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.FindPostsOfTheDiscoverer;
                command.Parameters.AddWithValue("@discovererID", discovererID);
                command.Parameters.AddWithValue("@postTitle", postTitle);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return CreatePostFromSqlDataReader(reader);
                }
            }
        }

        /// <summary>
        /// 获取一个用户所收藏的所有帖子
        /// </summary>
        /// <param name="discovererID"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetFavoritePosts(int discovererID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetFavoritePosts;
                command.Parameters.AddWithValue("@discovererID", discovererID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return CreatePostFromSqlDataReader(reader);
                }
            }
        }

        /// <summary>
        /// 获取一个帖子被收藏的次数
        /// </summary>
        /// <param name="postID">帖子ID</param>
        /// <returns>此帖子被收藏的次数</returns>
        public int GetFavoritesCountOfThePost(int postID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetFavoritesCountOfThePost;
                command.Parameters.AddWithValue("@postID", postID);
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@count",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
                connection.Open();
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.Parameters[1].Value);
            }
        }

        /// <summary>
        /// 获取一个用户的粉丝数量
        /// </summary>
        /// <param name="idolID">用户ID</param>
        /// <returns>粉丝数量</returns>
        public int GetFunsCountOfTheIdol(int idolID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetFunsCount;
                command.Parameters.AddWithValue("@idolID", idolID);
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@funsCount",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
                connection.Open();
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.Parameters[1].Value);
            }
        }

        /// <summary>
        /// 获取一个用户的所有粉丝
        /// </summary>
        /// <param name="idolID">用户ID</param>
        /// <returns>此用户的所有粉丝</returns>
        public IEnumerable<Discoverer> GetFunsOfTheIdol(int idolID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetFunsOfTheIdol;
                command.Parameters.AddWithValue("@idolID", idolID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return CreateDiscovererFromSqlDataReader(reader);
                }
            }
        }

        /// <summary>
        /// 读取一个 SqlDataReader 的数据以创建 Discoverer 对象
        /// </summary>
        /// <param name="reader">一个用于读取数据的 SqlDataReader 实例</param>
        /// <returns>一个新的 Discoverer 对象</returns>
        private Discoverer CreateDiscovererFromSqlDataReader(SqlDataReader reader)
            => new Discoverer
            {
                BasicInfo = new BasicInfo
                {
                    ID = reader.GetInt32(0),
                    SignInName = reader.GetString(1),
                    Password = reader.GetString(2),
                    Sex = (Sex)reader.GetInt32(3),
                    AreaOfInterest = (Field)reader.GetInt32(4),
                    SignUpTime = reader.GetDateTime(5),
                    AvatarPath = reader.GetString(6),
                    ProfileBackgroundImagePath = reader.GetString(7)
                },
                ContactInfo = new ContactInfo
                {
                    Email = reader.IsDBNull(8) ? null : reader.GetString(8),
                    QQ = reader.IsDBNull(9) ? null : reader.GetString(9),
                    WeChat = reader.IsDBNull(10) ? null : reader.GetString(10),
                    BlogAddress = reader.IsDBNull(11) ? null : reader.GetString(11)
                }
            };

        /// <summary>
        /// 获取一个用户的帖子数量
        /// </summary>
        /// <param name="discovererID">用户ID</param>
        /// <returns>此用户的帖子数量</returns>
        public int GetPostsCountOfTheDiscoverer(int discovererID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetPostsCountOfTheDiscoverer;
                command.Parameters.AddWithValue("@discovererID", discovererID);
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@postCount",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
                connection.Open();
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.Parameters[1].Value);
            }
        }

        /// <summary>
        /// 获取一个用户的所有帖子
        /// </summary>
        /// <param name="discovererId">用户ID</param>
        /// <returns>此用户的所有帖子</returns>
        public IEnumerable<Post> GetPostsOfTheDiscoverer(int discovererId)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetPostsOfTheDiscoverer;
                command.Parameters.AddWithValue("@id", discovererId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return CreatePostFromSqlDataReader(reader);
                }
            }
        }

        /// <summary>
        /// 获取一个用户可能感兴趣的所有帖子
        /// </summary>
        /// <param name="discovererID">用户ID</param>
        /// <returns>用户可能感兴趣的帖子</returns>
        public IEnumerable<Post> GetRecommendedForTheDiscoverer(int discovererID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetRecommendedForTheDiscoverer;
                command.Parameters.AddWithValue("@discovererID", discovererID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return CreatePostFromSqlDataReader(reader);
                }
            }
        }

        /// <summary>
        /// 检查一个用户是否收藏了一个帖子
        /// </summary>
        /// <param name="discovererID">用户ID</param>
        /// <param name="postID">帖子ID</param>
        /// <returns>True: 帖子已经被此用户已经收藏过了</returns>
        public bool IsFavoritedAPost(int discovererID, int postID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.IsFavoritedAPost;
                command.Parameters.AddWithValue("@discovererID", discovererID);
                command.Parameters.AddWithValue("@postID", postID);
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@isFavorited",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
                connection.Open();
                command.ExecuteNonQuery();
                return Convert.ToBoolean(command.Parameters[2].Value);
            }
        }

        /// <summary>
        /// 检查一个用户是否是另一个用户的粉丝
        /// </summary>
        /// <param name="funsID">粉丝ID</param>
        /// <param name="idolID">被关注者ID</param>
        /// <returns>True: 已经是另一个用户的粉丝, 否则 False</returns>
        public bool IsFuns(int funsID, int idolID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.IsFuns;
                command.Parameters.AddWithValue("@funsID", funsID);
                command.Parameters.AddWithValue("@idolID", idolID);
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@isFuns",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
                connection.Open();
                command.ExecuteNonQuery();
                return Convert.ToBoolean(command.Parameters[2].Value);
            }
        }

        /// <summary>
        /// 删除一个帖子
        /// </summary>
        /// <param name="postID"></param>
        public void RemoveAPost(int postID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.RemoveAPost;
                command.Parameters.AddWithValue("@postID", postID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 以用户名搜索搜索用户
        /// </summary>
        /// <param name="discovererSignInName">用户名</param>
        /// <returns>搜索到的用户列表</returns>
        public IEnumerable<Discoverer> SearchDiscoverers(string discovererSignInName)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.SearchDiscoverers;
                command.Parameters.AddWithValue("@discovererName", discovererSignInName);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return CreateDiscovererFromSqlDataReader(reader);
                }
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="signInName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>用户实例</returns>
        public Discoverer SignIn(string signInName, string password)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.SignIn;
                var parameters = new SqlParameter[]
                {
                    new SqlParameter
                    {
                        ParameterName = "@signInName",
                        SqlDbType = SqlDbType.VarChar,
                        Size = 16,
                        Value = signInName
                    },
                    new SqlParameter
                    {
                        ParameterName = "@password",
                        SqlDbType = SqlDbType.VarChar,
                        Size = 16,
                        Value = password
                    }
                };
                command.Parameters.AddRange(parameters);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    return null;
                }

                return new Discoverer
                {
                    BasicInfo = new BasicInfo
                    {
                        ID = reader.GetInt32(0),
                        SignInName = reader.GetString(1),
                        Password = reader.GetString(2),
                        Sex = (Sex)reader.GetInt32(3),
                        AreaOfInterest = (Field)reader.GetInt32(4),
                        SignUpTime = reader.GetDateTime(5),
                        AvatarPath = reader.GetString(6),
                        ProfileBackgroundImagePath = reader.GetString(7)
                    },
                    ContactInfo = new ContactInfo
                    {
                        Email = reader.IsDBNull(8) ? String.Empty : reader.GetString(8),
                        QQ = reader.IsDBNull(9) ? String.Empty : reader.GetString(9),
                        WeChat = reader.IsDBNull(10) ? String.Empty : reader.GetString(10),
                        BlogAddress = reader.IsDBNull(11) ? String.Empty : reader.GetString(11)
                    }
                };
            }
        }

        /// <summary>
        /// 更新用户资料
        /// </summary>
        /// <param name="discoverer">用户资料</param>
        public void UpdateDiscovererInfo(Discoverer discoverer)
        {
            // UpdateDiscovererProfile 存储过程的参数名称和参数值的映射表
            var updateDiscovererProfileParameterValues =
                new Dictionary<string, object>
                {
                    ["@id"] = discoverer.BasicInfo.ID,
                    ["@signInName"] = discoverer.BasicInfo.SignInName,
                    ["@password"] = discoverer.BasicInfo.Password,
                    ["@sex"] = discoverer.BasicInfo.Sex,
                    ["@areaOfInterest"] = discoverer.BasicInfo.AreaOfInterest,
                    ["@signUpTime"] = discoverer.BasicInfo.SignUpTime,
                    ["@avatarPath"] = discoverer.BasicInfo.AvatarPath,
                    ["@profileBackgroundImagePath"] = discoverer.BasicInfo.ProfileBackgroundImagePath,
                    ["@email"] = discoverer.ContactInfo.Email,
                    ["@qq"] = discoverer.ContactInfo.QQ,
                    ["@weChat"] = discoverer.ContactInfo.WeChat,
                    ["@blogAddress"] = discoverer.ContactInfo.BlogAddress
                };
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.UpdateDiscovererProfile;

                foreach (KeyValuePair<string, object> parameter in
                         updateDiscovererProfileParameterValues)
                {
                    command.Parameters
                           .AddWithValue(parameter.Key, parameter.Value);
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 更新帖子信息
        /// </summary>
        /// <param name="post"></param>
        public void UpdatePostInfo(Post post)
        {
            // UpdatePostInfo 存储过程的参数名称和参数值的映射表
            var updatePostInfoParameterValues =
                new Dictionary<string, object>
                {
                    ["@id"] = post.ID,
                    ["@title"] = post.Title,
                    ["@url"] = post.Url,
                    ["@creationTime"] = post.CreationTime,
                    ["@authorID"] = post.AuthorID,
                    ["@content"] = post.Content,
                    ["@postCategory"] = post.PostCategory,
                    ["@lastEditedTime"] = post.LastEditedTime,
                    ["@iconPath"] = post.IconPath
                };

            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.UpdatePostInfo;
                foreach (KeyValuePair<string, object> parameter in updatePostInfoParameterValues)
                {
                    command.Parameters
                           .AddWithValue(parameter.Key, parameter.Value);
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 获取一个用户关注的所有用户的列表
        /// </summary>
        /// <param name="funsID">用户ID</param>
        /// <returns>Ta 关注的所有用户的列表</returns>
        public IEnumerable<Discoverer> GetIdols(int funsID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetIdols;
                command.Parameters.AddWithValue("@discovererID", funsID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return CreateDiscovererFromSqlDataReader(reader);
                }
            }
        }

        /// <summary>
        /// 获取一个用户关注的人数
        /// </summary>
        /// <param name="funsID">用户ID</param>
        /// <returns>Ta 关注的人数</returns>
        public int GetIdolsCount(int funsID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetIdolsCount;
                command.Parameters.AddWithValue("@discovererID", funsID);
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@idolCount",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
                connection.Open();
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.Parameters[1].Value);
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="signInName">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="areaOfInterest">感兴趣的领域</param>
        public void SignUp(string signInName,
                           string password,
                           string email,
                           Field areaOfInterest)
        {
            var SignUpParameterValues
                = new Dictionary<string, object>
                {
                    ["@signInName"] = signInName,
                    ["@password"] = password,
                    ["@email"] = email,
                    ["@areaOfInterest"] = areaOfInterest
                };

            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.SignUp;
                foreach (KeyValuePair<string, object> parameter in SignUpParameterValues)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 通过 ID 查找用户
        /// </summary>
        /// <param name="discovererID">用户ID</param>
        /// <returns>查找到的用户或null(找不到此用户)</returns>
        public Discoverer GetDiscovererByID(int discovererID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetDiscovererByID;
                command.Parameters.AddWithValue("@id", discovererID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                return reader.Read()
                       ? CreateDiscovererFromSqlDataReader(reader)
                       : null;
            }
        }

        /// <summary>
        /// 查询一个用户和另一个用户关注的人的关系
        /// </summary>
        /// <param name="userID">此用户的ID</param>
        /// <param name="anotherUserID">另一个用户的ID</param>
        /// <returns>关系键值对列表</returns>
        public IEnumerable<KeyValuePair<Discoverer, bool>> ThisUsersRelationshipWithAnotherUsersIdols(
            int userID, 
            int anotherUserID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ThisUsersRelationshipWithAnotherUsersIdols;
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@anotherUserID", anotherUserID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new KeyValuePair<Discoverer, bool>(
                        CreateDiscovererFromSqlDataReader(reader),
                        reader.IsDBNull(12) ? false : true);
                }
            }
        }

        /// <summary>
        /// 查询一个用户和另一个用户的粉丝的关系
        /// </summary>
        /// <param name="userID">此用户的ID</param>
        /// <param name="anotherUserID">另一个用户的ID</param>
        /// <returns>关系键值对列表</returns>
        public IEnumerable<KeyValuePair<Discoverer, bool>> ThisUsersRelationshipWithAnotherUsersFuns(int userID, int anotherUserID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ThisUsersRelationshipWithAnotherUsersFuns;
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@anotherUserID", anotherUserID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new KeyValuePair<Discoverer, bool>(
                        CreateDiscovererFromSqlDataReader(reader),
                        reader.IsDBNull(12) ? false : true);
                }
            }
        }

        /// <summary>
        /// 查询一个用户与另一个用户发布的帖子的关系列表
        /// </summary>
        /// <param name="userID">此用户ID</param>
        /// <param name="anotherUserID">另一个用户ID</param>
        /// <returns>关系列表键值对列表</returns>
        public IEnumerable<KeyValuePair<Post, bool>> ThisUsersRelationshipWithAnotherUsersPostedPosts(int userID, int anotherUserID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ThisUsersRelationshipWithAnotherUsersPostedPosts;
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@anotherUserID", anotherUserID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new KeyValuePair<Post, bool>(
                        CreatePostFromSqlDataReader(reader),
                        reader.IsDBNull(9) ? false : true);
                }
            }
        }

        /// <summary>
        /// 查询一个用户与另一个用户收藏的帖子的关系列表
        /// </summary>
        /// <param name="userID">此用户ID</param>
        /// <param name="anotherUserID">另一个用户ID</param>
        /// <returns>关系列表键值对列表</returns>
        public IEnumerable<KeyValuePair<Post, bool>> ThisUsersRelationshipWithAnotherUsersFavoritedPosts(int userID, int anotherUserID)
        {
            using (var connection = new SqlConnection(GetDataBaseConnectionString()))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ThisUsersRelationshipWithAnotherUsersFavoritedPosts;
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@anotherUserID", anotherUserID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new KeyValuePair<Post, bool>(
                        CreatePostFromSqlDataReader(reader),
                        reader.IsDBNull(9) ? false : true);
                }
            }
        }
    }
}