using FSM.Enums;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        stateEnum = PlayerStateEnum.Hurt;
    }

    public override void OnEnter()
    {
        stateMachine.Controller.IsHurt = true;
        stateMachine.Controller.SetVelocity(Vector2.zero);
        //触发动画
        stateMachine.AnimationController.TriggerHurt();
    }

    public override void OnExit()
    {
    }

	public override void OnLogicUpdate()
    {
        if (!stateMachine.Controller.IsHurt)
        {
            stateMachine.SwitchState(PlayerStateEnum.Idle);
            return;
        }
    }
}