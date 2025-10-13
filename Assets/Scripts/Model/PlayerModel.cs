using System;
using UnityEngine;

[System.Serializable]//实现可序列化
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
	/// 攻击力
	/// </summary>
	public float Attack { get; set; } = 10f;

	/// <summary>
	/// 防御值
	/// </summary>
	public float Defence { get; set; } = 5f;

	/// <summary>
	/// 闪避制
	/// </summary>
	public float Evasion { get; set; } = 10f;

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
		Health = MathF.Max(Health - rFinalDamage, 0.0f);
	}

}
