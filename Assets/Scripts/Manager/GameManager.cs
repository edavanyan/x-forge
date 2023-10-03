using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject character;
    public GameObject environment;

    public LayerManager layerManager;
    public EnemyManager enemyManager;
    public ShootingManager shootingManager;

    private void Start()
    {
        Instance = this;
    }
}
