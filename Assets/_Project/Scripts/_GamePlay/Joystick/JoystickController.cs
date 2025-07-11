using UnityEngine;
using VirtueSky.Inspector;
using VirtueSky.Variables;

public class JoystickController : MonoBehaviour
{
    [HeaderLine("core")] [SerializeField] private DynamicJoystick dynamicJoystick;

    [HeaderLine("Variable")] [SerializeField]
    private Vector3Variable joyStickDirectionVariable;

    private void FixedUpdate()
    {
        joyStickDirectionVariable.Value = new Vector3(dynamicJoystick.Horizontal, 0, dynamicJoystick.Vertical);
    }
}