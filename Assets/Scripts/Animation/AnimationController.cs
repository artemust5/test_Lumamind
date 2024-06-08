using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator AnimationBody;
    [SerializeField] private FloatingJoystick _floatingJoystickJoystick;

    private float spead;

    private void FixedUpdate()
    {
        spead = new Vector3(_floatingJoystickJoystick.Horizontal, 0, _floatingJoystickJoystick.Vertical).magnitude;
        AnimationBody.SetFloat("Spead", spead);
    }
}
