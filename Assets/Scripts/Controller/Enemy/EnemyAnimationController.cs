using UnityEngine;

/// <summary>
/// 敌人动画控制器
/// </summary>
[RequireComponent(typeof(Animator))]
public class EnemyAnimationController : MonoBehaviour
{
	private Animator m_animator = null;
	private EnemyController m_enemy = null;

	//Animator参数
	private readonly int m_animID_IsWalk = Animator.StringToHash("isWalk");
	private readonly int m_animID_IsDead = Animator.StringToHash("isDead");
	private readonly int m_animID_IsChase = Animator.StringToHash("isChase");
	private readonly int m_animID_Hurt = Animator.StringToHash("hurt");

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_enemy = GetComponent<EnemyController>();
	}

	private void Update()
	{
		//更新动画状态
		UpdateAnimationStatus();
	}

	public void TriggerHurt()
	{
		m_animator.SetTrigger(m_animID_Hurt);
	}

	/// <summary>
	/// 更新动画状态
	/// </summary>
	private void UpdateAnimationStatus()
	{
		m_animator.SetBool(m_animID_IsWalk, m_enemy.IsWalk);
		m_animator.SetBool(m_animID_IsChase, m_enemy.IsChase);
		m_animator.SetBool(m_animID_IsDead, m_enemy.IsDead);
	}

}
