using System;
using UnityEngine;
using PlayerInput = Player.PlayerInput;

namespace UI
{
    public class SideMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private PlayerInput playerInput;

        public event Action<bool> OnSideMenuSetActive;

        private bool _isActive;

        private void Start()
        {
            playerInput.onSideMenuOpen += OnSideMenu;
        }

        private void OnDestroy()
        {
            playerInput.onSideMenuOpen -= OnSideMenu;
        }

        public bool IsActive => _isActive;

        private void OnSideMenu()
        {
            switch (_isActive)
            {
                case true:
                    Hide();
                    break;
                case false:
                    Show();
                    break;
            }

            OnSideMenuSetActive?.Invoke(_isActive);
        }

        private void Show()
        {
            _isActive = true;
            menu.SetActive(true);
        }

        private void Hide()
        {
            _isActive = false;
            menu.SetActive(false);
        }
    }
}