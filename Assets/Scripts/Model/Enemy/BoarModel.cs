using System;

[Serializable]
public class BoarModel : EnemyModel
{
	/// <summary>
	/// 巡逻速度
	/// </summary>
	public float PatrolSpeed => base.BaseSpeed;

	/// <summary>
	/// 追击速度
	/// </summary>
	public float ChaseSpeed => 1.5f * base.BaseSpeed;
}
