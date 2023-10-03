using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Transform destination;

    private float waveDelay = 3f;

    private ComponentPool<Enemy> enemyPool;

    public event Action<Enemy> enemySpawn;

    private void Start()
    {
        enemyPool = new ComponentPool<Enemy>(enemyPrefab);
        StartCoroutine(SpawnScheduler());
    }

    private IEnumerator SpawnScheduler()
    {
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                var enemy = SpawnEnemy();

                Vector2 position;
                if (UnityEngine.Random.value < 0.25f)
                {
                    position = Camera.main.ScreenToWorldPoint(new Vector2(-100,
                        UnityEngine.Random.Range(0, Screen.height)));
                }
                else if (UnityEngine.Random.value < 0.5f)
                {
                    position = Camera.main.ScreenToWorldPoint(new Vector2(
                        UnityEngine.Random.Range(0, Screen.width), -100));
                }
                else if (UnityEngine.Random.value < 0.75f)
                {
                    position = Camera.main.ScreenToWorldPoint(new Vector2(
                        UnityEngine.Random.Range(0, Screen.width), Screen.height + 100));
                }
                else
                {
                    position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + 100,
                        UnityEngine.Random.Range(0, Screen.height)));
                }
                enemy.transform.position = new Vector3(position.x, position.y, 0);

                var movement = enemy.gameObject.GetComponent<EnemyMovement>();
                movement.destination = destination;
                movement.moveSpeed = UnityEngine.Random.Range(0.9f, 1.1f);
            }

            waveDelay = UnityEngine.Random.Range(2, 10);
            yield return new WaitForSeconds(waveDelay);
        }
    }

    private Enemy SpawnEnemy()
    {
        var enemy = enemyPool.NewItem();
        enemySpawn?.Invoke(enemy);
        return enemy;
    }

    public void KillEnemy(Enemy enemy)
    {
        enemyPool.DestoryItem(enemy);
    }
}
