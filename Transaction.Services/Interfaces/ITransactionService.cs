using Transaction.Domain;

namespace Transaction.Services
{
    public interface ITransactionService
    {
        string UpsertTransaction(TransactionModel transactionModel);

        string GenrateInvoices(string StartDate, string EndDate);

    }
}
