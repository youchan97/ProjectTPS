using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Gun
{
    void Start()
    {
        gunStrategy = new SnipeStrategy(this);
    }
}
