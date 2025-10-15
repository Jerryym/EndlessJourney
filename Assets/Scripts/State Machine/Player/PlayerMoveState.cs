using FSM.Enums;
using UnityEngine;

/// <summary>
/// 角色移动状态: 处理行走和奔跑的逻辑
/// </summary>
public class PlayerMoveState : PlayerState
{
	/// <summary>
	/// 移动速度
	/// </summary>
	private float m_MoveSpeed;

	public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = PlayerStateEnum.Move;
	}

	public override void OnEnter()
	{
	}

	public override void OnExit()
	{
		stateMachine.Controller.SetVelocity(Vector2.zero);
	}

	public override void OnUpdate()
	{
		var controller = stateMachine.Controller;
		//空闲
		if (controller.inputDirection.magnitude < 0.1f)
		{
			stateMachine.SwitchState(PlayerStateEnum.Idle);
			return;
		}

		//跳跃
		if (controller.IsOnGround && controller.IsJump)
		{
			stateMachine.SwitchState(PlayerStateEnum.Jump);
			return;
		}

		//下蹲
		if (controller.IsSquat)
		{
			stateMachine.SwitchState(PlayerStateEnum.Squat);
			return;
		}

		//翻转
		controller.Flip();
	}

	public override void OnPhysicsUpdate()
	{
		var controller = stateMachine.Controller;
		float speed = (controller.IsRunningMode) ? controller.Player.RunSpeed : controller.Player.WalkSpeed;

		//设置当前速度
		Vector2 volcity = new Vector2(speed * Time.deltaTime * controller.inputDirection.x, controller.GetVelocity.y);
		stateMachine.Controller.SetVelocity(volcity);
	}
}
