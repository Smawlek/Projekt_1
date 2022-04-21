using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishAI : EnemyAI
{
    public int bulletDamage = 5;

    public float bulletSpeed = 5f;
	public float speed = 1f;
    public float timeBetweenAttack = 2f;
    public float attackActiveTime = 1f;

    public bool isAttackActive = true;
    public bool canShoot = true;

    public Transform leftCheck;
    public Transform rightCheck;
    public Transform attackTransform;
    public Transform shootingPointDown;

    public GameObject bullet;

    public List<Sprite> attackSprites = new List<Sprite>();

    private int activeAttackAnimSprite = 0;

    private float remainingTimeBetweenAttack;
    private float remainingActiveTime;
    private float attackAnimTimeStep = 0.5f;
    private float remainingAttackAnimTimeStep;

    private bool isAttackSet = false;

    private JellyfishAttackControl attackControl;

    // Start is called before the first frame update
    void Start()
    {
        SetAttackControl();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();

        Move();

        if(isAttackActive)
		{
            if (remainingTimeBetweenAttack <= 0)
            {
                if (!isAttackSet)
                {
                    Attack();
                } else if (isAttackSet && remainingActiveTime > 0)
                {
                    AttackAnimation();

                    remainingActiveTime -= Time.deltaTime;
                } else if (remainingActiveTime <= 0)
				{
                    isAttackSet = false;
                    remainingTimeBetweenAttack = timeBetweenAttack;

                    attackTransform.gameObject.SetActive(false);
				}
			} else
			{
                remainingTimeBetweenAttack -= Time.deltaTime;
			}
		}
    }

    public void ShootDown()
	{
        GameObject g = Instantiate(bullet, shootingPointDown.position, shootingPointDown.rotation);

        g.GetComponent<JellyfishBullet>().jellyfishAI = this;
    }

    private void SetAttackControl()
	{
        remainingTimeBetweenAttack = timeBetweenAttack;
        remainingAttackAnimTimeStep = attackAnimTimeStep;

        attackTransform.gameObject.SetActive(false);

        attackControl = attackTransform.GetComponent<JellyfishAttackControl>();
        attackControl.SetVariables(enemyInfo.damage);
	}

    private void Attack()
	{
        remainingActiveTime = attackActiveTime;

        attackTransform.gameObject.SetActive(true);

        isAttackSet = true;
	}

    private void AttackAnimation()
	{
        if(remainingAttackAnimTimeStep <= 0)
		{
            activeAttackAnimSprite++;

            if(activeAttackAnimSprite >= attackSprites.Count)
			{
                activeAttackAnimSprite = 0;
			}

            attackTransform.GetComponent<SpriteRenderer>().sprite = attackSprites[activeAttackAnimSprite];
            remainingAttackAnimTimeStep = attackAnimTimeStep;
		} else
		{
            remainingAttackAnimTimeStep -= Time.deltaTime;
		}
	}

    private void Move()
    {
        RaycastHit2D leftDetect = Physics2D.Raycast(leftCheck.position, Vector2.left, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
        RaycastHit2D rightDetect = Physics2D.Raycast(rightCheck.position, Vector2.right, 0.1f, 1 << LayerMask.NameToLayer("Ground"));

        if(leftDetect)
		{
            isFacingRight = true;
		} 
        
        if (rightDetect)
		{
            isFacingRight = false;
		}

        int a = isFacingRight ? 1 : -1;

        enemyInfo.enemyRig.velocity = new Vector2(a * speed, 0);
    }

    private void CheckVariables()
	{
        if (speed <= 0)
        {
            speed = 1f;
        }
    }
}
