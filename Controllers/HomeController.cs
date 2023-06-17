using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext db;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Chef> allChefs = db.Chefs.Include(d => d.AllDishes).ToList();
        return View("AllChefs", allChefs);
    }

    [HttpGet("dishes")]
    public IActionResult Dishes()
    {
        List<Dish> allDishes = db.Dishes.Include(c => c.Creator).ToList();
        return View("AllDishes", allDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        ViewBag.chefs = db.Chefs.ToList();
        return View("NewDish");
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.chefs = db.Chefs.ToList();
            return View("NewDish");
        }

        db.Dishes.Add(newDish);

        db.SaveChanges();

        return RedirectToAction("Dishes");
    }

    [HttpGet("chefs/new")]
    public IActionResult NewChef()
    {
        return View("NewChef");
    }

    [HttpPost("chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (!ModelState.IsValid)
        {
            return View("NewChef");
        }

        db.Chefs.Add(newChef);

        db.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
