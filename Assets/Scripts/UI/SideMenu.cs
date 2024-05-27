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

        public event Action<bool> OnSideMenuSetActive;
        private bool _isActive;
        public bool IsActive => _isActive;

        private void Start()
        {
            playerInput.onSideMenuOpen += OnSideMenu;
        }

        public void Show()
        {
            _isActive = true;
            menu.SetActive(true);
        }

        public void Hide()
        {
            _isActive = false;
            menu.SetActive(false);
        }

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
    }
}