using System;

public class LevelResources
{

    public Action<ResourceDataPacket> OnMoneyAmountChanged;
    public int Money 
    { 
        get => _money;
        set
        {
            _money = value;
            OnMoneyAmountChanged?.Invoke(new() { Money = value });
        }
    }
    private int _money;

    public LevelResources(int startAmount)
    {
        Money = startAmount;
    }

    public bool TryWithdrawMoney(int amount)
    {
        if (Money < amount)
            return false;

        Money -= amount;
        return true;
    }
    public void AddMoney(int amount) 
        => Money += amount;
}
