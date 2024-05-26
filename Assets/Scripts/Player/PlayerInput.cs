using System;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        // Start is called before the first frame update
        private void Start()
        {
            _inputActions.FreeMovement.Enable();
        }

        public Vector3 MoveInput => _inputActions.FreeMovement.Movement.ReadValue<Vector3>();
    }
}