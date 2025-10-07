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

	#region Unity消息
	private void OnEnable()
	{
		healthEvent.OnEventRaised += OnHealthEvent;
	}

	private void OnDisable()
	{
		healthEvent.OnEventRaised -= OnHealthEvent;
	}
	#endregion

	private void OnHealthEvent(Character character)
	{
		var healthPercent = character.currentHealth / character.maxHealth;
		playerStateHUD.OnHealthChange(healthPercent);
	}
}
