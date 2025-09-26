using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("角色属性")]
	/// <summary>
	/// 角色最大血量
	/// </summary>
	public float maxHealth = 100f;
	/// <summary>
	/// 攻击力
	/// </summary>
	public float attack;
	/// <summary>
	/// 防御值
	/// </summary>
	public float defense;
	/// <summary>
	/// 魔法值
	/// </summary>
	public float mana = 50f;
	/// <summary>
	/// 闪避值
	/// </summary>
	public float evasion = 10f;

	[SerializeField]
	/// <summary>
	/// 角色当前血量
	/// </summary>
	private float m_currentHealth;

	private void Awake()
	{
		m_currentHealth = maxHealth;
	}
}
