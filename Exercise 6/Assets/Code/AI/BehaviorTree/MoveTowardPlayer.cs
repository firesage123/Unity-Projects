/// <summary>
/// Beavhior that drives in a straight line toward the player.
/// </summary>
public class MoveTowardPlayer : BehaviorTreeNode
{
    public override bool Tick (AITankControl tank) {
        tank.MoveTowards(Player.position);
        return false;
    }
}
