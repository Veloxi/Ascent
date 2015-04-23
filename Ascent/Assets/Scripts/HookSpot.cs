using UnityEngine;
using System.Collections;

public class HookSpot : MonoBehaviour {
	public bool canGrapple;
	public Vector3 grappleLoc;
	public Transform lineCheckStart, lineCheckEnd;
	public bool lineCheck;
	// Use this for initialization
	void Start () {
		grappleLoc = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (lineCheckStart.position, lineCheckEnd.position);
		lineCheck = Physics2D.Linecast (lineCheckStart.position, lineCheckEnd.position);
	}
	void OnTriggerEnter2D(Collider2D target){
		if (!lineCheck && target.gameObject.tag == "grappleSpot") {
			canGrapple = true;
			grappleLoc = target.transform.position;
		}// else {
		//	canGrapple = false;
		//}
	}
	void OnTriggerExit2D(Collider2D target){
		if (target.gameObject.tag == "grappleSpot") {
			canGrapple = false;
		}
	}
}
