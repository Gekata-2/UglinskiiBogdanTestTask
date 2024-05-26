using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shared
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        public void LoadSceneAsync(string sceneName)
        {
            StartCoroutine(LoadSceneAsyncRoutine(sceneName));
        }
        private IEnumerator LoadSceneAsyncRoutine(string sceneName)
        {
            AsyncOperation loadLoading = SceneManager.LoadSceneAsync(sceneName);
            while (!loadLoading.isDone)
            {
                yield return null;
            }
        }
    }
}
