using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {
	private Vector3 pos;
	public HookSpot hookSpot;
	// Use this for initialization
	//public int count = 0;

	//ASHLEYS COMP

	void Start () {
		//pos = hookSpot.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.Lerp(this.transform.position, hookSpot.grappleLoc, Time.deltaTime * 10f);
	}
	void OnEnable(){
		pos = hookSpot.transform.position;
	}
}
