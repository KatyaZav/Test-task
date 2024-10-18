using UnityEngine;

public class Player : MonoBehaviour, IInitable
{
    [Header("Components")]
    [SerializeField] private BarUpdater _healthBar;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    [Header("Settings")]
    [SerializeField] private float _hp;
    [SerializeField] private float _speed;

    private Mover _mover;
    private Health _health;

    public void Init()
    {
        _health = new Health(_hp);
        _mover = new Mover(_rigidbody, _animator, _speed);
        
        _health.DeadEvent += Dead;
    }
    
    public void SetMovement(Vector3 direction)
    {
        _mover.SetMovement(direction);
    }

    private void OnDestroy()
    {
        _health.DeadEvent -= Dead;        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            float damage = enemy.Damage;
            _health.TakeDamage(damage);
            _healthBar.SetAmmount(_health.CurrentHealth, _health.MaxHealth);
        }
    }

    private void Dead()
    {
        Debug.Log("player dead");
    }
}
