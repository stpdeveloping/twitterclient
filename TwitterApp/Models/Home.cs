using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TwitterApp.Common;
using TwitterApp.Controllers;

namespace TwitterApp.Models
{
    public class Home
    {
        public bool FirstLoad { get; set; } = false;
        public string ErrorMessage { get; set; }
        public string ErrorMessage2 { get; set; }
        private List<Status> _tweets { get; set; }
        public List<Status> Tweets
        {
            get => _tweets?.Where(tweet => _tweets.IndexOf(tweet) >= NumberOfSearchResults - 10 && _tweets.IndexOf(tweet) < NumberOfSearchResults).ToList();
            set => _tweets = value;
        }
        public List<Status> PossibleTweets { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Search field cannot be empty"), 
            StringLength(25, ErrorMessage = "Search field cannot contain more than 25 characters")]
        public string SearchInput { get; set; }
        public int NumberOfSearchResults { get; set; } = 10;
        public int? NumberOfSearchTweets { get => _tweets?.Count; }
    }
}