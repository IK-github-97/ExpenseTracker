using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpenseTrackerDbContext _context;

        public HomeController(ILogger<HomeController> logger, ExpenseTrackerDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Expenses()
        {
            var allexpenses = _context.Expenses.ToList();
            var totalexpenses = allexpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalexpenses;
            return View(allexpenses);
        }
        public IActionResult CreateEditExpenses(int? id)
        {
            if (id != null)
            {
                var editeexpense = _context.Expenses.SingleOrDefault(Expense => Expense.Id == id);
                return View(editeexpense);
            }
            return View();
        }
        public IActionResult DeleteExpenses(int id)
        {
            var deleteexpense = _context.Expenses.SingleOrDefault(Expense => Expense.Id == id);
            _context.Expenses.Remove(deleteexpense);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }
        public IActionResult CreateEditExpensesForm(Expense model)
        {
            if(model.Id == 0)
            {
                _context.Expenses.Add(model); //create
            }
            else
            {
                _context.Expenses.Update(model); //edit
            }            
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }
        public IActionResult Index()
        {
            return View();
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
}
