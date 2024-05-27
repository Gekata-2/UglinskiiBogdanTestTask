using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float scrollSpeed;
        [SerializeField] private Transform cameraView;

        public enum State
        {
            Free,
            Inspect,
            Locked
        }

        private PlayerInput _input;
        private PlayerCore _core;
        private MouseMovement _mouseMovement;

        private Transform _inspectedObject;
        private State _state;

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

        private void OnDestroy()
        {
            _mouseMovement.OnHorizontalMove -= OnMouseHorizontalMove;
            _core.OnObjectToInspectHit -= OnObjectToInspect;
        }

        public void SetState(State state) => _state = state;

        public void RemoveInspectableObject()
        {
            ResetRot();
            _inspectedObject = null;
        }

        private void ResetRot()
        {
            if (_inspectedObject == null)
            {
                transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            }
            else
            {
                Vector3 dir = _inspectedObject.position - transform.position;
                transform.forward = new Vector3(dir.x, 0, dir.z);
            }
        }

        private void OnObjectToInspect(Transform obj) => _inspectedObject = obj;

        private void OnMouseHorizontalMove(float value) => transform.Rotate(Vector3.up * value);

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

            Vector3 posDelta = forwardMovement + sideMovement + upwardsMovement;

            transform.Translate(posDelta * (speed * Time.deltaTime), Space.World);
        }


        private void MoveInspect()
        {
            if (_inspectedObject == null)
                return;

            Vector2 moveInput = _input.RotateAroundObjInput.normalized;
            float scrollInput = _input.ScrollInput();
            
            Vector3 inspectedObjectPosition = _inspectedObject.position;
            Vector3 dir = inspectedObjectPosition - cameraView.position;

            transform.RotateAround(inspectedObjectPosition, Vector3.up,
                rotationSpeed * moveInput.x * Time.deltaTime);
            transform.RotateAround(inspectedObjectPosition, -transform.right,
                rotationSpeed * moveInput.y * Time.deltaTime);
            
            transform.Translate(
                dir.normalized *
                (scrollInput * dir.magnitude * scrollSpeed * Time.deltaTime),
                Space.World);
        }
    }
}