using FSM.Enums;

/// <summary>
/// 追击状态
/// </summary>
public class ChaseState : EnemyState
{
    public ChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        base.stateEnum = EnemyStateEnum.Chase;
    }

    public override void OnEnter() { }

    public override void OnExit() { }

    public override void OnLogicUpdate() { }

    public override void OnPhysicsUpdate() { }
}