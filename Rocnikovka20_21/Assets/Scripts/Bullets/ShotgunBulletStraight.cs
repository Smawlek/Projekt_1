using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletStraight : Bullet
{
    // 1 - straigt
    // 2 - up
    // 3 - down
    public int mode = 1;

    // Start is called before the first frame update
    void Start()
    {
        gunManager = gunTransform.GetComponent<ShotgunManager>();

        StartFunctions();

        if (mode == 2)
		{
            //rb.velocity = new Vector3(transform.up.x, transform.up.y + 3, transform.up.z) * speed;

            if (player.GetComponent<PlayerInfo>().facingRight)
			{
                transform.eulerAngles = new Vector3(0, 0, -130);
            } else
			{
                transform.eulerAngles = new Vector3(0, 180, -130);
            }
		} else if (mode == 3)
		{
            //rb.velocity = new Vector3(transform.up.x, transform.up.y - 3, transform.up.z) * speed;

            if (player.GetComponent<PlayerInfo>().facingRight)
			{
                transform.eulerAngles = new Vector3(0, 0, -50);
            } else
			{
                transform.eulerAngles = new Vector3(0, 180, -50);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFunctions();
    }
}
