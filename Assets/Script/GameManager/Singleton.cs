using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool isShuttingDown = false;

    public static T Instance
    {
        get
        {
            if (isShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                 "' already destroyed. Returning null.");
                return null;
            }

            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드될 때 이벤트 등록
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // 씬이 로드될 때 자동으로 싱글톤 오브젝트를 파괴하는 메소드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lobby") // 특정 씬에서만 파괴하고 싶다면 이 조건을 사용
        {
            Destroy(gameObject); // 싱글톤 오브젝트 파괴
        }
    }

    protected virtual void OnApplicationQuit()
    {
        isShuttingDown = true;
    }
}