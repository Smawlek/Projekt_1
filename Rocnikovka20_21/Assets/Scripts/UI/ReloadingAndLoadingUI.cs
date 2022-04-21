using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadingAndLoadingUI : MonoBehaviour
{
	public RectTransform mainIcon;
	public float timeStep = 0.05f;
	public float oneStepAngle = 35;

	float startTime;
	// Use this for initialization
	void Start()
	{
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time - startTime >= timeStep)
		{
			Vector3 iconAngle = mainIcon.localEulerAngles;
			iconAngle.z += oneStepAngle;

			mainIcon.localEulerAngles = iconAngle;

			startTime = Time.time;
		}
	}
}
