using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishBullet : Bullet
{
    public JellyfishAI jellyfishAI;

    // Start is called before the first frame update
    void Start()
    {
        StartFunctions();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFunctions();
    }

    public override void SetVariables()
    {
        damage = jellyfishAI.bulletDamage;
        speed = jellyfishAI.bulletSpeed;

        SetSpeed();
	}

    public void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.gameObject.tag == "Player")
		{
            player = collision.transform;

            player.GetComponent<PlayerInfo>().TakeDamage(damage);
		}

        Destroy(transform.gameObject);
    }
}
