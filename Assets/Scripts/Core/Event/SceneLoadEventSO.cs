using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoad Event")]
public class SceneLoadEventSO : GameEventSO
{
	public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;

	/// <summary>
	/// 场景加载请求
	/// </summary>
	/// <param name="gameScene">目标加载场景</param>
	/// <param name="position">目标场景中Player的位置</param>
	/// <param name="isFadeScreen">是否渐入渐出</param>
	public void SceneLoadRequest(GameSceneSO gameScene, Vector3 position, bool isFadeScreen)
	{
		LoadRequestEvent?.Invoke(gameScene, position, isFadeScreen);
	}
}
