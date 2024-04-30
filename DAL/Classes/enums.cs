namespace DAL.Classes
{
    public enum Src
    {
        PayOnline,
        PaymentDB,
        Printer,
        Allocation,
    }

    public enum TransactionType
    {
        AddCredit,
        UseCredit,
        CorrectCredit
    } 
}