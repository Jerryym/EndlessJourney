using UnityEngine;

/// <summary>
/// 宝箱控制器
/// </summary>
public class ChestController : MonoBehaviour, IInteractable
{
	/// <summary>
	/// 宝箱打开的图片
	/// </summary>
	public Sprite openChest;
	/// <summary>
	/// 宝箱关闭的图片
	/// </summary>
	public Sprite closeChest;

	private SpriteRenderer m_spriteRenderer = null;
	private bool m_isOpen = false;

	#region Unity 生命周期函数
	private void Awake()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		m_spriteRenderer.sprite = m_isOpen ? openChest : closeChest;
	}
	#endregion

	public void TriggerAction()
	{
		Debug.Log("Open Chest");
		if (!m_isOpen)
		{
			OpenChest();
		}
	}

	private void OpenChest()
	{
		m_spriteRenderer.sprite = openChest;
		m_isOpen = true;
		this.gameObject.tag = "Untagged";
	}
}
