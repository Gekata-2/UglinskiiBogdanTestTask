using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform cameraView;
        private PlayerInput _input;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            MoveFree();
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
    }
}