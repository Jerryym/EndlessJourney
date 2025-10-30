using FSM.Enums;
using UnityEngine;

public class BoarPatrolState : EnemyState
{
	public BoarPatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = EnemyStateEnum.Patrol;
	}

	public override void OnEnter()
	{
		var controller = stateMachine.Controller;
		controller.IsWalk = true;
		controller.CurrentSpeed = controller.Enemy.enemyBasic.PatrolSpeed;
	}

	public override void OnLogicUpdate()
	{
		var controller = stateMachine.Controller;
		//发现敌人, 进入追击状态
		if (controller.FindPlayer())
		{
			stateMachine.SwitchState(EnemyStateEnum.Chase);
			return;
		}

		//前方没有地面 || 朝向右侧且碰撞到右墙 || 朝向左侧且碰撞到左墙 => 停止移动, 进入等待
		if (!controller.IsOnGround || (controller.TouchRight && controller.FaceDir.x > 0) || (controller.TouchLeft && controller.FaceDir.x < 0))
		{
			controller.IsWait = true;
			controller.IsWalk = false;
			stateMachine.SwitchState(EnemyStateEnum.Idle);
			return;
		}
	}

	public override void OnPhysicsUpdate()
    {
		var controller = stateMachine.Controller;
		Vector2 speed = new Vector2(controller.CurrentSpeed * controller.FaceDir.x * Time.deltaTime, controller.GetVelocity.y);
		controller.SetVelocity(speed);
    }
}
