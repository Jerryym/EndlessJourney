/// <summary>
/// 状态机接口类: 定义了所有状态类必须实现的生命周期方法, 实现了逻辑更新、物理更新
/// </summary>
public interface IState
{
	/// <summary>
	/// 状态进入时调用: 用于初始化状态
	/// </summary>
	void OnEnter();
	/// <summary>
	/// 状态逻辑更新: 在 Unity 的 Update() 中调用, 用于处理输入、游戏逻辑、状态转换(SwitchState)等操作
	/// </summary>
	void OnLogicUpdate();
	/// <summary>
	/// 状态物理更新: 在 Unity 的 FixedUpdate() 中调用, 用于给gameObject施加力、修改速度等操作
	/// </summary>
	void OnPhysicsUpdate();
	/// <summary>
	/// 状态退出时调用: 用于清理资源、停止动画等
	/// </summary>
	void OnExit();
}
