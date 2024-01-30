using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    void Start()
    {
        gunStrategy = new ShotGunStrategy(this);
    }

}
