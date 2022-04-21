using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesCheck : MonoBehaviour
{
    public bool isLocked;

    public Transform lockT;
    public Transform imgT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(bool isLocked)
	{
        this.isLocked = isLocked;

        if(isLocked)
		{
            lockT.gameObject.SetActive(true);
            imgT.gameObject.SetActive(false);
		} else
		{
            lockT.gameObject.SetActive(false);
            imgT.gameObject.SetActive(true);
        }
	}
}
