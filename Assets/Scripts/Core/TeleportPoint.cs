using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
	/// <summary>
	/// 目标位置
	/// </summary>
	public Vector3 targetPosition;

	public void TriggerAction()
	{
		Debug.Log("Teleport");
	}

}
