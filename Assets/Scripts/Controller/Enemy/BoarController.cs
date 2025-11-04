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
		//Hurt
		m_stateMachine.AddState(EnemyStateEnum.Hurt, new BoarHurtState(m_stateMachine));
		//初始状态: Patrol
		m_stateMachine.SwitchState(EnemyStateEnum.Patrol);	
	}

	protected override void TakeDamage(Transform attacker)
	{
		m_stateMachine.SwitchState(EnemyStateEnum.Hurt);

		//受击后退
		Vector2 dirVec = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
		m_rigidBody.AddForce(dirVec * m_enemy.HurtForce, ForceMode2D.Impulse);
	}
}
