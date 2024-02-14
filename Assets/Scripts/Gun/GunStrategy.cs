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
        if(gun.BulletCount > 0)
        {
            Transform camTransform = Camera.main.transform;
            ownerPlayer.transform.forward = camTransform.forward;
            ownerPlayer.anim.SetBool("IsShoot", true);
            Physics.Raycast(camTransform.position, camTransform.forward, out hit, gun.attackRange);
            GameObject bulletEffect = PoolManager.Instance.UseObject();
            bulletEffect.transform.position = hit.point;
            gun.BulletCount--;
        }
        else
        {
            Debug.Log("Åº¾à ¾øÀ½");
        }
        /*if (hit.transform.gameObject.GetComponent<IHitable>() != null)
            Debug.Log("¸Â¾Ò´Ù");
        //gun.Attack(hit.transform.gameObject.GetComponent<IHitable>());
        else
            Debug.Log("¾È¸Â¾Ò´Ù");*/
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
            Debug.Log("ÁÜ Ç®±â");
            isZoom = false;
        }
        else
        {
            Debug.Log("ÁÜ");
            isZoom = true;
        }
    }

    public void ChangeShoot()
    {
        if (isAuto)
        {
            Debug.Log("´Ü¹ß");
            isAuto = false;
        }
        else
        {
            Debug.Log("¿¬¹ß");
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
                Debug.Log("½ú´Ù");
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


