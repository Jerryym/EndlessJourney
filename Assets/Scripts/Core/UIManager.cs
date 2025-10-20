using UnityEngine;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : MonoBehaviour
{
	public HUD_PlayerState playerStateHUD;

	[Header("事件监听")]
	public GameEventFloat healthEvent;
	public GameEventFloat powerEvent;

	private void OnEnable()
	{
		healthEvent.Subscribe(OnHealthEvent);
	}

	private void OnDisable()
	{
		healthEvent.UnSubscribe(OnHealthEvent);
	}

	private void OnHealthEvent(float healthPercent)
	{
		playerStateHUD.UpdateHealth(healthPercent);
	}

}
