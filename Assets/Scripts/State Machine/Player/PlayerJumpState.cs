using FSM.Enums;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
	private int m_jumpCount = 0;

	public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = PlayerStateEnum.Jump;
	}

	public override void OnEnter()
	{
		Debug.Log("进入跳跃状态!");
		m_jumpCount = 0;
		if (stateMachine.Controller.IsJump)
		{
			Jump();
		}
	}

	public override void OnExit()
	{
		//重置跳跃状态
		m_jumpCount = 0;
		stateMachine.Controller.IsJump = false;
	}

	public override void OnLogicUpdate()
	{
		var controller = stateMachine.Controller;
		//落地检测
		if (controller.IsOnGround && controller.GetVelocity.y <= 0.01f)
		{
			//切换状态
			stateMachine.SwitchState(controller.GetVelocity.magnitude > 0.0f ? PlayerStateEnum.Idle : PlayerStateEnum.Move);
			return;
		}

		//多段跳检测
		bool isAerially = controller.GetVelocity.y < 0.0f;
		if (!controller.IsOnGround && isAerially)
		{
			if (m_jumpCount < controller.Player.playerMovement.MaxJumpCount && controller.IsJump)
			{
				Jump();
			}
		}

		//翻转
		controller.Flip();
	}

	public override void OnPhysicsUpdate()
	{
		var controller = stateMachine.Controller;
		float speed = controller.Player.playerMovement.BaseSpeed;
		Vector2 volcity = new Vector2(speed * Time.deltaTime * controller.inputDirection.x, controller.GetVelocity.y);
		stateMachine.Controller.SetVelocity(volcity);
	}

	private void Jump()
	{
		m_jumpCount++;
		stateMachine.Controller.SetJumpForce();
		stateMachine.Controller.IsJump = false;
	}
}
