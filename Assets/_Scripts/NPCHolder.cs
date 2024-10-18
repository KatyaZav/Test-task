using System.Collections.Generic;
using UnityEngine;

public class NPCHolder : MonoBehaviour
{
    [SerializeField] NPCSpawner _spawner;
    [SerializeField] Transform _player;

    private List<Enemy> _enemies = new List<Enemy>();

    public int EnemiesCount => _enemies.Count;

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.Init(_player);

        enemy.DeadEvent += RemoveEnemy;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemy.DeadEvent -= RemoveEnemy;
        _enemies.Remove(enemy);

        if (EnemiesCount < 5)
            _spawner.Spawn();
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.DeadEvent -= RemoveEnemy;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Updater();
        }
    }
}
