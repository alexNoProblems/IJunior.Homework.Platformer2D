using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;
    private const KeyCode PunchKey = KeyCode.Z;
    private const KeyCode VampirismKey = KeyCode.V;

    public Vector2 Movement { get; private set; }
    public bool JumpRequested { get; private set; }
    public bool PunchRequested { get; private set; }
    public bool VampirismRequested { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(JumpKey))
            JumpRequested = true;

        if (Input.GetKeyDown(PunchKey))
            PunchRequested = true;

        if (Input.GetKeyDown(VampirismKey))
        {
            VampirismRequested = true;
        }

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

    public void ClearVampirismRequested()
    {
        VampirismRequested = false;
    }
}
