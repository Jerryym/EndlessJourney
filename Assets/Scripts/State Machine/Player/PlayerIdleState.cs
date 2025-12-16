using FSM.Enums;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
	public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = PlayerStateEnum.Idle;
	}

	public override void OnLogicUpdate()
	{
		var controller = stateMachine.Controller;
		//移动
		if (controller.inputDirection.magnitude > 0.1f)
		{
			stateMachine.SwitchState(PlayerStateEnum.Move);
			return;
		}

		//跳跃
		if (!controller.IsOnGround || (controller.IsOnGround && controller.IsJump))
		{
			stateMachine.SwitchState(PlayerStateEnum.Jump);
			return;
		}

		//下蹲
		if (controller.IsOnGround && controller.IsSquat)
		{
			stateMachine.SwitchState(PlayerStateEnum.Squat);
			return;
		}

		//滑铲
		if (controller.IsOnGround && controller.IsSlide)
		{
			stateMachine.SwitchState(PlayerStateEnum.Slide);
			return;
		}

		//攻击
		if (controller.IsOnGround && controller.IsAttack)
		{
			stateMachine.SwitchState(PlayerStateEnum.Attack);
			return;
		}
	}

	public override void OnPhysicsUpdate()
	{
		stateMachine.Controller.SetVelocity(Vector2.zero);
	}
}
