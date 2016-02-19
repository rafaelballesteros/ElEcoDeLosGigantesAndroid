using UnityEngine;
using System.Collections;
public class Inicio : MonoBehaviour {

	private float X;
	private float Y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Y = Screen.height;
		X = Screen.width;
	
	}

	void OnGUI()
	{
			
		if(GUI.Button(new Rect(X * 0.4f, Y * 0.5f, X * 0.175f, Y * 0.08f), "Jugar")|| Input.GetKeyDown("enter") == true) {

			Application.LoadLevel("Login");
		}
	}
}
