/// <summary>
/// 状态机基类
/// </summary>
public class StateMachine
{
	/// <summary>
	/// 当前状态
	/// </summary>
	public IState currentState;

	public virtual void Update()
	{
		currentState?.OnUpdate();
	}

	public virtual void FixedUpdate()
	{
		currentState?.OnPhysicsUpdate();
	}

	/// <summary>
	/// 切换状态
	/// </summary>
	/// <param name="newState">新状态</param>
	public virtual void SwitchState(IState newState)
	{
		if (newState == currentState)
		{
			return;
		}

		//退出当前状态
		currentState?.OnExit();
		//更新状态
		currentState = newState;
		//初始化新状态
		currentState.OnEnter();
	}
}
