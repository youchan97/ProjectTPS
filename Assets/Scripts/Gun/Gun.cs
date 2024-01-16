using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour, IAttackable
{
    [SerializeField]
    protected int damage;//���ݷ�
    [SerializeField]
    protected float attackRange;//���� �Ÿ�
    [SerializeField]
    protected int bulletCount;//�Ѿ�
    [SerializeField]
    protected int maxBulletCount;//�ִ� ���� �Ѿ�
    protected int remainBulletCount;//���� �Ѿ�

    public int BulletCount
    {
        get { return bulletCount; }
        set 
        { 
            bulletCount = value;
        }
    }

    public int RemainBulletCount
    {
        get { return remainBulletCount; }
        set
        { 
            remainBulletCount = value;
        }
    }

    public void Attack(IHitable hitable)
    {
        hitable.Hit(damage);
    }

    private void Update()
    {
        Debug.DrawRay(this.gameObject.transform.position, -(this.gameObject.transform.right), Color.red);
    }

    public void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(this.gameObject.transform.position,
            -(this.gameObject.transform.right), out hit, attackRange);
        if (hit.transform.gameObject.TryGetComponent(out IHitable hitable))
        {
            Debug.Log("�¾Ҵ�");
            Attack(hitable);
        }
        else
        {
            Debug.Log("�� �¾Ҵ�");
        }
    }
}