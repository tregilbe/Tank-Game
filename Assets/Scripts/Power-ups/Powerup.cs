[System.Serializable]
public class Powerup
{
    public float speedModifier;
    public float healthModifier;
    public float maxHealthModifier;

    public float duration; // in seconds
    public bool isPermanent;

    public void OnActivate(TankData target)
    {
        target.moveSpeed += speedModifier;
        target.currentHealth += healthModifier;
        target.maxHealth += maxHealthModifier;
    }

    public void OnDeactivate(TankData target)
    {
        target.moveSpeed -= speedModifier;
        target.currentHealth -= healthModifier;
        target.maxHealth -= maxHealthModifier;
    }
}