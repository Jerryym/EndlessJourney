using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Transform参数事件
/// </summary>
[CreateAssetMenu(menuName = "Event/Game Event(Transform)", fileName = "Transform GameEvent")]
public class TransformGameEventSO : GameEventSO
{
	private UnityAction<Transform> m_Action;

	public void Raise(Transform transform)
	{
		m_Action?.Invoke(transform);
	}

	/// <summary>
	/// 注册事件监听
	/// </summary>
	/// <param name="action"></param>
	public void Subscribe(UnityAction<Transform> action)
	{
		m_Action += action;
	}

	/// <summary>
	/// 取消事件监听
	/// </summary>
	/// <param name="action"></param>
	public void UnSubscribe(UnityAction<Transform> action)
	{
		m_Action -= action;
	}
}
