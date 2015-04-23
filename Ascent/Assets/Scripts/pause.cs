﻿
using System.Collections;
using System;
using UnityEngine;

public class pause : MonoBehaviour
{
	bool paused = false;
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			paused = togglePause ();
				}
	}
	void OnGUI()
	{
		if(paused)
		{
			GUILayout.Label("Game is paused!");
			if(GUILayout.Button("Click me to unpause"))
				paused = togglePause();
		}
	}
	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);
		}
	}
}
