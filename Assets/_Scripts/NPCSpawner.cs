using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private float _spawnTime;
    [SerializeField] NPCHolder _NPCHolder;
    [SerializeField] private int _maxNPCCount;

    [SerializeField] Transform _leftBorder, _rightBorder;

    private float _timer;

    private void Start()
    {
        for (var i = 0; i < 5; i++)
        {
            Spawn();
        }
    }

    private void Update()
    {
        if (_NPCHolder.EnemiesCount > _maxNPCCount)
            return;

        _timer += Time.deltaTime;

        if (_timer > _spawnTime)
        {
            _timer = 0;
            Spawn();
        }
    }

    public void Spawn()
    {
        var choosedEnemy = ChooseEnemy(_enemies);
        Enemy enemy = Instantiate(choosedEnemy, GetRandomPosition(), Quaternion.identity, transform);
        _NPCHolder.AddEnemy(enemy);
    }

    private Enemy ChooseEnemy(Enemy[] enemy)
    {
        int index = UnityEngine.Random.Range(0, enemy.Length);
        return enemy[index];
    }

    private Vector3 GetRandomPosition()
    {
        float x = UnityEngine.Random.Range(_leftBorder.position.x, _rightBorder.position.x);
        float y = _leftBorder.position.y;
        float z = UnityEngine.Random.Range(_leftBorder.position.z, _rightBorder.position.z);

        return new Vector3(x, y, z);
    }
}
