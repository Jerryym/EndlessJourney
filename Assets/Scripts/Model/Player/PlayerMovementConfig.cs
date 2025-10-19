using System;
using UnityEngine;

/// <summary>
/// Player移动配置：管理移动(行走、奔跑)参数、跳跃参数、滑铲相关参数
/// </summary>
[Serializable]
public class PlayerMovementConfig
{
	#region === 移动 ===
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
	#endregion

	#region === 跳跃 ===
	[field: SerializeField]
	public float JumpForce { get; private set; } = 12f;
	[field: SerializeField]
	public int MaxJumpCount { get; private set; } = 2;
	#endregion

	#region === 滑铲 ===
	/// <summary>
	/// 滑铲距离
	/// </summary>
	[field: SerializeField]
	public float SlideDistance { get; private set; }

	/// <summary>
	/// 滑铲速度
	/// </summary>
	[field: SerializeField]
	public float SlideSpeed { get; private set; }

	/// <summary>
	/// 滑铲所需的体力消耗
	/// </summary>
	[field: SerializeField]

	public float SlideCost { get; private set; }
	#endregion
}
