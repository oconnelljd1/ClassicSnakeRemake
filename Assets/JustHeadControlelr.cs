using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class JustHeadControlelr : MonoBehaviour {

	private Rigidbody myRB;
	private List<GameObject> bodySegments = new List<GameObject>();
	private bool start;
	private GameObject lastBodyPart;
	private float initialX;
	private float initialZ;
	private float score = 1000;
	private float rotation;
	//private int segmentIndex = 0;

	[SerializeField]
	private int initialThrust;

	private float thrust = 4.0f;

	[SerializeField]
	private int turnSpeed;

	[SerializeField]
	private GameObject bodyPrefab;

	[SerializeField]
	private Text foodText;

	// Use this for initialization
	void Start () {
		myRB = gameObject.GetComponent<Rigidbody> ();
		start = true;
		CreateSegment ();

		initialX = transform.position.x;
		initialZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.W)) {
			myRB.AddForce (transform.forward * initialThrust);
		} else if (Input.GetKey (KeyCode.W)) {
			myRB.AddForce (transform.forward * thrust);
		}
		*/
		//myRB.velocity = new Vector3(0, 0, 0);

		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D)) {
			//I don't want to do anything if holding both keys	
		} else if (Input.GetKey (KeyCode.A)) {
			gameObject.transform.Rotate (Vector3.up * Time.deltaTime * -turnSpeed, Space.World);
		} else if (Input.GetKey (KeyCode.D)) {
			gameObject.transform.Rotate (Vector3.up * Time.deltaTime * turnSpeed, Space.World);
		}

		//rotation = transform.rotation.eulerAngles.y;
		//Debug.Log (rotation);

		if(Input.GetKey( KeyCode.W)){
			//myRB.AddForce (transform.forward * thrust);
			transform.Translate(Vector3.forward * Time.deltaTime * thrust);
		}

		if (score > 0) {
			score = score - (50 * Time.deltaTime);	
		} else if (score <= 0) {
			ResetGame ();
		}
		foodText.text = "" +  Mathf.Floor (score);

	}

	void OnTriggerEnter(Collider trigger){
		if (trigger.tag == "Food") {
			Debug.Log ("Created a segment!");
			CreateSegment ();
			score = score + 1000;
		} else if (trigger.tag == "Body" || trigger.tag == "Wall") {
			ResetGame ();
		}
	}

	void CreateSegment(){
		GameObject newSegment = Instantiate (bodyPrefab) as GameObject;
		if (start) {
			start = false;
			newSegment.GetComponent <BodyController> ().SetParent (gameObject);
		}else if (!start) {
			newSegment.GetComponent <BodyController> ().SetParent (lastBodyPart);
		}
		newSegment.GetComponent <BodyController> ().PlaceChild ();
		lastBodyPart = newSegment;
		bodySegments.Add (newSegment);
	}

	private void ResetGame() {
		foreach (GameObject body in bodySegments) {
			body.SetActive (false);;
			Object.Destroy (body);
		}
		bodySegments.Clear ();
		Vector3 temp = transform.position;
		temp.x = initialX;
		temp.z = initialZ;
		transform.position = temp;
		start = true;
		score = 1000;
		transform.rotation = Quaternion.identity;
		myRB.velocity = new Vector3 (0, 0, 0);
		CreateSegment ();
	}
}