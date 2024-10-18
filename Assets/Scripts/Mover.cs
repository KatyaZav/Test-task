using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private string _runName;
    
    public void SetMovement(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speed;

        if (_animator != null)
        {
           _animator.SetBool(_runName, direction != Vector3.zero);
        }
    }
}
