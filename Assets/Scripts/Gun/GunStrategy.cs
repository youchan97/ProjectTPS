using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class GunStrategy
{
    protected Gun gun;
    protected Player ownerPlayer;
    RaycastHit hit;
    protected InputAction zoomAction;
    protected InputAction changeShootAction;
    protected InputAction autoShootAction;
    protected InputAction shootAction;
    public abstract void Shoot();

    public GunStrategy(Gun gun)
    {
        this.gun = gun;
        ownerPlayer = gun.GetComponentInParent<Player>();
        zoomAction = ownerPlayer.playerInput.actions["Zoom"];
        changeShootAction = ownerPlayer.playerInput.actions["ChangeShoot"];
        shootAction = ownerPlayer.playerInput.actions["Shoot"];
        autoShootAction = ownerPlayer.playerInput.actions["AutoShoot"];
    }

    public virtual void ClickShoot()
    {
        Physics.Raycast(gun.bulletLine.transform.position,
            -(gun.bulletLine.transform.right), out hit, gun.attackRange);
        ownerPlayer.anim.SetBool("IsShoot", true);
        if (hit.transform.gameObject.GetComponent<IHitable>() != null)
            gun.Attack(hit.transform.gameObject.GetComponent<IHitable>());
        else
            Debug.Log("안맞았다");
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
            Debug.Log("줌 풀기");
            isZoom = false;
        }
        else
        {
            Debug.Log("줌");
            isZoom = true;
        }
    }

    public void ChangeShoot()
    {
        if (isAuto)
        {
            Debug.Log("단발");
            isAuto = false;
        }
        else
        {
            Debug.Log("연발");
            isAuto = true;
        }
    }
    public override void Shoot()
    {
        if(zoomAction.triggered)
        {
            Zoom();
        }
        if (changeShootAction.triggered)
        {
            ChangeShoot();
        }

        if (isAuto)
        {
            
        }
        else
        {
            if(shootAction.triggered)
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


