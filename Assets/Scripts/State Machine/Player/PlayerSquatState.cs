using FSM.Enums;

public class PlayerSquatState : PlayerState
{
	public PlayerSquatState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = PlayerStateEnum.Squat;
	}

	public override void OnEnter()
	{
		//修改碰撞体尺寸
		stateMachine.Controller.ModifyColliderSize();
	}

	public override void OnExit()
	{
		//修改碰撞体尺寸
		stateMachine.Controller.ModifyColliderSize();
	}

	public override void OnLogicUpdate()
	{
		var controller = stateMachine.Controller;
		if (controller.IsOnGround && !controller.IsSquat)
		{
			//切换状态
			stateMachine.SwitchState(controller.GetVelocity.magnitude > 0.0f ? PlayerStateEnum.Idle : PlayerStateEnum.Move);
			return;
		}
		
		//翻转
		controller.Flip();
	}

	public override void OnPhysicsUpdate()
	{
		var controller = stateMachine.Controller;
		if (controller.IsOnGround && controller.IsSquat)
		{
			//修改碰撞体尺寸
			controller.ModifyColliderSize();
		}
	}
}
