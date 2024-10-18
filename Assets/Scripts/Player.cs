using UnityEngine;

public class Player : MonoBehaviour, IInitable
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Health _health;

    [SerializeField] private float _hp;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;

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
        }
    }


    private void Dead()
    {
        Debug.Log("player dead");
    }
}
