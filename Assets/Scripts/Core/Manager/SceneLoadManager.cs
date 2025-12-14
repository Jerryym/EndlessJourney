using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
	/// <summary>
	/// 第一次加载场景对象
	/// </summary>
	public GameSceneSO firstLoadScene;
	/// <summary>
	/// 场景加载事件
	/// </summary>
	public SceneLoadEventSO loadSceneEvent;
	/// <summary>
	/// 渐入渐出动画时间
	/// </summary>
	public float fadeDuration;

	private GameSceneSO m_currentScene;
	private GameSceneSO m_targetScene;
	private Vector3 m_targetPost;
	private bool m_isFadeScreen = false;

	private void Awake()
	{
		m_currentScene = firstLoadScene;
		LoadScene(m_currentScene);
	}

	private void OnEnable()
	{
		loadSceneEvent.LoadRequestEvent += OnLoadRequestEvent;
	}

	private void OnDisable()
	{
		loadSceneEvent.LoadRequestEvent -= OnLoadRequestEvent;
	}

	private void OnLoadRequestEvent(GameSceneSO gameScene, Vector3 position, bool isFadeScreen)
	{
		m_targetScene = gameScene;
		m_targetPost = position;
		m_isFadeScreen = isFadeScreen;

		Debug.Log("Target Scene: " + gameScene.name);

		//卸载当前场景
		if (m_currentScene)
		{
			StartCoroutine(UnLoadScene());
		}
	}

	/// <summary>
	/// 加载场景
	/// </summary>
	/// <param name="targetScene"></param>
	private void LoadScene(GameSceneSO targetScene)
	{
		targetScene.sceneRef.LoadSceneAsync(LoadSceneMode.Additive, m_isFadeScreen);
	}

	private IEnumerator UnLoadScene()
	{
		//启用渐入渐出
		if (m_isFadeScreen)
		{

		}
		yield return new WaitForSeconds(fadeDuration);

		//卸载当前场景
		yield return m_currentScene.sceneRef.UnLoadScene();

		//加载新场景
		LoadScene(m_targetScene);
	}
}
