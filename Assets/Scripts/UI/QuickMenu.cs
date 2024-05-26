using System;
using Player;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuickMenu : MonoBehaviour
    {
        [SerializeField] private Button resume, exit;
        [SerializeField] private GameObject layout;
        [SerializeField] private PlayerInput playerInput;

        public bool IsActive => layout.activeSelf;
        public event Action onResume;

        private void Start()
        {
            resume.onClick.AddListener(OnResume);
            exit.onClick.AddListener(OnExit);
            playerInput.onQuickMenuOpen += OnPlayerQuickMenuOpen;
        }

        private void OnPlayerQuickMenuOpen()
        {
            if (!layout.activeSelf)
            {
                layout.SetActive(true);
            }
        }

        private void OnResume()
        {
            layout.SetActive(false);
            onResume?.Invoke();
        }

        private void OnExit()
        {
            SceneLoader.Instance.LoadSceneAsync("MainMenu");
        }
    }
}