using UnityEngine;
using System.Collections;

public class FoodController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		positionMe ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider trigger){
		if (trigger.tag == "Player") {
			positionMe ();
		}
	}

	void positionMe() {
		Vector3 temp = gameObject.transform.position;
		temp.x = Random.Range (-14, 14);
		temp.z = Random.Range (-14, 14);
		Debug.Log (temp.x + ", " + temp.z);
		gameObject.transform.position = temp;
	}
}
