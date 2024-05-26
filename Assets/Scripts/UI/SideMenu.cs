using System;
using UnityEngine;
using UnityEngine.UI;
using PlayerInput = Player.PlayerInput;

namespace UI
{
    public class SideMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Button spawnObject;
        public event Action onSpawnObject;
        public event Action<bool> OnSideMenuSetActive;
        private bool _isActive;

        private void Start()
        {
            playerInput.onSideMenuOpen += OnSideMenu;
            spawnObject.onClick.AddListener(OnSpawnObject);
        }

        private void OnSpawnObject()
        {
            onSpawnObject?.Invoke();
        }

        private void OnSideMenu()
        {
            switch (_isActive)
            {
                case true:
                    _isActive = false;
                    menu.SetActive(false);
                    break;
                case false:
                    _isActive = true;
                    menu.SetActive(true);
                    break;
            }

            OnSideMenuSetActive?.Invoke(_isActive);
        }
    }
}