using UnityEngine;
using UnityEngine.UI;

public class HUD_PlayerState : MonoBehaviour
{
	[Header("UI组件")]
	public Image healthImage;
	public Image healthDelayImage;
	public Image powerImage;

	private float m_healthAmount = 1;
	private float m_powerAmount = 1;

	private void Awake()
	{
		//初始化UI
		healthImage.fillAmount = m_healthAmount;
		powerImage.fillAmount = m_powerAmount;
	}

	private void Update()
	{
		//血量
		if (healthImage.fillAmount > m_healthAmount)
		{
			healthImage.fillAmount -= Time.deltaTime * 1.5f;
		}

		if (healthDelayImage.fillAmount > healthImage.fillAmount)
		{
			healthDelayImage.fillAmount -= Time.deltaTime * 0.5f;
		}

		//体力
		if (powerImage.fillAmount > m_powerAmount)
		{
			powerImage.fillAmount -= Time.deltaTime;
		}
	}

	public void UpdateHealth(float percentage)
	{
		m_healthAmount = percentage;
	}

	public void UpdatePower(float percentage)
	{
		m_powerAmount = percentage;
	}
}
