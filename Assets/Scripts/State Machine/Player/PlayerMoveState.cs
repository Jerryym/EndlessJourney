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

	public override void OnPhysicsUpdate()
	{
		var controller = stateMachine.Controller;
		bool isWalk = controller.IsRunningMode;
		float speed = (isWalk) ? controller.Player.RunSpeed : controller.Player.WalkSpeed;

		//设置当前速度
		Vector2 volcity = new Vector2(speed * Time.deltaTime * controller.inputDirction.x, controller.GetVelocity.y);
		stateMachine.Controller.SetVelocity(volcity);
	}

	public override void OnUpdate()
	{
	}
}
