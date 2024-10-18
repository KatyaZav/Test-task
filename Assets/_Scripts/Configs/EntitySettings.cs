using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EntityData", menuName = "Configs/EntityData", order = 51)]
public class EntitySettings : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;

    public float MaxHealth => _maxHealth;
    public float Speed => _speed;
    public float Damage => _damage;
}
