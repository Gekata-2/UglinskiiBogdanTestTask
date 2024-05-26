using System;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float scrollSpeed;
        [SerializeField] private Transform cameraView;
        private PlayerInput _input;
        private MouseMovement _mouseMovement;
        private PlayerCore _core;
        private Transform _inspectedObject;
        private State _state;

        public enum State
        {
            Free,
            Inspect,
            Locked
        }

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _core = GetComponent<PlayerCore>();
            _mouseMovement = cameraView.GetComponent<MouseMovement>();
        }

        void Start()
        {
            _mouseMovement.OnHorizontalMove += OnMouseHorizontalMove;
            _core.OnObjectToInspectHit += OnObjectToInspect;
        }

        public void SetState(State state)
        {
            _state = state;
            switch (state)
            {
                case State.Free:
                    if (_inspectedObject != null)
                    {
                        ResetRot();
                        _inspectedObject = null;
                    }

                    break;
                case State.Inspect:
                    break;
                case State.Locked:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void ResetRot()
        {
            Vector3 dir = _inspectedObject.position - transform.position;
            transform.forward = new Vector3(dir.x, 0, dir.z);
        }

        private void OnObjectToInspect(Transform obj)
        {
            _inspectedObject = obj;
        }

        private void OnMouseHorizontalMove(float val)
        {
            transform.Rotate(Vector3.up * val);
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Free:
                    MoveFree();
                    break;
                case State.Inspect:
                    MoveInspect();
                    break;
                case State.Locked:
                default:
                    break;
            }
        }

        private void MoveFree()
        {
            Vector3 input = _input.MoveInput.normalized;
            Vector3 sideMovement = cameraView.right * input.x;
            Vector3 upwardsMovement = cameraView.up * input.y;
            Vector3 forwardMovement = cameraView.forward * input.z;

            var posDelta = forwardMovement + sideMovement + upwardsMovement;

            transform.Translate(posDelta * (speed * Time.deltaTime), Space.World);
        }

        private void MoveInspect()
        {
            Vector2 input = _input.RotateAroundObjInput.normalized;

            var inspectedObjectPosition = _inspectedObject.position;
            transform.RotateAround(inspectedObjectPosition, _inspectedObject.up,
                rotationSpeed * input.x * Time.deltaTime);
            transform.RotateAround(inspectedObjectPosition, -transform.right,
                rotationSpeed * input.y * Time.deltaTime);

            float scroll = _input.ScrollInput();


            Vector3 dir = inspectedObjectPosition - cameraView.position;
            transform.Translate(
                dir.normalized * (scroll * dir.magnitude * scrollSpeed * Time.deltaTime),
                Space.World);
        }
    }
}