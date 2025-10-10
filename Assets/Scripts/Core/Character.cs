using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
	[Header("Property")]
	/// <summary>
	/// 最大血量
	/// </summary>
	public float maxHealth = 100f;
	/// <summary>
	/// 当前血量
	/// </summary>
	public float currentHealth;
	/// <summary>
	/// 最大体力值
	/// </summary>
	public float maxPower;
	/// <summary>
	/// 当前体力值
	/// </summary>
	public float currentPower;
	
	public float PowerRecoveryRate = 1f;
	/// <summary>
	/// 攻击力
	/// </summary>
	public float attack;

	[Header("Invincibility")]
	/// <summary>
	/// 无敌时间
	/// </summary>
	public float invincibilityTime = 2f;

	[Header("Events")]
	/// <summary>
	/// 血量变化事件
	/// </summary>
	public UnityEvent<Character> OnHealthChange;
	/// <summary>
    /// 体力值变化事件
    /// </summary>
	public UnityEvent<Character> OnPowerChange;
	/// <summary>
	/// 受伤事件
	/// </summary>
	public UnityEvent<Transform> OnTakeDamage;
	/// <summary>
	/// 死亡事件
	/// </summary>
	public UnityEvent OnDead;

	/// <summary>
	/// 当前无敌时间
	/// </summary>
	protected float m_rCurrentInvincibityTime = 0.0f;
	/// <summary>
	/// 是否无敌
	/// </summary>
	protected bool m_bIsInvincible = false;

	/// <summary>
	/// 受到伤害
	/// </summary>
	/// <param name="attacker"></param>
	public abstract void TakeDamage(Character attacker);
	/// <summary>
    /// 体力值变化
    /// </summary>
    /// <param name="powerCost"></param>
	public abstract void PowerChange(float powerCost);

	private void Start()
	{
		//初始化当前血量
		currentHealth = maxHealth;
		OnHealthChange?.Invoke(this);

		//初始化当前体力值
		currentPower = maxPower;
		OnPowerChange?.Invoke(this);
	}

	/// <summary>
	/// 触发无敌
	/// </summary>
	/// <exception cref="NotImplementedException"></exception>
	protected void TriggerInvincible()
	{
		if (m_bIsInvincible == false)
		{
			m_bIsInvincible = true;
			m_rCurrentInvincibityTime = invincibilityTime;
		}
	}

	/// <summary>
	/// 无敌计时器: 在子类的Update中调用
	/// </summary>
	protected void InvincibityTimer()
	{
		if (m_bIsInvincible)
		{
			m_rCurrentInvincibityTime -= Time.deltaTime;
			if (m_rCurrentInvincibityTime <= 0.0f)
			{
				m_bIsInvincible = false;
			}
		}
	}

	#region Property
	public bool IsInvincible
	{
		set { m_bIsInvincible = value; }
		get { return m_bIsInvincible; }
	}
	#endregion
}
