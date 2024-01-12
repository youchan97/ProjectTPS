using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private State curState;
    public PlayerInput playerInput;
    public Vector3 pos;
    public StarterAssetsInputs sa;
    public ThirdPersonController tpController;
    public bool isShoot;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        sa = GetComponent<StarterAssetsInputs>();
        tpController = GetComponent<ThirdPersonController>();
    }
    private void Start()
    {
        curState = new IdleState(this);
        isShoot = curState.shootAction.triggered;
    }

    private void Update()
    {
        curState = curState.InputState();
    }
}
