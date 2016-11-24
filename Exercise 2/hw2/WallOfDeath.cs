using UnityEngine;

/// <summary>
/// Destroys wayward objects that run into it.
/// </summary>
public class WallOfDeath : MonoBehaviour {
    internal void OnTriggerEnter2D(Collider2D thing) {
        if (thing.gameObject.tag == "Tank") {
            ScoreManager.IncreaseScore(thing.gameObject, -50);
        }
        Destroy(thing.gameObject);
    }
}
