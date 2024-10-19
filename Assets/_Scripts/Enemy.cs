using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action<Enemy> DeadEvent;
   
    [Header("Components")]
    [SerializeField] private BarUpdater _healthBar;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    [Header("Settings")]
    [SerializeField] private EntitySettings _setting;
    [SerializeField] private ParticleSystem _deadParticle, _damageParticle;
    [SerializeField] private float _particleDeastroyTime;

    private Mover _mover;
    private Health _health;
    private Timer _timer;

    private Transform _player;
    private bool _isDead = false;


    public float Damage() => _setting.Damage;

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
        _healthBar.SetAmmount(_health.CurrentHealth, _health.MaxHealth);

        var particle = Instantiate(_damageParticle, transform);
        Destroy(particle.gameObject, _particleDeastroyTime);
    }

    public void Init(Transform player)
    {
        _player = player;
        _isDead = false;

        _health = new Health(_setting.MaxHealth);
        _mover = new Mover(_rigidbody, _animator, _setting.Speed);
        _timer = new Timer(_setting.CoolDown);

        _health.DeadEvent += OnDead;
    }

    public void Updater()
    {
        Vector3 direction = _player.position - transform.position;
        direction = direction.normalized;
        _mover.SetMovement(direction);
        //transform.LookAt(_player);
    }

    private void OnDestroy()
    {
        _health.DeadEvent -= OnDead;
    }

    protected virtual void OnDead()
    {
        DeadEvent?.Invoke(this);
        Debug.Log($"{gameObject.name} dead");

        var particle = Instantiate(_deadParticle, transform);
        Destroy(particle.gameObject, _particleDeastroyTime);

        Destroy(gameObject);
    }
}
