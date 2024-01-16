using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class State
{
    public Player player;
    public InputAction shootAction;
    public StarterAssetsInputs sa;
    public static float time = 0;

    public State(Player player)
    {
        this.player = player;
        shootAction = player.playerInput.actions["Shoot"];
    }


    public virtual State InputState()
    {
        return this;
    }
}

public class IdleState : State
{
    public IdleState(Player player) : base(player)
    {
    }

    public override State InputState()
    {
        Debug.Log("기본");
        if (player.sa.move != Vector2.zero)
            return new MoveState(player);
        if(player.sa.jump)
            return new JumpState(player);
        if(player.sa.sprint)
            return new SprintState(player);
        if(shootAction.triggered)
        {
            return new ShootState(player);
        }
        return this;
    }
}

public class MoveState : State
{
    public MoveState(Player player) : base(player)
    {
    }

    public override State InputState()
    {
        Debug.Log("무브");
        if(player.sa.move == Vector2.zero)
            return new IdleState(player);
        if (player.sa.jump)
            return new JumpState(player);
        if (player.sa.sprint)
            return new SprintState(player);
        if (shootAction.triggered)
            return new ShootState(player);
        return this;
    }
}

public class JumpState : State
{
    public JumpState(Player player) : base(player)
    {
    }

    public override State InputState()
    {
        Debug.Log("점프");
        if (!player.sa.jump && player.tpController.Grounded)
        {
            if (player.sa.move != Vector2.zero)
                return new MoveState(player);
            else
                return new IdleState(player);
        }
        return this;
    }
}
public class SprintState : State
{
    public SprintState(Player player) : base(player)
    {
    }

    public override State InputState()
    {
        Debug.Log("달리기");
        if(!player.sa.sprint)
        {
            if (player.sa.move != Vector2.zero)
                return new MoveState(player);
            else
                return new IdleState(player);
        }

        return this;
    }
}

public class ShootState : State
{
    
    
    public ShootState(Player player) : base(player)
    {
    }

    public override State InputState()
    {
        time += Time.deltaTime;
        if (Input.GetMouseButton(0) && time > 0.15f)
        {
            Debug.Log("탕");
            player.transform.forward = player.cam.transform.forward;
            player.sa.move = Vector2.zero;
            player.anim.SetBool("IsShoot", true);
            player.playerGun.Shoot();
            time = 0;
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            player.anim.SetBool("IsShoot", false);
            return new IdleState(player);
        }
        return this;
    }
}

