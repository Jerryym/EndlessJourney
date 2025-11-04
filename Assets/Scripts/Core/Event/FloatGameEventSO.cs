using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Float参数事件
/// </summary>
[CreateAssetMenu(menuName = "Event/Game Event(Float)", fileName = "Float GameEvent")]
public class FloatGameEventSO : GameEventSO
{
	private UnityAction<float> m_Action;

	public void Raise(float value)
	{
		m_Action?.Invoke(value);
	}

	/// <summary>
	/// 注册事件监听
	/// </summary>
	/// <param name="action"></param>
	public void Subscribe(UnityAction<float> action)
	{
		m_Action += action;
	}

	/// <summary>
	/// 取消事件监听
	/// </summary>
	/// <param name="action"></param>
	public void UnSubscribe(UnityAction<float> action)
	{
		m_Action -= action;
	}
}
