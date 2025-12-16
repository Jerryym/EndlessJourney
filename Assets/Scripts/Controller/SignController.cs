using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

/// <summary>
/// Sign控制器: 用于控制玩家与场景和场景中的物体交互
/// </summary>
public class SignController : MonoBehaviour
{
	public GameObject signSprite;
	public Transform playerTrans;
	private bool m_canPress = false;

	private Animator m_animator = null;
	private PlayerInputControl m_inputActions = null;

	private IInteractable m_targetItem = null;

	private void Awake()
	{
		m_animator = signSprite.GetComponent<Animator>();

		m_inputActions = new PlayerInputControl();
		m_inputActions.Gameplay.Confirm.started += OnConfirm;
	}

	private void OnEnable()
	{
		m_inputActions.Enable();
		InputSystem.onActionChange += OnActionChange;
	}

	private void OnDisable()
	{
		m_canPress = false;
	}

	private void Update()
	{
		signSprite.GetComponent<SpriteRenderer>().enabled = m_canPress;
		signSprite.transform.localScale = playerTrans.localScale;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Chest") || other.CompareTag("Teleport"))//宝箱 || 传送门
		{
			m_canPress = true;
			m_targetItem = other.GetComponent<IInteractable>();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		m_canPress = false;
	}

	private void OnActionChange(object obj, InputActionChange actionChange)
	{
		if (actionChange == InputActionChange.ActionStarted)
		{
			var inputControl = ((InputAction)obj).activeControl;
			Debug.Log("当前控制器类型: " + inputControl.device);
			switch (inputControl.device)
			{
				case Keyboard:
					m_animator.Play("keyboard");
					break;
				case XInputController:
					m_animator.Play("xbox");
					break;
			}
		}
	}

	private void OnConfirm(InputAction.CallbackContext context)
	{
		if (m_canPress && m_targetItem != null)
		{
			m_targetItem.TriggerAction();
		}
	}
}
