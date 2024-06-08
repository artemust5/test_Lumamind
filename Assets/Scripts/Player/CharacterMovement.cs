using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _gravityForce = 20f;

    private float _currentAttractionCharacter = 0;
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GravityHandling();
    }
    public void MoveCharacter (Vector3 moveDirection)
    {
        moveDirection = moveDirection * _moveSpeed;
        moveDirection.y = _currentAttractionCharacter;
        _characterController.Move(moveDirection * Time.deltaTime);
    }
    public void RotateCharacter (Vector3 rotateDirection)
    {
        if (_characterController.isGrounded)
        {
            if(Vector3.Angle(transform.forward, rotateDirection) > 0)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, rotateDirection, _rotateSpeed, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
    }

    private void GravityHandling()
    {
        if (!_characterController.isGrounded)
        {
            _currentAttractionCharacter -= _gravityForce * Time.deltaTime;
        }
        else
        {
            _currentAttractionCharacter = 0;
        }
    }
}
