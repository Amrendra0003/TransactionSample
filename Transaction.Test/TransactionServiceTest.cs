using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Transaction.Domain;
using Transaction.Repository;
using Transaction.Services;

namespace Transaction.Test
{
    [TestClass]
    public class TransactionServiceTest
    {
        [TestMethod]
        public void Test_Add_Transaction_TransactioId_Zero()
        {
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            TransactionModel transaction = new TransactionModel();
            transaction.TransactionId = 0;
            transaction.TransactionAmount = 244;
            transaction.TransactionDate = "2020-06-13";
            transaction.TransactionDescription = "Transaction For Testing";
            transaction.TransactionStatus = "billed";
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.UpsertTransaction(transaction);
            Assert.AreEqual("Success",result);
        }
        [TestMethod]
        public void Test_Add_Transaction_When_Model_Null()
        {
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            TransactionModel transaction = null;
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.UpsertTransaction(transaction);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void Test_Update_Transaction_When_TransactionId_NotNull()
        {
            TransactionModel transaction = new TransactionModel();
            transaction.TransactionId = 12;
            transaction.TransactionAmount = 256;
            transaction.TransactionDate = "2020-06-23";
            transaction.TransactionDescription = "Transaction For Testing";
            transaction.TransactionStatus = "billed";
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            RecordLIst.Add(transaction);
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.UpsertTransaction(transaction);
            Assert.AreEqual("Success", result);
        }
        [TestMethod]
        public void Test_Update_Transaction_When_TransactionId_NotNull_Return_ERROR()
        {
            TransactionModel transaction = new TransactionModel();
            transaction.TransactionId = 12;
            transaction.TransactionAmount = 256;
            transaction.TransactionDate = "2020-06-23";
            transaction.TransactionDescription = "Transaction For Testing";
            transaction.TransactionStatus = "billed";
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.UpsertTransaction(transaction);
            Assert.AreEqual("TransactionId not found!", result);
        }
        [TestMethod]
        public void Genrate_Invoices_When_Start_Date_IsNull()
        {
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.GenrateInvoices("","2020-06-14");
            Assert.AreEqual("Start OR End Date Not Found!", result);
        }
        [TestMethod]
        public void Genrate_Invoices_When_End_Date_IsNull()
        {
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.GenrateInvoices( "2020-06-14",null);
            Assert.AreEqual("Start OR End Date Not Found!", result);
        }
        [TestMethod]
        public void Genrate_Invoices_When_Both_Date_IsNull()
        {
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.GenrateInvoices(null, null);
            Assert.AreEqual("Start OR End Date Not Found!", result);
        }
        [TestMethod]
        public void Genrate_Invoice_No_Record_Found()
        {
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.GenrateInvoices("2020-06-19", "2020-12-12");
            Assert.AreEqual("No Record Found!", result);
        }
        [TestMethod]
        public void Genrate_Invoice_Out_Of_Range()
        {
            TransactionModel transaction = new TransactionModel();
            transaction.TransactionId = 12;
            transaction.TransactionAmount = 256;
            transaction.TransactionDate = "2019-06-23";
            transaction.TransactionDescription = "Transaction For Testing";
            transaction.TransactionStatus = "billed";
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            RecordLIst.Add(transaction);
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.GenrateInvoices("2020-06-19", "2020-12-12");
            Assert.AreEqual("Date Out Of Range", result);
        }
        [TestMethod]
        public void Genrate_Invoice_Success()
        {
            TransactionModel transaction = new TransactionModel();
            transaction.TransactionId = 12;
            transaction.TransactionAmount = 256;
            transaction.TransactionDate = "2020-06-23";
            transaction.TransactionDescription = "Transaction For Testing";
            transaction.TransactionStatus = "billed";
            var mockforpath = new Mock<IHostingEnvironment>();
            string BasePath = GetDirectoryPath(Directory.GetCurrentDirectory());
            mockforpath.Setup(x => x.ContentRootPath).Returns(BasePath);
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.UpsertTransaction(It.IsAny<List<TransactionModel>>()))
                .Returns("Success");
            List<TransactionModel> RecordLIst = new List<TransactionModel>();
            RecordLIst.Add(transaction);
            mock.Setup(x => x.GetTransaction())
                .Returns(RecordLIst);
            var transactionService = new TransactionService(mock.Object, mockforpath.Object);
            string result = transactionService.GenrateInvoices("2020-06-19", "2020-12-12");
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
