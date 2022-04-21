using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIshootin : MonoBehaviour
{
    public Transform player;
    public Transform submachineBulletImg;
    public Transform sniperBulletImg;
    public Transform shotgunBulletImg;

    public Transform bulletText;

    public Transform timeBetweenShootinBar;

    public Transform addAmmoText;

    private PlayerAnimationsController playerAnim;

	private void Start()
	{
        playerAnim = player.GetComponent<PlayerAnimationsController>();
	}

	public void UpdateBulletText(int inClip, int ammo)
    {
        bulletText.GetComponent<TextMeshProUGUI>().text = inClip + "/" + ammo;
    }

    public void UpdateAddAmmoText(int ammo)
	{
        addAmmoText.GetComponent<TextMeshProUGUI>().text = "+ " + ammo;

        playerAnim.FadeOutAddAmmo();
	}

    public void EnableSubmachineBulletImg(bool enabled)
    {
        submachineBulletImg.gameObject.SetActive(enabled);
    }

    public void EnableSniperBulletImg(bool enabled)
    {
        sniperBulletImg.gameObject.SetActive(enabled);
    }

    public void EnableShotgunBulletImg(bool enabled)
    {
        shotgunBulletImg.gameObject.SetActive(enabled);
    }

    public void UpdateTimeBetweenShootinBar(int minimum, int maximum, int current)
	{
        timeBetweenShootinBar.GetComponent<ProgressBar>().SetValues(minimum, maximum, current);
	}
}
