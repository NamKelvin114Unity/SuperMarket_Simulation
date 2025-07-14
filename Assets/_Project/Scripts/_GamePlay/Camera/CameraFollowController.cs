using UnityEngine;
using VirtueSky.Inspector;
using VirtueSky.Variables;

public class CameraFollowController : MonoBehaviour
{
    [HeaderLine("Variable")] [SerializeField]
    private Vector3Variable offsetCameraFollowVariable;

    [SerializeField] private Vector3Variable targetPositionVariable;


    private void LateUpdate()
    {
        transform.position = targetPositionVariable.Value + offsetCameraFollowVariable.Value;
    }
}