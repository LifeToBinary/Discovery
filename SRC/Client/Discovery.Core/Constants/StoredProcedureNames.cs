using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery.Core.Constants
{
    /// <summary>
    /// Discovery 数据库所有存储过程名字的常量表示
    /// </summary>
    public class StoredProcedureNames
    {
        /// <summary>
        /// 添加一个帖子到数据库
        /// </summary>
        public const string AddANewPost = "AddANewPost";

        /// <summary>
        /// 取消关注一个用户
        /// </summary>
        public const string CancelConcern = "CancelConcern";

        /// <summary>
        /// 取消收藏一个帖子
        /// </summary>
        public const string CancelFavorite = "CancelFavorite";

        /// <summary>
        /// 关注一个用户
        /// </summary>
        public const string ConcernADiscoverer = "ConcernADiscoverer";

        /// <summary>
        /// 检查一个用户是否存在
        /// </summary>
        public const string CheckUserNameIsExists = "CheckUserNameIsExists";

        /// <summary>
        /// 收藏一个帖子
        /// </summary>
        public const string FavoriteAPost = "FavoriteAPost";

        /// <summary>
        /// 搜索帖子
        /// </summary>
        public const string SearchPostsFromAllDiscoverers = "SearchPostsFromAllDiscoverers";

        /// <summary>
        /// 搜索一个用户的帖子
        /// </summary>
        public const string FindPostsOfTheDiscoverer = "FindPostsOfTheDiscoverer";

        /// <summary>
        /// 获取一个用户所收藏的所有帖子
        /// </summary>
        public const string GetFavoritePosts = "GetFavoritePosts";

        /// <summary>
        /// 获取一个帖子被收藏的次数
        /// </summary>
        public const string GetFavoritesCountOfThePost = "GetFavoritesCountOfThePost";

        /// <summary>
        /// 获取一个用户的粉丝数量
        /// </summary>
        public const string GetFunsCount = "GetFunsCount";

        /// <summary>
        /// 获取一个用户的所有粉丝
        /// </summary>
        public const string GetFunsOfTheIdol = "GetFunsOfTheIdol";

        /// <summary>
        /// 获取一个用户的帖子数量
        /// </summary>
        public const string GetPostsCountOfTheDiscoverer = "GetPostsCountOfTheDiscoverer";

        /// <summary>
        /// 获取一个用户的所有帖子
        /// </summary>
        public const string GetPostsOfTheDiscoverer = "GetPostsOfTheDiscoverer";

        /// <summary>
        /// 获取一个用户可能感兴趣的所有帖子
        /// </summary>
        public const string GetRecommendedForTheDiscoverer = "GetRecommendedForTheDiscoverer";

        /// <summary>
        /// 检查一个用户是否收藏了一个帖子
        /// </summary>
        public const string IsFavoritedAPost = "IsFavoritedAPost";

        /// <summary>
        /// 检查一个用户是否是另一个用户的粉丝
        /// </summary>
        public const string IsFuns = "IsFuns";

        /// <summary>
        /// 删除一个帖子
        /// </summary>
        public const string RemoveAPost = "RemoveAPost";

        /// <summary>
        /// 以用户名搜索搜索用户
        /// </summary>
        public const string SearchDiscoverers = "SearchDiscoverers";

        /// <summary>
        /// 登录
        /// </summary>
        public const string SignIn = "SignIn";

        /// <summary>
        /// 用户注册
        /// </summary>
        public const string SignUp = "SignUp";

        /// <summary>
        /// 更新用户资料
        /// </summary>
        public const string UpdateDiscovererProfile = "UpdateDiscovererProfile";

        /// <summary>
        /// 更新帖子信息
        /// </summary>
        public const string UpdatePostInfo = "UpdatePostInfo";

        /// <summary>
        /// 获取一个用户关注的所有用户的列表
        /// </summary>
        public const string GetIdols = "GetIdols";

        /// <summary>
        /// 获取一个用户关注的人数
        /// </summary>
        public const string GetIdolsCount = "GetIdolsCount";

        /// <summary>
        /// 通过 ID 查找用户
        /// </summary>
        public const string GetDiscovererByID = "GetDiscovererByID";

        /// <summary>
        /// 查询一个用户和另一个用户关注的人的关系列表
        /// </summary>
        public const string ThisUsersRelationshipWithAnotherUsersIdols = "ThisUsersRelationshipWithAnotherUsersIdols";

        /// <summary>
        /// 查询一个用户和另一个用户的粉丝的关系列表
        /// </summary>
        public const string ThisUsersRelationshipWithAnotherUsersFuns = "ThisUsersRelationshipWithAnotherUsersFuns";

        /// <summary>
        /// 查询一个用户与另一个用户发布的帖子(s)的关系列表
        /// </summary>
        public const string ThisUsersRelationshipWithAnotherUsersPostedPosts = "ThisUsersRelationshipWithAnotherUsersPostedPosts";
    }
}
