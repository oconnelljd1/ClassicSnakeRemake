using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeadControlelr : MonoBehaviour {

	private Rigidbody myRB;
	private List<GameObject> bodySegments = new List<GameObject>();
	private bool start;
	//private int segmentIndex = 0;

	[SerializeField]
	private int initialThrust;

	[SerializeField]
	private int thrust;

	[SerializeField]
	private int turnSpeed;

	[SerializeField]
	private GameObject bodyPrefab;

	// Use this for initialization
	void Start () {
		myRB = gameObject.GetComponent<Rigidbody> ();
		start = true;
		CreateSegment ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.W)) {
			myRB.AddForce (transform.forward * initialThrust);
			for (int i = 0; i < bodySegments.Count; i++) {
				if (i == 0) {
					FaceParent (gameObject, bodySegments [i]);
				}else if (i > 0){
					FaceParent (bodySegments[i - 1], bodySegments[i]);
				}
				var rb = bodySegments [i].GetComponent<Rigidbody> ();
				rb.AddForce (bodySegments[i].transform.forward * initialThrust);

			}
		} else if (Input.GetKey (KeyCode.W)) {
			myRB.AddForce (transform.forward * thrust);
			for (int i = 0; i < bodySegments.Count; i++) {
				if (i == 0) {
					FaceParent (gameObject, bodySegments [i]);
				}else if (i > 0){
					FaceParent (bodySegments[i - 1], bodySegments[i]);
				}
				var rb = bodySegments [i].GetComponent<Rigidbody> ();
				rb.AddForce (bodySegments[i].transform.forward * thrust);
				//rb.AddForce (transform.forward * thrust);
			}
		}

		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D)) {
			//I don't want to do anything if im holding down keys down	
		} else if (Input.GetKey (KeyCode.A)) {
			gameObject.transform.Rotate (Vector3.up * Time.deltaTime * -turnSpeed, Space.World);
		} else if (Input.GetKey (KeyCode.D)) {
			gameObject.transform.Rotate (Vector3.up * Time.deltaTime * turnSpeed, Space.World);
		}


	}

	void OnTriggerEnter(Collider trigger){
		if (trigger.tag == "Food") {
			Debug.Log ("hit food");
			CreateSegment ();
		} else if (trigger.tag == "Body" || trigger.tag == "Wall") {
			
		}
	}

	void CreateSegment(){
		GameObject newSegment = Instantiate (bodyPrefab) as GameObject;
		//bodySegments.Add (newSegment);
		if (start) {
			start = false;
			PlaceSegment (gameObject, newSegment);
		}else if (!start) {
			Debug.Log (bodySegments [bodySegments.Count - 1].transform.position.x + ", " + bodySegments [bodySegments.Count - 1].transform.position.z);
			PlaceSegment (bodySegments[bodySegments.Count - 1] , newSegment);
		}
		bodySegments.Add (newSegment);
	}

	void PlaceSegment (GameObject jesus, GameObject desciple){
		//segmentIndex++;
		float dirJ = jesus.transform.rotation.eulerAngles.y;
		Vector3 tempJ = jesus.transform.position;
		float dX = Mathf.Cos (dirJ);
		float dZ = Mathf.Sin (dirJ);
		Vector3 tempD = desciple.transform.position;
		tempD.x = tempJ.x + dX;
		tempD.z = tempJ.z + dZ;
		desciple.transform.position = tempD;
	}

	void FaceParent (GameObject jesus, GameObject desciple){
		//Debug.Log ("turning");
		Vector3 direction = jesus.transform.position - desciple.transform.position;
		direction.y = 0;
		desciple.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), turnSpeed * Time.deltaTime);
	}
}