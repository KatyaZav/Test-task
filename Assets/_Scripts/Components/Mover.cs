using UnityEngine;

public class Mover
{
    private const string _runName = "run";
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float _speed;

    public Mover(Rigidbody rigidbody, Animator animator, float speed)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _speed = speed;
    }

    public void SetMovement(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speed;

        if (_animator != null)
        {
           _animator.SetBool(_runName, direction != Vector3.zero);
        }
    }
}
