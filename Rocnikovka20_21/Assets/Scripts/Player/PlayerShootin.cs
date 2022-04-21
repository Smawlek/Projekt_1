using Luminosity.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootin : MonoBehaviour
{
    public int ammo = 60;

    public Transform gun;
    public Transform shootingPoint;
    public Transform clipPoint;
    public Transform gunTransform;
    public Transform player;

    public Transform ui;
    public Transform reloadingUI;

    private UIshootin uiShootin;
    private PlayerInfo playerInfo;
    public UI_Controller uiController;

    private int maxAmmo = 90;
    private int maxInClip = 30;
    private int inClip = 30;

    private int shootingMode = 0;
    private float timeBetweenShootin = 0f;

    private bool reloading = false;
    private bool checkingClips = false;

    private List<Transform> ammoClips = new List<Transform>();
    public List<GunManager> modes = new List<GunManager>();

    public AudioSource reloadAudio;

    // Start is called before the first frame update
    void Start()
    {
        uiShootin = ui.GetComponent<UIshootin>();
        playerInfo = player.GetComponent<PlayerInfo>();

        modes.Add(gunTransform.GetComponent<SubmachineGunManager>());
        modes.Add(gunTransform.GetComponent<ShotgunManager>());
        modes.Add(gunTransform.GetComponent<SniperGunManager>());

        reloadingUI.gameObject.SetActive(false);

        uiShootin.UpdateBulletText(inClip, ammo);
    }

    // Update is called once per frame
    void Update()
    {
        if(!uiController.isOverlayActive)
		{
            // Kontrola náklonu zbraně
            CheckWeaponRotation();
            CheckTimeBetweenShootin();

            // Přebíjení
            if (InputManager.GetButtonDown("Reload") && inClip < maxInClip && ammo != 0)
            {
                Reload();
            }

            // Střelba
            if (InputManager.GetButtonDown("Shooting") && inClip > 0 && timeBetweenShootin <= 0 && !reloading)
            {
                Shoot();
            }
        }

    }

    private void CheckTimeBetweenShootin()
	{
        timeBetweenShootin -= Time.deltaTime;

        if(shootingMode == 2)
		{
            uiShootin.UpdateTimeBetweenShootinBar((int)modes[shootingMode].timeBetweenShootin, 0, (int)timeBetweenShootin);
        } else
		{
            uiShootin.UpdateTimeBetweenShootinBar(1, 0, (int)timeBetweenShootin);
        }
	}

    private void CheckWeaponRotation()
	{
        float vertical = InputManager.GetAxisRaw("Vertical");

        int yRotation = 0;

        if(!playerInfo.facingRight)
		{
            yRotation = 180;
		}

        if(vertical == 0)
		{
            gunTransform.eulerAngles = new Vector3(0, 0 + yRotation, 0);
            shootingPoint.eulerAngles = new Vector3(0, 0 + yRotation, -90);
        }

        if(vertical > 0)
		{
            gunTransform.eulerAngles = new Vector3(gun.rotation.x, gun.rotation.y + yRotation, 10);
            shootingPoint.eulerAngles = new Vector3(shootingPoint.rotation.x, shootingPoint.rotation.y + yRotation, -74);
		}

        if(vertical < 0)
		{
            gunTransform.eulerAngles = new Vector3(gun.rotation.x, gun.rotation.y + yRotation, -10);
            shootingPoint.eulerAngles = new Vector3(shootingPoint.rotation.x, shootingPoint.rotation.y + yRotation, -100);
        }
    }

    private void Shoot()
	{
        timeBetweenShootin = modes[shootingMode].Shoot(player, shootingPoint);

        inClip--;

        uiShootin.UpdateBulletText(inClip, ammo);
    }

    private void Reload()
	{
        reloading = true;

        reloadingUI.gameObject.SetActive(true);

        gun.GetComponent<SpriteRenderer>().sprite = modes[shootingMode].gunWithoutClipSprite;

        GameObject a = Instantiate(modes[shootingMode].clip, clipPoint.position, clipPoint.rotation);
        a.GetComponent<ClipController>().player = player;

        reloadAudio.Play();

        StartCoroutine(FinishReloading());
    }

    public void RestartBullets()
	{
        if(inClip + ammo < maxAmmo / 2)
		{
            ammo = maxAmmo / 2;
		}

        Reload();
	}

    private IEnumerator FinishReloading()
	{
        yield return new WaitForSecondsRealtime(modes[shootingMode].timeToReload);

        int howMany;

        if (inClip != 0)
        {
            howMany = maxInClip - inClip;
        }
        else
        {
            if (ammo >= maxInClip)
            {
                howMany = maxInClip;
            }
            else
            {
                howMany = ammo;
            }
        }

        if (ammo - howMany >= 0)
        {
            inClip += howMany;
            ammo -= howMany;
        }

        uiShootin.UpdateBulletText(inClip, ammo);

        reloadingUI.gameObject.SetActive(false);

        gun.GetComponent<SpriteRenderer>().sprite = modes[shootingMode].gunSprite;

        reloading = false;
    }

    public bool AddAmmo(int amount)
	{
        if(ammo + amount <= maxAmmo)
		{
            ammo += amount;

            uiShootin.UpdateAddAmmoText(amount);
            uiShootin.UpdateBulletText(inClip, ammo);

            return true;
		} else if(ammo < maxAmmo)
		{
            int a = maxAmmo - ammo;
            ammo += a;

            uiShootin.UpdateAddAmmoText(a);
            uiShootin.UpdateBulletText(inClip, ammo);

            return true;
		} else
		{
            uiShootin.UpdateBulletText(inClip, ammo);

            return false;
        }
    }

    public void AddClip(Transform clip)
	{
        if(ammoClips.Count + 1 > 5)
		{
            List<Transform> c = new List<Transform>();

            for (int i = 1; i < 5; i++)
            {
                c.Add(ammoClips[i]);
            }

            c.Add(clip);
            ammoClips[0].GetComponent<ClipController>().DestroyClip();

            ammoClips = c;
		} else
		{
            ammoClips.Add(clip);
        }

        if(!checkingClips)
		{
            checkingClips = true;

            StartCoroutine(CheckClips());
		}
	}

    private IEnumerator CheckClips()
	{
        bool a;

        for(int i = 0; i < ammoClips.Count; i++)
		{
            a = false;

            // Čas existence
            if (ammoClips[i].GetComponent<ClipController>().GetTimeOfExistence() >= 10f)
			{
                a = true;
			}

            // Vzdálenost od hráče
            if (ammoClips[i].GetComponent<ClipController>().GetDistanceFromPlayer() >= 20f)
            {
                a = true;
            }

            // Kontrola
            if(a)
			{
                ammoClips[i].GetComponent<ClipController>().DestroyClip();
                ammoClips.RemoveAt(i);
            }
        }

        yield return new WaitForSecondsRealtime(2f);

        if(ammoClips.Count > 0)
		{
            StartCoroutine(CheckClips());
        } else
		{
            checkingClips = false;
		}
    }

    public bool ChangeShootingMode(int mode)
	{
        if(mode == shootingMode)
		{
            return false;
		}

        if(shootingMode >= modes.Count)
		{
            shootingMode = 0;
		}

        switch(shootingMode)
		{
            case 0:
                uiShootin.EnableSubmachineBulletImg(false);
                break;

            case 1:
                uiShootin.EnableShotgunBulletImg(false);
                break;

            case 2:
                uiShootin.EnableSniperBulletImg(false);
                break;
		}

        shootingMode = mode;

        switch (shootingMode)
        {
            case 0:
                uiShootin.EnableSubmachineBulletImg(true);             
                break;

            case 1:
                uiShootin.EnableShotgunBulletImg(true);
                break;

            case 2:
                uiShootin.EnableSniperBulletImg(true);
                break;
        }

        gun.GetComponent<SpriteRenderer>().sprite = modes[shootingMode].gunSprite;

        maxInClip = modes[shootingMode].inClip;
        ammo += inClip;
        
        if(ammo - maxInClip <= 0)
		{
            inClip = ammo;
            ammo = 0;
		} else
		{
            inClip = maxInClip;
            ammo -= maxInClip;
		}

        if(ammo > maxAmmo)
		{
            ammo = maxAmmo;
		}

        uiShootin.UpdateBulletText(inClip, ammo);

        return true;
	}
}
