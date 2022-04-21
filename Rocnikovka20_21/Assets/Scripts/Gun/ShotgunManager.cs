using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunManager : GunManager
{
    public GameObject bulletUp;
    public GameObject bulletDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override float Shoot(Transform player, Transform shootingPoint)
	{
        GameObject b = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        b.GetComponent<Bullet>().player = player;
        b.GetComponent<Bullet>().gunTransform = gunTransform;

        b = Instantiate(bulletUp, shootingPoint.position - new Vector3(0, 0.55f, 0), shootingPoint.rotation);
        b.GetComponent<Bullet>().player = player;
        b.GetComponent<Bullet>().gunTransform = gunTransform;

        b = Instantiate(bulletDown, shootingPoint.position + new Vector3(0, 0.55f, 0), shootingPoint.rotation);
        b.GetComponent<Bullet>().player = player;
        b.GetComponent<Bullet>().gunTransform = gunTransform;

        gunsSound.Play();

        return timeBetweenShootin;
    }
}
