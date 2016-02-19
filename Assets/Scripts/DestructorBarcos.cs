using UnityEngine;
using System.Collections;

public class DestructorBarcos : MonoBehaviour {

	public GameObject gManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Barco" )
		{
			Debug.Log("Destruir");

			GM4 gameManagerBarcos = gManager.GetComponent<GM4>();
			Barcos movimiento_Canoas = other.gameObject.GetComponent<Barcos>();
			if(movimiento_Canoas.bPuertoCorrecto)gameManagerBarcos.iPuntosBuenos++;
			else gameManagerBarcos.iPuntosMalos++;

			Destroy(other.gameObject);
		

		}
}


}