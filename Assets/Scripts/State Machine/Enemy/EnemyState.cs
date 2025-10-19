/// <summary>
/// 敌人状态基类
/// </summary>
public abstract class EnemyState : IState
{

	#region 接口
	public virtual void OnEnter() { }
	public virtual void OnLogicUpdate() { }
	public virtual void OnPhysicsUpdate() { }
	public virtual void OnExit() { }
	#endregion
}
