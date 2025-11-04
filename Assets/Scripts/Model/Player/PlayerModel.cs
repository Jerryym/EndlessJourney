using System;
using UnityEngine;

[Serializable]//实现可序列化
public class PlayerModel
{
	/// <summary>
	/// 基础属性
	/// </summary>
	[field: SerializeField]
	public PlayerBasicStats playerBasic = null;
	/// <summary>
	/// 战斗属性
	/// </summary>
	[field: SerializeField]
	public PlayerCombatStats playerCombat = null;
	/// <summary>
	/// 移动配置
	/// </summary>
	[field: SerializeField]
	public PlayerMovementConfig playerMovement = null;

	/// <summary>
	/// 受伤击退力
	/// </summary>
	[field: SerializeField]
	public float HurtForce { get; set; } = 8f;

	[field: SerializeField]
	/// <summary>
	/// 最大连续未触发闪避次数
	/// </summary>
	public int maxMissesCount { get; set; } = 5;
	public bool IsMiss => m_isMiss;

	private float m_currentHealth = 0.0f;
	private float m_currentPower = 0.0f;
	/// <summary>
	/// 连续未闪避的次数
	/// </summary>
	private int m_consecutiveMisses = 0;
	private bool m_isMiss = false;

	public PlayerModel() 
	{
		playerBasic = new PlayerBasicStats();
		playerCombat = new PlayerCombatStats();
		playerMovement = new PlayerMovementConfig();

		m_currentHealth = playerBasic.MaxHealth;
		m_currentPower = playerBasic.MaxPower;
	}

	/// <summary>
	/// 受到伤害
	/// </summary>
	/// <param name="damage"></param>
	public void TakeDamage(float damage)
	{
		//闪避判定
		float rEvasionChance = playerCombat.Evasion / (playerCombat.Evasion + 150f);
		float rRandValue = UnityEngine.Random.value;
		if (m_consecutiveMisses >= maxMissesCount || rRandValue < rEvasionChance)
		{
			m_consecutiveMisses = 0;
			m_isMiss = true;
			Debug.Log("玩家闪避成功！");
			return;
		}
		m_consecutiveMisses++;
		m_isMiss = false;

		//伤害计算
		float rDamageReductionRate = playerCombat.Defence / (playerCombat.Defence + 10f);
		float rFinalDamage = damage * (1f - rDamageReductionRate);
		rFinalDamage = Mathf.Max(0f, rFinalDamage);

		//生命值扣除
		m_currentHealth = Mathf.Max(m_currentHealth - rFinalDamage, 0.0f);
	}

	/// <summary>
	/// 判断当前体力值是否足够执行滑铲
	/// </summary>
	/// <returns>如果当前体力大于或等于滑铲消耗的体力，则返回true；否则返回false</returns>
	public bool CanSlide() => m_currentPower >= playerMovement.SlideCost;

	/// <summary>
	/// 消耗体力
	/// </summary>
	public void ConsumePower(float cost)
	{
		if (cost > m_currentPower)
		{
			m_currentPower = 0;
			return;
		}
		m_currentPower -= cost;
	}

	public float Health
	{
		get { return m_currentHealth; }
		set { m_currentHealth = value; }
	}

	public float Power
	{
		get { return m_currentPower; }
		set { m_currentPower = value; }
	}
}
