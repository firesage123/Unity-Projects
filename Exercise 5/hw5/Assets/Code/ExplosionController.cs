using UnityEngine;

/// <summary>
/// Simple sequencer to play an explosion animation, apply forces to objects in the areas,
/// and then delete itself.
/// </summary>
public class ExplosionController : MonoBehaviour
{
    /// <summary>
    /// How long to apply forces for.
    /// </summary>
    public float ExplosionDuration = 0.1f;
    
    void OnTriggerEnter2D(Collider2D other) {
        Invoke("Destruct", ExplosionDuration);
    }
    
    void Destruct() {
        Destroy(gameObject);   
    }
}
