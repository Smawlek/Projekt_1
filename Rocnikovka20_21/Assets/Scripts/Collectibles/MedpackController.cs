using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedpackController : MonoBehaviour
{
    public int heal = 10;
    public float floatStrength = 0.25f;

    private Vector2 floatY;
    private float originalY;


    // Start is called before the first frame update
    void Start()
    {
        this.originalY = this.transform.position.y;
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

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            bool a = obj.GetComponent<PlayerInfo>().Heal(heal);

            if (a)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
