using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public float timeBetweenShootin = 0f;
    public float timeToReload = 1f;

    public int inClip = 30;
    public int damage = 1;

    public Sprite gunSprite;
    public Sprite gunWithoutClipSprite;

    public GameObject clip;
    public GameObject bullet;

    public GunManager thisManager;

    public Transform gunTransform;

    public AudioSource gunsSound;

    // Start is called before the first frame 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual float Shoot(Transform player, Transform shootingPoint)
	{
        GameObject b = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);

        gunsSound.Play();

        b.GetComponent<Bullet>().player = player;
        b.GetComponent<Bullet>().gunTransform = gunTransform;

        return timeBetweenShootin;
    }
}
