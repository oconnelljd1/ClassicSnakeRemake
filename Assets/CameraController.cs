using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Vector3 temp = transform.localPosition;
			temp.z *= -1;
			//temp.y -= 0.5f;
			transform.localPosition = temp;
			Quaternion tempR = transform.localRotation;
			tempR.y = 180;
			transform.localRotation = tempR;
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			Vector3 temp = transform.localPosition;
			temp.z *= -1;
			//temp.y -= 0.5f;
			transform.localPosition = temp;
			Quaternion tempR = transform.localRotation;
			tempR.y = 0;
			transform.localRotation = tempR;
			//transform.localEulerAngles.y = 0;
		}
	}
}
