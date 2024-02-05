using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Gun : MonoBehaviour, IAttackable, IGetable, IThrowable
{
    [SerializeField]
    protected int damage;//���ݷ�
    public float attackRange;//���� �Ÿ�
    [SerializeField]
    protected int bulletCount;//�Ѿ�
    public int maxBulletCount;//�ִ� ���� �Ѿ�
    protected int remainBulletCount;//���� �Ѿ�
    public GunStrategy gunStrategy;
    public GameObject bulletLine;
    [SerializeField]
    private Image aim;

    public int BulletCount
    {
        get { return bulletCount; }
        set 
        { 
            bulletCount = value;
            if(bulletCount <= 0)
            {
                bulletCount = 0;
            }
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
        //Debug.DrawRay(bulletLine.transform.position, -(bulletLine.transform.right), Color.red);
    }

    /*public void Shoot()
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
    }*/

    public void Get(Player player)
    {
        this.gameObject.transform.parent = player.hasGunObject.transform;
        this.gameObject.GetComponent<MeshCollider>().enabled = false;
        player.playerGun = this;
        //aim.enabled = true;
        gunStrategy.ownerPlayer = player;
    }

    public void Throw()
    {
        this.gameObject.GetComponent<MeshCollider>().enabled=true;
    }

    public void ReLoad()
    {
        BulletCount = maxBulletCount;
    }
}
