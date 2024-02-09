public interface IDamageable
{
    public float MaxHP { get; protected set; }
    public float MinHP { get; protected set; }
    public float HP { get; protected set; }

    public void ApplyDamage(float amount) // beware of smol trolling
    {
        if (amount < 0 && HP < MaxHP)   OnHealed(-amount - (HP - amount - MaxHP));
        else if (amount > 0)            OnDamaged(amount - (HP - amount));
        else if (amount > HP)           OnCriticalHealth();

        HP -= amount;

        if (HP > MaxHP)         HP = MaxHP;
        else if (HP <= MinHP)   HP = MinHP;
    }

    protected void OnHealed(float healAmount);
    protected void OnDamaged(float damageAmount);
    protected void OnCriticalHealth();
}