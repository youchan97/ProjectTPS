using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    bool isZoom;
	bool isAutoShot;
    private void Start()
    {
        gunStrategy = new RifleStrategy(this);
        isZoom = false;
		isAutoShot = false;
    }

    private void Update()
    {
		if (gunStrategy.ownerPlayer != null)
		{
			if(gunStrategy.ownerPlayer.zoomAction.triggered)
				Zoom();
			if (gunStrategy.ownerPlayer.changeShootAction.triggered)
				isAutoShot = isAutoShot ? false : true;
			if (gunStrategy.ownerPlayer.shootAction.triggered)
				gunStrategy.Shoot();
		}
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
