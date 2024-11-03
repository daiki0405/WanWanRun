using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager2 : MonoBehaviour
{

   [SerializeField] private GameObject player ;


    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player") ;
        Vector3 cube = player.transform.position;
        Vector3 sphere = this.transform.position;
        float dis = Vector3.Distance(cube,sphere);

        if(dis > 100.0f)
        {
            Destroy(this.gameObject) ;
        }


    
    }
}