using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
	[Header("Basic Property")]
	/// <summary>
	/// 巡逻速度
	/// </summary>
	public float patrolSpeed;
	/// <summary>
	/// 追击速度
	/// </summary>
	public float chaseSpeed;
	/// <summary>
	/// 当前速度
	/// </summary>
	public float currentSpeed;
	/// <summary>
	/// 受击力
	/// </summary>
	public float hurtForce = 2;
	/// <summary>
	/// 朝向
	/// </summary>
	protected Vector3 m_faceDir;

	[Header("Timer")]
	public float timer;
	private float m_rTimeCounter;
	public float lostPlayerTimer;
	private float m_rLostTimeCounter;

	[HideInInspector] public Rigidbody2D rigidBody = null;
	[HideInInspector] public Animator animator = null;
	[HideInInspector] public PhysicsCheck check = null;

	[Header("Detection")]
	public Vector2 checkBoxOffset;
	public Vector2 checkBoxSize;
	public float detectionDistance;
	public LayerMask checkLayer;

	protected bool m_bWait = false;
	protected bool m_bIsWalk = true;
	protected bool m_bIsHurt = false;
	protected bool m_bIsDead = false;
	protected bool m_bIsAttack = false;
	protected bool m_bIsChase = false;

	/// <summary>
	/// 当前状态
	/// </summary>
	private State m_state;
	/// <summary>
	/// 巡逻状态
	/// </summary>
	protected State m_patrolState;
	/// <summary>
	/// 追击状态
	/// </summary>
	protected State m_chaseState;

	#region Unity 消息
	protected virtual void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		check = GetComponent<PhysicsCheck>();

		//初始化当前速度
		currentSpeed = patrolSpeed;
		//初始化定时器
		m_rTimeCounter = timer;
		m_rLostTimeCounter = lostPlayerTimer;
	}

	private void OnEnable()
	{
		//初始化默认状态
		m_state = m_patrolState;
		m_state.OnEnter(this);
	}

	private void Update()
	{
		//撞墙等待
		m_faceDir = new Vector3(-transform.localScale.x, 0, 0);
		//状态更新
		m_state.OnUpdate();

		if (m_bWait)
		{ 
			Timer();
		}
		base.InvincibityTimer();
	}

	private void OnDisable()
	{
		m_state.OnExit();
	}

	private void FixedUpdate()
	{
		if (!m_bIsHurt && !m_bIsDead && !m_bWait)
		{
			Move();
		}
		m_state.OnPhysicsUpdate();
	}
	#endregion

	#region Property
	public Vector2 FaceDir
	{
		get { return m_faceDir; }
	}

	public float LostTimeCounter
	{
		get { return m_rLostTimeCounter; }
	}

	public bool IsWalk
	{
		set { m_bIsWalk = value; }
		get { return m_bIsWalk; }
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

	public bool IsWait
	{
		set { m_bWait = value; }
		get { return m_bWait; }
	}

	public bool IsChase
	{
		set { m_bIsChase = value; }
		get { return m_bIsChase; }
	}
	#endregion

	public override void TakeDamage(Character attacker)
	{
		//判断Player是否攻击了
		PlayerController controller = attacker.gameObject.GetComponent<PlayerController>();
		//if (controller != null && controller.IsAttack != true)
		//{
		//	return;
		//}

		//判断是否无敌
		if (m_bIsInvincible)
		{
			return;
		}

		//生命值扣除
		base.currentHealth -= attacker.attack;
		base.currentHealth = Mathf.Max(base.currentHealth, 0.0f);
		if (currentHealth > 0.0f)
		{
			m_bIsHurt = true;
			//触发无敌
			base.TriggerInvincible();
			//触发受伤动画
			base.OnTakeDamage?.Invoke(attacker.transform);
		}
		else
		{
			//触发死亡动画
			base.OnDead?.Invoke();
		}
	}

	public void OnDamage(Transform attacker)
	{
		if (!m_bIsHurt)
		{
			return;
		}

		//受击转身
		if (attacker.transform.position.x - transform.position.x > 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		if (attacker.transform.position.x - transform.position.x < 0)
		{
			transform.localScale = Vector3.one;
		}

		//受击后退
		rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
		Vector2 dirVec = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
		StartCoroutine(OnDamage(dirVec));
	}

	public virtual void Move()
	{
		rigidBody.velocity = new Vector2(currentSpeed * m_faceDir.x * Time.deltaTime, rigidBody.velocity.y);
	}

	public void Dead()
	{
		m_bIsDead = true;
		gameObject.layer = 2;
	}

	/// <summary>
	/// 销毁资源
	/// </summary>
	public void DestoryResource()
	{
		Destroy(this.gameObject);
	}

	/// <summary>
	/// 定时器
	/// </summary>
	private void Timer()
	{
		//等待定时器
		if (m_bWait)
		{
			m_rTimeCounter -= Time.deltaTime;
			if (m_rTimeCounter <= 0)
			{
				transform.localScale = new Vector3(m_faceDir.x, 1, 1);

				//重置定时器
				m_bWait = false;
				m_rTimeCounter = timer;
			}
		}

		//丢失Player焦点定时器
		if (!FindPlayer() && m_rLostTimeCounter > 0)
		{
			m_rLostTimeCounter -= Time.deltaTime;
		}
		else if (FindPlayer())
		{
			//重置
			m_rLostTimeCounter = lostPlayerTimer;
		}
	}

	public bool FindPlayer()
	{
		return Physics2D.BoxCast(transform.position + (Vector3)checkBoxOffset, checkBoxSize, 0, m_faceDir, detectionDistance, checkLayer);
	}

	public void SwitchState(EnemyStateType state)
	{
		m_state.OnExit();
		switch (state)
		{
			case EnemyStateType.None:
				break;
			case EnemyStateType.Patrol:
				m_state = m_patrolState;
				break;
			case EnemyStateType.Chase:
				m_state = m_chaseState;
				break;
			case EnemyStateType.Die:
				break;
			default:
				break;
		}
		m_state.OnEnter(this);
	}

	private IEnumerator OnDamage(Vector2 dirVec)
	{
		rigidBody.AddForce(dirVec * hurtForce, ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.5f);
		m_bIsHurt = false;
	}

	public override void PowerChange(float powerCost)
	{
	}
}
