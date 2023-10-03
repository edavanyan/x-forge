using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 10f;

    private ComponentPool<Bullet> bulletPool;

    private List<Bullet> activeBullets = new List<Bullet>();
    private List<Bullet> removedBullets = new List<Bullet>();

    private void Start()
    {
        bulletPool = new ComponentPool<Bullet>(bulletPrefab.GetComponent<Bullet>(), null);
    }

    void Update()
    {
        var screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        foreach (var bullet in activeBullets)
        {
            if (bullet.transform.position.x > screenBounds.x ||
                bullet.transform.position.y > screenBounds.y ||
                bullet.transform.position.x < -screenBounds.x ||
                bullet.transform.position.y < -screenBounds.y)
            {
                DestroyBullet(bullet);
            }
        }

        foreach (var bullet in removedBullets)
        {
            activeBullets.Remove(bullet);
            bulletPool.DestoryItem(bullet);
        }
        removedBullets.Clear();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        foreach (var bullet in activeBullets)
        {
            var rb = bullet.rb;
            rb.MovePosition(rb.position + bullet.lookDir.normalized * bulletForce * Time.fixedDeltaTime);
        }
    }

    private void DestroyBullet(Bullet bullet)
    {
        if (bullet.gameObject.activeSelf)
        {
            removedBullets.Add(bullet);
        }
    }

    void Shoot()
    {
        var bullet = CreateBullet();
        bullet.lookDir = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
        var angle = Mathf.Atan2(bullet.lookDir.y, bullet.lookDir.x) * Mathf.Rad2Deg - 45f;

        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.rotation = angle;
    }

    private Bullet CreateBullet()
    {
        var bullet = bulletPool.NewItem();
        bullet.transform.position = firePoint.position;
        activeBullets.Add(bullet);

        RegisterHitListener(bullet);

        return bullet;
    }

    private void RegisterHitListener(Bullet bullet)
    {
        bullet.enemyHit = enemy =>
        {
            GameManager.Instance.enemyManager.KillEnemy(enemy);
            StartCoroutine(RemoveBulletOnMainThread(bullet));
        };
    }

    IEnumerator RemoveBulletOnMainThread(Bullet bullet)
    {
        yield return new WaitForFixedUpdate();

        DestroyBullet(bullet);
    }
}
