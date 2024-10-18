using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySettings : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxHealth;

    public float MaxHealth => _maxHealth;
    public float Speed => _speed;
}
