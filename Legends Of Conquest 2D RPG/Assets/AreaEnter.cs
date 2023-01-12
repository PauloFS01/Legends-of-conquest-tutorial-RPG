using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AreaEnter : MonoBehaviour
{
    public string transitionAreaName;

    void Start()
    {
        if(transitionAreaName == Player.instance.transitionName)
        {
            Player.instance.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
