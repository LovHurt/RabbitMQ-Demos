namespace BankingContract;

public interface IMoneyCommand
{
    public int Money { get; set; }

    public DateTime Date { get; set; }
}

public class MoneyCommand: IMoneyCommand
{
    public int Money { get; set; }
    public DateTime Date { get; set; }
}