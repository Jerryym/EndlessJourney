using UnityEngine;

public class EnemyAttackHitBox : MonoBehaviour
{
	[Header("攻击配置")]
	/// <summary>
	/// 攻击倍率
	/// </summary>
	[Range(1.0f, 2.0f)]
	[Tooltip("攻击倍率")]
	public float attackMultiplier = 1.0f;
	/// <summary>
	/// 攻击频率
	/// </summary>
	[Tooltip("攻击频率")]
	public float attackFrequency = 1.0f;

	public EnemyController Controller
	{
		get { return m_controller; }
		set { m_controller = value; }
	}
	private EnemyController m_controller = null;

	private void OnTriggerStay2D(Collider2D other)
	{
		if (m_controller)
		{
            //触发攻击
			m_controller.Attack(other.transform, attackMultiplier);
        }
	}
}
