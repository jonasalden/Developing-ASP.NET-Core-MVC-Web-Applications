using Microsoft.AspNetCore.Mvc;
using AspNetCore_MiddleWareServices.Services;
using System.Text;
using System;

namespace AspNetCore_03_MiddleWareServices.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        readonly IPollResultsService _pollResults;
        #endregion

        #region Constructor
        public HomeController(IPollResultsService pollResults)
        {
            _pollResults = pollResults;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            if (Request.Query.ContainsKey("submitted") == true)
            {
                var results = new StringBuilder();
                var voteList = _pollResults.GetVoteResult();

                foreach (var gameVotes in voteList)
                {
                    results.Append($"Game name: {gameVotes.Key}. Votes: {gameVotes.Value}{Environment.NewLine}");
                }
                return Content(results.ToString());
            }
            else
            {
                return Redirect("poll-questions.html");
            }
        }
        #endregion
    }
}