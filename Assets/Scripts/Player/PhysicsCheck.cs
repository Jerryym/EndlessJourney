using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
	/// <summary>
	/// 半径
	/// </summary>
	public float radius = 1f;
	/// <summary>
	/// 图层
	/// </summary>
	public LayerMask layer;

	[Header("状态")]
	public bool isOnGround;

	private void Update()
	{
		//状态检测
		StatusCheck();
	}

	private void OnDrawGizmosSelected()
	{
		//绘制碰撞地面检测圆
		Gizmos.DrawWireSphere(transform.position, radius);
	}

	/// <summary>
	/// 状态检测
	/// </summary>
	private void StatusCheck()
	{
		isOnGround = Physics2D.OverlapCircle(transform.localPosition, radius, layer);
	}
}
