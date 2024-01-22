using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour, IGetable
{
    public int healValue;


    public void Use(Player player)
    {
        player.playerHp += healValue;
    }

    public void Get(Player player)
    {
        player.healItems.Add(this);
    }
}
