using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour {

	public GameObject leader;
//	private Rigidbody myRB;
	private Rigidbody leaderRB;
	private int thrust = 4;

	// Use this for initialization
	void Start () {
		//myRB = GetComponent<Rigidbody> ();
		Color currentColor = Color.green;
		currentColor.b = Random.value;
		currentColor.r = Random.value;
		GetComponent<Renderer> ().material.color = currentColor;
	}
	
	// Update is called once per frame
	void Update () {		
		transform.LookAt (leader.transform);
		/*
		if (Input.GetKeyDown (KeyCode.W)) {
			myRB.AddForce (transform.forward * initialThrust);
		} else if (Input.GetKey (KeyCode.W)) {
			myRB.AddForce (transform.forward * thrust);
		}
		*/
		float distance = Mathf.Sqrt (Mathf.Pow (leader.transform.position.x - transform.position.x, 2) + Mathf.Pow (leader.transform.position.z - transform.position.z, 2));
		if (distance > 1) {
			transform.Translate(Vector3.forward * Time.deltaTime * thrust);
		}
		//*/
		/*
		myRB.velocity = new Vector3 (0, 0, 0);

		if(Input.GetKey( KeyCode.W)){
			transform.Translate(Vector3.forward * Time.deltaTime * thrust);
		}
		*/
	}

	public void SetParent (GameObject Parent){
		leader = Parent;
		Debug.Log (leader);
	}

	public void PlaceChild(){
		float dirL = leader.transform.rotation.eulerAngles.y;
		Vector3 tempL = leader.transform.position;
		float dX = Mathf.Sin (dirL);
		float dZ = - Mathf.Cos (dirL);
		Vector3 tempF = transform.position;
		tempF.x = tempL.x + dX;
		tempF.z = tempL.z + dZ;
		transform.position = tempF;
	}
}
