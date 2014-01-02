using System.Web.Mvc;
using Domain;
using KenKenBuilder;
using KenKenWebapp.Models;

namespace KenKenWebapp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var puzzle = new SimpleKenKenPuzzleBuilder().Build(DifficultyLevel.Easy, GridSize.FourByFour);
            return View(new PuzzleModel(puzzle));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}