using System;
using UnityEngine;

[Serializable]//实现可序列化
public class PlayerModel
{
	/// <summary>
	/// 最大生命值
	/// </summary>
	public float MaxHealth { get; } = 100f;
	/// <summary>
	/// 当前生命值
	/// </summary>
	public float Health { get; private set; } = 10f;
	
	/// <summary>
	/// 最大体力值
	/// </summary>
	[field: SerializeField]
	public float MaxStamina { get; set; }
	/// <summary>
	/// 当前体力值
	/// </summary>
	public float Stamina { get; private set; }
	
	/// <summary>
	/// 攻击力
	/// </summary>
	[field: SerializeField]
	public float Attack { get; set; } = 10f;
	/// <summary>
	/// 防御值
	/// </summary>
	[field: SerializeField]
	public float Defence { get; set; } = 5f;
	/// <summary>
	/// 闪避值
	/// </summary>
	[field: SerializeField]
	public float Evasion { get; set; } = 10f;

	/// <summary>
	/// 基础移动速度
	/// </summary>
	[field: SerializeField]
	public float BaseSpeed { get; private set; }
	/// <summary>
	/// 行走速度
	/// </summary>
	public float WalkSpeed => BaseSpeed;
	/// <summary>
	/// 奔跑速度
	/// </summary>
	public float RunSpeed => 2 * BaseSpeed;

	[field: SerializeField]
	public float HurtForce { get; set; } = 8f;

	/// <summary>
	/// 滑铲距离
	/// </summary>
	[field: SerializeField]
	public float SlideDistance { get; private set; }
	/// <summary>
	/// 滑铲速度
	/// </summary>
	[field: SerializeField]
	public float SlideSpeed {  get; private set; }
	/// <summary>
	/// 滑铲所需的体力消耗
	/// </summary>
	[field: SerializeField]
	public float SlideCost { get; set; }

	/// <summary>
	/// 初始化
	/// </summary>
	public void Init()
	{
		Health = MaxHealth;
		Stamina = MaxStamina;
	}

	/// <summary>
	/// 受到伤害
	/// </summary>
	/// <param name="damage"></param>
	public void TakeDamage(float damage)
	{
		//闪避判定
		float rEvasionChance = Evasion / (Evasion + 150f);
		float rRandValue = UnityEngine.Random.value;
		if (rRandValue < rEvasionChance)
		{
			return;
		}

		//伤害计算
		float rDamageReductionRate = Defence / (Defence + 10f);
		float rFinalDamage = damage * (1f - rDamageReductionRate);
		rFinalDamage = Mathf.Max(0f, rFinalDamage);

		//生命值扣除
		Health = Mathf.Max(Health - rFinalDamage, 0.0f);
	}

}
