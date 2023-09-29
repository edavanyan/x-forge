using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public Bullet daggerPrefab;

    public float daggerForce = 20f;

    private ComponentPool<Bullet> daggerPool;

    private void Start()
    {
        daggerPool = new ComponentPool<Bullet>(daggerPrefab);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        var lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 45f;
        var dagger = daggerPool.NewItem();
        var rb = dagger.GetComponent<Rigidbody2D>();

        rb.rotation = angle;
        rb.AddForce(lookDir.normalized * daggerForce, ForceMode2D.Impulse);
    }
}
