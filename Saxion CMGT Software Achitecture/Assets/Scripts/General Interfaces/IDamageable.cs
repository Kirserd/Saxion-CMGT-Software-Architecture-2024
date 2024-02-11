public interface IDamageable
{
    public float MaxHP { get; }
    public float MinHP { get; }
    public float HP { get; protected set; }

    public virtual void ApplyDamage(float amount) // beware of smol trolling
    {
        if (amount < 0 && HP < MaxHP)   OnHealed(-amount - (HP - amount - MaxHP));
        else if (amount > 0)            OnDamaged(amount - (HP - amount));
        else if (amount > HP)           OnCriticalHealth();

        HP -= amount;

        if (HP > MaxHP)         HP = MaxHP;
        else if (HP <= MinHP)   HP = MinHP;
    }

    protected abstract void OnHealed(float healAmount);
    protected abstract void OnDamaged(float damageAmount);
    protected abstract void OnCriticalHealth();
}