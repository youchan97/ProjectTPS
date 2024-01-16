using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    float Hp { get; set; }
    void Hit(float damage);
}

public interface IAttackable
{
    void Attack(IHitable hitable);
}




public class InterFace : MonoBehaviour
{
}
