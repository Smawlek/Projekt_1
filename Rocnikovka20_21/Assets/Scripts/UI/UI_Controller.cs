using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    public Transform mainMenu;
    public Transform menuTopBar;
    public Transform upgradesMenu;
    public Transform ui;
    public Transform quitGame;

    public TextMeshProUGUI versionText;

    public bool isOverlayActive = false;
    private bool isESCmenuActive = false;
    private bool isUpgradeMenuActive = false;

    private UI_Upgrades uiUpgrades;

    // Start is called before the first frame update
    void Start()
    {
        versionText.text = "v. " + GameInfo.version;

        uiUpgrades = ui.GetComponent<UI_Upgrades>();
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.GetButtonDown("ESC"))
		{
            if(!isUpgradeMenuActive)
			{
                MainMenu();
            } else
			{
                UpgradeMenu();
			}
		}

        if (InputManager.GetButtonDown("TAB") && !isESCmenuActive)
        {
            UpgradeMenu();
        }
    }

    public IEnumerator ShowUpgradeText(GameObject g)
	{
        uiUpgrades.FadeUpgradeText(true);

        yield return new WaitForSeconds(2f);

        uiUpgrades.FadeUpgradeText(false);

        Destroy(g);
    }

    private void UpgradeMenu()
	{
        if (!isUpgradeMenuActive)
        {
            isOverlayActive = true;
            isUpgradeMenuActive = true;

            upgradesMenu.GetComponent<UpgradesControl>().CheckUpgrades();

            ui.gameObject.SetActive(false);
            menuTopBar.gameObject.SetActive(true);
            upgradesMenu.gameObject.SetActive(true);
        }
        else
        {
            isOverlayActive = false;
            isUpgradeMenuActive = false;

            upgradesMenu.GetComponent<UpgradesControl>().HideText();
            ;

            ui.gameObject.SetActive(true);
            menuTopBar.gameObject.SetActive(false);
            upgradesMenu.gameObject.SetActive(false);
        }

        StopTime();
    }

    public void TurnOffESCMenu()
	{
        isOverlayActive = false;
        isESCmenuActive = false;
        
        ui.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);
    }

    private void MainMenu()
	{
        if(!isESCmenuActive)
		{
            isOverlayActive = true;
            isESCmenuActive = true;

            ui.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        } else
		{
            TurnOffESCMenu();
        }

        StopTime();
	}

    private void StopTime()
	{
        if(isOverlayActive)
		{
            Time.timeScale = 0f;
		} else
		{
            Time.timeScale = 1f;
		}
	}
}
