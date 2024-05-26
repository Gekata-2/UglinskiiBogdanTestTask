using System;
using UnityEngine;

namespace Player
{
    public class MouseMovement : MonoBehaviour
    {
        [SerializeField] private float xSensitivity;
        [SerializeField] private float ySensitivity;
        private PlayerInputActions _input;
        public event Action<float> onHorizontalMove;
        private float _xRotation;

        private void Awake()
        {
            _input = new PlayerInputActions();
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _input.FreeMovement.Enable();
            LockCursor();
        }

        private Vector2 MouseDelta => _input.FreeMovement.MouseMovement.ReadValue<Vector2>();

        // Update is called once per frame
        private void Update()
        {
            Vector2 mouseInput = MouseDelta;
            mouseInput.x = mouseInput.x * Time.deltaTime * xSensitivity;
            mouseInput.y = mouseInput.y * Time.deltaTime * ySensitivity;
         
            _xRotation -= mouseInput.y;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            if (Mathf.Abs(mouseInput.x) > 0.001f)
            {
                onHorizontalMove?.Invoke(mouseInput.x);
            }
        }
    }
}