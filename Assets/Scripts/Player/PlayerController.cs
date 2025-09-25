using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	/// <summary>
	/// 玩家输入操作的控制类实例: 用于处理玩家的键盘、手柄等输入事件
	/// </summary>
	public PlayerInputControl inputActions;
	/// <summary>
	/// 玩家的输入方向
	/// </summary>
	public Vector2 inputDir;
	/// <summary>
	/// 角色X方向移动速度
	/// </summary>
	public float speedX;
	/// <summary>
	/// 施加给角色向上的跳跃力
	/// </summary>
	public float jumpFocus = 12;

	private SpriteRenderer m_spriteRenderer = null;
	private Rigidbody2D m_rigidBody = null;
	private PhysicsCheck m_check = null;

	/// <summary>
	/// 角色跑步速度
	/// </summary>
	private float m_runSpeed;
	/// <summary>
	/// 角色走路速度
	/// </summary>
	private	float m_walkSpeed => speedX / 2.5f;
	private bool m_isWalking = false;

	private void Awake()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_check = GetComponent<PhysicsCheck>();
		m_runSpeed = speedX;

		inputActions = new PlayerInputControl();
		inputActions.Gameplay.Jump.started += Jump;
		inputActions.Gameplay.Walk.started += Walk;
	}

	private void OnEnable()
	{
		inputActions?.Enable();
	}

	private void OnDisable()
	{
		inputActions?.Disable();
	}

	private void Update()
	{
		inputDir = inputActions.Gameplay.Move.ReadValue<Vector2>();
	}

	private void FixedUpdate()
	{
		Move();
	}

	/// <summary>
	/// 移动
	/// </summary>
	private void Move()
	{
		m_rigidBody.velocity = new Vector2(speedX * Time.deltaTime * inputDir.x, m_rigidBody.velocity.y);

		//人物翻转（默认朝向X轴正半轴）
		if (inputDir.x != 0)
		{
			m_spriteRenderer.flipX = (inputDir.x > 0.0f) ? false : true;
		}
	}

	/// <summary>
	/// 跳跃
	/// </summary>
	/// <param name="obj"></param>
	/// <exception cref="NotImplementedException"></exception>
	private void Jump(InputAction.CallbackContext obj)
	{
		if (m_check.isOnGround)
		{
			Debug.Log("Jump");
			m_rigidBody.AddForce(transform.up * jumpFocus, ForceMode2D.Impulse);
		}
	}

	private void Walk(InputAction.CallbackContext obj)
	{
		if (m_check.isOnGround)
		{
			if (!m_isWalking)
			{
				speedX = m_walkSpeed;
				m_isWalking = true;
			}
			else
			{
				speedX = m_runSpeed;
				m_isWalking = false;
			}
		}
	}

}
