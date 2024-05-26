using System.Collections;
using Shared;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button start, exit;

        private void Start()
        {
            start.onClick.AddListener(OnStart);
            exit.onClick.AddListener(OnExit);
        }

        private void OnExit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        private void OnStart()
        {
            SceneLoader.Instance.LoadSceneAsync("MainScene");
        }
    }
}