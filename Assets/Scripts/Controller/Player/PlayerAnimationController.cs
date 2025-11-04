using System;
using UnityEngine;

/// <summary>
/// Player动画控制器
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
	private Animator m_animator = null;
	private PlayerController m_playerController = null;

	//Animator参数
	private readonly int m_animID_SpeedX = Animator.StringToHash("speedX");
	private readonly int m_animID_SpeedY = Animator.StringToHash("speedY");
	private readonly int m_animID_IsOnGround = Animator.StringToHash("isOnGround");
	private readonly int m_animID_IsSquat = Animator.StringToHash("isSquat");
	private readonly int m_animID_IsSlide = Animator.StringToHash("isSlide");
	private readonly int m_animID_IsAttack = Animator.StringToHash("isAttack");
	private readonly int m_animID_IsDead = Animator.StringToHash("isDead");
	private readonly int m_animID_Attack = Animator.StringToHash("attack");
	private readonly int m_animID_Hurt = Animator.StringToHash("hurt");

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
	private void UpdateAnimationStatus()
	{
		m_animator.SetFloat(m_animID_SpeedX, Mathf.Abs(m_playerController.GetVelocity.x));
		m_animator.SetFloat(m_animID_SpeedY, m_playerController.GetVelocity.y);
		m_animator.SetBool(m_animID_IsOnGround, m_playerController.IsOnGround);
		m_animator.SetBool(m_animID_IsSquat, m_playerController.IsSquat);
		m_animator.SetBool(m_animID_IsSlide, m_playerController.IsSlide);
		m_animator.SetBool(m_animID_IsAttack, m_playerController.IsAttack);
		m_animator.SetBool(m_animID_IsDead, m_playerController.IsDead);
	}

	/// <summary>
	/// 触发受伤动画
	/// </summary>
	public void TriggerHurt()
	{
		m_animator.SetTrigger(m_animID_Hurt);
	}

	/// <summary>
	/// 触发攻击动画
	/// </summary>
	public void TriggerAttack()
	{
		m_animator.SetTrigger(m_animID_Attack);
	}
}
