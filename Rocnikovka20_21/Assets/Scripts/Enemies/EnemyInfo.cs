using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public int health;
    public int damage;

    public EnemyAI ai;

    public bool canTakeKnockback = true;

    public Rigidbody2D enemyRig;

    // Knockback parametry
    public float knockbackLenght = 0.15f;

    private bool knockFromRight;
    private float knockback;
    private float knockbackCount;

    // Startovní parametry
    private int startHealth;
    private Vector3 startPos;
    private Quaternion startRot;
    private bool startFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        TakeStartInfo();
        ai.SetAI(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
		{
            Die();
		}

        KnockbackCheck();
    }

    public void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Player")
		{
            collision.transform.GetComponent<PlayerInfo>().TakeDamage(damage);
		}
    }

    public void TakeDamage(int damage, bool isItKnockFromRight, float knockbackForce)
	{
        health -= damage;

        if(canTakeKnockback)
		{
            ai.IsKnocked(true);

            knockback = knockbackForce;
            knockFromRight = isItKnockFromRight;
            knockbackCount = knockbackLenght;
        }
	}

    private void KnockbackCheck()
	{

        if(knockbackCount > 0)
		{
            if (knockFromRight)
            {
                enemyRig.velocity = new Vector2(-knockback, knockback);
            }
            else if (!knockFromRight)
            {
                enemyRig.velocity = new Vector2(knockback, knockback);
            }

            knockbackCount -= Time.deltaTime;
        } else
		{
            ai.IsKnocked(false);
		}
    }

    private void Die()
	{
        transform.gameObject.SetActive(false);
	}

    private void TakeStartInfo()
	{
        startHealth = health;
        startPos = this.transform.position;
        startRot = this.transform.rotation;
        startFacingRight = ai.GetFacingRight();
	}

    public void Restart()
	{
        health = startHealth;
        this.transform.position = startPos;
        this.transform.rotation = startRot;
        ai.SetFacingRight(startFacingRight);

        transform.gameObject.SetActive(true);
    }
}
