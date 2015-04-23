using UnityEngine;
using System.Collections;

public class stop_rotation : MonoBehaviour {
	Quaternion rotation;
	// Use this for initialization
	void Start () {
		 rotation = transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = rotation;
	}
}
