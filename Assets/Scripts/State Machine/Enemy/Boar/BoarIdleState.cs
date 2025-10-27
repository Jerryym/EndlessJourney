using FSM.Enums;
using UnityEngine;

public class BoarIdleState : IdleState
{
	public BoarIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = EnemyStateEnum.Idle;
	}

	public override void OnEnter()
    {
		stateMachine.Controller.SetVelocity(Vector2.zero);
    }

	public override void OnLogicUpdate()
    {
		var controller = stateMachine.Controller;

		//巡逻
		if (!controller.FindPlayer() && controller.IsWalk && !controller.IsWait)
		{
			stateMachine.SwitchState(EnemyStateEnum.Patrol);
			return;
		}

		//追击
		if (controller.FindPlayer())
		{
			stateMachine.SwitchState(EnemyStateEnum.Chase);
			return;
		}
    }
}
