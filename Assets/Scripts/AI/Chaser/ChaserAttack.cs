public class ChaserAttack : AIAttack
{
    private ChaserMovement chaserMovement;

    protected new void Update()
    {
        if (chaserMovement.CanAttack)
            base.Update();
    }
}