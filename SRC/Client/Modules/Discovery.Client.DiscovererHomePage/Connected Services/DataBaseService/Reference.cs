﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Discovery.Client.DiscovererHomePage.DataBaseService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DataBaseService.IDataBaseService")]
    public interface IDataBaseService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/DiscovererIsExists", ReplyAction="http://tempuri.org/IDataBaseService/DiscovererIsExistsResponse")]
        bool DiscovererIsExists(string discovererSignInName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/DiscovererIsExists", ReplyAction="http://tempuri.org/IDataBaseService/DiscovererIsExistsResponse")]
        System.Threading.Tasks.Task<bool> DiscovererIsExistsAsync(string discovererSignInName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/SignIn", ReplyAction="http://tempuri.org/IDataBaseService/SignInResponse")]
        Discovery.Core.Model.Discoverer SignIn(string signInName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/SignIn", ReplyAction="http://tempuri.org/IDataBaseService/SignInResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer> SignInAsync(string signInName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/SignUp", ReplyAction="http://tempuri.org/IDataBaseService/SignUpResponse")]
        void SignUp(string signInName, string password, Discovery.Core.Enums.Sex sex, Discovery.Core.Enums.Field areaOfInterest, System.DateTime signUpTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/SignUp", ReplyAction="http://tempuri.org/IDataBaseService/SignUpResponse")]
        System.Threading.Tasks.Task SignUpAsync(string signInName, string password, Discovery.Core.Enums.Sex sex, Discovery.Core.Enums.Field areaOfInterest, System.DateTime signUpTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetDiscovererByID", ReplyAction="http://tempuri.org/IDataBaseService/GetDiscovererByIDResponse")]
        Discovery.Core.Model.Discoverer GetDiscovererByID(int discovererID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetDiscovererByID", ReplyAction="http://tempuri.org/IDataBaseService/GetDiscovererByIDResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer> GetDiscovererByIDAsync(int discovererID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/UpdateDiscovererInfo", ReplyAction="http://tempuri.org/IDataBaseService/UpdateDiscovererInfoResponse")]
        void UpdateDiscovererInfo(Discovery.Core.Model.Discoverer discoverer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/UpdateDiscovererInfo", ReplyAction="http://tempuri.org/IDataBaseService/UpdateDiscovererInfoResponse")]
        System.Threading.Tasks.Task UpdateDiscovererInfoAsync(Discovery.Core.Model.Discoverer discoverer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/UpdatePostInfo", ReplyAction="http://tempuri.org/IDataBaseService/UpdatePostInfoResponse")]
        void UpdatePostInfo(Discovery.Core.Model.Post post);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/UpdatePostInfo", ReplyAction="http://tempuri.org/IDataBaseService/UpdatePostInfoResponse")]
        System.Threading.Tasks.Task UpdatePostInfoAsync(Discovery.Core.Model.Post post);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetPostsOfTheDiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/GetPostsOfTheDiscovererResponse")]
        Discovery.Core.Model.Post[] GetPostsOfTheDiscoverer(int discovererId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetPostsOfTheDiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/GetPostsOfTheDiscovererResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> GetPostsOfTheDiscovererAsync(int discovererId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/RemoveAPost", ReplyAction="http://tempuri.org/IDataBaseService/RemoveAPostResponse")]
        void RemoveAPost(int postID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/RemoveAPost", ReplyAction="http://tempuri.org/IDataBaseService/RemoveAPostResponse")]
        System.Threading.Tasks.Task RemoveAPostAsync(int postID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/AddANewPost", ReplyAction="http://tempuri.org/IDataBaseService/AddANewPostResponse")]
        int AddANewPost(Discovery.Core.Model.Post post);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/AddANewPost", ReplyAction="http://tempuri.org/IDataBaseService/AddANewPostResponse")]
        System.Threading.Tasks.Task<int> AddANewPostAsync(Discovery.Core.Model.Post post);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/FindPostsOfTheDiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/FindPostsOfTheDiscovererResponse")]
        Discovery.Core.Model.Post[] FindPostsOfTheDiscoverer(int discovererID, string postTitle);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/FindPostsOfTheDiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/FindPostsOfTheDiscovererResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> FindPostsOfTheDiscovererAsync(int discovererID, string postTitle);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetFunsOfTheIdol", ReplyAction="http://tempuri.org/IDataBaseService/GetFunsOfTheIdolResponse")]
        Discovery.Core.Model.Discoverer[] GetFunsOfTheIdol(int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetFunsOfTheIdol", ReplyAction="http://tempuri.org/IDataBaseService/GetFunsOfTheIdolResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer[]> GetFunsOfTheIdolAsync(int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetFavoritePosts", ReplyAction="http://tempuri.org/IDataBaseService/GetFavoritePostsResponse")]
        Discovery.Core.Model.Post[] GetFavoritePosts(int discovererID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetFavoritePosts", ReplyAction="http://tempuri.org/IDataBaseService/GetFavoritePostsResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> GetFavoritePostsAsync(int discovererID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetRecommendedForTheDiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/GetRecommendedForTheDiscovererResponse")]
        Discovery.Core.Model.Post[] GetRecommendedForTheDiscoverer(int discovererID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetRecommendedForTheDiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/GetRecommendedForTheDiscovererResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> GetRecommendedForTheDiscovererAsync(int discovererID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/FindPostFromAllDiscoverers", ReplyAction="http://tempuri.org/IDataBaseService/FindPostFromAllDiscoverersResponse")]
        Discovery.Core.Model.Post[] FindPostFromAllDiscoverers(string postTitle);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/FindPostFromAllDiscoverers", ReplyAction="http://tempuri.org/IDataBaseService/FindPostFromAllDiscoverersResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> FindPostFromAllDiscoverersAsync(string postTitle);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/SearchDiscoverers", ReplyAction="http://tempuri.org/IDataBaseService/SearchDiscoverersResponse")]
        Discovery.Core.Model.Discoverer[] SearchDiscoverers(string discovererSignInName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/SearchDiscoverers", ReplyAction="http://tempuri.org/IDataBaseService/SearchDiscoverersResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer[]> SearchDiscoverersAsync(string discovererSignInName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetPostsCountOfTheDiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/GetPostsCountOfTheDiscovererResponse")]
        int GetPostsCountOfTheDiscoverer(int discovererID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetPostsCountOfTheDiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/GetPostsCountOfTheDiscovererResponse")]
        System.Threading.Tasks.Task<int> GetPostsCountOfTheDiscovererAsync(int discovererID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/CancelConcern", ReplyAction="http://tempuri.org/IDataBaseService/CancelConcernResponse")]
        void CancelConcern(int funsID, int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/CancelConcern", ReplyAction="http://tempuri.org/IDataBaseService/CancelConcernResponse")]
        System.Threading.Tasks.Task CancelConcernAsync(int funsID, int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/CancelFavorite", ReplyAction="http://tempuri.org/IDataBaseService/CancelFavoriteResponse")]
        void CancelFavorite(int discovererID, int postId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/CancelFavorite", ReplyAction="http://tempuri.org/IDataBaseService/CancelFavoriteResponse")]
        System.Threading.Tasks.Task CancelFavoriteAsync(int discovererID, int postId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/ConcernADiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/ConcernADiscovererResponse")]
        void ConcernADiscoverer(int funsID, int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/ConcernADiscoverer", ReplyAction="http://tempuri.org/IDataBaseService/ConcernADiscovererResponse")]
        System.Threading.Tasks.Task ConcernADiscovererAsync(int funsID, int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/FavoriteAPost", ReplyAction="http://tempuri.org/IDataBaseService/FavoriteAPostResponse")]
        void FavoriteAPost(int discovererID, int postID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/FavoriteAPost", ReplyAction="http://tempuri.org/IDataBaseService/FavoriteAPostResponse")]
        System.Threading.Tasks.Task FavoriteAPostAsync(int discovererID, int postID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetFunsCountOfTheIdol", ReplyAction="http://tempuri.org/IDataBaseService/GetFunsCountOfTheIdolResponse")]
        int GetFunsCountOfTheIdol(int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetFunsCountOfTheIdol", ReplyAction="http://tempuri.org/IDataBaseService/GetFunsCountOfTheIdolResponse")]
        System.Threading.Tasks.Task<int> GetFunsCountOfTheIdolAsync(int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/IsFuns", ReplyAction="http://tempuri.org/IDataBaseService/IsFunsResponse")]
        bool IsFuns(int funsID, int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/IsFuns", ReplyAction="http://tempuri.org/IDataBaseService/IsFunsResponse")]
        System.Threading.Tasks.Task<bool> IsFunsAsync(int funsID, int idolID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/IsFavoritedAPost", ReplyAction="http://tempuri.org/IDataBaseService/IsFavoritedAPostResponse")]
        bool IsFavoritedAPost(int discovererID, int postID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/IsFavoritedAPost", ReplyAction="http://tempuri.org/IDataBaseService/IsFavoritedAPostResponse")]
        System.Threading.Tasks.Task<bool> IsFavoritedAPostAsync(int discovererID, int postID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetFavoritesCountOfThePost", ReplyAction="http://tempuri.org/IDataBaseService/GetFavoritesCountOfThePostResponse")]
        int GetFavoritesCountOfThePost(int postID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetFavoritesCountOfThePost", ReplyAction="http://tempuri.org/IDataBaseService/GetFavoritesCountOfThePostResponse")]
        System.Threading.Tasks.Task<int> GetFavoritesCountOfThePostAsync(int postID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetIdols", ReplyAction="http://tempuri.org/IDataBaseService/GetIdolsResponse")]
        Discovery.Core.Model.Discoverer[] GetIdols(int funsID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetIdols", ReplyAction="http://tempuri.org/IDataBaseService/GetIdolsResponse")]
        System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer[]> GetIdolsAsync(int funsID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetIdolsCount", ReplyAction="http://tempuri.org/IDataBaseService/GetIdolsCountResponse")]
        int GetIdolsCount(int funsID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/GetIdolsCount", ReplyAction="http://tempuri.org/IDataBaseService/GetIdolsCountResponse")]
        System.Threading.Tasks.Task<int> GetIdolsCountAsync(int funsID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/ThisUsersRelationshipWithAnotherUsersIdols", ReplyAction="http://tempuri.org/IDataBaseService/ThisUsersRelationshipWithAnotherUsersIdolsRes" +
            "ponse")]
        System.Collections.Generic.KeyValuePair<Discovery.Core.Model.Discoverer, bool>[] ThisUsersRelationshipWithAnotherUsersIdols(int userID, int anotherUserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/ThisUsersRelationshipWithAnotherUsersIdols", ReplyAction="http://tempuri.org/IDataBaseService/ThisUsersRelationshipWithAnotherUsersIdolsRes" +
            "ponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<Discovery.Core.Model.Discoverer, bool>[]> ThisUsersRelationshipWithAnotherUsersIdolsAsync(int userID, int anotherUserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/ThisUsersRelationshipWithAnotherUsersFuns", ReplyAction="http://tempuri.org/IDataBaseService/ThisUsersRelationshipWithAnotherUsersFunsResp" +
            "onse")]
        System.Collections.Generic.KeyValuePair<Discovery.Core.Model.Discoverer, bool>[] ThisUsersRelationshipWithAnotherUsersFuns(int userID, int anotherUserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataBaseService/ThisUsersRelationshipWithAnotherUsersFuns", ReplyAction="http://tempuri.org/IDataBaseService/ThisUsersRelationshipWithAnotherUsersFunsResp" +
            "onse")]
        System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<Discovery.Core.Model.Discoverer, bool>[]> ThisUsersRelationshipWithAnotherUsersFunsAsync(int userID, int anotherUserID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataBaseServiceChannel : Discovery.Client.DiscovererHomePage.DataBaseService.IDataBaseService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataBaseServiceClient : System.ServiceModel.ClientBase<Discovery.Client.DiscovererHomePage.DataBaseService.IDataBaseService>, Discovery.Client.DiscovererHomePage.DataBaseService.IDataBaseService {
        
        public DataBaseServiceClient() {
        }
        
        public DataBaseServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataBaseServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataBaseServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataBaseServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool DiscovererIsExists(string discovererSignInName) {
            return base.Channel.DiscovererIsExists(discovererSignInName);
        }
        
        public System.Threading.Tasks.Task<bool> DiscovererIsExistsAsync(string discovererSignInName) {
            return base.Channel.DiscovererIsExistsAsync(discovererSignInName);
        }
        
        public Discovery.Core.Model.Discoverer SignIn(string signInName, string password) {
            return base.Channel.SignIn(signInName, password);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer> SignInAsync(string signInName, string password) {
            return base.Channel.SignInAsync(signInName, password);
        }
        
        public void SignUp(string signInName, string password, Discovery.Core.Enums.Sex sex, Discovery.Core.Enums.Field areaOfInterest, System.DateTime signUpTime) {
            base.Channel.SignUp(signInName, password, sex, areaOfInterest, signUpTime);
        }
        
        public System.Threading.Tasks.Task SignUpAsync(string signInName, string password, Discovery.Core.Enums.Sex sex, Discovery.Core.Enums.Field areaOfInterest, System.DateTime signUpTime) {
            return base.Channel.SignUpAsync(signInName, password, sex, areaOfInterest, signUpTime);
        }
        
        public Discovery.Core.Model.Discoverer GetDiscovererByID(int discovererID) {
            return base.Channel.GetDiscovererByID(discovererID);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer> GetDiscovererByIDAsync(int discovererID) {
            return base.Channel.GetDiscovererByIDAsync(discovererID);
        }
        
        public void UpdateDiscovererInfo(Discovery.Core.Model.Discoverer discoverer) {
            base.Channel.UpdateDiscovererInfo(discoverer);
        }
        
        public System.Threading.Tasks.Task UpdateDiscovererInfoAsync(Discovery.Core.Model.Discoverer discoverer) {
            return base.Channel.UpdateDiscovererInfoAsync(discoverer);
        }
        
        public void UpdatePostInfo(Discovery.Core.Model.Post post) {
            base.Channel.UpdatePostInfo(post);
        }
        
        public System.Threading.Tasks.Task UpdatePostInfoAsync(Discovery.Core.Model.Post post) {
            return base.Channel.UpdatePostInfoAsync(post);
        }
        
        public Discovery.Core.Model.Post[] GetPostsOfTheDiscoverer(int discovererId) {
            return base.Channel.GetPostsOfTheDiscoverer(discovererId);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> GetPostsOfTheDiscovererAsync(int discovererId) {
            return base.Channel.GetPostsOfTheDiscovererAsync(discovererId);
        }
        
        public void RemoveAPost(int postID) {
            base.Channel.RemoveAPost(postID);
        }
        
        public System.Threading.Tasks.Task RemoveAPostAsync(int postID) {
            return base.Channel.RemoveAPostAsync(postID);
        }
        
        public int AddANewPost(Discovery.Core.Model.Post post) {
            return base.Channel.AddANewPost(post);
        }
        
        public System.Threading.Tasks.Task<int> AddANewPostAsync(Discovery.Core.Model.Post post) {
            return base.Channel.AddANewPostAsync(post);
        }
        
        public Discovery.Core.Model.Post[] FindPostsOfTheDiscoverer(int discovererID, string postTitle) {
            return base.Channel.FindPostsOfTheDiscoverer(discovererID, postTitle);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> FindPostsOfTheDiscovererAsync(int discovererID, string postTitle) {
            return base.Channel.FindPostsOfTheDiscovererAsync(discovererID, postTitle);
        }
        
        public Discovery.Core.Model.Discoverer[] GetFunsOfTheIdol(int idolID) {
            return base.Channel.GetFunsOfTheIdol(idolID);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer[]> GetFunsOfTheIdolAsync(int idolID) {
            return base.Channel.GetFunsOfTheIdolAsync(idolID);
        }
        
        public Discovery.Core.Model.Post[] GetFavoritePosts(int discovererID) {
            return base.Channel.GetFavoritePosts(discovererID);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> GetFavoritePostsAsync(int discovererID) {
            return base.Channel.GetFavoritePostsAsync(discovererID);
        }
        
        public Discovery.Core.Model.Post[] GetRecommendedForTheDiscoverer(int discovererID) {
            return base.Channel.GetRecommendedForTheDiscoverer(discovererID);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> GetRecommendedForTheDiscovererAsync(int discovererID) {
            return base.Channel.GetRecommendedForTheDiscovererAsync(discovererID);
        }
        
        public Discovery.Core.Model.Post[] FindPostFromAllDiscoverers(string postTitle) {
            return base.Channel.FindPostFromAllDiscoverers(postTitle);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Post[]> FindPostFromAllDiscoverersAsync(string postTitle) {
            return base.Channel.FindPostFromAllDiscoverersAsync(postTitle);
        }
        
        public Discovery.Core.Model.Discoverer[] SearchDiscoverers(string discovererSignInName) {
            return base.Channel.SearchDiscoverers(discovererSignInName);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer[]> SearchDiscoverersAsync(string discovererSignInName) {
            return base.Channel.SearchDiscoverersAsync(discovererSignInName);
        }
        
        public int GetPostsCountOfTheDiscoverer(int discovererID) {
            return base.Channel.GetPostsCountOfTheDiscoverer(discovererID);
        }
        
        public System.Threading.Tasks.Task<int> GetPostsCountOfTheDiscovererAsync(int discovererID) {
            return base.Channel.GetPostsCountOfTheDiscovererAsync(discovererID);
        }
        
        public void CancelConcern(int funsID, int idolID) {
            base.Channel.CancelConcern(funsID, idolID);
        }
        
        public System.Threading.Tasks.Task CancelConcernAsync(int funsID, int idolID) {
            return base.Channel.CancelConcernAsync(funsID, idolID);
        }
        
        public void CancelFavorite(int discovererID, int postId) {
            base.Channel.CancelFavorite(discovererID, postId);
        }
        
        public System.Threading.Tasks.Task CancelFavoriteAsync(int discovererID, int postId) {
            return base.Channel.CancelFavoriteAsync(discovererID, postId);
        }
        
        public void ConcernADiscoverer(int funsID, int idolID) {
            base.Channel.ConcernADiscoverer(funsID, idolID);
        }
        
        public System.Threading.Tasks.Task ConcernADiscovererAsync(int funsID, int idolID) {
            return base.Channel.ConcernADiscovererAsync(funsID, idolID);
        }
        
        public void FavoriteAPost(int discovererID, int postID) {
            base.Channel.FavoriteAPost(discovererID, postID);
        }
        
        public System.Threading.Tasks.Task FavoriteAPostAsync(int discovererID, int postID) {
            return base.Channel.FavoriteAPostAsync(discovererID, postID);
        }
        
        public int GetFunsCountOfTheIdol(int idolID) {
            return base.Channel.GetFunsCountOfTheIdol(idolID);
        }
        
        public System.Threading.Tasks.Task<int> GetFunsCountOfTheIdolAsync(int idolID) {
            return base.Channel.GetFunsCountOfTheIdolAsync(idolID);
        }
        
        public bool IsFuns(int funsID, int idolID) {
            return base.Channel.IsFuns(funsID, idolID);
        }
        
        public System.Threading.Tasks.Task<bool> IsFunsAsync(int funsID, int idolID) {
            return base.Channel.IsFunsAsync(funsID, idolID);
        }
        
        public bool IsFavoritedAPost(int discovererID, int postID) {
            return base.Channel.IsFavoritedAPost(discovererID, postID);
        }
        
        public System.Threading.Tasks.Task<bool> IsFavoritedAPostAsync(int discovererID, int postID) {
            return base.Channel.IsFavoritedAPostAsync(discovererID, postID);
        }
        
        public int GetFavoritesCountOfThePost(int postID) {
            return base.Channel.GetFavoritesCountOfThePost(postID);
        }
        
        public System.Threading.Tasks.Task<int> GetFavoritesCountOfThePostAsync(int postID) {
            return base.Channel.GetFavoritesCountOfThePostAsync(postID);
        }
        
        public Discovery.Core.Model.Discoverer[] GetIdols(int funsID) {
            return base.Channel.GetIdols(funsID);
        }
        
        public System.Threading.Tasks.Task<Discovery.Core.Model.Discoverer[]> GetIdolsAsync(int funsID) {
            return base.Channel.GetIdolsAsync(funsID);
        }
        
        public int GetIdolsCount(int funsID) {
            return base.Channel.GetIdolsCount(funsID);
        }
        
        public System.Threading.Tasks.Task<int> GetIdolsCountAsync(int funsID) {
            return base.Channel.GetIdolsCountAsync(funsID);
        }
        
        public System.Collections.Generic.KeyValuePair<Discovery.Core.Model.Discoverer, bool>[] ThisUsersRelationshipWithAnotherUsersIdols(int userID, int anotherUserID) {
            return base.Channel.ThisUsersRelationshipWithAnotherUsersIdols(userID, anotherUserID);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<Discovery.Core.Model.Discoverer, bool>[]> ThisUsersRelationshipWithAnotherUsersIdolsAsync(int userID, int anotherUserID) {
            return base.Channel.ThisUsersRelationshipWithAnotherUsersIdolsAsync(userID, anotherUserID);
        }
        
        public System.Collections.Generic.KeyValuePair<Discovery.Core.Model.Discoverer, bool>[] ThisUsersRelationshipWithAnotherUsersFuns(int userID, int anotherUserID) {
            return base.Channel.ThisUsersRelationshipWithAnotherUsersFuns(userID, anotherUserID);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<Discovery.Core.Model.Discoverer, bool>[]> ThisUsersRelationshipWithAnotherUsersFunsAsync(int userID, int anotherUserID) {
            return base.Channel.ThisUsersRelationshipWithAnotherUsersFunsAsync(userID, anotherUserID);
        }
    }
}
