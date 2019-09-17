using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TwitterApp.Common;
using TwitterApp.Common.Interfaces;
using TwitterApp.Models;

namespace TwitterApp.Controllers
{
    public class TweetsController : Controller
    {
        private static string tweeterApiAddress = "https://api.twitter.com/1.1/search/tweets.json";
        private static HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));

        private ICacheService cacheProvider = new InMemoryCache();
        private async Task<ActionResult> ControllerHandler(Func<Task<ActionResult>> customHandler)
        {
            try
            {
                return await customHandler();
            }
            catch
            {
                HomeController.currentState.ErrorMessage = 
                    HomeController.currentState.ErrorMessage2 = 
                    "Twitter is unavailable now";
                return View("Index", HomeController.currentState);
            }
        }

        private async Task<Wrapper> GetTweets(string apiQuery)
        {
            var response = await client.GetAsync(apiQuery);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Wrapper>(responseBody);
        }

        private async Task<List<Status>> GetPossibleTweets()
        {
            var query = $"{tweeterApiAddress}?q={Url.Encode("@POSSIBLE")}&count=5&result_type=recent";
            var wrapper = await cacheProvider.GetOrSet("PossibleTweets", async () => await GetTweets(query));
            return wrapper.statuses;
        }
        private async Task UpdatePossibleFeed()
        {            
            var possibleTweets = new List<Status>(await GetPossibleTweets());
            HomeController.currentState.PossibleTweets = possibleTweets;
            HomeController.currentState.ErrorMessage2 = possibleTweets.Any() ? "" : "There are no tweets from POSSIBLE";
        }

        public async Task<ActionResult> Index()
        {
            return await ControllerHandler(async () =>
            {
                HomeController.currentState = new Home();
                await UpdatePossibleFeed();
                HomeController.currentState.FirstLoad = true;
                return View("Index", HomeController.currentState);
            });              
        }

        public async Task<ActionResult> GetTweetsBySearchQuery(string SearchInput)
        {
            HomeController.currentState.SearchInput = SearchInput;
            await UpdatePossibleFeed();
            if (!string.IsNullOrEmpty(SearchInput))
                if (SearchInput.Length <= 25)
                    return await ControllerHandler(async () =>
                    {
                        var query = $"{tweeterApiAddress}?q={Url.Encode(SearchInput)}&count=100";
                        var json = await GetTweets(query);            
                        if (json.statuses.Any())
                        {
                            HomeController.currentState.Tweets = json.statuses;
                            HomeController.currentState.SearchInput = SearchInput;
                            HomeController.currentState.FirstLoad = false;
                            HomeController.currentState.ErrorMessage = null;                           
                        }
                        else HomeController.currentState.ErrorMessage = "Your search did not return any results";
                        return View("Index", HomeController.currentState);
                    });
                else HomeController.currentState.ErrorMessage = "Too long search query";             
            else HomeController.currentState.ErrorMessage = "Empty search query";

            return View("Index", HomeController.currentState);
        }
    }
}