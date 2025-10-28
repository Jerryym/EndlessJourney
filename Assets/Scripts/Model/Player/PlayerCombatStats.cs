using System;
using UnityEngine;

/// <summary>
/// Player战斗属性
/// </summary>
[Serializable]
public class PlayerCombatStats
{
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
}
