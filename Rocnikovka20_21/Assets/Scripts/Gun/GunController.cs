using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float timeBetweenShootinForSub = 0f;
    public float timeBetweenShootinForShotgun = .5f;
    public float timeBetweenShootinForSniper = 3f;

    public Sprite gunSprite;
    public Sprite gunWithoutClipSprite;

    public GameObject clip;
    public GameObject submachineBullet;
    public GameObject shotgunBulletUp;
    public GameObject shotgunBulletDown;
    public GameObject shotgunBulletStraight;
    public GameObject sniperBullet;

    private int inClipForSubmachine = 30;
    private int inClipForShotgun = 10;
    private int inClipForSniper = 3;

    /*
    private float timeToReloadForSubmachine = 1f;
    private float timeToReloadForShotgun = 2f;
    private float timeToReloadForSniper = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetInClipForSubmachine()
	{
        return inClipForSubmachine;
	}

    public int GetInClipForShotgun()
	{
        return inClipForShotgun;
	}

    public int GetInClipForSniper()
	{
        return inClipForSniper;
	}

    public float GetTimeToReloadForSubmachine()
	{
        return timeToReloadForSubmachine;
	}

    public float GetTimeToReloadForShotGun()
	{
        return timeBetweenShootinForShotgun;
	}

    public float GetTimeToReloadForSniper()
	{
        return timeBetweenShootinForSniper;
	}*/
}
