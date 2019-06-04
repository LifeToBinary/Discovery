using System;
using System.Collections.Generic;
using System.ServiceModel;
using Discovery.Core.Enums;
using Discovery.Core.Model;

namespace Discovery.Server.RemoteService
{
    [ServiceContract]
    public interface IDataBaseService
    {
        [OperationContract]
        bool DiscovererIsExists(string discovererSignInName);

        [OperationContract]
        Discoverer SignIn(string signInName, string password);

        [OperationContract]
        void SignUp(string signInName,
                    string password,
                    string email,
                    Field areaOfInterest);
        [OperationContract]
        void UploadFile(byte[] data, string path);
        [OperationContract]
        Discoverer GetDiscovererByID(int discovererID);

        [OperationContract]
        void UpdateDiscovererInfo(Discoverer discoverer);

        [OperationContract]
        void UpdatePostInfo(Post post);

        [OperationContract]
        IEnumerable<Post> GetPostsOfTheDiscoverer(int discovererId);

        [OperationContract]
        void RemoveAPost(int postID);

        [OperationContract]
        int AddANewPost(Post post);

        [OperationContract]
        IEnumerable<Post> FindPostsOfTheDiscoverer(int discovererID, string postTitle);

        [OperationContract]
        IEnumerable<Discoverer> GetFunsOfTheIdol(int idolID);

        [OperationContract]
        IEnumerable<Post> GetFavoritePosts(int discovererID);

        [OperationContract]
        IEnumerable<Post> GetRecommendedForTheDiscoverer(int discovererID);

        [OperationContract]
        IEnumerable<Post> FindPostFromAllDiscoverers(string postTitle);

        [OperationContract]
        IEnumerable<Discoverer> SearchDiscoverers(string discovererSignInName);

        [OperationContract]
        int GetPostsCountOfTheDiscoverer(int discovererID);

        [OperationContract]
        void CancelConcern(int funsID, int idolID);

        [OperationContract]
        void CancelFavorite(int discovererID, int postId);

        [OperationContract]
        void ConcernADiscoverer(int funsID, int idolID);

        [OperationContract]
        void FavoriteAPost(int discovererID, int postID);

        [OperationContract]
        int GetFunsCountOfTheIdol(int idolID);

        [OperationContract]
        bool IsFuns(int funsID, int idolID);

        [OperationContract]
        bool IsFavoritedAPost(int discovererID, int postID);

        [OperationContract]
        int GetFavoritesCountOfThePost(int postID);

        [OperationContract]
        IEnumerable<Discoverer> GetIdols(int funsID);

        [OperationContract]
        int GetIdolsCount(int funsID);

        [OperationContract]
        IEnumerable<KeyValuePair<Discoverer, bool>> ThisUsersRelationshipWithAnotherUsersIdols(
            int userID,
            int anotherUserID);

        [OperationContract]
        IEnumerable<KeyValuePair<Discoverer, bool>> ThisUsersRelationshipWithAnotherUsersFuns(
            int userID,
            int anotherUserID);

        [OperationContract]
        IEnumerable<KeyValuePair<Post, bool>> ThisUsersRelationshipWithAnotherUsersPostedPosts(
            int userID,
            int anotherUserID);

        [OperationContract]
        IEnumerable<KeyValuePair<Post, bool>> ThisUsersRelationshipWithAnotherUsersFavoritedPosts(
            int userID,
            int anotherUserID);
    }
}