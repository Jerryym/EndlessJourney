using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
	public Transform playerTrans;
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
	private bool m_isLoading = false;

	private void Awake()
	{
		firstLoadScene.sceneRef.LoadSceneAsync(LoadSceneMode.Additive);
		m_currentScene = firstLoadScene;
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
		if (m_isLoading)
			return;

		m_targetScene = gameScene;
		m_targetPost = position;
		m_isFadeScreen = isFadeScreen;
		m_isLoading = true;

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
		var loadOpt = targetScene.sceneRef.LoadSceneAsync(LoadSceneMode.Additive);
		loadOpt.Completed += OnLoadCompleted;
	}

	private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
	{
		if (m_currentScene != m_targetScene)
		{
			m_currentScene = m_targetScene;
			playerTrans.position = m_targetPost;
			if (m_isFadeScreen)
			{
				//渐出
			}

			m_isLoading = false;
		}
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
