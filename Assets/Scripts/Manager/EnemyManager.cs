using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Transform destination;

    private ComponentPool<Enemy> enemyPool;

    public event Action<Enemy> enemySpawn;

    private void Start()
    {
        enemyPool = new ComponentPool<Enemy>(enemyPrefab);

        for (int i = 0; i < 10; i++)
        {
            var enemy = SpawnEnemy();

            Vector2 position;
            if (UnityEngine.Random.value < 0.25f)
            {
                position = Camera.main.ScreenToWorldPoint(new Vector2(0,
                    UnityEngine.Random.Range(0, Screen.height)));
            }
            else if (UnityEngine.Random.value < 0.5f)
            {
                position = Camera.main.ScreenToWorldPoint(new Vector2(
                    UnityEngine.Random.Range(Screen.width, 0), 0));
            }
            else if (UnityEngine.Random.value < 0.75f)
            {
                position = Camera.main.ScreenToWorldPoint(new Vector2(
                    UnityEngine.Random.Range(Screen.width, 0), Screen.height));
            }
            else
            {
                position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,
                    UnityEngine.Random.Range(0, Screen.height)));
            }
            enemy.transform.position = new Vector3(position.x, position.y, 0);

            var movement = enemy.gameObject.GetComponent<EnemyMovement>();
            movement.destination = destination;
            movement.moveSpeed = UnityEngine.Random.Range(0.9f, 1.1f);
        }
    }

    private Enemy SpawnEnemy()
    {
        var enemy = enemyPool.NewItem();
        enemySpawn?.Invoke(enemy);
        return enemy;
    }
}
