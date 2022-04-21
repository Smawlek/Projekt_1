using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCheckpointController : MonoBehaviour
{
    public Transform voidControl;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            if (voidControl != null)
            {
                voidControl.GetComponent<VoidController>().SaveCheckpoint(spawnPoint);
            }
        }
    }
}
