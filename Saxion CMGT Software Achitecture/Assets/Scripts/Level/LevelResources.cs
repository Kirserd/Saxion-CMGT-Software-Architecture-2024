public class LevelResources
{
    public int Money => _money;
    private int _money;

    public LevelResources(int startAmount)
    {
        _money = startAmount;
    }

    public bool TryWithdrawMoney(int amount)
    {
        if (_money < amount)
            return false;

        _money -= amount;
        return true;
    }
    public void AddMoney(int amount) 
        => _money += amount;
}
