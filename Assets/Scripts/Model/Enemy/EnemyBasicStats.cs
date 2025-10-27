using System;
using UnityEngine;

[Serializable]
public class EnemyBasicStats
{
    /// <summary>
	/// 最大生命值
	/// </summary>
	[field: SerializeField]
    public float MaxHealth { get; private set; } = 100f;

   	/// <summary>
	/// 攻击力
	/// </summary>
	[field: SerializeField]
	public float Attack { get; private set; } = 10f;

	/// <summary>
	/// 基础移动速度
	/// </summary>
	[field: SerializeField]
	public float BaseSpeed { get; private set; } = 100.0f;
	
	/// <summary>
    /// 追击速度倍率
    /// </summary>
	[field: SerializeField]
	public float ChaseSpeedMultiplier { get; private set; } = 1.5f;

	/// <summary>
	/// 巡逻速度
	/// </summary>
	public float PatrolSpeed => BaseSpeed;

	/// <summary>
	/// 追击速度
	/// </summary>
	public float ChaseSpeed => ChaseSpeedMultiplier * BaseSpeed;
    
    public EnemyBasicStats() {}
}