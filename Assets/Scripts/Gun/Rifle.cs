using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    private void Start()
    {
        gunStrategy = new RifleStrategy(this);
    }


}
