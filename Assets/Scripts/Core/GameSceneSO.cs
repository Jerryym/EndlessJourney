using Scene.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Game Scene/GameScene SO")]
public class GameSceneSO : ScriptableObject
{
	/// <summary>
	/// 场景类型
	/// </summary>
	public SceneType sceneType = SceneType.Location;
	/// <summary>
	/// 场景资源引用
	/// </summary>
	public AssetReference sceneRef;
	/// <summary>
	/// 初始位置
	/// </summary>
	public Vector3 initialPos;
}
