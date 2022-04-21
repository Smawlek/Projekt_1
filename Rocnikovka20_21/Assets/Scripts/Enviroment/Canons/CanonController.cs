using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    public int damage = 1;

    public float bulletSpeed = 1f;
    public float timeBetweenShootin = 2f;

    public bool fireImmediately = false;
    public bool bulletDestroyableByPlayerBullets = true;

    public Transform bullet;
    public Transform shootingPoint;

    private float remainingTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
       if(fireImmediately)
		{
            Shoot();
		}

        remainingTime = timeBetweenShootin;
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingTime <= 0)
		{
            Shoot();
		} else
		{
            remainingTime -= Time.deltaTime;
		}
    }

    private void Shoot()
	{
        GameObject b = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation).gameObject;

        b.GetComponent<CanonBullet>().canonController = this;

        remainingTime = timeBetweenShootin;
    }
}
