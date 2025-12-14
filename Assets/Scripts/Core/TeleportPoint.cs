using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
	/// <summary>
	/// 场景加载事件
	/// </summary>
	public SceneLoadEventSO sceneLoadEvent;
	/// <summary>
	/// 目标场景
	/// </summary>
	public GameSceneSO targetScene;
	/// <summary>
	/// 目标位置
	/// </summary>
	public Vector3 targetPosition;

	public void TriggerAction()
	{
		Debug.Log("Teleport");
		sceneLoadEvent.SceneLoadRequest(targetScene, targetPosition, true);
	}

}
