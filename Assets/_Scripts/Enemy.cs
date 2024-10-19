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
    private Player _playerComponent;

    private bool _isDead = false;
    private bool _canHit = false;

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

        _timer.TimerOverEvent += ActivateCanHit;
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

    private void OnCollisionEnter(Collision collision)
    {
        _playerComponent = collision.gameObject.GetComponent<Player>();
    }

    private void OnCollisionExit(Collision collision)
    {
        var plr = collision.gameObject.GetComponent<Player>();

        if (plr != null)
        {
            _playerComponent = null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_playerComponent == null)
            return;

        if (_canHit == false)
            return;

        OnHit();
    }
    
    private void ActivateCanHit()
    {
        _canHit = true;
        _timer.Stop();
    }

    private void OnHit()
    {
        _canHit = false;
        _timer.Start();

        _playerComponent.TakeDamage(_setting.Damage);
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
