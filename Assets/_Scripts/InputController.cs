using UnityEngine;

public class InputController : MonoBehaviour, IInitable
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Player _player;

    private bool _isWorking = false;

    public void Init()
    {
        _isWorking = true;
    }

    void Update()
    {
        if (_isWorking == false)
            return;

        Vector3 direction = Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal;
        _player.SetMovement(direction);
    }
}
