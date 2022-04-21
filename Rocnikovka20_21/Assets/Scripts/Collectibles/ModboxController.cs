using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModboxController : MonoBehaviour
{
    public int changeToMode = 1;
    public float floatStrength = 0.25f;

    private Vector2 floatY;
    private float originalY;

    // Start is called before the first frame update
    void Start()
    {
        this.originalY = this.transform.position.y;

        if (changeToMode >= 3 || changeToMode < 0)
        {
            changeToMode = 0;
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

    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            bool a = obj.GetComponent<PlayerShootin>().ChangeShootingMode(changeToMode);

            if (a)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
