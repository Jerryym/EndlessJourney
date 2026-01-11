using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEvent SO")]
public class FadeEventSO : GameEventSO
{
	public UnityAction<Color, float, bool> FadeEvent;

	/// <summary>
	/// 渐入
	/// </summary>
	/// <param name="duration"></param>
	public void FadeIn(float duration)
	{
		Raise(Color.black, duration, true);
	}

	/// <summary>
	/// 渐出
	/// </summary>
	/// <param name="duration"></param>
	public void FadeOut(float duration)
	{
		Raise(Color.clear, duration, false);
	}

	public void Raise(Color targetColor, float duration, bool isFadeIn)
	{
		FadeEvent?.Invoke(targetColor, duration, isFadeIn);
	}
}
