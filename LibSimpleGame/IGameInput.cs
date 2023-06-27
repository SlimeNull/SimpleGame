namespace LibSimpleGame
{
    public interface IGameInput
    {
        float GetAxis(string axisName);
        float GetAxisRaw(string axisName);

        bool GetButton(string buttonName);
        bool GetButtonDown(string buttonName);
        bool GetButtonUp(string buttonName);

        Point GetCursorPosition();
    }
}
