using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BreadStuff
{
    public class BreadInput : MonoBehaviour
    {
        private Vector2 moveInput;
        private Vector2 lookInput;
        private bool isJumping;
        private bool isUsingAbilityOne;
        private bool isUsingAbilityTwo;

        public Vector2 MoveInput => moveInput;
        public Vector2 LookInput => lookInput;
        public bool IsJumping => isJumping;
        public bool IsUsingAbilityOne => isUsingAbilityOne;
        public bool IsUsingAbilityTwo => isUsingAbilityTwo;

        public void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            StartCoroutine(JumpInput(value.isPressed));
        }

        public void OnLook(InputValue value)
        {
            lookInput = value.Get<Vector2>();
        }

        public void OnAbilityOne(InputValue value)
        {
            isUsingAbilityOne = value.isPressed;
        }

        public void OnAbilityTwo(InputValue value)
        {
            isUsingAbilityTwo = value.isPressed;
        }

        IEnumerator JumpInput(bool newJumpState)
        {
            isJumping = newJumpState;
            yield return new WaitForSeconds(.1f);
            isJumping = !isJumping;
        }
    }
}