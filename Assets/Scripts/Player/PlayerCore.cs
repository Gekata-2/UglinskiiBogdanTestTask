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
       
        private PlayerInput _input;

        private enum State
        {
            Free,
            Inspect
        }

        private State _state;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();

            SetMoveMouseState(MouseMovement.State.Free,
                CursorLockMode.Locked,
                PlayerMovement.State.Free);

            SetGlobalState(State.Free);
        }

        private void Start()
        {
            _input.onInspect += OnInspect;
            _input.onCancel += OnCancel;
            _input.onQuickMenuOpen += OnQuickMenuOpen;

            sideMenu.OnSideMenuSetActive += OnSideMenuOpen;
            quickMenu.onResume += OnResume;
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
        }


        private void OnSideMenuOpen(bool isActive)
        {
            switch (_state)
            {
                case State.Free:
                    if (isActive)
                    {
                        SetMoveMouseState(MouseMovement.State.Locked,
                            CursorLockMode.Confined,
                            PlayerMovement.State.Locked);
                    }
                    else
                    {
                        mouseMovement.SetState(MouseMovement.State.Free, CursorLockMode.Locked);
                        playerMovement.SetState(PlayerMovement.State.Free);
                        SetMoveMouseState(MouseMovement.State.Free,
                            CursorLockMode.Locked,
                            PlayerMovement.State.Free);
                    }

                    break;
                case State.Inspect:
                    if (isActive)
                    {
                        SetMoveMouseState(MouseMovement.State.Locked,
                            CursorLockMode.Confined,
                            PlayerMovement.State.Locked);
                    }
                    else
                    {
                        SetMoveMouseState(MouseMovement.State.LookAt,
                            CursorLockMode.Locked,
                            PlayerMovement.State.Inspect);
                    }

                    break;
                default:
                    break;
            }
        }

        private void OnResume()
        {
            SetMoveMouseState(MouseMovement.State.Free,
                CursorLockMode.Locked,
                PlayerMovement.State.Free);
        }

        private void OnQuickMenuOpen()
        {
            SetMoveMouseState(MouseMovement.State.Locked,
                CursorLockMode.Confined,
                PlayerMovement.State.Locked);
        }

        private void OnCancel()
        {
            SetGlobalState(State.Free);
            SetMoveMouseState(MouseMovement.State.Free,
                CursorLockMode.Locked,
                PlayerMovement.State.Free);
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

                    SetMoveMouseState(MouseMovement.State.LookAt,
                        CursorLockMode.Locked,
                        PlayerMovement.State.Inspect);

                    SetGlobalState(State.Inspect);
                }
            }
        }

        private void SetMoveMouseState(MouseMovement.State mouseState, CursorLockMode cursorLockMode,
            PlayerMovement.State moveState)
        {
            mouseMovement.SetState(mouseState, cursorLockMode);
            playerMovement.SetState(moveState);
        }
    }
}