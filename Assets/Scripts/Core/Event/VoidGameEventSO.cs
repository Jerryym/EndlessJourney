using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 无参数事件
/// </summary>
[CreateAssetMenu(menuName = "Event/Game Event(Void)", fileName = "Void GameEvent")]
public class VoidGameEventSO : GameEventSO
{
	private UnityAction m_action;

	public void Raise()
	{
		m_action?.Invoke();
	}

	public void Subscribe(UnityAction action)
	{
		m_action += action;
	}

	public void Unsubscribe(UnityAction action)
	{
		m_action -= action;
	}
}
