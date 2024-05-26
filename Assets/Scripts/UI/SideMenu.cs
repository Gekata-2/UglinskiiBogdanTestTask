using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
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