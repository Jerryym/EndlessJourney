using FSM.Enums;
using UnityEngine;

public class HurtState : EnemyState
{
	public HurtState(EnemyStateMachine stateMachine) : base(stateMachine)
	{
		stateEnum = EnemyStateEnum.Hurt;
	}

	public override void OnEnter()
	{
		stateMachine.Controller.IsHurt = true;
		stateMachine.Controller.SetVelocity(Vector2.zero);
	}

	public override void OnExit()
	{
	}

	public override void OnLogicUpdate()
	{
		if (!stateMachine.Controller.IsHurt)
		{
			stateMachine.SwitchState(EnemyStateEnum.Idle);
			return;
		}
	}

	public override void OnPhysicsUpdate()
	{
	}
}
