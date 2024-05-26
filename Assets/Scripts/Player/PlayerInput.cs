using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action onInspect;
        public event Action onCancel;
        public event Action onSideMenuOpen;
        public event Action onQuickMenuOpen;
        private PlayerInputActions _inputActions;


        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }


        private void Start()
        {
            EnableFreeMovementActions();

            _inputActions.FreeMovement.Inspect.performed += OnInspect;
            _inputActions.FreeMovement.Menu.performed += OnQuickMenuOpen;

            _inputActions.Inspection.Cancel.performed += OnCancel;

            _inputActions.UI.Enable();
            _inputActions.UI.SideMenu.performed += OnSideMenuOpen;
        }

        private void OnSideMenuOpen(InputAction.CallbackContext obj)
        {
            onSideMenuOpen?.Invoke();
        }

        private void OnQuickMenuOpen(InputAction.CallbackContext obj)
        {
            onQuickMenuOpen?.Invoke();
        }

        private void OnCancel(InputAction.CallbackContext obj)
        {
            onCancel?.Invoke();
        }

        private void OnInspect(InputAction.CallbackContext obj)
        {
            onInspect?.Invoke();
        }

        public Vector3 MoveInput => _inputActions.FreeMovement.Movement.ReadValue<Vector3>();
        public Vector2 MouseDelta => _inputActions.FreeMovement.MouseMovement.ReadValue<Vector2>();
        public Vector2 RotateAroundObjInput => _inputActions.Inspection.Rotation.ReadValue<Vector2>();

        public float ScrollInput()
        {
            float input = _inputActions.Inspection.Zoom.ReadValue<Vector2>().y;

            if (input >= 0.01f)
                return 1;
            if (input <= -0.01f)
                return -1;
            else
                return 0;
        }

        public void EnableInspectionActions()
        {
            _inputActions.Inspection.Enable();
            _inputActions.FreeMovement.Disable();
        }

        public void EnableFreeMovementActions()
        {
            _inputActions.Inspection.Disable();
            _inputActions.FreeMovement.Enable();
        }
    }
}