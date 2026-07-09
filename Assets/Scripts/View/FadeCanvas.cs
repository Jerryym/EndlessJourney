using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
	public Image fadeImage;

	[Header("监听")]
	public FadeEventSO fadeEventListener;

	private void OnEnable()
	{
		fadeEventListener.FadeEvent += OnFadeEvent;
	}

	private void OnDisable()
	{
		fadeEventListener.FadeEvent -= OnFadeEvent;
	}

	private void OnFadeEvent(Color targetColor, float duration, bool isFadeIn)
	{
		fadeImage.DOBlendableColor(targetColor, duration);
	}
}
