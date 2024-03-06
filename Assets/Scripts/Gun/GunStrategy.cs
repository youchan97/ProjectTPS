using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class GunStrategy
{
    protected Gun gun;
    public Player ownerPlayer;
    RaycastHit hit;
    public Transform camTransform;
    public abstract void Shoot();

    public GunStrategy(Gun gun)
    {
        this.gun = gun;
        camTransform = Camera.main.transform;
    }

    public virtual void ClickShoot()
    {
        if(gun.BulletCount > 0)
        {
            ownerPlayer.transform.forward = Vector3.Lerp(ownerPlayer.transform.forward,camTransform.forward, Time.deltaTime * 300);
            ownerPlayer.anim.SetBool("IsShoot", true);
            Physics.Raycast(camTransform.position, camTransform.forward, out hit, gun.attackRange);
            GameObject bulletEffect = PoolManager.Instance.UseObject();
            bulletEffect.transform.position = hit.point;
            gun.BulletCount--;
        }
        else
        {
            Debug.Log("탄약 없음");
        }
        /*if (hit.transform.gameObject.GetComponent<IHitable>() != null)
            Debug.Log("맞았다");
        //gun.Attack(hit.transform.gameObject.GetComponent<IHitable>());
        else
            Debug.Log("안맞았다");*/
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
    bool isAuto;
    public RifleStrategy(Gun gun) : base(gun)
    {
        isAuto = false;
    }
    public override void Shoot()
    {
        base.ClickShoot();
        AutoShoot();
    }
    public void AutoShoot()
    {
        Debug.Log(ownerPlayer.shootAction.triggered);
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


