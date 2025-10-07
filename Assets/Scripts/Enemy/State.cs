/// <summary>
/// 敌人状态
/// </summary>
public enum EnemyStateType
{
	None = -1,
	Patrol,
	Chase,
	Die
}

public abstract class State
{
	protected Enemy m_enemy;

	/// <summary>
	/// 当状态被激活时调用
	/// </summary>
	public abstract void OnEnter(Enemy enemy);

	/// <summary>
	/// 当状态即将被切换时调用
	/// </summary>
	public abstract void OnExit();

	/// <summary>
	/// 状态的“每帧”更新逻辑，在 Unity 的 Update() 循环中调用
	/// </summary>
	public abstract void OnUpdate();

	/// <summary>
	/// 状态的“固定时间步长”更新逻辑，在 Unity 的 FixedUpdate() 循环中调用
	/// </summary>
	public abstract void OnPhysicsUpdate();
}
