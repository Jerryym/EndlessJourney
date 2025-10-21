using System;
using UnityEngine;

[Serializable]//实现可序列化
public abstract class EnemyModel
{
	/// <summary>
	/// 最大生命值
	/// </summary>
	[field: SerializeField]
	public float MaxHealth { get; }
	/// <summary>
	/// 当前生命值
	/// </summary>
	[field: SerializeField]
	public float Health { get; private set; }
	/// <summary>
	/// 攻击力
	/// </summary>
	[field: SerializeField]
	public float Attack { get; set; } = 10f;

	/// <summary>
	/// 基础移动速度
	/// </summary>
	[field: SerializeField]
	public float BaseSpeed { get; private set; }

	/// <summary>
	/// 初始化函数
	/// </summary>
	public void Init()
	{
		Health = MaxHealth;
	}

	/// <summary>
	/// 受到伤害
	/// </summary>
	/// <param name="damage"></param>
	public void TakeDamage(float damage)
	{
		Health = Mathf.Max(Health - damage, 0.0f);
	}
}
