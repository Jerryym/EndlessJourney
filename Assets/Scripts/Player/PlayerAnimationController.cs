using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色动画控制器
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
	private Animator m_animator = null;
	private PlayerController m_playerController = null;

	//Animator参数
	private readonly int animID_SpeedX = Animator.StringToHash("speedX");
	private readonly int animID_SpeedY = Animator.StringToHash("speedY");
	private readonly int animID_IsOnGround = Animator.StringToHash("isOnGround");
	private readonly int animID_IsSquat = Animator.StringToHash("isSquat");
	private readonly int animID_IsSlide = Animator.StringToHash("isSlide");

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_playerController = GetComponent<PlayerController>();
	}

	private void Update()
	{
		//更新动画状态
		UpdateAnimationStatus();
	}

	/// <summary>
	/// 更新动画状态
	/// </summary>
	/// <exception cref="NotImplementedException"></exception>
	private void UpdateAnimationStatus()
	{
		m_animator.SetFloat(animID_SpeedX, Mathf.Abs(m_playerController.GetVelocity.x));
		m_animator.SetFloat(animID_SpeedY, m_playerController.GetVelocity.y);
		m_animator.SetBool(animID_IsOnGround, m_playerController.IsOnGround);
		m_animator.SetBool(animID_IsSquat, m_playerController.IsSquat);
		m_animator.SetBool(animID_IsSlide, m_playerController.IsSlide);
		//m_animator.SetBool("isAttack", m_playerController.IsAttack);
		//m_animator.SetBool("isDead", m_playerController.IsDead);
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
