using FSM.Enums;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
	public PlayerHurtState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		this.stateEnum = PlayerStateEnum.Hurt;
	}

	public override void OnEnter()
	{
		stateMachine.Controller.SetVelocity(Vector2.zero);
	}

	public override void OnExit()
	{
	}

	public override void OnLogicUpdate()
	{
	}

	public override void OnPhysicsUpdate()
	{
	}
}
