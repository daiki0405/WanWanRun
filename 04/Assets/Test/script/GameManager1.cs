using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager1 : MonoBehaviour
{
    public GameObject ground;
    public GameObject player;
    private float _spawnpoint = 0;
    void Update()
    {
        if (player.transform.position.x < _spawnpoint + 30)
        {
            Instantiate(ground, new Vector3(_spawnpoint, -8, 0), Quaternion.identity);
            _spawnpoint -= 50;
        }

    
    }
}