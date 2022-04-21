using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesControl : MonoBehaviour
{
    public Transform player;
    public Transform doubleJumpTransform;
    public Transform dashTransform;
    public Transform wallJumpTransform;
    public Transform title;
    public Transform description;


    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
        CheckUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetVariables()
	{
        
	}

    public void CheckUpgrades()
	{
        if(player.GetComponent<PlayerMovement>().additionalJumps > 1)
		{
            doubleJumpTransform.GetComponent<UpgradesCheck>().Show(false);
		} else
		{
            doubleJumpTransform.GetComponent<UpgradesCheck>().Show(true);
        }

        if(player.GetComponent<PlayerMovement>().canDash)
		{
            dashTransform.GetComponent<UpgradesCheck>().Show(false);
		} else
		{
            dashTransform.GetComponent<UpgradesCheck>().Show(true);
        }

        if(player.GetComponent<PlayerMovement>().isWallJumpEnabled)
		{
            wallJumpTransform.GetComponent<UpgradesCheck>().Show(false);
		} else
		{
            wallJumpTransform.GetComponent<UpgradesCheck>().Show(true);
        }
	}

    public void HideText()
	{
        title.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
	}

    public void ShowText()
	{
        title.gameObject.SetActive(true);
        description.gameObject.SetActive(true);
    }

    public void PutOnLockedText()
	{
        title.GetComponent<TextMeshProUGUI>().text = "[LOCKED]";
        description.GetComponent<TextMeshProUGUI>().text = "??????????????????";
    }

    // Funkce pro tlačítka
    public void DoubleJumpButton()
	{
        ShowText();

        if(doubleJumpTransform.GetComponent<UpgradesCheck>().isLocked)
		{
            PutOnLockedText();
		} else
		{
            title.GetComponent<TextMeshProUGUI>().text = "Double Jump";
            description.GetComponent<TextMeshProUGUI>().text = "When mid air press SPACE to do additional jump";
        }
	}

    public void DashButton()
	{
        ShowText();

        if (dashTransform.GetComponent<UpgradesCheck>().isLocked)
        {
            PutOnLockedText();
        }
        else
        {
            title.GetComponent<TextMeshProUGUI>().text = "Dash";
            description.GetComponent<TextMeshProUGUI>().text = "Press LEFT SHIFT to do DASH";
        }
    }

    public void WallJumpButton()
	{
        ShowText();

        if (wallJumpTransform.GetComponent<UpgradesCheck>().isLocked)
        {
            PutOnLockedText();
        }
        else
        {
            title.GetComponent<TextMeshProUGUI>().text = "Wall Jump";
            description.GetComponent<TextMeshProUGUI>().text = "When wall sliding press SPACE to jump from the wall";
        }
    }
}
