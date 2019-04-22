using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AliceReadsGibson {
    public class AutoWalk : MonoBehaviour
    {
        
        public float forwardSpeed = 200f;
        
        private CharacterController character;
        public bool Walking = false;
        
        // Start is called before the first frame update
        void Start()
        {
            character = GetComponentInChildren<CharacterController>();
        }
    /*
        // Update is called once per frame
        void Update()
        {
            var moveDirection = Vector3.zero;

            if (Walking) {
                moveDirection = transform.TransformDirection(GvrVRHelpers.GetHeadRotation() * Vector3.forward) * forwardSpeed;
            }
            if (!character.isGrounded)
            {
                moveDirection.y -= Physics.gravity.y * Time.deltaTime;
            }
            if (moveDirection != Vector3.zero)
            {
                character.SimpleMove(moveDirection * Time.deltaTime);
            }
        }
        */
        
        public void SetWalking(bool value) {
            Walking = value;
        }

        public void ToggleWalking() {
            Walking = !Walking;
        }

    }
}
