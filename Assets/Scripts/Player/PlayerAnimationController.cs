using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色动画控制器
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
	private Animator m_animator = null;
	private Rigidbody2D m_rigidbody = null;
	private PhysicsCheck m_physicsCheck = null;
	private PlayerController m_playerController = null;

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_physicsCheck = GetComponent<PhysicsCheck>();
		m_playerController = GetComponent<PlayerController>();
	}

	private void Update()
	{
		//设置动画状态
		SetAnimationStatus();
	}

	/// <summary>
	/// 设置动画状态
	/// </summary>
	/// <exception cref="NotImplementedException"></exception>
	private void SetAnimationStatus()
	{
		m_animator.SetFloat("speedX", Mathf.Abs(m_rigidbody.velocity.x));
		m_animator.SetFloat("speedY", m_rigidbody.velocity.y);
		m_animator.SetBool("isOnGround", m_physicsCheck.isOnGround);
		//m_animator.SetBool("isSquat", m_playerController.IsSquat);
		//m_animator.SetBool("isDead", m_playerController.IsDead);
		//m_animator.SetBool("isAttack", m_playerController.IsAttack);
		//m_animator.SetBool("isSlide", m_playerController.IsSlide);
	}

	/// <summary>
	/// 触发受伤动画
	/// </summary>
	public void TriggerHurt()
	{
		m_animator.SetTrigger("hurt");
	}

	/// <summary>
	/// 触发攻击动画
	/// </summary>
	public void TriggerAttack()
	{
		m_animator.SetTrigger("attack");
	}
}
