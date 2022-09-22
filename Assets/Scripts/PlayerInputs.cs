using UnityEngine;

[System.Serializable]
public class _Input
{
    public KeyCode primary;
    public KeyCode alternate;

    public bool Pressed()
    {
        return UnityEngine.Input.GetKey(primary) || UnityEngine.Input.GetKey(alternate);
    }
    public bool PressedDown()
    {
        return UnityEngine.Input.GetKeyDown(primary) || UnityEngine.Input.GetKeyDown(alternate);
    }
    public bool PressedUp()
    {
        return UnityEngine.Input.GetKeyUp(primary) || UnityEngine.Input.GetKeyUp(alternate);
    }
}


public class PlayerInputs : MonoBehaviour
{
    public _Input Forward;
    public _Input Backward;
    public _Input Right;
    public _Input Left;
    public _Input Sprint;
    public _Input Jump;

    public int MoveAxisForwardRaw
    {
        get
        {
            if (Forward.Pressed() && Backward.Pressed()) { return 0; }
            else if (Forward.Pressed()) { return 1; }
            else if (Backward.Pressed()) { return -1; }
            else { return 0; }
        }
    }
    public int MoveAxisRightRaw
    {
        get
        {
            if (Right.Pressed() && Left.Pressed()) { return 0; }
            else if (Right.Pressed()) { return 1; }
            else if (Left.Pressed()) { return -1; }
            else { return 0; }
        }
    }

    public static float MouseXInput { get => UnityEngine.Input.GetAxis("Mouse X"); }
    public static float MouseYInput { get => UnityEngine.Input.GetAxis("Mouse Y"); }
    public static float MouseScrollInput { get => UnityEngine.Input.GetAxis("Mouse ScrollWheel"); }
}
