using FSM.Enums;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputControl), typeof(PhysicsCheck))]
public class PlayerController : MonoBehaviour
{
	/// <summary>
	/// 输入方向
	/// </summary>
	public Vector2 inputDirction;

	public bool IsWalkingMode => m_inputActions.Gameplay.Walk.IsPressed();
	public bool IsRunningMode => !IsWalkingMode;

	[SerializeField]
	private PlayerModel m_player = new PlayerModel();
	public PlayerModel Player => m_player;

	[Header("无敌")]
	#region 无敌
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

	[Header("物理材质")]
	#region 物理材质
	/// <summary>
	/// 光滑物理材质
	/// </summary>
	public PhysicsMaterial2D smoothMat;
	/// <summary>
	/// 粗糙物理材质
	/// </summary>
	public PhysicsMaterial2D roughMat;
	#endregion

	#region 组件
	private Rigidbody2D m_rigidBody = null;
	private CapsuleCollider2D m_collider2D = null;
	private PhysicsCheck m_check = null;
	#endregion

	private PlayerInputControl m_inputActions = null;
	private PlayerStateMachine m_stateMachine = null;

	#region Unity消息
	private void Awake()
	{
		//初始化组件
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_check = GetComponent<PhysicsCheck>();

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
		inputDirction = m_inputActions.Gameplay.Move.ReadValue<Vector2>();
		//修改物理材质
		ModifyPhysicMat();
		//状态机更新
		m_stateMachine.Update();
	}

	private void FixedUpdate()
	{
		m_stateMachine.FixedUpdate();
	}
	#endregion

	/// <summary>
	/// 设置速度
	/// </summary>
	/// <param name="velocity"></param>
	public void SetVelocity(Vector2 velocity)
	{
		m_rigidBody.velocity = velocity;
	}

	/// <summary>
	/// 获取当前速度
	/// </summary>
	public Vector2 GetVelocity => m_rigidBody.velocity;

	/// <summary>
	/// 启动无敌计时
	/// </summary>
	public void StartInvincibility()
	{
		m_invincibilityDuration = invincibilityTimer;
	}

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

		//Attack

		//初始化默认状态为Idle
		m_stateMachine.SwitchState(PlayerStateEnum.Idle);
	}

	#region 私有方法
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

	#region 事件函数
	private void Jump(InputAction.CallbackContext context)
	{
	}
	#endregion
}
