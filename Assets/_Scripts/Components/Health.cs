using System;

public class Health
{
    public Action DeadEvent;

    private float _health;
    private float _maxHealth;

    public Health(float health)
    {
        _health = health;
        _maxHealth = health;
    }

    public float CurrentHealth => _health;
    public float MaxHealth => _maxHealth;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0 )
            DeadEvent?.Invoke();
    }
}
