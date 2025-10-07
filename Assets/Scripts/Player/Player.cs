using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
	[Header("Property")]
	/// <summary>
	/// 防御值
	/// </summary>
	public float defense = 5f;
	/// <summary>
	/// 闪避值
	/// </summary>
	public float evasion = 10f;
	/// <summary>
	/// 最大连续未触发闪避次数
	/// </summary>
	public int maxMissesCount = 5;

	/// <summary>
	/// 连续未闪避的次数
	/// </summary>
	private int m_iConsecutiveMisses = 0;

	private void Update()
	{
		base.InvincibityTimer();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("River"))
		{
			currentHealth = 0;
			//触发死亡动画
			OnDead?.Invoke();
			//触发血量变化
			OnHealthChange?.Invoke(this);
		}
	}

	public override void TakeDamage(Character attacker)
	{
		if (m_bIsInvincible)
		{
			return;
		}

		//闪避判定
		float rEvasionChance = evasion / (evasion + 150f);
		if (m_iConsecutiveMisses >= maxMissesCount || Random.value < rEvasionChance)
		{
			m_iConsecutiveMisses = 0;
			Debug.Log("玩家闪避成功！");
			return;
		}
		m_iConsecutiveMisses ++;

		//伤害计算
		float rDamageReductionRate = defense / (defense + 100f);
		float rDamage = attacker.attack * (1f - rDamageReductionRate);

		//生命值扣除
		currentHealth -= rDamage;
		currentHealth = Mathf.Max(currentHealth, 0.0f);
		Debug.Log("current health = " +  currentHealth);
		if (currentHealth > 0.0f)
		{
			//触发无敌
			base.TriggerInvincible();
			//触发受伤动画
			base.OnTakeDamage?.Invoke(attacker.transform);
		}
		else
		{
			//触发死亡动画
			OnDead?.Invoke();
		}

		//触发血量变化
		OnHealthChange?.Invoke(this);
	}

}
