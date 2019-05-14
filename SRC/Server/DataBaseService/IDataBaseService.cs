using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Discovery.Model;

namespace DataBaseService
{
    [ServiceContract]
    public interface IDataBaseService
    {
        [OperationContract]
        bool DiscovererIsExists(string discovererSignInName);

        [OperationContract]
        Discoverer SignIn(string signInName, string password);

        [OperationContract]
        void UpdateDiscovererInfo(Discoverer discoverer);

        [OperationContract]
        void UpdatePostInfo(Post post);

        [OperationContract]
        IEnumerable<Post> GetPostsOfTheDiscoverer(string discovererId);

        [OperationContract]
        void RemoveAPost(string postID);

        [OperationContract]
        int AddANewPost(Post post);

        [OperationContract]
        IEnumerable<Post> FindPostsOfTheDiscoverer(string postTitle);

        [OperationContract]
        IEnumerable<Discoverer> GetFunsOfTheIdol(string idolID);

        [OperationContract]
        IEnumerable<Post> GetFavoritePosts(string discovererID);

        [OperationContract]
        IEnumerable<Post> GetRecommendedForTheDiscoverer(string discovererID);

        [OperationContract]
        IEnumerable<Post> FindPostFromAllDiscoverers(string postTitle);

        [OperationContract]
        IEnumerable<Discoverer> SearchDiscoverers(string discovererSignInName);

        [OperationContract]
        int GetPostsCountOfTheDiscoverer(string discovererID);

        [OperationContract]
        void CancelConcern(string funsID, string idolID);

        [OperationContract]
        void CancelFavorite(string discovererID, string postId);

        [OperationContract]
        void ConcernADiscoverer(string funsID, string idolID);

        [OperationContract]
        void FavoriteAPost(string discovererID, string postID);

        [OperationContract]
        int GetFunsCountOfTheIdol(string idolID);

        [OperationContract]
        bool IsFuns(string funsID, string idolID);

        [OperationContract]
        bool IsFavorite(string discovererID, string postID);

        [OperationContract]
        int GetFavoritesCountOfThePost(string postID);
    }
}
