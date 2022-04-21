using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : Bullet
{
    public CanonController canonController;

    public bool destroyableByPlayerBullets = true;

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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool fromRight = true;

            collision.gameObject.GetComponent<PlayerInfo>().TakeDamage(damage);

            if(collision.transform.position.x > this.transform.position.x)
			{
                fromRight = false;
			}

            collision.gameObject.GetComponent<PlayerMovement>().ActiveKnockBack(fromRight);

            DestroyBullet();
        } else if(collision.gameObject.tag == "Bullet" && !destroyableByPlayerBullets)
		{
            // Ignorace kolize
		}
        else
        {
            DestroyBullet();
        }
    }

	public override void SetVariables()
	{
        damage = canonController.damage;
        speed = canonController.bulletSpeed;
        destroyableByPlayerBullets = canonController.bulletDestroyableByPlayerBullets;

        SetSpeed();
	}

    private void DestroyBullet()
	{
        Destroy(transform.gameObject);
    }
}
