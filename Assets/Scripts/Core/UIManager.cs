using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : MonoBehaviour
{
	public PlayerStateHUD playerStateHUD;

	[Header("事件监听")]
	public CharacterEventSO healthEvent;
	public CharacterEventSO powerEvent;

	#region Unity消息
	private void OnEnable()
	{
		healthEvent.OnEventRaised += OnHealthEvent;
		powerEvent.OnEventRaised += OnPowerEvent;
	}

	private void OnDisable()
	{
		healthEvent.OnEventRaised -= OnHealthEvent;
		powerEvent.OnEventRaised -= OnPowerEvent;
	}
	#endregion

	private void OnHealthEvent(Character character)
	{
		var healthPercent = character.currentHealth / character.maxHealth;
		playerStateHUD.OnHealthChange(healthPercent);
	}

	private void OnPowerEvent(Character character)
	{
		var powerPercent = character.currentPower / character.maxPower;
		playerStateHUD.OnPowerChange(powerPercent);
	}
}
