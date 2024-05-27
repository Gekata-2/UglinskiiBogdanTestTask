using System;
using UnityEngine;

namespace Player
{
    public class MouseMovement : MonoBehaviour
    {
        [SerializeField] private float xSensitivity;
        [SerializeField] private float ySensitivity;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerCore core;

        public enum State
        {
            Free,
            LookAt,
            Locked
        }

        public event Action<float> OnHorizontalMove;

        private State _state;

        private float _xRotation;
        private Transform _inspectedObject;


        private void Start()
        {
            core.OnObjectToInspectHit += OnObjectToInspect;
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Free:
                    MoveFree();
                    break;
                case State.LookAt:
                    LookAt();
                    break;
                case State.Locked:
                default:
                    break;
            }
        }

        private void OnDestroy()
        {
            core.OnObjectToInspectHit -= OnObjectToInspect;
        }

        private void OnObjectToInspect(Transform obj)
        {
            _inspectedObject = obj;
        }

        private void MoveFree()
        {
            Vector2 mouseInput = playerInput.MouseDelta;
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

        private void LookAt()
        {
            if (_inspectedObject == null)
                return;

            transform.LookAt(_inspectedObject);
        }

        public void RemoveInspectableObject() => _inspectedObject = null;

        public void SetState(State state, CursorLockMode lockMode)
        {
            _state = state;
            Cursor.lockState = lockMode;

            if (state == State.Free)
                _inspectedObject = null;
        }
    }
}