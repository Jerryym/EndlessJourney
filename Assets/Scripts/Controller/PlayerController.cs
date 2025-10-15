using FSM.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerInputControl))]
[RequireComponent(typeof(PhysicsCheck))]
public class PlayerController : MonoBehaviour
{
	#region === 玩家数据 ===
	/// <summary>
	/// 输入方向
	/// </summary>
	public Vector2 inputDirection;

	[SerializeField]
	private PlayerModel m_player = new PlayerModel();
	public PlayerModel Player => m_player;
	#endregion

	#region === 跳跃 ===
	[Header("跳跃")]
	public float jumpForce = 12f;
	
	/// <summary>
	/// 最大跳跃次数
	/// </summary>
	public int maxJumpCount = 2;
	
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

	#region === 无敌 ===
	[Header("无敌")]
	/// <summary>
	/// 无敌时间
	/// </summary>
	public float invincibilityTimer = 2f;
	
	/// <summary>
	/// 当前无敌剩余时间
	/// </summary>
	private float m_invincibilityDuration = 0.0f;
	
	/// <summary>
	/// 无敌状态
	/// </summary>
	public bool IsInvincible => m_invincibilityDuration > 0f;
	#endregion

	#region === 物理材质 ===
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

	#region === 输入与状态 ===
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

	//碰撞体尺寸信息
	private Vector2 m_coll2DSize;
	private Vector2 m_coll2DOffset;
	#endregion

	#region === 组件 ===
	private Rigidbody2D m_rigidBody = null;
	private CapsuleCollider2D m_collider2D = null;
	private PhysicsCheck m_check = null;
	#endregion

	#region === Unity 生命周期函数 ===
	private void Awake()
	{
		//初始化组件
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_collider2D = GetComponent<CapsuleCollider2D>();
		m_check = GetComponent<PhysicsCheck>();

		m_coll2DSize = m_collider2D.size;
		m_coll2DOffset = m_collider2D.offset;

		//初始化PlayerModel数据
		m_player.Init();

		//初始化状态机
		InitStateMachine();

		//输入控制
		m_inputActions = new PlayerInputControl();
		m_inputActions.Gameplay.Jump.started += Jump;
	}

	private void OnEnable()
	{
		//启动输入控制
		m_inputActions?.Enable();
	}

	private void OnDisable()
	{
		//禁用输入控制
		m_inputActions?.Disable();
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
	#endregion

	#region === 公共接口 ===
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
			m_rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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

	/// <summary>
	/// 启动无敌计时
	/// </summary>
	public void StartInvincibility() => m_invincibilityDuration = invincibilityTimer;
	#endregion

	#region === 私有方法 ===
	/// <summary>
	/// 初始化状态机
	/// </summary>
	private void InitStateMachine()
	{
		m_stateMachine = new PlayerStateMachine(this);
		//Idle
		m_stateMachine.AddState(PlayerStateEnum.Idle, new PlayerIdleState(m_stateMachine));
		//Move
		m_stateMachine.AddState(PlayerStateEnum.Move, new PlayerMoveState(m_stateMachine));
		//Jump
		m_stateMachine.AddState(PlayerStateEnum.Jump, new PlayerJumpState(m_stateMachine));
		//Squat
		m_stateMachine.AddState(PlayerStateEnum.Squat, new PlayerSquatState(m_stateMachine));
		//Attack

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
		if (m_invincibilityDuration > 0.0f)
		{
			m_invincibilityDuration -= Time.deltaTime;
		}
	}
	#endregion

	#region === 事件函数 ===
	private void Jump(InputAction.CallbackContext context)
	{
		m_isJump = true;
	}
	#endregion
}
