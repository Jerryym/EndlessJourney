namespace FSM.Enums
{
	/// <summary>
	/// 角色状态枚举
	/// </summary>
	public enum PlayerStateEnum
	{
		None = -1,
		Idle,
		Move,
		Jump,
		Squat,
		Slide,
		Attack
	}

	/// <summary>
	/// 敌人状态枚举
	/// </summary>
	public enum EnemyStateEnum
	{
		None = -1,
		Idle,
		Patrol,
		Chase
	}
}
