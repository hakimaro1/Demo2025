
using System;

public enum AccountType { Checking, Deposit }

public class Program
{
    public static void Main()
    {
        AccountType goldAccount;
        AccountType platinumAccount;

        goldAccount = AccountType.Checking;
        platinumAccount = AccountType.Deposit;

        Console.WriteLine("The Customer Account Type is {0}", goldAccount);
        Console.WriteLine("The Customer Account Type is {0}", platinumAccount);
    }
}
