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

    public bool UseRotation = true;
    public bool UseOrbit = true;

    public readonly ZoneSection Walk = new ZoneSection(2, 3, 4, 5, 11, 12);
    public readonly ZoneSection Rotate = new ZoneSection(1, 2, 3, 4, 5, 6);
    public readonly ZoneSection Strafe = new ZoneSection(7, 8, 15, 0);
    public readonly ZoneSection Orbit = new ZoneSection(9, 10, 13, 14);

    private ZonedVector2Input ZonedInput = new ZonedVector2Input();

    private bool Crouching;

    private CharacterController character;

    private void Start()
    {
        character = GetComponentInChildren<CharacterController>();
        StandingHeight = character.height;
        CrouchingHeight = CrouchingScale * StandingHeight;
        ZonedInput.Zones = 16;
        ZonedInput.Sections.Add(Walk);
        ZonedInput.Sections.Add(Strafe);
        if (UseRotation) {
            ZonedInput.Sections.Add(Rotate);
            if (UseOrbit) {
                ZonedInput.Sections.Add(Orbit);
            }
        }
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
            ZonedInput.Value = DaydreamController.TouchPos;

            if (ZonedInput.ActiveSections.Contains(Walk)) {
                moveDirection = transform.TransformDirection(GvrVRHelpers.GetHeadRotation() * Vector3.forward)
                                * DaydreamController.TouchPos.y 
                                * forwardSpeed;
            }
            if (ZonedInput.ActiveSections.Contains(Orbit) || ZonedInput.ActiveSections.Contains(Strafe)) {
                moveDirection += transform.TransformDirection(GvrVRHelpers.GetHeadRotation() * Vector3.right) 
                                * DaydreamController.TouchPos.x 
                                * sidewaysSpeed;
            }
            
            if (ZonedInput.ActiveSections.Contains(Orbit)) {
                transform.Rotate(0,
                    -DaydreamController.TouchPos.x 
                    * rotationSpeed 
                    * Time.deltaTime, 
                0);
            } else if (ZonedInput.ActiveSections.Contains(Rotate)) {
                transform.Rotate(0,
                    DaydreamController.TouchPos.x 
                    * rotationSpeed 
                    * Time.deltaTime, 
                0);
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
