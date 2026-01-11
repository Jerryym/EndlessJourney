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
	/// 渐入渐出动画时间
	/// </summary>
	public float fadeDuration;

	[Header("事件广播")]
	/// <summary>
	/// 场景加载事件
	/// </summary>
	public SceneLoadEventSO loadSceneEvent;
	/// <summary>
	/// 场景加载完成后事件
	/// </summary>
	public VoidGameEventSO afterLoadSceneEvent;
	/// <summary>
	/// 场景渐入渐出事件
	/// </summary>
	public FadeEventSO fadeEvent;

	/// <summary>
	/// 当前场景
	/// </summary>
	private GameSceneSO m_currentScene;
	/// <summary>
	/// 目标场景
	/// </summary>
	private GameSceneSO m_targetScene;
	/// <summary>
	/// 目标位置
	/// </summary>
	private Vector3 m_targetPos;
	/// <summary>
	/// 是否渐入渐出
	/// </summary>
	private bool m_isFadeScreen = false;
	/// <summary>
	/// 是否正在加载场景
	/// </summary>
	private bool m_isLoading = false;

	private void Awake()
	{
		StartNewGame();
	}

	private void OnEnable()
	{
		loadSceneEvent.LoadRequestEvent += OnLoadRequestEvent;
	}

	private void OnDisable()
	{
		loadSceneEvent.LoadRequestEvent -= OnLoadRequestEvent;
	}
	
	private void StartNewGame()
	{
		//加载第一个场景
		OnLoadRequestEvent(firstLoadScene, firstLoadScene.initialPos, true);
	}

	private void OnLoadRequestEvent(GameSceneSO gameScene, Vector3 position, bool isFadeScreen)
	{
		if (m_isLoading)
			return;

		m_isLoading = true;
		m_targetScene = gameScene;
		m_targetPos = position;
		m_isFadeScreen = isFadeScreen;

		Debug.Log("Target Scene: " + gameScene.name);

		//卸载当前场景
		if (m_currentScene)
		{
			StartCoroutine(UnLoadScene());
		}
		else
		{
			LoadScene();
		}
	}

	/// <summary>
	/// 加载场景
	/// </summary>
	private void LoadScene()
	{
		var loadOpt = m_targetScene.sceneRef.LoadSceneAsync(LoadSceneMode.Additive);
		loadOpt.Completed += OnLoadCompleted;
	}

	/// <summary>
	/// 场景加载完成
	/// </summary>
	/// <param name="obj"></param>
	private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
	{
		if (m_currentScene != m_targetScene)
		{
			m_currentScene = m_targetScene;
			playerTrans.position = m_targetPos;
			playerTrans.gameObject.SetActive(true);
			if (m_isFadeScreen)
			{
				//渐出
				fadeEvent.FadeOut(fadeDuration);
			}

			m_isLoading = false;

			//广播场景加载完成事件
			afterLoadSceneEvent.Raise();
		}
	}

	private IEnumerator UnLoadScene()
	{
		//启用渐入
		if (m_isFadeScreen)
		{
			fadeEvent.FadeIn(fadeDuration);
		}
		yield return new WaitForSeconds(fadeDuration);
		
		//卸载当前场景
		yield return m_currentScene.sceneRef.UnLoadScene();

		//加载新场景
		playerTrans.gameObject.SetActive(false);
		LoadScene();
	}
}
