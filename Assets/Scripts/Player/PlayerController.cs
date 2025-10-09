using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
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
	public float jumpForce = 12;
	/// <summary>
	/// 受击力
	/// </summary>
	public float hurtForce = 8;
	private bool m_bIsHurt = false;

	[Header("Physic Mats")]
	/// <summary>
	/// 光滑物理材质
	/// </summary>
	public PhysicsMaterial2D smoothMat;
	/// <summary>
	/// 粗糙物理材质
	/// </summary>
	public PhysicsMaterial2D roughMat;

	private SpriteRenderer m_spriteRenderer = null;
	private Rigidbody2D m_rigidBody = null;
	private CapsuleCollider2D m_collider2D = null;
	private PhysicsCheck m_check = null;
	private PlayerInputControl m_inputActions = null;
	private PlayerAnimationController m_animationController = null;

	/// <summary>
	/// 角色跑步速度
	/// </summary>
	private float m_rRunSpeed;
	/// <summary>
	/// 角色走路速度
	/// </summary>
	private float m_rWalkSpeed;
	private bool m_bIsWalking = false;

	private Vector2 m_collSize;
	private Vector2 m_collOffset;

	private bool m_bIsSquat = false;
	private bool m_bIsDead = false;
	private bool m_bIsAttack = false;

	private void Awake()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_collider2D = GetComponent<CapsuleCollider2D>();
		m_check = GetComponent<PhysicsCheck>();
		m_inputActions = new PlayerInputControl();
		m_animationController = GetComponent<PlayerAnimationController>();

		m_rWalkSpeed = speedX / 2.5f;
		m_rRunSpeed = speedX;
		m_collSize = m_collider2D.size;
		m_collOffset = m_collider2D.offset;

		m_inputActions.Gameplay.Jump.started += Jump;
		m_inputActions.Gameplay.Walk.started += Walk;
		m_inputActions.Gameplay.Attack.started += Attack;
		m_inputActions.Gameplay.Slide.started += Slide;
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
		//修改物理材质
		ModifyPhysicMat();
	}

	private void FixedUpdate()
	{
		if (!m_bIsHurt && !m_bIsAttack)
		{
			Move();
		}
	}

	#region Property
	public bool IsSquat
	{
		set { m_bIsSquat = value; }
		get { return m_bIsSquat; }
	}

	public bool IsHurt
	{
		set { m_bIsHurt = value; }
		get { return m_bIsHurt; }
	}

	public bool IsDead
	{
		set { m_bIsDead = value; }
		get { return m_bIsDead; }
	}

	public bool IsAttack
	{
		set { m_bIsAttack = value; }
		get { return m_bIsAttack; }
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

	public void OnDamage(Transform attacker)
	{
		m_bIsHurt = true;
		m_rigidBody.velocity = Vector2.zero;

		//受伤后退
		Vector2 dirVec = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
		m_rigidBody.AddForce(dirVec * hurtForce, ForceMode2D.Impulse);
	}

	/// <summary>
	/// 死亡
	/// </summary>
	public void Dead()
	{
		m_bIsDead = true;
		//禁止输入
		m_inputActions.Gameplay.Disable();
	}

	/// <summary>
	/// 修改物理材质
	/// </summary>
	private void ModifyPhysicMat()
	{
		m_collider2D.sharedMaterial = m_check.isOnGround ? roughMat : smoothMat;
	}

	#region Event Function
	private void Jump(InputAction.CallbackContext obj)
	{
		if (m_check.isOnGround)
		{
			Debug.Log("Jump");
			m_rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
		}
	}

	private void Walk(InputAction.CallbackContext obj)
	{
		if (m_check.isOnGround)
		{
			if (!m_bIsWalking)
			{
				speedX = m_rWalkSpeed;
				m_bIsWalking = true;
			}
			else
			{
				speedX = m_rRunSpeed;
				m_bIsWalking = false;
			}
		}
	}

	private void Attack(InputAction.CallbackContext obj)
	{
		//触发攻击动画
		m_animationController.TriggerAttack();
		m_bIsAttack = true;
	}

	private void Slide(InputAction.CallbackContext context)
	{

	}
	#endregion

}
