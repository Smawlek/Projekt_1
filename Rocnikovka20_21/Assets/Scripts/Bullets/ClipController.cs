using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipController : MonoBehaviour
{
    public Transform obj;
    public Rigidbody2D rb;
    public Collider2D clipCollider;
    public Collider2D playerCollider;
    public Transform player;

    private float timeOfExistence = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = player.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, clipCollider, true);

        player.GetComponent<PlayerShootin>().AddClip(obj);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        timeOfExistence += Time.deltaTime;
    }

    public void DestroyClip()
	{
        Destroy(obj.gameObject);
	}

    public float GetTimeOfExistence()
	{
        return timeOfExistence;
	}

    public float GetDistanceFromPlayer()
	{
        return Vector3.Distance(player.position, obj.position);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
            Physics2D.IgnoreCollision(collision, clipCollider, true);
        }
	}
}
