using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBullet : MonoBehaviour, IGetable
{
    public void Get(Player player)
    {
        if (player.playerGun != null)
        {
            player.playerGun.RemainBulletCount += player.playerGun.oneBulletCartridge;
            Debug.Log(player.playerGun.RemainBulletCount);
        }
    }
}
