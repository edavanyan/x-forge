using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    public int health;

    public void Free()
    {
        Die();
    }

    public void New()
    {
        gameObject.SetActive(true);
    }

    private void Die()
    {
        gameObject.SetActive(false);
        transform.position = new Vector3(int.MaxValue, int.MaxValue, 0);
    }
}
