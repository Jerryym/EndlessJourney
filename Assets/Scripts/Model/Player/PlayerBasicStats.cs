using System;
using UnityEngine;

/// <summary>
/// Player基础属性
/// </summary>
[Serializable]
public class PlayerBasicStats
{
	/// <summary>
	/// 最大生命值
	/// </summary>
	[field: SerializeField]
	public float MaxHealth { get; private set; } = 100f;
	/// <summary>
	/// 当前生命值
	/// </summary>
	public float Health { get; set; } = 10f;

	/// <summary>
	/// 最大体力值
	/// </summary>
	[field: SerializeField]
	public float MaxPower { get; private set; } = 80f;
	/// <summary>
	/// 当前体力值
	/// </summary>
	public float Power { get; set; } = 10f;

	public PlayerBasicStats()
	{
		Health = MaxHealth;
		Power = MaxPower;
	}
}
