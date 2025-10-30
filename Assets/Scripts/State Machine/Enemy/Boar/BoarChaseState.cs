using FSM.Enums;
using UnityEngine;

public class BoarChaseState : EnemyState
{
    public BoarChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = EnemyStateEnum.Chase;
	}

    public override void OnEnter()
    {
        var controller = stateMachine.Controller;
        controller.IsWalk = true;
        controller.IsChase = true;
        controller.CurrentSpeed = controller.Enemy.enemyBasic.ChaseSpeed;
    }

    public override void OnExit()
    {
        var controller = stateMachine.Controller;
        controller.IsChase = false;
    }

    public override void OnLogicUpdate()
    {
        var controller = stateMachine.Controller;
        //丢失玩家焦点
        if (controller.IsLostPlayerFocus)
        {
            stateMachine.SwitchState(EnemyStateEnum.Patrol);
            return;
        }

        if (!controller.IsOnGround || (controller.TouchRight && controller.FaceDir.x > 0) || (controller.TouchLeft && controller.FaceDir.x < 0))
        {
            //翻转
			controller.transform.localScale = new Vector3(controller.FaceDir.x, 1, 1);
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
