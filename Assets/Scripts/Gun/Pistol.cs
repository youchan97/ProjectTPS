using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    void Start()
    {
        gunStrategy = new PistolStrategy(this);
    }
}
