using VirtueSky.Variables;

public interface IJoystickMovement
{
    Vector3Variable JoystickVariable { get; set; }

    Vector3Variable OwnerPositionVariable { get; set; }

    void OnJoystickMovement();
}