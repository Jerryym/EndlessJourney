using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
	public CinemachineImpulseSource impulseSource = null;
	private CinemachineConfiner2D m_confiner2D = null;

	[Header("事件监听")]
	/// <summary>
	/// 摄像机震动事件
	/// </summary>
	public VoidGameEventSO cameraShakeEvent;
	/// <summary>
	/// 场景加载完成事件
	/// </summary>
	public VoidGameEventSO afterLoadSceneEvent;

	private void Awake()
	{
		m_confiner2D = GetComponent<CinemachineConfiner2D>();
	}
	
	private void OnEnable()
	{
		cameraShakeEvent.Subscribe(OnCameraShakeEvent);
		afterLoadSceneEvent.Subscribe(OnAfterLoadSceneEvent);
	}

	private void OnDisable()
	{
		cameraShakeEvent.Unsubscribe(OnCameraShakeEvent);
		afterLoadSceneEvent.Unsubscribe(OnAfterLoadSceneEvent);
	}

	private void OnCameraShakeEvent()
	{
		impulseSource.GenerateImpulse();
	}

	private void OnAfterLoadSceneEvent()
	{
		GetCameraBounds();
	}

	/// <summary>
	/// 获取相机边界
	/// </summary>
	private void GetCameraBounds()
	{
		var boundObj = GameObject.FindGameObjectWithTag("Bounds");
		if (boundObj)
		{
			m_confiner2D.m_BoundingShape2D = boundObj.GetComponent<Collider2D>();
			//清理缓存
			m_confiner2D.InvalidateCache();
		}
	}
}
