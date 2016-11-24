using UnityEngine;

public class TargetBox : MonoBehaviour
{
    /// <summary>
    /// Targets that move past this point score automatically.
    /// </summary>
    public static float OffScreen;
    internal void Start() {
        OffScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-100, 0, 0)).x;
    }

    internal void Update()
    {
        if (transform.position.x > OffScreen)
            Scored();
    }
	bool scoring = false;
	private void Scored()
    {
		if (this.scoring == false) {
			this.gameObject.GetComponent<Renderer> ().material.color = Color.green; 
			ScoreKeeper.AddToScore (this.gameObject.GetComponent<Rigidbody2D> ().mass);
			this.scoring = true;
		}
    }
	internal void OnCollisionEnter2D (Collision2D thing){
		if (thing.gameObject.tag == "Ground"){
			Scored ();														
		}
	}
}
