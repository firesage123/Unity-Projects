using UnityEngine;

/// <summary>
/// Behavior that runs to a random location
/// </summary>
public class Flee : BehaviorTreeNode
{
    /// <summary>
    /// Where we're running to
    /// </summary>
    private Vector3 goal;

    /// <summary>
    /// Run toward the goal
    /// </summary>
    /// <param name="tank">Tank being controlled</param>
    /// <returns>True if behavior wants to keep running</returns>
    public override bool Tick(AITankControl tank)
    {
        do {
            //goal = SpawnController.FindFreeLocation(1);
            float randomX = Random.Range(SpawnController.XMin, SpawnController.XMax);
            float randomY = Random.Range(SpawnController.YMin, SpawnController.YMax);
            goal = new Vector3(randomX, randomY, 0f);
            
        } while (WallBetween(tank.transform.position, goal));
        tank.MoveTowards(goal);
        return false;
    }
}
