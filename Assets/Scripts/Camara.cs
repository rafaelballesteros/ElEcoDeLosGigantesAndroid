using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour {


	public GameObject goTarget;
	private Vector3 vOffset;

	// Use this for initialization
	void Start () {

		// buscamos el player en la escena

		goTarget = GameObject.Find("Player_Eco(Clone)");
		vOffset = transform.position - goTarget.transform.position;
	}

	// Update is called once per frame
	void Update () {

		//la posicion del player es igual a la posicion de la camara 

		Vector3 diseredPosition = goTarget.transform.position + vOffset;
		transform.position = diseredPosition;
	}
}
