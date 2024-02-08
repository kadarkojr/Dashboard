using System;
using System.Collections.Generic;

namespace TransactionInfo.Models;

public partial class TransactionsData
{
    public int Id { get; set; }

    public string? Product_Name { get; set; }

    public int? Transaction_Values { get; set; }

    public int? Transaction_Count { get; set; }
}
