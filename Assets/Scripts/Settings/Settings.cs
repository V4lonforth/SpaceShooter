using UnityEngine;

public class Settings : MonoBehaviour
{
    public ControlType controlType;

    private InputController inputController;

    private void Start()
    {
        inputController = FindObjectOfType<InputController>();

        ChangeSettings();
    }

    public void ChangeSettings()
    {
        inputController.SetControlType(controlType);
    }
}
