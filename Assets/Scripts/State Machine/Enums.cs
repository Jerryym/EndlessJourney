namespace FSM.Enums
{
	/// <summary>
	/// Player状态枚举
	/// </summary>
	public enum PlayerStateEnum
	{
		None = -1,
		Idle,
		Move,
		Jump,
		Squat,
		Slide,
		Attack,
		Hurt
	}

	/// <summary>
	/// 敌人状态枚举
	/// </summary>
	public enum EnemyStateEnum
	{
		None = -1,
		Idle,
		Patrol,
		Chase,
		Hurt
	}
}

namespace Scene.Enums
{
	/// <summary>
	/// 场景类型
	/// </summary>
	public enum SceneType
	{
		Location,
		Menu
	}
}
