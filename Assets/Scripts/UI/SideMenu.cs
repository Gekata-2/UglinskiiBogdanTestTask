using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class SideMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menu;

        private PlayerInputActions _input;
        private bool _isActive;

        private void Awake()
        {
            _input = new PlayerInputActions();
            _input.FreeMovement.Enable();
        }
        
        private void Start()
        {
            _input.FreeMovement.SideMenu.performed += OnSideMenu;
        }

        private void OnSideMenu(InputAction.CallbackContext obj)
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
        }
        
    }
}