using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateHUD : MonoBehaviour
{
	public Image healthImage;
	public Image healthDelayImage;
	public Image powerImage;

	private bool m_bIsRecover = false;

	private void Update()
	{
		if (healthDelayImage.fillAmount > healthImage.fillAmount)
		{
			healthDelayImage.fillAmount -= Time.deltaTime * 0.5f;
		}

		if (m_bIsRecover)
		{
			powerImage.fillAmount += Time.deltaTime * 0.4f;
			if (powerImage.fillAmount >= 1f)
			{
				m_bIsRecover = false;
			}
		}
	}

	public void OnHealthChange(float percentage)
	{
		healthImage.fillAmount = percentage;
	}

	public void OnPowerChange(float percentage)
	{
		powerImage.fillAmount = percentage;
		m_bIsRecover = true;
	}
}
