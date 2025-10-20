using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_PlayerState : MonoBehaviour
{
	[Header("UI组件")]
	public Image healthImage;
	public Image healthDelayImage;
	public Image powerImage;

	private void Awake()
	{
		//初始化UI
		healthImage.fillAmount = 1;
		powerImage.fillAmount = 1;
	}

	private void Update()
	{
		if (healthDelayImage.fillAmount > healthImage.fillAmount)
		{
			healthDelayImage.fillAmount -= Time.deltaTime * 0.5f;
		}
	}

	public void UpdateHealth(float percentage)
	{
		healthImage.fillAmount = percentage;
	}
}
