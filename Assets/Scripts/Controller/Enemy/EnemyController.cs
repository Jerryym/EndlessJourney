using FSM.Enums;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PhysicsCheck))]
[RequireComponent(typeof(EnemyAnimationController))]
public abstract class EnemyController : MonoBehaviour
{
	public EnemyModel Enemy => m_enemy;
	[SerializeField]
	protected EnemyModel m_enemy = new EnemyModel();

	#region 基础属性
	/// <summary>
	/// 朝向
	/// </summary>
	public Vector2 FaceDir => m_faceDir;
	protected Vector2 m_faceDir;

	/// <summary>
	/// 当前速度
	/// </summary>
	public float CurrentSpeed
    {
        get { return m_currentSpeed; }
        set { m_currentSpeed = value; }
    }
	protected float m_currentSpeed = 0.0f;
	#endregion

	/// <summary>
	/// 状态机
	/// </summary>
	protected EnemyStateMachine m_stateMachine = null;

	[Header("Detection")]
	public Vector2 checkBoxOffset;
	public Vector2 checkBoxSize = Vector2.one;
	[Tooltip("检测距离")]
	public float detectionDistance;
	[Tooltip("检测图层")]
	public LayerMask checkLayer;

	#region 计时器
	[Header("Timer")]
	[Tooltip("等待计时器")]
	public float waitTimer = 0.3f;
	private float m_waitDuration = 0.0f;

	[Tooltip("无敌计时器")]
	public float invincibilityTimer = 0.2f;
	private float m_invincibilityDuration = 0.0f;
	public bool IsInvincible => m_invincibilityDuration > 0f;

	[Tooltip("丢失玩家焦点计时器")]
	public float lostPlayerFocusTimer = 0.5f;
	private float m_lostPlayerFocusDuration = 0.0f;
	public bool IsLostPlayerFocus => m_lostPlayerFocusDuration <= 0.0f;
	#endregion

	#region 状态
	/// <summary>
	/// 移动状态
	/// </summary>
	public bool IsWalk
    {
        get { return m_isWalk; }
        set { m_isWalk = value; }
    }
	protected bool m_isWalk = false;

	/// <summary>
    /// 追击状态
    /// </summary>
	public bool IsChase
	{
		get { return m_isChase; }
		set { m_isChase = value; }
	}
	private bool m_isChase = false;

	/// <summary>
    /// 等待状态
    /// </summary>
	public bool IsWait
    {
		get { return m_isWait; }
		set { m_isWait = value; }
    }
	protected bool m_isWait = false;

	/// <summary>
	/// 受击状态
	/// </summary>
	public bool IsHurt
	{
		get { return m_isHurt; }
		set { m_isHurt = value; }
	}
	protected bool m_isHurt = false;

	/// <summary>
	/// 死亡状态
	/// </summary>
	public bool IsDead => m_isDead;
	protected bool m_isDead = false;
	#endregion

	#region 事件
	[Header("事件")]
	/// <summary>
	/// 血量变化事件
	/// </summary>
	public GameEventFloat OnHealthChange;
	/// <summary>
	/// 受伤事件
	/// </summary>
	public GameEventTransform OnTakeDamage;
	/// <summary>
	/// 死亡事件
	/// </summary>
	public GameEventVoid OnDeath;
	#endregion

	#region 组件
	protected Rigidbody2D m_rigidBody = null;
	protected PhysicsCheck m_check = null;
	#endregion

	#region Unity 生命周期函数
	protected virtual void Awake()
	{
		//初始化组件
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_check = GetComponent<PhysicsCheck>();
		//初始化状态机
		m_stateMachine = new EnemyStateMachine(this, GetComponent<EnemyAnimationController>());
		//初始化计时器
		m_waitDuration = waitTimer;
		m_lostPlayerFocusDuration = lostPlayerFocusTimer;
	}

	protected virtual void Update()
	{
		//更新朝向
		m_faceDir = new Vector3(-transform.localScale.x, 0, 0);
		//更新状态机
		m_stateMachine.Update();
		//撞墙等待
		if (m_isWait)
		{
			WaitTimer();
		}
		//丢失玩家焦点计时
		LostPlayerFocusTimer();
		//无敌计时
		InvincibilityTimer();
	}

	protected virtual void FixedUpdate()
	{
		m_stateMachine.FixedUpdate();
	}

	private void OnEnable()
	{
		OnTakeDamage.Subscribe(TakeDamage);
		OnDeath.Subscribe(Death);
	}

	private void OnDisable()
	{
		OnTakeDamage.UnSubscribe(TakeDamage);
		OnDeath.Unsubscribe(Death);
	}
	#endregion

	#region 公共接口
	/// <summary>
	/// 设置速度
	/// </summary>
	/// <param name="velocity"></param>
	public void SetVelocity(Vector2 velocity) => m_rigidBody.velocity = velocity;
	/// <summary>
	/// 获取速度
	/// </summary>
	public Vector2 GetVelocity => m_rigidBody.velocity;

	public bool IsOnGround => m_check.isOnGround;

	public bool TouchLeft => m_check.isTouchLeft;

	public bool TouchRight => m_check.isTouchRight;

	public bool FindPlayer()
	{
		return Physics2D.BoxCast(transform.position + (Vector3)checkBoxOffset, checkBoxSize, 0, m_faceDir, detectionDistance, checkLayer);
	}
	
	/// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="attacker">攻击者</param>
    /// <param name="damage">伤害值</param>
	public void GetHurt(Transform attacker, float damage)
	{
		if (!IsInvincible)
		{
			//触发受伤
			OnTakeDamage.Raise(attacker);
			//计算血量
			m_enemy.TakeDamage(damage);
			Debug.Log("Enemy Health = " + m_enemy.Health);
			if (m_enemy.Health <= 0)
			{
				//触发死亡
				OnDeath.Raise();
			}
			else
			{
				//触发无敌
				m_invincibilityDuration = invincibilityTimer;
			}
		}
    }
	#endregion

	/// <summary>
	/// 初始化状态机
	/// </summary>
	protected abstract void InitStateMachine();

	/// <summary>
	/// 受击
	/// </summary>
	/// <param name="transform"></param>
	protected abstract void TakeDamage(Transform transform);

	/// <summary>
	/// 死亡
	/// </summary>
	protected void Death()
	{
		m_isDead = true;
		DestroyObject();
	}

	/// <summary>
	/// 销毁对象
	/// </summary>
	protected void DestroyObject()
	{
		Destroy(this.gameObject);
	}

	private Transform GetParentByTag(Transform child, string tag)
	{
		Transform transform = child.parent;
		while (transform != null)
		{
			if (transform.CompareTag(tag))
			{
				return transform;
			}
			transform = transform.parent;
		}
		return null;
	}

	#region Timer
	/// <summary>
	/// 等待计时器
	/// </summary>
	/// <returns></returns>
	private void WaitTimer()
	{
		if (m_isWait)
		{
			m_waitDuration -= Time.deltaTime;
			if (m_waitDuration <= 0.0f)
			{
				//翻转
				transform.localScale = new Vector3(m_faceDir.x, 1, 1);
				//重置状态
				m_isWait = false;
				//重置计时器
				m_waitDuration = waitTimer;
				//切换状态
				m_stateMachine.SwitchState(EnemyStateEnum.Patrol);
			}
        }
	}

	/// <summary>
	/// 无敌计时器
	/// </summary>
	private void InvincibilityTimer()
	{
		if (IsInvincible)
		{
			if (m_invincibilityDuration > 0.0f)
			{
				m_invincibilityDuration -= Time.deltaTime;
			}
		}
	}
	
	/// <summary>
    /// 丢失玩家焦点计时器
    /// </summary>
	private void LostPlayerFocusTimer()
    {
		if (!FindPlayer() && m_lostPlayerFocusDuration > 0.0f)
		{
			m_lostPlayerFocusDuration -= Time.deltaTime;
		}
		else if (FindPlayer())
		{
			//重置计时器
			m_lostPlayerFocusDuration = lostPlayerFocusTimer;
        }
	}
	#endregion
}
