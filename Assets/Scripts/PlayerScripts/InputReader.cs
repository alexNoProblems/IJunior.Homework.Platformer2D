using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space; 

    public Vector2 Movement { get; private set; }
    public bool IsJumpPressed { get; private set; }

    private void Update()
    {
        float xAxis = Input.GetAxisRaw(Horizontal);

        Movement = new Vector2(xAxis, 0).normalized;

        IsJumpPressed = Input.GetKeyDown(JumpKey);
    }
}
