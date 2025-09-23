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

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_physicsCheck = GetComponent<PhysicsCheck>();
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
	}
}
