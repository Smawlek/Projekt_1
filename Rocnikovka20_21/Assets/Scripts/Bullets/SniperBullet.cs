using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        gunManager = gunTransform.GetComponent<SniperGunManager>();

        StartFunctions();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFunctions();
    }
}
