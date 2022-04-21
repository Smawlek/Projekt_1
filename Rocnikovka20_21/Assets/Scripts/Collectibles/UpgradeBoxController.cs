using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBoxController : MonoBehaviour
{
    public int mode = 1;
    public float floatStrength = 0.25f;

    private Vector2 floatY;
    private float originalY;


    // Start is called before the first frame update
    void Start()
    {
        this.originalY = this.transform.position.y;

        if (mode < 1 || mode > 3)
		{
            mode = 1;
		}
    }

    // Update is called once per frame
    void Update()
    {
        Float(); 
    }

    private void Float()
    {
        floatY = transform.position;
        floatY.y = originalY + (Mathf.Sin(Time.time) * floatStrength);
        transform.position = floatY;
    }

    public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
            if(mode == 1)
			{
                collision.GetComponent<PlayerMovement>().UnlockDoubleJump();
			} else if(mode == 2)
			{
                collision.GetComponent<PlayerMovement>().UnlockDash();
            } else if(mode == 3)
			{
                collision.GetComponent<PlayerMovement>().UnlockWallJump();
            }

            StartCoroutine(collision.GetComponent<UI_Controller>().ShowUpgradeText(this.gameObject));
		}
	}
}
