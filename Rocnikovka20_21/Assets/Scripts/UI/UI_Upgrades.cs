using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using TMPro;

public class UI_Upgrades : MonoBehaviour
{
    public Transform upgradeText;

    private Animator upgradeTextAnim;

    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetVariables()
	{
        upgradeTextAnim = upgradeText.GetComponent<Animator>();
	}

    public void FadeUpgradeText(bool fadeIn)
	{
        if(fadeIn)
		{
            upgradeTextAnim.SetTrigger("UpgradeTextFadeIn");
		} else
		{
            upgradeTextAnim.SetTrigger("UpgradeTextFadeOut");
		}
	}
}
