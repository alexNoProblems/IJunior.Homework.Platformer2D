using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;
    private const KeyCode PunchKey = KeyCode.Z;

    public Vector2 Movement { get; private set; }
    public bool JumpRequested { get; private set; }
    public bool PunchRequested { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(JumpKey))
            JumpRequested = true;
        
        if (Input.GetKeyDown(PunchKey))
            PunchRequested = true;

        float xAxis = Input.GetAxisRaw(Horizontal);
        Movement = new Vector2(xAxis, 0).normalized;
    }

    public void ClearJumpRequest()
    {
        JumpRequested = false;
    }

    public void ClearPunchRequest()
    {
        PunchRequested = false;
    }
}
