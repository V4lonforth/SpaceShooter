public class ChaserAttack : AIAttack
{
    private ChaserMovement chaserMovement;

    protected new void Start()
    {
        base.Start();
        chaserMovement = GetComponent<ChaserMovement>();
    }

    protected new void Update()
    {
        if (chaserMovement.CanAttack)
            base.Update();
        else
            Wait();
    }
}