using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackpadNavControls : MonoBehaviour
{

    public float rotationSpeed = 100f;
    public float forwardSpeed = 200f;
    public float sidewaysSpeed = 150f;
    public float crouchingSpeed = 1f;
    public float CrouchingScale = 0.5f;

    private float StandingHeight = 1.6f;
    private float CrouchingHeight = 0.8f;

    private readonly float UpperRotateThreshold = 0.2f;
    private readonly float LowerRotateThreshold = 0.5f;
    private readonly float LinearThreshold = 0.2f;
    private readonly float RotationToSideStepThreshold = 0.2f;
    private readonly float RotateAndSideStepThreshold = -0.3f;

    private bool Crouching;

    private CharacterController character;

    private Camera HeadCamera;

    private void Start()
    {
        HeadCamera = GetComponentInChildren<Camera>();
        character = GetComponentInChildren<CharacterController>();
        StandingHeight = character.height;
        CrouchingHeight = CrouchingScale * StandingHeight;
    }

    private void Update()
    {
        GvrControllerInputDevice DaydreamController = GvrControllerInput.GetDevice(GvrControllerHand.Dominant);
        if (DaydreamController == null || !DaydreamController.IsDominantHand) return; // non-dominant use for different controls

        if (Crouching != DaydreamController.GetButton(GvrControllerButton.App))
        {
            Crouching = DaydreamController.GetButton(GvrControllerButton.App);

            var desiredHeight = (Crouching ? CrouchingHeight : StandingHeight);
            var originHeight = (Crouching ? StandingHeight : CrouchingHeight);

            character.height = desiredHeight;
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - (originHeight - desiredHeight),
                transform.position.z);
        }

        var moveDirection = Vector3.zero;
        if (DaydreamController.GetButton(GvrControllerButton.TouchPadTouch))
        {
            var touchPos = DaydreamController.TouchPos;
            //------ movement -----
            //          Y+
            //      X-      X+
            //          Y-
            //---------------------
            var movementForward = touchPos.y > LinearThreshold
                || (touchPos.y < -LinearThreshold && Mathf.Abs(touchPos.x) < LowerRotateThreshold)
                ? touchPos.y : 0;

            var rotationMotion = (touchPos.y >= RotationToSideStepThreshold && Mathf.Abs(touchPos.x) > UpperRotateThreshold)
                ? touchPos.x : 0;

            var movementSideways = (touchPos.y < RotationToSideStepThreshold && Mathf.Abs(touchPos.x) > UpperRotateThreshold)
                ? touchPos.x : 0;

            rotationMotion -= (Mathf.Abs(movementSideways) > 0
                && touchPos.y < RotateAndSideStepThreshold
                && Mathf.Abs(touchPos.x) > UpperRotateThreshold)
                ? touchPos.x/2 : 0;


            if (Mathf.Abs(rotationMotion) > 0)
            {
                transform.Rotate(0, rotationMotion * rotationSpeed * Time.deltaTime, 0);
            }
            if (Mathf.Abs(movementForward) > 0)
            {
                var forward = GvrVRHelpers.GetHeadForward();
                moveDirection = transform.TransformDirection(forward) * movementForward * forwardSpeed;
            }
            if (Mathf.Abs(movementSideways) > 0)
            {
                moveDirection += transform.TransformDirection(Vector3.right) * movementSideways * sidewaysSpeed;
            }
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
}
