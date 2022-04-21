using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishShootingCheck : MonoBehaviour
{
    public Transform jellyfish;

    private JellyfishAI jellyfishAI;

    // Start is called before the first frame update
    void Start()
    {
        jellyfishAI = jellyfish.GetComponent<JellyfishAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
            jellyfishAI.ShootDown();
		}
	}
}
