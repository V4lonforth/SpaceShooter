using UnityEngine;

public class InputController : MonoBehaviour
{
    public Vector2 MovementDirection { get; private set; }
    public Vector2 AttackDirection { get; private set; }

    public bool TryingActivateAbility { get; private set; }
    public bool IsPointingOnMap { get; private set; }
    public Vector2 PointingPosition { get; private set; }

    private bool attackByJoystick;

    private GameObject activeAbilityButton;

    private VirtualJoystick attackJoystick;
    private VirtualJoystick movementJoystick;

    private InteractableUI[] interactableUIs;

    private Transform player;
    private ActiveAbility playerActiveAbility;

    private void Awake()
    {
        Input.simulateMouseWithTouches = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerActiveAbility = player.GetComponent<ActiveAbility>();
        interactableUIs = FindObjectsOfType<InteractableUI>();

        foreach (InteractableUI interactableUI in interactableUIs)
        {
            switch (interactableUI.type)
            {
                case InteractableUIType.AttackJoystick:
                    attackJoystick = (VirtualJoystick)interactableUI;
                    break;
                case InteractableUIType.MovementJoystick:
                    movementJoystick = (VirtualJoystick)interactableUI;
                    break;
                case InteractableUIType.ActiveAbility:
                    activeAbilityButton = interactableUI.gameObject;
                    break;
            }
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        TryingActivateAbility = false;
        IsPointingOnMap = false;
        AttackDirection = attackJoystick.Axes;
        MovementDirection = movementJoystick.Axes;

        Vector2 keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (keyboardInput != Vector2.zero)
            MovementDirection = keyboardInput;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && IsTouchAvailable(touch.fingerId))
            {
                TouchScene(touch.position);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (IsTouchAvailable(-1))
            {
                TouchScene(Input.mousePosition);
            }
        }
    }

    public void SetControlType(ControlType controlType)
    {
        switch (controlType)
        {
            case ControlType.OneJoystick:
                activeAbilityButton.SetActive(true);
                attackJoystick.gameObject.SetActive(false);
                attackByJoystick = false;
                break;
            case ControlType.TwoJoysticks:
                activeAbilityButton.SetActive(false);
                attackJoystick.gameObject.SetActive(true);
                attackByJoystick = true;
                break;
        }
    }

    public void PressAbility()
    {
        TryingActivateAbility = true;
        if (playerActiveAbility.AbilityAvailable)
            playerActiveAbility.AbilityPressed = true;
    }

    private void TouchScene(Vector2 position)
    {
        IsPointingOnMap = true;
        PointingPosition = position;

        if (attackByJoystick)
        {
            TryingActivateAbility = true;
        }
        else
        {
            position = Camera.main.ScreenToWorldPoint(position);
            AttackDirection = (position - (Vector2)player.position).normalized;
        }
    }

    private bool IsTouchAvailable(int pointerId)
    {
        foreach (InteractableUI interactableUI in interactableUIs)
            if (interactableUI.IsPressed && interactableUI.PointerId == pointerId)
                return false;
        return true;
    }
}
