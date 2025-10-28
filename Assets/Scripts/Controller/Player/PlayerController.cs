using FSM.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerInputControl))]
[RequireComponent(typeof(PhysicsCheck))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerController : MonoBehaviour
{
	#region 玩家数据
	/// <summary>
	/// 输入方向
	/// </summary>
	public Vector2 inputDirection;

	[SerializeField]
	private PlayerModel m_player = new PlayerModel();
	public PlayerModel Player => m_player;
	#endregion

	#region 跳跃状态
	/// <summary>
	/// 跳跃状态
	/// </summary>
	public bool IsJump
	{
		get { return m_isJump; }
		set { m_isJump = value; }
	}
	private bool m_isJump = false;
	#endregion

	#region 滑铲状态
	/// <summary>
	/// 滑铲状态
	/// </summary>
	public bool IsSlide
	{
		get { return m_isSlide; }
		set { m_isSlide = value; }
	}
	private bool m_isSlide = false;
	#endregion

	#region 攻击状态
	public bool IsAttack
	{
		get { return m_isAttack; }
		set { m_isAttack = value; }
	}
	private bool m_isAttack = false;
	#endregion

	#region 无敌
	[Header("无敌")]
	[Tooltip("无敌计时器")]
	public float invincibilityTimer = 2f;
	private float m_invincibilityDuration = 0.0f;
	/// <summary>
	/// 无敌状态
	/// </summary>
	public bool IsInvincible => m_invincibilityDuration > 0f;
	#endregion

	#region 物理材质
	[Header("物理材质")]
	/// <summary>
	/// 光滑物理材质
	/// </summary>
	public PhysicsMaterial2D smoothMat;
	/// <summary>
	/// 粗糙物理材质
	/// </summary>
	public PhysicsMaterial2D roughMat;
	#endregion

	#region 输入与状态
	private PlayerInputControl m_inputActions = null;
	private PlayerStateMachine m_stateMachine = null;

	/// <summary>
	/// 行走状态
	/// </summary>
	public bool IsWalkingMode => m_inputActions.Gameplay.Walk.IsPressed();
	
	/// <summary>
	/// 奔跑状态
	/// </summary>
	public bool IsRunningMode => !IsWalkingMode;
	
	/// <summary>
	/// 下蹲状态
	/// </summary>
	public bool IsSquat => m_inputActions.Gameplay.Squat.IsPressed();

	/// <summary>
	/// 受击状态
	/// </summary>
	public bool IsHurt
	{
		get { return m_isHurt; }
		set { m_isHurt = value; }
	}
	private bool m_isHurt = false;

	/// <summary>
	/// 死亡状态
	/// </summary>
	public bool IsDead => m_isDead;
	private bool m_isDead = false;
	#endregion

	#region 事件
	[Header("事件")]
	/// <summary>
	/// 血量变化事件
	/// </summary>
	public GameEventFloat OnHealthChange;
	/// <summary>
	/// 体力变化事件
	/// </summary>
	public GameEventFloat OnPowerChange;
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
	private Rigidbody2D m_rigidBody = null;
	private CapsuleCollider2D m_collider2D = null;
	private PhysicsCheck m_check = null;

	//碰撞体尺寸信息
	private Vector2 m_coll2DSize;
	private Vector2 m_coll2DOffset;
	#endregion

	#region Unity 生命周期函数
	private void Awake()
	{
		//初始化组件
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_collider2D = GetComponent<CapsuleCollider2D>();
		m_check = GetComponent<PhysicsCheck>();
		m_coll2DSize = m_collider2D.size;
		m_coll2DOffset = m_collider2D.offset;
		//初始化状态机
		InitStateMachine();
		//输入控制
		m_inputActions = new PlayerInputControl();
		m_inputActions.Gameplay.Jump.started += Jump;
		m_inputActions.Gameplay.Slide.started += Slide;
		m_inputActions.Gameplay.Attack.started += Attack;
	}

	private void OnEnable()
	{
		//启动输入控制
		m_inputActions?.Enable();
		//事件注册
		OnTakeDamage.Subscribe(TakeDamage);
		OnDeath.Subscribe(Death);
	}

	private void OnDisable()
	{
		//禁用输入控制
		m_inputActions?.Disable();
		//取消事件注册
		OnTakeDamage.UnSubscribe(TakeDamage);
		OnDeath.Unsubscribe(Death);
	}

	private void Update()
	{
		//读取输入方向
		inputDirection = m_inputActions.Gameplay.Move.ReadValue<Vector2>();
		//修改物理材质
		ModifyPhysicMat();
		//状态机更新
		m_stateMachine.Update();
		//无敌计时
		InvincibilityTimer();
	}

	private void FixedUpdate()
	{
		m_stateMachine.FixedUpdate();
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("River"))//落入河中，触发死亡
		{
			//触发血量变化事件
			m_player.Health = 0;
			OnHealthChange.Raise(m_player.Health / m_player.playerBasic.MaxHealth);
			//触发死亡事件
			OnDeath.Raise();
		}
		else if (other.CompareTag("Enemy"))//敌人，触发受伤
		{
			if (IsInvincible)//无敌状态，不触发受伤
			{
				return;
			}

			var enemyController = other.GetComponent<EnemyController>();
			//触发受伤事件
			OnTakeDamage.Raise(other.transform);
			//计算血量
			m_player.TakeDamage(enemyController.Enemy.enemyBasic.Attack);
			//触发血量变化事件
			OnHealthChange.Raise(m_player.Health / m_player.playerBasic.MaxHealth);
			if (m_player.Health <= 0)
			{
				//触发死亡事件
				OnDeath.Raise();
			}
            else
			{
				//进入无敌状态
				m_invincibilityDuration = invincibilityTimer;
            }
		}
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

	public void MovePosition(Vector2 newPos) => m_rigidBody.MovePosition(newPos);

	public bool IsOnGround => m_check.isOnGround;

	public bool TouchLeft => m_check.isTouchLeft;

	public bool TouchRight => m_check.isTouchRight;

	/// <summary>
	/// 翻转
	/// </summary>
	public void Flip()
	{
		if (inputDirection.x < -0.01f)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else if (inputDirection.x > 0.01f)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
	}

	/// <summary>
	/// 设置跳跃力
	/// </summary>
	public void SetJumpForce()
	{
		if (m_isJump)
		{
			m_rigidBody.AddForce(transform.up * m_player.playerMovement.JumpForce, ForceMode2D.Impulse);
		}
	}

	/// <summary>
	/// 修改碰撞体尺寸
	/// </summary>
	public void ModifyColliderSize()
	{
		m_collider2D.size = IsSquat ? new Vector2(0.7f, 1.7f) : m_coll2DSize;
		m_collider2D.offset = IsSquat ? new Vector2(-0.05f, 0.85f) : m_coll2DOffset;
	}

	public void ConsumePower()
	{
		if (m_isSlide)
		{
			m_player.ConsumePower(m_player.playerMovement.SlideCost);
		}

		//触发体力事件
		float powerPercent = m_player.Power / m_player.playerBasic.MaxPower;
		OnPowerChange.Raise(powerPercent);
	}
	#endregion

	#region 私有方法
	/// <summary>
	/// 初始化状态机
	/// </summary>
	private void InitStateMachine()
	{
		m_stateMachine = new PlayerStateMachine(this, GetComponent<PlayerAnimationController>());
		//Idle
		m_stateMachine.AddState(PlayerStateEnum.Idle, new PlayerIdleState(m_stateMachine));
		//Move
		m_stateMachine.AddState(PlayerStateEnum.Move, new PlayerMoveState(m_stateMachine));
		//Jump
		m_stateMachine.AddState(PlayerStateEnum.Jump, new PlayerJumpState(m_stateMachine));
		//Squat
		m_stateMachine.AddState(PlayerStateEnum.Squat, new PlayerSquatState(m_stateMachine));
		//Slide
		m_stateMachine.AddState(PlayerStateEnum.Slide, new PlayerSlideState(m_stateMachine));
		//Attack
		m_stateMachine.AddState(PlayerStateEnum.Attack, new PlayerAttackState(m_stateMachine));
		//Hurt
		m_stateMachine.AddState(PlayerStateEnum.Hurt, new PlayerHurtState(m_stateMachine));

		//初始化默认状态为Idle
		m_stateMachine.SwitchState(PlayerStateEnum.Idle);
	}
	
	/// <summary>
	/// 修改物理材质
	/// </summary>
	private void ModifyPhysicMat()
	{
		m_collider2D.sharedMaterial = m_check.isOnGround ? roughMat : smoothMat;
	}

	/// <summary>
	/// 无敌计时器
	/// </summary>
	private void InvincibilityTimer()
	{
		if (IsInvincible)
        {
			Debug.Log("无敌: " + m_invincibilityDuration);
			if (m_invincibilityDuration > 0.0f)
			{
				m_invincibilityDuration -= Time.deltaTime;
			}
        }
	}

	/// <summary>
	/// 受击
	/// </summary>
	private void TakeDamage(Transform attacker)
	{
		m_stateMachine.SwitchState(PlayerStateEnum.Hurt);

		//受击后退
		Vector2 dirVec = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
		m_rigidBody.AddForce(dirVec * m_player.HurtForce, ForceMode2D.Impulse);
	}

	/// <summary>
	/// 死亡
	/// </summary>
	private void Death()
	{
		m_isDead = true;
		//禁用碰撞
		gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		//禁用输入控制
		m_inputActions.Gameplay.Disable();
	}
	#endregion

	#region 事件函数
	private void Jump(InputAction.CallbackContext context)
	{
		m_isJump = true;
	}

	private void Slide(InputAction.CallbackContext obj)
	{
		if (m_check.isOnGround && !m_isSlide)
		{
			if (m_player.CanSlide() != true)
			{
				return;
			}
			m_isSlide = true;
		}
	}

	private void Attack(InputAction.CallbackContext obj)
	{
		m_isAttack = true;
	}
	#endregion
}
