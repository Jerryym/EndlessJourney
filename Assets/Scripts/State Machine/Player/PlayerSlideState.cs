using FSM.Enums;
using UnityEngine;

public class PlayerSlideState : PlayerState
{
	/// <summary>
	/// 滑铲目标点
	/// </summary>
	private Vector3 m_targetPos;

	public PlayerSlideState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = PlayerStateEnum.Slide;
	}

	public override void OnEnter()
	{
		var controller = stateMachine.Controller;
		//计算目标点
		m_targetPos = new Vector3(controller.transform.position.x + controller.Player.playerMovement.SlideDistance * controller.transform.localScale.x, controller.transform.position.y);
		//设置图层
		controller.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		//消耗体力
		controller.ConsumePower();
	}

	public override void OnExit()
	{
		ResetState();
	}

	public override void OnLogicUpdate()
	{
		var controller = stateMachine.Controller;
		//跳跃
		if (controller.IsOnGround && controller.IsJump)
		{
			stateMachine.SwitchState(PlayerStateEnum.Jump);
			return;
		}

		//不在地面上 或 左侧/右侧碰撞到墙体, 退出当前状态
		if (!controller.IsOnGround || 
			(controller.TouchLeft && controller.transform.localScale.x < 0) || 
			(controller.TouchRight && controller.transform.localScale.x > 0))
		{
			stateMachine.SwitchState(PlayerStateEnum.Idle);
			return;
		}
	}

	public override void OnPhysicsUpdate()
	{
		var controller = stateMachine.Controller;
		if (Mathf.Abs(m_targetPos.x - controller.transform.position.x) <= 0.1f)
		{
			stateMachine.SwitchState(PlayerStateEnum.Idle);
			return;
		}

		//滑铲位移
		float speed = controller.transform.localScale.x * controller.Player.playerMovement.SlideSpeed;
		Vector2 position = new Vector2(controller.transform.position.x + speed, controller.transform.position.y);
		controller.MovePosition(position);
	}

	private void ResetState()
	{
		var controller = stateMachine.Controller;
		controller.IsSlide = false;
		controller.gameObject.layer = LayerMask.NameToLayer("Player");
	}
}
