using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Transaction.Domain;

namespace Transaction.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IHostingEnvironment _env;
        public TransactionRepository(IHostingEnvironment env)
        {
            _env = env;
        }

        public string UpsertTransaction(List<TransactionModel> transactionModel)
        {
            try
            {
                string Path = _env.ContentRootPath + "/Transactions/CSVRecords/TransactionRecords.csv";
                using (var writer = new StreamWriter(Path))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<TransactionModel>();
                    csv.NextRecord();
                    csv.WriteRecords(transactionModel.ToList());
                }
                return "Success";
            }
            catch (Exception ex)
            {

                return (ex.Message.ToString());
            }

        }

        public List<TransactionModel> GetTransaction()
        {
                List<TransactionModel> RecordLIst = new List<TransactionModel>();
                string Path = _env.ContentRootPath + "/Transactions/CSVRecords/TransactionRecords.csv";
                using (var reader = new StreamReader(Path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.AutoMap<TransactionModel>();
                    RecordLIst = csv.GetRecords<TransactionModel>().ToList();
                }
                return RecordLIst.ToList();
        }
    }
}
