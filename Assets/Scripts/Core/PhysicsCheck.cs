using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
	[Header("Basic Param")]
	/// <summary>
	/// 底部位移
	/// </summary>
	public Vector2 bottomOffset;
	/// <summary>
	/// 半径
	/// </summary>
	public float radius = 1f;
	/// <summary>
	/// 图层
	/// </summary>
	public LayerMask layer;

	[Header("状态")]
	public bool isOnGround = true;
	public bool isTouchLeft;
	public bool isTouchRight;

	private Vector2 m_leftOffset;
	private Vector2 m_rightOffset;

	#region Unity 消息
	private void Awake()
	{
		CapsuleCollider2D collider2D = GetComponent<CapsuleCollider2D>();
		Vector3 boundSize = collider2D.bounds.size;
		Vector2 offset = collider2D.offset;
		m_leftOffset = new Vector2(-boundSize.x / 2 + offset.x, boundSize.y / 2);
		m_rightOffset = new Vector2(boundSize.x / 2 + offset.x, boundSize.y / 2);
	}

	private void Update()
	{
		//状态检测
		StatusCheck();
	}

	private void OnDrawGizmosSelected()
	{
		//绘制碰撞地面检测圆
		Gizmos.DrawWireSphere(transform.localPosition + new Vector3(bottomOffset.x * transform.localScale.x, bottomOffset.y, 0), radius);

		//绘制左右侧碰撞检测圆
		Gizmos.DrawWireSphere(transform.position + (Vector3)m_leftOffset, radius);
		Gizmos.DrawWireSphere(transform.position + (Vector3)m_rightOffset, radius);
	}
	#endregion

	/// <summary>
	/// 状态检测
	/// </summary>
	private void StatusCheck()
	{
		//地面检测
		isOnGround = Physics2D.OverlapCircle(transform.localPosition + new Vector3(bottomOffset.x * transform.localScale.x, bottomOffset.y, 0), radius, layer);

		//左右侧检测
		isTouchLeft = Physics2D.OverlapCircle(transform.localPosition + (Vector3)m_leftOffset, radius, layer);
		isTouchRight = Physics2D.OverlapCircle(transform.localPosition + (Vector3)m_rightOffset, radius, layer);
	}
}
