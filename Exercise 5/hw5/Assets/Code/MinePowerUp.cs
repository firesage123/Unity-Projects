using UnityEngine;

/// <summary>
/// A simple powerup that teleports you to a random free location
/// </summary>
public class MinePowerUp : PowerUpController {
    
    public GameObject Wall;
    
    /// <summary>
    /// Drops a mine
    /// </summary>
    public override void Fire()
    {
        Instantiate(Wall, transform.position+(transform.forward*2), transform.rotation);
        PowerUpDone();
    }
}
