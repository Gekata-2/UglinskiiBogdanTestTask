using System;
using Objects;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerCore : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayers;
        [SerializeField] private Transform cameraView;
        [SerializeField] private QuickMenu quickMenu;
        [SerializeField] private SideMenu sideMenu;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private MouseMovement mouseMovement;

        public event Action<Transform> OnObjectToInspectHit;
        public event Action<State> OnGlobalStateChanged;
        private PlayerInput _input;

        private InspectableObject _inspectableObject;

        public enum State
        {
            Free,
            Inspect
        }

        private State _state;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();

            SetFreeState();

            SetGlobalState(State.Free);
        }

        private void Start()
        {
            _input.onInspect += OnInspect;
            _input.onCancel += OnCancelInspect;
            _input.onQuickMenuOpen += OnQuickMenuOpen;

            sideMenu.OnSideMenuSetActive += OnSideMenuOpen;
            quickMenu.onResume += OnResume;
            ObjectsController.Instance.onObjectRemoved += OnObjectRemovedFromRegistry;
        }


        private void OnDestroy()
        {
            _input.onInspect -= OnInspect;
            _input.onCancel -= OnCancelInspect;
            _input.onQuickMenuOpen -= OnQuickMenuOpen;

            sideMenu.OnSideMenuSetActive -= OnSideMenuOpen;
            quickMenu.onResume -= OnResume;
            ObjectsController.Instance.onObjectRemoved -= OnObjectRemovedFromRegistry;

            // if (_inspectableObject != null)
            // {
            //     _inspectableObject.onDestroy -= OnInspectableObjectDestroyed;
            // }
        }

        private void SetGlobalState(State state)
        {
            _state = state;
            switch (state)
            {
                case State.Free:
                    _input.EnableFreeMovementActions();
                    break;
                case State.Inspect:
                    _input.EnableInspectionActions();
                    break;
                default:
                    break;
            }

            OnGlobalStateChanged?.Invoke(_state);
        }


        private void OnSideMenuOpen(bool isActive)
        {
            if (quickMenu.IsActive)
                return;
            switch (_state)
            {
                case State.Free:
                    if (isActive)
                        SetMenuOpenState();
                    else
                        SetFreeState();
                    break;
                case State.Inspect:
                    if (isActive)
                        SetMenuOpenState();
                    else
                        SetInspectState();
                    break;
                default:
                    break;
            }
        }

        private void OnResume()
        {
            if (sideMenu.IsActive)
                return;

            SetFreeState();
        }

        private void OnQuickMenuOpen()
        {
            SetMenuOpenState();
        }

        private void OnCancelInspect()
        {
            if (sideMenu.IsActive)
            {
                RemoveInspectableObject();
                SetGlobalState(State.Free);
                return;
            }


            SetFreeState();
        }


        private void OnInspect()
        {
            if (Physics.Raycast(cameraView.transform.position,
                    cameraView.transform.forward,
                    out RaycastHit hit,
                    1111f, interactableLayers))
            {
                if (hit.transform.TryGetComponent<InspectableObject>(out var obj))
                {
                    OnObjectToInspectHit?.Invoke(hit.transform);
                    _inspectableObject = obj;
                    // obj.onDestroy += OnInspectableObjectDestroyed;
                    SetInspectState();
                    SetGlobalState(State.Inspect);
                }
            }
        }

        private void OnObjectRemovedFromRegistry(string objName)
        {
            if (_inspectableObject == null)
                return;
            
            if (_inspectableObject.name == objName)
                OnInspectableObjectDestroyed();
        }

        private void OnInspectableObjectDestroyed()
        {
            RemoveInspectableObject();
            SetMoveMouseState(MouseMovement.State.Locked,
                CursorLockMode.Confined,
                PlayerMovement.State.Locked);
            SetGlobalState(State.Free);
        }

        private void RemoveInspectableObject()
        {
            playerMovement.RemoveInspectableObject();
            mouseMovement.RemoveInspectableObject();
            _inspectableObject = null;
        }


        //Locked-Confined-Locked
        private void SetMenuOpenState()
        {
            SetMoveMouseState(MouseMovement.State.Locked,
                CursorLockMode.Confined,
                PlayerMovement.State.Locked);
        }

        //LookAt-Locked-Inspect
        private void SetInspectState()
        {
            SetMoveMouseState(MouseMovement.State.LookAt,
                CursorLockMode.Locked,
                PlayerMovement.State.Inspect);
        }

        //Free-Locked-Free
        private void SetFreeState()
        {
            SetMoveMouseState(MouseMovement.State.Free,
                CursorLockMode.Locked,
                PlayerMovement.State.Free);
        }

        private void SetMoveMouseState(MouseMovement.State mouseState, CursorLockMode cursorLockMode,
            PlayerMovement.State moveState)
        {
            mouseMovement.SetState(mouseState, cursorLockMode);
            playerMovement.SetState(moveState);
        }
    }
}