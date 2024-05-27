using System;
using Player;
using UnityEngine;

namespace UI
{
    public class ControlsUI : MonoBehaviour
    {
        [SerializeField] private GameObject freeControls, inspectControls;
        [SerializeField] private PlayerCore playerCore;

        private void Start()
        {
            playerCore.OnGlobalStateChanged += OnGlobalStateChanged;
            ShowFree();
        }

        private void OnDestroy()
        {
            playerCore.OnGlobalStateChanged -= OnGlobalStateChanged;
        }

        private void OnGlobalStateChanged(PlayerCore.State state)
        {
            switch (state)
            {
                case PlayerCore.State.Free:
                    ShowFree();
                    break;
                case PlayerCore.State.Inspect:
                    ShowInspect();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void ShowFree()
        {
            freeControls.SetActive(true);
            inspectControls.SetActive(false);
        }

        private void ShowInspect()
        {
            freeControls.SetActive(false);
            inspectControls.SetActive(true);
        }
    }
}