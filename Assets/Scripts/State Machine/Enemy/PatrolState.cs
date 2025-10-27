using FSM.Enums;

/// <summary>
/// 巡逻状态
/// </summary>
public abstract class PatrolState : EnemyState
{
    public PatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        base.stateEnum = EnemyStateEnum.Patrol;
    }

	public override void OnEnter() { }

	public override void OnExit() { }

	public override void OnLogicUpdate() { }

	public override void OnPhysicsUpdate() { }
}