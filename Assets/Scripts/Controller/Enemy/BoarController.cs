using FSM.Enums;
using UnityEngine;

public class BoarController : EnemyController
{
	#region Unity 生命周期函数
	protected override void Awake()
	{
		base.Awake();
		//初始化状态机
		InitStateMachine();
	}
	#endregion

	protected override void InitStateMachine()
	{
		//Idle
		m_stateMachine.AddState(EnemyStateEnum.Idle, new BoarIdleState(m_stateMachine));
		//Patrol
		m_stateMachine.AddState(EnemyStateEnum.Patrol, new BoarPatrolState(m_stateMachine));
		//Chase
		m_stateMachine.AddState(EnemyStateEnum.Chase, new BoarChaseState(m_stateMachine));
		//初始状态: Patrol
		m_stateMachine.SwitchState(EnemyStateEnum.Patrol);	
    }
}