using System.Threading.Tasks;
using System.Web.Mvc;
using TwitterApp.Models;

namespace TwitterApp.Controllers
{
    public class HomeController : Controller
    {
        public static Home currentState { get; set; }

        [HttpPost]
        public ActionResult ChangePage(bool isNext)
        {
            currentState.NumberOfSearchResults = isNext ? 
                currentState.NumberOfSearchResults + 10 :
                currentState.NumberOfSearchResults - 10;
            return View("~/Views/Tweets/Index.cshtml", currentState);        
        }
    }
}