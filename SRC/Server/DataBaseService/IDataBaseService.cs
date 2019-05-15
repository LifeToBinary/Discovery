using System;
using System.Collections.Generic;
using System.ServiceModel;
using Discovery.Core.Enums;
using Discovery.Core.Model;

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
        void SignUp(string signInName, 
                    string password, 
                    Sex sex, 
                    Field areaOfInterest, 
                    DateTime signUpTime);

        [OperationContract]
        Discoverer GetDiscovererByID(int discovererID);

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
        IEnumerable<Post> FindPostsOfTheDiscoverer(string discovererID, string postTitle);

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
        bool IsFavoritedAPost(string discovererID, string postID);

        [OperationContract]
        int GetFavoritesCountOfThePost(string postID);

        [OperationContract]
        IEnumerable<Discoverer> GetIdols(string funsID);

        [OperationContract]
        int GetIdolsCount(string funsID);
    }
}
