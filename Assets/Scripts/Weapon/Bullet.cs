using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Bullet : MonoBehaviour, IPoolable
{
    public void Free()
    {
        gameObject.SetActive(false);
    }

    public void New()
    {
        gameObject.SetActive(true);
    }
}
