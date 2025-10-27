using System;
using UnityEngine;

[Serializable]//实现可序列化
public class EnemyModel
{
	/// <summary>
	/// 基础属性
	/// </summary>
	[field: SerializeField]
	public EnemyBasicStats enemyBasic = null;

	/// <summary>
	/// 受伤击退力
	/// </summary>
	[field: SerializeField]
	public float HurtForce { get; set; } = 2f;

	private float m_currentHealth;

	public EnemyModel()
	{
		enemyBasic = new EnemyBasicStats();

		m_currentHealth = enemyBasic.MaxHealth;
	}
	
	public float Health
    {
		get { return m_currentHealth; }
		set { m_currentHealth = value; }
    }
}
