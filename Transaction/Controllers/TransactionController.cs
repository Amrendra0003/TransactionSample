using Microsoft.AspNetCore.Mvc;
using Transaction.Domain;
using Transaction.Services;

namespace Transaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }
        /// <summary>
        /// This method is used to insert and update the transaction in TransactionRecords.csv
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpsertTransaction")]
        public ActionResult UpsertTransaction([FromForm]TransactionModel transactionModel)
        {

            var transactionStatus = _transactionService.UpsertTransaction(transactionModel);
            return Ok(transactionStatus);
        }

        [HttpPost]
        [Route("GenrateInvoices")]
        public ActionResult GenrateInvoices(string StartDate, string EndDate)
        {
            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                return Ok("Please enter StartDate and EndDate !");
            }
            var ReturData = _transactionService.GenrateInvoices(StartDate, EndDate);
            return Ok(ReturData);
        }
    }
}