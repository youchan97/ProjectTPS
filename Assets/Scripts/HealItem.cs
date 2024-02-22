using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour, IGetable
{
    int healValue = 30;


    public void Use(Player player)
    {
        player.Hp += healValue;
        player.healItems.Remove(this);
    }

    public void Get(Player player)
    {
        player.healItems.Add(this);
        this.GetComponent<MeshCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}
