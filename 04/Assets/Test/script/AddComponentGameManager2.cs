using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddComponentGameManager2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<GameManager2>() ;
    }


}
