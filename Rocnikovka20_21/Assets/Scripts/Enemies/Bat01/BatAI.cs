using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : EnemyAI
{
    public float speed = 1f;
    public float bulletSpeed = 5f;

    public int bulletDamage = 5;

    public Rigidbody2D enemyRb;

    public bool moreAgressive = false;
    public bool upgraded = false;
    public bool bulletDestroyableByPlayer = false;

    public GameObject bullet;

    public List<Transform> shootingPoints = new List<Transform>();

    private bool isMoving = false;
    private float desX;
    private float count;

    private Collider2D playerCol;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(speed <= 0)
		{
            speed = 1f;
		}

        if(!knocked)
		{
            if(isMoving)
			{
                Fly();
			}
		}
    }

    private void Fly()
	{
        if(moreAgressive)
		{
            if (playerCol.transform.position.x != transform.position.x)
            {
                if (playerCol.transform.position.x < transform.position.x)
                {
                    enemyRb.velocity = new Vector2(-speed, -speed / 2);
                }
                else
                {
                    enemyRb.velocity = new Vector2(speed, -speed / 2);
                }
            }
            else
            {
                enemyRb.velocity = new Vector2(0, -speed);
            }
        } else
		{
            if(count <= 0)
			{
                desX = playerCol.transform.position.x;
            }

            if (desX != transform.position.x)
            {
                if (desX < transform.position.x)
                {
                    enemyRb.velocity = new Vector2(-speed, -speed / 2);
                }
                else
                {
                    enemyRb.velocity = new Vector2(speed, -speed / 2);
                }
            }
            else
            {
                enemyRb.velocity = new Vector2(0, -speed);
            }

            count -= Time.deltaTime;
        }
	}

    private void ShootOnCollision()
	{
		for(int i = 0; i < shootingPoints.Count; i++)
		{
            GameObject g = Instantiate(bullet, shootingPoints[i].position, shootingPoints[i].rotation);

            g.GetComponent<BatBulletsController>().batController = this;
        }
	}

	public void OnCollisionEnter2D(Collision2D collision)
    {
        if(isMoving)
		{
            if (collision.gameObject.tag == "Player")
			{
                enemyInfo.health = 0;
			} else
			{
                enemyInfo.health = 0;

                if(upgraded)
				{
                    ShootOnCollision();
				}
            }

            isMoving = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
            col.transform.GetComponent<PlayerInfo>().AddKilledEnemy(transform);
            count = 2;
            playerCol = col;
            desX = col.transform.position.x;
            isMoving = true;
		}
	}
}
