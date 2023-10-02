using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject character;
    public GameObject environment;

    public LayerManager layerManager;
    public EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        //layerManager.RegisterEntity(character.GetComponent<SpriteRenderer>());
        //for (var i = 0; i < environment.transform.childCount; i++)
        //{
        //    var child = environment.transform.GetChild(i);
        //    layerManager.RegisterEntity(child.GetComponent<SpriteRenderer>());
        //}
        //enemyManager.enemySpawn += enemy => layerManager.RegisterEntity(enemy.GetComponent<SpriteRenderer>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
