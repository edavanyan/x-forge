using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 10f;

    private ComponentPool<Bullet> bulletPool;

    private List<Bullet> activeBullets = new List<Bullet>();
    private List<Bullet> removedBullets = new List<Bullet>();

    private void Start()
    {
        bulletPool = new ComponentPool<Bullet>(bulletPrefab.GetComponent<Bullet>());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

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

        foreach(var bullet in removedBullets)
        {
            activeBullets.Remove(bullet);
        }
        removedBullets.Clear();
    }

    private void DestroyBullet(Bullet bullet)
    {
        removedBullets.Add(bullet);
        bulletPool.DestoryItem(bullet);
    }

    void Shoot()
    {
        Vector2 lookDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 45f;

        var bullet = bulletPool.NewItem();
        bullet.transform.position = firePoint.position;
        activeBullets.Add(bullet);

        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.rotation = angle;
        rb.AddForce(lookDir.normalized * bulletForce, ForceMode2D.Impulse);
    }
}
