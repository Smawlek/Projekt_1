using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishAttackControl : MonoBehaviour
{
    private int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVariables(int damage)
	{
        this.damage = damage;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
            collision.GetComponent<PlayerInfo>().TakeDamage(damage);
		}
	}
}
