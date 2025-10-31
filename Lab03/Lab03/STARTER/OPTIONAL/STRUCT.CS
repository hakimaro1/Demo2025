using System;
public enum AccountType	{Checking, Deposit}

public struct BankAccount
{
    public long accNo;
    public decimal accBal;
    public AccountType accType;
}


class Test
{
    static void Main()
    {
		BankAccount goldAccount;

		
		goldAccount.accNo = 123; //remove this line and add code below

		goldAccount.accType = AccountType.Checking;
		goldAccount.accBal = (decimal)3200.00;
		
		Console.WriteLine("*** Account Summary ***");
		Console.WriteLine("Acct Number  {0}", goldAccount.accNo);
		Console.WriteLine("Acct Type    {0}", goldAccount.accType);
		Console.WriteLine("Acct Balance ${0}", goldAccount.accBal);
    }
}



