using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rifle : Gun
{
    bool isZoom;
	bool isAutoShoot;
    private void Start()
    {
        gunStrategy = new RifleStrategy(this);
        isZoom = false;
        isAutoShoot = false;
    }

    private void Update()
    {
		if (gunStrategy.ownerPlayer != null)
		{
			if(gunStrategy.ownerPlayer.zoomAction.triggered)
				Zoom();
            if (gunStrategy.ownerPlayer.changeShootAction.triggered)
                ChangeShoot();
			if (gunStrategy.ownerPlayer.shootAction.triggered)
				gunStrategy.Shoot();
		}
    }

    void ChangeShoot()
    {
        Debug.Log(gunStrategy.ownerPlayer.playerInput.currentActionMap);
        isAutoShoot = isAutoShoot ? false : true;
        Debug.Log(isAutoShoot);
        if (isAutoShoot)
        {
            gunStrategy.ownerPlayer.playerInput.SwitchCurrentActionMap("AutoShoot");
            gunStrategy.ownerPlayer.InputInitialize();
        }
        else
        {
            gunStrategy.ownerPlayer.playerInput.SwitchCurrentActionMap("Player");
            gunStrategy.ownerPlayer.InputInitialize();
        }
        Debug.Log(gunStrategy.ownerPlayer.playerInput.currentActionMap);
    }

    public void Zoom()
    {
        if (isZoom)
        {
            Debug.Log("¡‹ «Æ±‚");
            gunStrategy.ownerPlayer.zoomCam.Priority = 9;
            isZoom = false;
        }
        else
        {
            Debug.Log("¡‹");
            gunStrategy.ownerPlayer.zoomCam.Priority = 11;
            gunStrategy.ownerPlayer.transform.forward = Vector3.Lerp(gunStrategy.ownerPlayer.transform.forward, gunStrategy.camTransform.forward, Time.deltaTime * 300);
            isZoom = true;
        }
    }
}
