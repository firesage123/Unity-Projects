using UnityEngine;

/// <summary>
/// A falling yummy.
/// This just makes the object spin.
/// </summary>
public class Yummy : MonoBehaviour {
    /// <summary>
    /// Desired rotation speed
    /// </summary>
    public float RotateSpeed = 500f;

    /// <summary>
    /// Update the object state before we draw the next frame
    /// </summary>
	internal void Update() {
        transform.Rotate(0, 0, Time.deltaTime * RotateSpeed);
	}
}