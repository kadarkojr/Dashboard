namespace TransactionInfo.Models.ViewModels
{
    public class TransactionsDataVM
    {
        public List<TransactionsData> TransactionList { get; set; }
        public TransactionsData TransactionMVP { get; set; }
        public TransactionsData TransactionMUP { get; set; }
        public TransactionsData PURatio { get; set; }
        public int? TotalValue { get; set; }
        public int? TotalTransactions { get; set;}
    }
}
