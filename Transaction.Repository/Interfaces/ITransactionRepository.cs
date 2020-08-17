
using System.Collections.Generic;
using Transaction.Domain;

namespace Transaction.Repository
{
    public interface ITransactionRepository
    {
        List<TransactionModel> GetTransaction();
        string UpsertTransaction(List<TransactionModel> transactionModel);

    }
}
