using UnityEngine;

/// <summary>
/// Implements player control of tanks, as well as collision detection.
/// </summary>
public class TankControl : MonoBehaviour {
    /// <summary>
    /// How fast to drive
    /// </summary>
    public float ForwardSpeed = 1f;
    /// <summary>
    /// How fast to turn
    /// </summary>
    public float RotationSpeed = 80f;
    /// <summary>
    /// Delay between shooting
    /// </summary>
    public float FireCooldown = 0.5f;

    public float fireTime = 0.0f;

    /// <summary>
    /// Keyboard controls for the player.
    /// </summary>
    public KeyCode ForwardKey, LeftKey, RightKey, FireKey;

    /// <summary>
    /// Prefab for the bullets we fire.
    /// </summary>
    public GameObject Projectile;

    /// <summary>
    /// Current rotation of the tank (in degrees).
    /// We need this because Unity's 2D system is built on top of its 3D system and so they don't
    /// give you a method for finding the rotation that doesn't require you to know what a quaternion
    /// is and what Euler angles are.  We haven't talked about those yet.
    /// </summary>
    private float Rotation {
        get {
            return transform.rotation.eulerAngles.z;
        }
        set {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, value)); // don't worry about this just yet
        }
    }

    internal void Update() {
        if (Input.GetKey(ForwardKey)) {
            transform.position += transform.up * ForwardSpeed * Time.deltaTime;
        }
        if (Input.GetKey(LeftKey)) {
            Rotation += RotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(RightKey)) {
            Rotation -= RotationSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(FireKey)) {
            if (Time.time > fireTime) {
                fireTime = Time.time + FireCooldown;
                GameObject missile = Instantiate(Projectile);
                Projectile projectileComponent = missile.GetComponent<Projectile>();
                projectileComponent.Init(this.gameObject, transform.position + 1.5f * transform.up, transform.up);
            }
        }
    }

    internal void OnTriggerEnter2D(Collider2D thing) {
        if (thing.gameObject.tag == "Projectile") {
            ScoreManager.IncreaseScore(thing.gameObject.GetComponent<Projectile>().Creator, 10);
            Destroy(thing.gameObject);
        }
        else if (thing.gameObject.tag == "Mine") {
            ScoreManager.IncreaseScore(this.gameObject, -20);
            Destroy(thing.gameObject);
        }
    }
}
