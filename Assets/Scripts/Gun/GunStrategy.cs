using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class GunStrategy
{
    protected Gun gun;
    public Player ownerPlayer;
    RaycastHit hit;
    public abstract void Shoot();

    public GunStrategy(Gun gun)
    {
        this.gun = gun;
    }

    public virtual void ClickShoot()
    {
        Physics.Raycast(gun.bulletLine.transform.position,
            -(gun.bulletLine.transform.right), out hit, gun.attackRange);
        ownerPlayer.anim.SetBool("IsShoot", true);
        GameObject bulletEffect = PoolManager.Instance.UseObject();
        bulletEffect.transform.position = hit.transform.position;
        /*if (hit.transform.gameObject.GetComponent<IHitable>() != null)
            Debug.Log("�¾Ҵ�");
        //gun.Attack(hit.transform.gameObject.GetComponent<IHitable>());
        else
            Debug.Log("�ȸ¾Ҵ�");*/
    }
}

public class PistolStrategy : GunStrategy
{
    public PistolStrategy(Gun gun) : base(gun)
    {
    }

    public override void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
            base.ClickShoot();
    }
}

public class ShotGunStrategy : GunStrategy
{
    public ShotGunStrategy(Gun gun) : base(gun)
    {
    }

    public override void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
            base.ClickShoot();
    }
}

public class RifleStrategy : GunStrategy
{
    bool isZoom = false;
    bool isAuto = false;
    public RifleStrategy(Gun gun) : base(gun)
    {
    }

    public void Zoom()
    {
        if (isZoom)
        {
            Debug.Log("�� Ǯ��");
            isZoom = false;
        }
        else
        {
            Debug.Log("��");
            isZoom = true;
        }
    }

    public void ChangeShoot()
    {
        if (isAuto)
        {
            Debug.Log("�ܹ�");
            isAuto = false;
        }
        else
        {
            Debug.Log("����");
            isAuto = true;
        }
    }
    public override void Shoot()
    {
        if(ownerPlayer.zoomAction.triggered)
        {
            Zoom();
        }
        if (ownerPlayer.changeShootAction.triggered)
        {
            ChangeShoot();
        }

        if (isAuto)
        {
            
        }
        else
        {
            if(ownerPlayer.shootAction.triggered)
            {
                base.ClickShoot();
            }
        }
    }
}

public class SnipeStrategy : GunStrategy
{
    bool isZoom = false;
    public SnipeStrategy(Gun gun) : base(gun)
    {
    }

    public override void Shoot()
    {
        if (ownerPlayer.playerInput.actions["Zoom"].triggered)
        {
            if (isZoom)
            {
                isZoom = false;
            }
            else
            {
                isZoom = true;
            }
        }
        if (Input.GetMouseButtonDown(0))
            base.ClickShoot();
    }
}


