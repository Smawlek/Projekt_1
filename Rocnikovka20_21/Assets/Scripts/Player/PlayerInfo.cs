using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int health = 100;

    public bool facingRight = true;

    public Transform healthBar;
    public Transform ui;
    public Transform lastSavestation;
    public Transform savingUiTransform;

    public PlayerAnimationsController playerAnimController;
    public PlayerMovement playerMovement;
    public PlayerShootin playerShootin;

    private int maxHealth = 100;

    private bool gameOverCalled = false;

    private UIHealth uiHealth;

    private List<Transform> killedEnemies = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        savingUiTransform.gameObject.SetActive(false);
        
        uiHealth = ui.GetComponent<UIHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();
    }

    private void UpdateHealthUI()
	{
        healthBar.GetComponent<ProgressBar>().SetValues(0, maxHealth, health);
        uiHealth.UpdateHealthText(maxHealth, health);
	}

    public void TakeDamage(int damage)
	{
        health -= damage;
	}

    public bool Heal(int health)
	{
        if(this.health >= maxHealth)
		{
            return false;
		} else if (this.health + health > maxHealth)
		{
            int a = this.health + health - maxHealth;

            this.health += health - a;

            return true;
		} else
		{
            this.health += health;

            return true;
        }
	}

    private void CheckVariables()
	{
        if(health > maxHealth)
		{
            health = maxHealth;
		}

        if(health <= 0 && !gameOverCalled)
		{
            gameOverCalled = true;
            playerMovement.canMove = false;
            playerMovement.canDash = false;

            Gameover();
		}

        UpdateHealthUI();
	}

    private void Gameover()
	{
        playerAnimController.FadeInBS();
        
        StartCoroutine(Respawning());
    }

    private IEnumerator Respawning()
	{
        yield return new WaitForSeconds(1f);

        playerShootin.RestartBullets();

        transform.position = lastSavestation.GetComponent<Savestation>().respawnPoint.position;
        health = maxHealth;
        transform.eulerAngles = new Vector3(0, 0, 0);
        facingRight = true;

        ReviveKilledEnemies();

        yield return new WaitForSeconds(2f);

        playerMovement.canMove = true;
        playerMovement.canDash = true;

        playerAnimController.FadeOutBS();

        gameOverCalled = false;
    }

    public void SaveSavestation(Transform savestation)
	{
        lastSavestation = savestation;

        savingUiTransform.gameObject.SetActive(true);

        StartCoroutine(SavingUI());
	}

    private IEnumerator SavingUI()
	{
        yield return new WaitForSeconds(2f);

        savingUiTransform.gameObject.SetActive(false);
    }

    public void AddKilledEnemy(Transform t)
	{
        bool contains = false;

        for(int i = 0; i < killedEnemies.Count; i++)
		{
            if(killedEnemies[i] == t)
			{
                contains = true;
                break;
			}
		}

        if(!contains)
		{
            killedEnemies.Add(t);
		}
	}

    private void ReviveKilledEnemies()
	{
        for(int i = 0; i < killedEnemies.Count; i++)
		{
            killedEnemies[i].GetComponent<EnemyInfo>().Restart();
		}

        killedEnemies.Clear();
	}
}
