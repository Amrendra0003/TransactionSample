using System;
using System.Collections.Generic;
using GemBox.Document;
using Microsoft.AspNetCore.Hosting;
using Transaction.Domain;
using Transaction.Repository;

namespace Transaction.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHostingEnvironment _env;
        public TransactionService(ITransactionRepository transactionRepository, IHostingEnvironment env)
        {
            _transactionRepository = transactionRepository;
            _env = env;
        }
        public string UpsertTransaction(TransactionModel transactionModel)
        {
            if (transactionModel == null)
                return null;
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            string labelText = string.Empty;
            string Path = _env.ContentRootPath + "/Transactions/CSVRecords/TransactionRecords.csv";
            RecordLIst = _transactionRepository.GetTransaction();
            if (transactionModel.TransactionId != 0)
            {
                var getIndex = RecordLIst.FindIndex(x => x.TransactionId == transactionModel.TransactionId);
                if (getIndex != -1)
                {
                    RecordLIst[getIndex].TransactionStatus = transactionModel.TransactionStatus;
                    if (transactionModel.TransactionStatus.ToLower() == "paid")
                    {
                        labelText = "TransactionId  " + "TransactionAmount  " + "TransactionDate    " + "TransactionDescription " + "TransactionStatus  " + Environment.NewLine;
                        labelText = labelText + transactionModel.TransactionId + "   " + transactionModel.TransactionAmount + "   " + transactionModel.TransactionDate + " " + transactionModel.TransactionDescription + " " + "paid";
                        createPDF(labelText, transactionModel);
                    }
                }
                else
                    return "TransactionId not found!";
            }
            else
            {
                Random random = new Random();
                transactionModel.TransactionId = random.Next(1, 1000);
                RecordLIst.Add(transactionModel);
            }
            return _transactionRepository.UpsertTransaction(RecordLIst);
        }

        public string GenrateInvoices(string StartDate, string EndDate)
        {
            if (string.IsNullOrWhiteSpace(StartDate) || string.IsNullOrWhiteSpace(EndDate))
                return "Start OR End Date Not Found!";
            string labelText = string.Empty;
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            List<TransactionModel> filterLIst = new List<TransactionModel>();

            RecordLIst = _transactionRepository.GetTransaction();
            if (RecordLIst.Count == 0)
                return "No Record Found!";
            foreach (var record in RecordLIst)
            {
                if (Convert.ToDateTime(record.TransactionDate) >= Convert.ToDateTime(StartDate) && Convert.ToDateTime(record.TransactionDate) <= Convert.ToDateTime(EndDate))
                {
                    record.TransactionStatus = "billed";
                    labelText = "TransactionId  " + "TransactionAmount  " + "TransactionDate    " + "TransactionDescription " + "TransactionStatus  " + Environment.NewLine;
                    labelText = labelText + record.TransactionId + "   " + record.TransactionAmount + "   " + record.TransactionDate + " " + record.TransactionDescription + " " + "billed";
                    createPDF(labelText, record);
                    filterLIst.Add(record);
                }
                else
                    return "Date Out Of Range";

            }
            return _transactionRepository.UpsertTransaction(filterLIst);
        }

        void createPDF(string labelText, TransactionModel transactionModel)
        {
            // Create a new empty document.
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            var document = new DocumentModel();
            // Add document content. \Transaction\Transactions\Invoices\
            document.Sections.Add(
                    new Section(document,
                    new Paragraph(document, labelText)));
            string filename = _env.ContentRootPath + @"\Transactions\Invoices\Invoice_" + transactionModel.TransactionId +"_"+ DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".pdf";
            // Save the generated document as PDF file.
            document.Save(filename);
        }
    }
}
