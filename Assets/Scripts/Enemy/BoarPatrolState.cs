public class BoarPatrolState : State
{
	public override void OnEnter(Enemy enemy)
	{
		base.m_enemy = enemy;
		base.m_enemy.currentSpeed = base.m_enemy.patrolSpeed;
	}

	public override void OnExit()
	{
		m_enemy.IsWalk = false;
	}

	public override void OnUpdate()
	{
		//发现Player切换到追击状态
		if (m_enemy.FindPlayer())
		{
			m_enemy.SwitchState(EnemyStateType.Chase);
		}

		if (!m_enemy.check.isOnGround || (m_enemy.FaceDir.x < 0 && m_enemy.check.isTouchLeft) || 
			(m_enemy.FaceDir.x > 0 && m_enemy.check.isTouchRight))
		{
			m_enemy.IsWait = true;
			m_enemy.IsWalk = false;
		}
		else
		{
			m_enemy.IsWalk = true;
		}
	}

	public override void OnPhysicsUpdate()
	{
	}
}
