using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateController : MonoBehaviour
{
    public int ammo = 30;
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

    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            bool a = obj.GetComponent<PlayerShootin>().AddAmmo(ammo);

            if(a)
			{
                Destroy(this.gameObject);
			}
        }
    }
}
