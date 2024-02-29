using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    bool isZoom;
    private void Start()
    {
        gunStrategy = new RifleStrategy(this);
        isZoom = false;
    }

    private void Update()
    {
        if(gunStrategy.ownerPlayer != null && gunStrategy.ownerPlayer.zoomAction.triggered)
            Zoom();
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
