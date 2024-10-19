using System;
using UnityEngine;

public class Player : MonoBehaviour, IInitable
{
    [Header("Components")]
    [SerializeField] private BarUpdater _healthBar;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    [Header("Settings")]
    [SerializeField] private EntitySettings _setting;
    [SerializeField] private float _raduis;
    [SerializeField] private LayerMask _enemyMask;

    private Mover _mover;
    private Health _health;
    private Timer _timer;

    private bool _canHit = false;

    public void Init()
    {
        _health = new Health(_setting.MaxHealth);
        _mover = new Mover(_rigidbody, _animator, _setting.Speed);
        _timer = new Timer(_setting.CoolDown);

        _health.DeadEvent += OnDead;
        _timer.TimerOverEvent += ActivateCanHit;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
        _healthBar.SetAmmount(_health.CurrentHealth, _health.MaxHealth);
    }

    public void SetMovement(Vector3 direction)
    {
        _mover.SetMovement(direction);
    }

    private void OnDestroy()
    {
        _health.DeadEvent -= OnDead;        
        _timer.TimerOverEvent -= ActivateCanHit;
    }

    private void Update()
    {
        _timer.UpdateTime();

        if (_canHit == false)
            return;

        var enemies = Physics.OverlapSphere(transform.position, _raduis, _enemyMask.value);
        print(enemies.Length);

        if (enemies.Length == 0)
            return;

        var min = Mathf.Infinity;
        GameObject finalTarget = enemies[0].gameObject;

        foreach(var enemy in enemies)
        {
            var distance = (enemy.transform.position - transform.position);
            if (distance.magnitude < min)
            {
                finalTarget = enemy.gameObject;
                min = distance.magnitude;
            }
        }

        OnHit(finalTarget);
    }

    private void ActivateCanHit()
    {
        _canHit = true;
        _timer.Stop();
    }

    private void OnHit(GameObject target)
    {
        _canHit = false;
        _timer.Start();

        target.GetComponent<Enemy>().TakeDamage(_setting.Damage);
    }

    private void OnDead()
    {
        Debug.Log("player dead");
    }
}
