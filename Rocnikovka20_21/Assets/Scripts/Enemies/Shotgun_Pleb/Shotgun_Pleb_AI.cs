using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Pleb_AI : EnemyAI
{
    public float speed = 3f;
    public float enragedDistance = 3f;
    public float shootingDistance = 2f;
    public float runningAwayDistance = 1f;

    public int maxInClip = 5;

    public Transform wallCheck, gun, clipPoint, gunSpriteRenderer;
    public Sprite gunWithoutClip;
    public AudioSource reloadAudio;

    private int inClip;

    private float remainingTimeBtwShootin = 0;

    private bool canShoot = true;
    private bool reloading = false;

    private GunManager shotgun;

    // Start is called before the first frame update
    void Start()
    {
        inClip = maxInClip;
        shotgun = gun.GetComponent<ShotgunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();

        float distanceFromPlayer = Vector2.Distance(PlayerMovement.PlayerTransform.position, transform.position);

        if(inClip <= 0)
		{
            Reload();
		}

        if(distanceFromPlayer > enragedDistance)
		{
            Patrol();
        } else if(Math.Abs(PlayerMovement.PlayerTransform.position.y - transform.position.y) > 7.5f)
		{
            Patrol();
		} else
		{
            if(!canShoot || reloading)
			{
                RunAway();
			} else if(distanceFromPlayer <= enragedDistance && distanceFromPlayer > shootingDistance)
			{
                // Go near the player
                GoNearThePlayer();
            } else if(distanceFromPlayer <= shootingDistance && distanceFromPlayer > runningAwayDistance && canShoot && inClip > 0)
			{
                // Shoot the player
                enemyInfo.enemyRig.velocity = new Vector2(0, enemyInfo.enemyRig.velocity.y);

                Shoot();

            } else
			{
                // Run away
                RunAway();
			}
		}
    }

    void OnDrawGizmosSelected()
    {
        // Enraged Area
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enragedDistance);

        // Shooting Area
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingDistance);

        // Run Away Area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, runningAwayDistance);
    }

    private void CheckRotation()
	{
        if(isFacingRight)
		{
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        } else
		{
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
	}

    private void CheckVariables()
	{
        if(remainingTimeBtwShootin > 0)
		{
            remainingTimeBtwShootin -= Time.deltaTime;
		} else
		{
            canShoot = true;
		}
	}

    private void Patrol()
	{
        RaycastHit2D detect = Physics2D.Raycast(wallCheck.position, Vector2.up, 0.1f, 1 << LayerMask.NameToLayer("Ground"));

        if (detect)
        {
            isFacingRight = !isFacingRight;

            CheckRotation();
        }

        int a = isFacingRight ? 1 : -1;

        enemyInfo.enemyRig.velocity = new Vector2(a * speed, enemyInfo.enemyRig.velocity.y);
    }

    private void GoNearThePlayer()
	{
        int isPlayerRight = 1;

        if (PlayerMovement.PlayerTransform.position.x < transform.position.x)
        {
            isPlayerRight = -1;
        }

        isFacingRight = isPlayerRight == -1 ? false : true;

        CheckRotation();

        enemyInfo.enemyRig.velocity = new Vector2(isPlayerRight * speed, enemyInfo.enemyRig.velocity.y);
    }

    private void RunAway()
	{
        int isPlayerRight = -1;

        if(PlayerMovement.PlayerTransform.position.x < transform.position.x)
		{
            isPlayerRight = 1;
		}

        isFacingRight = isPlayerRight == -1 ? false : true;

        CheckRotation();

        enemyInfo.enemyRig.velocity = new Vector2(isPlayerRight * speed, enemyInfo.enemyRig.velocity.y);
    }

    private void Shoot()
	{
        inClip--;
        canShoot = false;
        remainingTimeBtwShootin = shotgun.Shoot(PlayerMovement.PlayerTransform, wallCheck);
    }

    private void Reload()
	{
        reloading = true;

        gunSpriteRenderer.GetComponent<SpriteRenderer>().sprite = gunWithoutClip;

        GameObject a = Instantiate(shotgun.clip, clipPoint.position, clipPoint.rotation);
        a.GetComponent<ClipController>().player = PlayerMovement.PlayerTransform;

        reloadAudio.Play();

        StartCoroutine(FinishReloading());
    }

    private IEnumerator FinishReloading()
    {
        yield return new WaitForSecondsRealtime(shotgun.timeToReload);

        inClip = maxInClip;

        gunSpriteRenderer.GetComponent<SpriteRenderer>().sprite = shotgun.gunSprite;

        reloading = false;
    }
}

