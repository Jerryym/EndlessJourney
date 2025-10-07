using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : State
{
	public override void OnEnter(Enemy enemy)
	{
		m_enemy = enemy;
		m_enemy.currentSpeed = m_enemy.chaseSpeed;
		m_enemy.IsChase = true;
	}

	public override void OnExit()
	{
		m_enemy.IsChase = false;
	}

	public override void OnUpdate()
	{
		//丢失Player
		if (m_enemy.lostPlayerTimer <= 0)
		{
			m_enemy.SwitchState(EnemyStateType.Patrol);
		}

		if (!m_enemy.check.isOnGround || (m_enemy.FaceDir.x < 0 && m_enemy.check.isTouchLeft) ||
			(m_enemy.FaceDir.x > 0 && m_enemy.check.isTouchRight))
		{
			m_enemy.transform.localScale = new Vector3(m_enemy.FaceDir.x, 1, 1);
		}
	}

	public override void OnPhysicsUpdate()
	{
	}
}
