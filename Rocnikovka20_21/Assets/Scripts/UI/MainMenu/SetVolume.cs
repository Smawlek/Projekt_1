using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetVolume : MonoBehaviour
{
	public TextMeshProUGUI volumeText;

    public void SetVolumeSettings(float volume)
	{
		float v = Mathf.Round(volume * 100f) / 100f;
		volumeText.text = "VOLUME: " + v * 100 + "%";
		Settings.volume = v;
	}
}
