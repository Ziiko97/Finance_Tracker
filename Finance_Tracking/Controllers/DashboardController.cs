using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Finance_Tracking.Controllers
{
    public class DashboardController : Controller
    {

        private readonly ApplicationDBContext _context;

        public DashboardController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<ActionResult> Index()
        {
            //last 7 days
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> SelectTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();

            //total income
            int TotalIncome = SelectTransactions
                .Where(i => i.Category.Type=="Income")
                .Sum(j => j.Amount);

            ViewBag.TotalIncome = TotalIncome.ToString();

            //total expense
            int TotalExpense = SelectTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);

            ViewBag.TotalExpense = TotalExpense.ToString();

            int Balance = TotalIncome - TotalExpense;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("sv-SE");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

            //expense by category
            ViewBag.DoughnutChartData = SelectTransactions
                .Where(i =>i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    amount = k.Sum(j =>j.Amount)
                });

            return View();
        }
    }
}

