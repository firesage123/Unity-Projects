using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdForce = 2;
    public GameObject ExplosionPrefab;
	private void Destruct (){
		Destroy(this.gameObject);
	}
	private void Boom (){
		this.GetComponent<PointEffector2D>().enabled = true;
		this.GetComponent<SpriteRenderer>().enabled = false;

		Instantiate (ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);
		Invoke ("Destruct", .1f);
	}
	internal void OnCollisionEnter2D (Collision2D thing){
		if (thing.relativeVelocity.magnitude >= ThresholdForce)
			Boom ();
	}
}

