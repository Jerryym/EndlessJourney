using System;
using UnityEngine;

/// <summary>
/// Player能力配置: 管理无敌计时器
/// </summary>
[Serializable]
public class PlayerAbilityConfig
{
	/// <summary>
	/// 无敌计时器
	/// </summary>
	[field: SerializeField]
	public float InvincibilityTimer { get; set; } = 2f;
}
