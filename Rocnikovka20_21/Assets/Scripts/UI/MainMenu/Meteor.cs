using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Transform meteor;

    public Rigidbody2D rb;

    public float plusX = 1f;
    public float plusY = 1f;
    public float forHowLong = 5f;
    public float waitTillStart = 0f;
    public float rotationForce = 1f;

    public bool rotate = false;

    private Vector2 startingPos;
    private Vector3 startingRot;

    private float remainingTime;
    private float tillStart;
    private bool canStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if(waitTillStart > 0)
		{
            canStart = false;
            tillStart = waitTillStart;
		} else
		{
            rb.velocity = new Vector2(plusX, plusY);
        }

        remainingTime = forHowLong;

        startingPos = meteor.position;
        startingRot = meteor.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitTillStart > 0)
		{
            waitTillStart -= Time.deltaTime;
            
            if(waitTillStart <= 0)
			{
                canStart = true;

                rb.velocity = new Vector2(plusX, plusY);
            }
		}

        if(canStart)
		{
            remainingTime -= Time.deltaTime;

            if(rotate)
			{
                meteor.eulerAngles = new Vector3(0, 0, meteor.eulerAngles.z + rotationForce);
            }

            if (remainingTime <= 0)
            {
                Restart();
            }
        }
    }

    public void Restart()
	{
        transform.position = startingPos;
        transform.eulerAngles = startingRot;

        remainingTime = forHowLong;
	}
}
