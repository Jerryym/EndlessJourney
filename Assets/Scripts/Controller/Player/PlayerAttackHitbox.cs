using UnityEngine;

/// <summary>
/// 攻击盒
/// </summary>
public class PlayerAttackHitbox : MonoBehaviour
{
	/// <summary>
	/// 攻击倍率
	/// </summary>
	[Range(1.0f, 2.0f)]
	[Tooltip("攻击倍率")]
	public float attackMultiplier;

	public PlayerController Controller
	{
		get { return m_controller; }
		set { m_controller = value; }
	}
	private PlayerController m_controller = null;

	private void OnTriggerStay2D(Collider2D other)
	{
		if (m_controller)
		{
			//触发攻击
			m_controller.Attack(other.transform, attackMultiplier);
		}
	}
}
