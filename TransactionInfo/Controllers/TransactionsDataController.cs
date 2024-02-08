using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TransactionInfo.Data;
using TransactionInfo.Models;
using TransactionInfo.Models.ViewModels;

namespace TransactionInfo.Controllers
{
    public class TransactionsDataController : Controller
    {
        private readonly InfoTransactionContext _db;
        public TransactionsDataVM TransactionDataVM { get; set; }

        public TransactionsDataController(InfoTransactionContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            List<TransactionsData> Transactions = _db.TransactionsDatas.ToList();
            var MVP = Transactions.OrderByDescending(u => u.Transaction_Values).FirstOrDefault();
            var MUP = Transactions.OrderByDescending(u => u.Transaction_Count).FirstOrDefault();
            var MEP = Transactions.OrderByDescending(u => u.Transaction_Values/u.Transaction_Count).FirstOrDefault();

            int? tv = 0;
            int? tt = 0;
            foreach(var trans in Transactions)
            {
                tv += trans.Transaction_Values;
                tt += trans.Transaction_Count;
                
            }
            var viewModel = new TransactionsDataVM
            {
                TransactionList = Transactions,
                TransactionMVP = MVP,
                TransactionMUP = MUP,
                PURatio = MEP,
                TotalValue = tv,
                TotalTransactions = tt
            };
            
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult GetTransactionValues()
        {
            List<object> datas = new List<object>();
            List<string?> labels = _db.TransactionsDatas.OrderByDescending(p=>p.Transaction_Values).Select(p=>p.Product_Name).Take(5).ToList();
            List<int?> Transactionvalues = _db.TransactionsDatas.OrderByDescending(p => p.Transaction_Values).Select(p => p.Transaction_Values).Take(5).ToList();
            List<string?> labels2 = _db.TransactionsDatas.OrderByDescending(p => p.Transaction_Count).Select(p => p.Product_Name).Take(5).ToList();
            List<int?> Transactioncount = _db.TransactionsDatas.OrderByDescending(p => p.Transaction_Count).Select(p => p.Transaction_Count).Take(5).ToList();
            datas.Add(labels);
            datas.Add(Transactionvalues);
            datas.Add(labels2);
            datas.Add(Transactioncount);
            return Json(new { data = datas });

        }
        [HttpGet]
        public ActionResult GetAll() 
        { 
            List<TransactionsData> Transactions = _db.TransactionsDatas.ToList();
            return Json(new { data = Transactions });
        }
        [HttpGet]
        public IActionResult DownloadCSV()
        {
            List<TransactionsData> transactions = _db.TransactionsDatas.ToList();

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(transactions);
                streamWriter.Flush();

                var csvData = memoryStream.ToArray();

                return File(csvData, "text/csv", "transactions.csv");
            }
        }

    }
}
