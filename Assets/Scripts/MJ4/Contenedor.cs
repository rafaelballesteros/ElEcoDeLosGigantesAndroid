using UnityEngine;
using System.Collections;

public class Contenedor : MonoBehaviour {

	public bool bContenedorlleno;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Barco" )
		{

			//Debug.Log("contenedor vacio");
			if(bContenedorlleno)bContenedorlleno = false;

		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Barco" )
		{


			//Debug.Log("contenedor lleno");
			if(bContenedorlleno == false)bContenedorlleno = true;

		}
	}




}
