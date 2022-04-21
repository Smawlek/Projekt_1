using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunAmmoControl : Bullet
{
    

    // Start is called before the first frame update
    void Start()
    {
        gunManager = gunTransform.GetComponent<SubmachineGunManager>();

        StartFunctions();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFunctions();
    }
	
}
