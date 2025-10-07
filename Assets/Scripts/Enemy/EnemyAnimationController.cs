using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人动画控制器
/// </summary>
public class EnemyAnimationController : MonoBehaviour
{
	private Enemy m_enemy = null;
	private Animator m_animator = null;

	private void Awake()
	{
		m_enemy = GetComponent<Enemy>();
		m_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		//设置动画状态
		SetAnimationStatus();
	}

	public void TriggerHurt()
	{
		m_animator.SetTrigger("hurt");
	}

	/// <summary>
	/// 设置动画状态
	/// </summary>
	private void SetAnimationStatus()
	{
		m_animator.SetBool("isWalk", m_enemy.IsWalk);
		m_animator.SetBool("isDead", m_enemy.IsDead);
		m_animator.SetBool("isRun", m_enemy.IsChase);
	}

}
