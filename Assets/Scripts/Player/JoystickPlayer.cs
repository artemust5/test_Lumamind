using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _floatingJoystickJoystick;
    [SerializeField] private CharacterMovement _characterMovement;

    void Update()
    {
        if (_floatingJoystickJoystick.Vertical != 0 || _floatingJoystickJoystick.Horizontal != 0)
        {
            _characterMovement.MoveCharacter(new Vector3(_floatingJoystickJoystick.Horizontal, 0, _floatingJoystickJoystick.Vertical));
            _characterMovement.RotateCharacter(new Vector3(_floatingJoystickJoystick.Horizontal, 0, _floatingJoystickJoystick.Vertical));
        }
    }
}
