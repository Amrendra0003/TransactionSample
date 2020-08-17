using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Transaction.Domain;
using Transaction.Repository;

namespace Transaction.Test
{
    [TestClass]
    public class TransactionRepositoryTest
    {
        [TestMethod]
        public void Test_Upsert_Transaction()
        {
            var mock = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mock.Setup(x => x.ContentRootPath).Returns(BasePath);
            List<TransactionModel> transactions = new List<TransactionModel>();
            TransactionModel transaction = new TransactionModel();
            transaction.TransactionId = 0;
            transaction.TransactionAmount = 244;
            transaction.TransactionDate = "2020-06-13";
            transaction.TransactionDescription = "Sample for updates testing";
            transaction.TransactionStatus = "billed";
            transactions.Add(transaction);
            var transactionRepository = new TransactionRepository(mock.Object);
            var result = transactionRepository.UpsertTransaction(transactions);
            Assert.AreEqual("Success", result);
        }
       
        [TestMethod]
        public void Test_Update_Transaction()
        {
            var mock = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mock.Setup(x => x.ContentRootPath).Returns(BasePath);
            List<TransactionModel> transactions = new List<TransactionModel>();
            TransactionModel transaction = new TransactionModel();
            transaction.TransactionId = 315;
            transaction.TransactionAmount = 244;
            transaction.TransactionDate = "2020-06-13";
            transaction.TransactionDescription = "Sample for updates testing";
            transaction.TransactionStatus = "billed";
            transactions.Add(transaction);
            var transactionRepository = new TransactionRepository(mock.Object);
            var result = transactionRepository.UpsertTransaction(transactions);
            Assert.AreEqual("Success", result);
        }
        private string GetDirectoryPath(string rawPath)
        {
            int index = rawPath.LastIndexOf("Transaction.Test");
            if (index > 0)
                rawPath = rawPath.Substring(0, index) + "Transaction";
            return (rawPath);
        }
    }
}
