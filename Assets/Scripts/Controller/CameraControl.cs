using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
	public CinemachineImpulseSource impulseSource = null;
	private CinemachineConfiner2D m_confiner2D = null;

	public VoidEventSO cameraShakeEvent;

	private void Awake()
	{
		m_confiner2D = GetComponent<CinemachineConfiner2D>();
	}

	private void Start()
	{
		GetCameraBounds();
	}

	private void OnEnable()
	{
		cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
	}

	private void OnDisable()
	{
		cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
	}

	private void OnCameraShakeEvent()
	{
		impulseSource.GenerateImpulse();
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
