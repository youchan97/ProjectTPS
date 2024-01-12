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
        Debug.Log("�⺻");
        if (player.sa.move != Vector2.zero)
            return new MoveState(player);
        if(player.sa.jump)
            return new JumpState(player);
        if(player.sa.sprint)
            return new SprintState(player);
        if(shootAction.triggered)
            return new ShootState(player);
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
        Debug.Log("����");
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
        Debug.Log("����");
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
        Debug.Log("�޸���");
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
        Debug.Log("��");
        player.sa.move = Vector2.zero;
        if(shootAction.triggered == false)
        {
            return new IdleState(player);
        }
        return this;
    }
}
