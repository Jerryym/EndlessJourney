using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
	private CapsuleCollider2D m_collider2D = null;
	private PhysicsCheck m_check = null;
	/// <summary>
	/// 玩家输入操作的控制类实例: 用于处理玩家的键盘、手柄等输入事件
	/// </summary>
	private PlayerInputControl m_inputActions = null;

	/// <summary>
	/// 角色跑步速度
	/// </summary>
	private float m_runSpeed;
	/// <summary>
	/// 角色走路速度
	/// </summary>
	private	float m_walkSpeed => speedX / 2.5f;
	private bool m_bIsWalking = false;
	private bool m_bIsSquat = false;
	private Vector2 m_collSize;
	private Vector2 m_collOffset;

	private void Awake()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_collider2D = GetComponent<CapsuleCollider2D>();
		m_check = GetComponent<PhysicsCheck>();

		m_runSpeed = speedX;
		m_collSize = m_collider2D.size;
		m_collOffset = m_collider2D.offset;

		m_inputActions = new PlayerInputControl();
		m_inputActions.Gameplay.Jump.started += Jump;
		m_inputActions.Gameplay.Walk.started += Walk;
	}

	private void OnEnable()
	{
		m_inputActions?.Enable();
	}

	private void OnDisable()
	{
		m_inputActions?.Disable();
	}

	private void Update()
	{
		inputDir = m_inputActions.Gameplay.Move.ReadValue<Vector2>();
	}

	private void FixedUpdate()
	{
		Move();
	}

	#region Property
	public bool IsSquat
	{
		set { m_bIsSquat = value; }
		get { return m_bIsSquat; }
	}
	#endregion

	/// <summary>
	/// 移动
	/// </summary>
	private void Move()
	{
		if (m_bIsSquat == false)
		{
			m_rigidBody.velocity = new Vector2(speedX * Time.deltaTime * inputDir.x, m_rigidBody.velocity.y);
		}

		//人物翻转（默认朝向X轴正半轴）
		if (inputDir.x != 0)
		{
			m_spriteRenderer.flipX = (inputDir.x > 0.0f) ? false : true;
		}

		//判断下段状态
		m_bIsSquat = inputDir.y < -0.5f && m_check.isOnGround;
		if (m_bIsSquat)
		{
			m_collider2D.size = new Vector2(0.7f, 1.7f);
			m_collider2D.offset = new Vector2(-0.05f, 0.85f);
		}
		else
		{
			m_collider2D.size = m_collSize;
			m_collider2D.offset = m_collOffset;
		}
	}

	#region Event Function
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
			if (!m_bIsWalking)
			{
				speedX = m_walkSpeed;
				m_bIsWalking = true;
			}
			else
			{
				speedX = m_runSpeed;
				m_bIsWalking = false;
			}
		}
	}
	#endregion

}
