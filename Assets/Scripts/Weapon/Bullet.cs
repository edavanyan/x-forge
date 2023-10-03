using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Bullet : MonoBehaviour, IPoolable
{
    public System.Action<Enemy> enemyHit;
    public Rigidbody2D rb;
    public Vector2 lookDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Free()
    {
        gameObject.SetActive(false);
    }

    public void New()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyHit?.Invoke(collision.GetComponent<Enemy>());
            enemyHit = null;
        }
    }
}
