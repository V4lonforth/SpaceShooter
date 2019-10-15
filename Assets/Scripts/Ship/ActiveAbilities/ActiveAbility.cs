using UnityEngine;

public abstract class ActiveAbility : MonoBehaviour
{
    public float cooldown;
    public bool shouldPointOnMap;

    public bool AbilityPressed { get; set; }
    public bool AbilityAvailable { get => timeToActivation <= 0f; }

    protected float timeToActivation;

    protected InputController inputController;

    protected void Start()
    {
        inputController = FindObjectOfType<InputController>();
    }

    protected void Update()
    {
        timeToActivation -= Time.deltaTime;
        if (inputController.TryingActivateAbility || AbilityPressed)
        {
            if (!shouldPointOnMap || inputController.IsPointingOnMap)
            {
                Activate();
                AbilityPressed = false;
                timeToActivation = cooldown;
            }
        }
    }

    protected abstract void Activate();
}
