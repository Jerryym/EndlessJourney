using UnityEngine;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : MonoBehaviour
{
	public HUD_PlayerState playerStateHUD;

	[Header("事件监听")]
	public FloatGameEventSO healthEvent;
	public FloatGameEventSO powerEvent;

	private void OnEnable()
	{
		healthEvent.Subscribe(OnHealthEvent);
		powerEvent.Subscribe(OnPowerChangeEvent);
	}

	private void OnDisable()
	{
		healthEvent.UnSubscribe(OnHealthEvent);
		powerEvent.UnSubscribe(OnPowerChangeEvent);
	}

	private void OnHealthEvent(float healthPercent)
	{
		playerStateHUD.UpdateHealth(healthPercent);
	}

	private void OnPowerChangeEvent(float powerPercent)
	{
		playerStateHUD.UpdatePower(powerPercent);
	}
}
