using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFullscreen : MonoBehaviour
{
    public void SetScreenMode(bool isOn)
	{
		Screen.fullScreen = isOn;
	}
}
