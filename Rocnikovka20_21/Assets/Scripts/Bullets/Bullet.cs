using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public int damage = 1;
    public float timeToDisappear = 5f;
    public float knockbackForce = 3;

    public Rigidbody2D rb;

    public Transform player;
    public Transform gunTransform;

    public GunManager gunManager;

    public bool willBulletDestroy = true;
    public bool friendly = true;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(friendly)
		{
            FriendlyCollision(collision);
		} else
		{
            HostileCollision(collision);
		}

        Destroy(transform.gameObject);
    }

    private void FriendlyCollision(Collision2D collision)
	{
        if (collision.gameObject.tag == "Enemy")
        {
            bool a = true;

            if (transform.position.x < collision.transform.position.x)
            {
                a = false;
            }

            player.GetComponent<PlayerInfo>().AddKilledEnemy(collision.transform);
            collision.transform.GetComponent<EnemyInfo>().TakeDamage(damage, a, knockbackForce);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    private void HostileCollision(Collision2D collision)
	{
        if(collision.gameObject.tag == "Player")
		{
            collision.transform.GetComponent<PlayerInfo>().TakeDamage(damage);
		} else
		{
            Destroy(transform.gameObject);
        }
    }

	public void StartFunctions()
	{
        SetSpeed();
        SetVariables();
	}

    public virtual void SetVariables()
	{
        damage = gunManager.damage;
    }

    public void UpdateFunctions()
	{
        TimeToDestroy();
	}

	public void SetSpeed()
	{
        rb.velocity = transform.up * speed;
    }

    public void TimeToDestroy()
	{
        timeToDisappear -= Time.deltaTime;

        if (timeToDisappear <= 0 && willBulletDestroy)
        {
            Destroy(transform.gameObject);
        }
    }
}
