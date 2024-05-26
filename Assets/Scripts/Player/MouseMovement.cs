using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class MouseMovement : MonoBehaviour
    {
        [SerializeField] private float xSensitivity;
        [SerializeField] private float ySensitivity;
        public event Action<float> OnHorizontalMove;

        private PlayerInputActions _input;
        private float _xRotation;
        private bool _canMove = true;


        private void Awake()
        {
            _input = new PlayerInputActions();
        }


        // Start is called before the first frame update
        private void Start()
        {
            _input.FreeMovement.Enable();
            _input.FreeMovement.SideMenu.performed += OnSideMenuOpen;
            LockCursor();
        }

        private void OnSideMenuOpen(InputAction.CallbackContext obj)
        {
            switch (_canMove)
            {
                case true:
                    _canMove = false;
                    UnlockCursor();
                    break;
                case false:
                    _canMove = true;
                    LockCursor();
                    break;
            }
        }

        private Vector2 MouseDelta => _input.FreeMovement.MouseMovement.ReadValue<Vector2>();

        // Update is called once per frame
        private void Update()
        {
            if (!_canMove)
                return;

            Vector2 mouseInput = MouseDelta;
            mouseInput.x = mouseInput.x * Time.deltaTime * xSensitivity;
            mouseInput.y = mouseInput.y * Time.deltaTime * ySensitivity;

            _xRotation -= mouseInput.y;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            if (Mathf.Abs(mouseInput.x) > 0.001f)
            {
                OnHorizontalMove?.Invoke(mouseInput.x);
            }
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}