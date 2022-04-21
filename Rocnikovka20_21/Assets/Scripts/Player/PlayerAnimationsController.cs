using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class PlayerAnimationsController : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public Transform player;
    public Transform ui;
    public Transform addAmmo;

    public UI_Controller uiController;

    private Animator uiAnimator;
    private Animator addAmmoAnimator;

    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
        FadeOutBS();
    }

    // Update is called once per frame
    void Update()
    {
        if(!uiController.isOverlayActive)
		{
            CheckDirection();
        }
    }

    private void SetVariables()
	{
        uiAnimator = ui.GetComponent<Animator>();
        addAmmoAnimator = addAmmo.GetComponent<Animator>();
	}

    private void CheckDirection()
	{
        float horizontal = InputManager.GetAxisRaw("Horizontal");

        if(horizontal > 0)
		{
            player.eulerAngles = new Vector3(0, 0, 0);

            playerInfo.facingRight = true;
		} else if (horizontal < 0)
		{
            player.eulerAngles = new Vector3(0, 180, 0);

            playerInfo.facingRight = false;
		}
    }

    public void FadeInBS()
	{
        uiAnimator.SetTrigger("FadeIn");
	}

    public void FadeOutBS()
	{
        uiAnimator.SetTrigger("FadeOut");
    }

    public void FadeOutAddAmmo()
	{
        addAmmoAnimator.SetTrigger("FadeOutAddAmmo");
    }
}
