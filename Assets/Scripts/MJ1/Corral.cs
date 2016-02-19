using UnityEngine;
using System.Collections;

public class Corral : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerStay2D(Collider2D other)
	{
		Vacas Vaca = other.gameObject.GetComponent<Vacas>();
		if(other.gameObject.tag == "Vaca" ){
			if(this.gameObject.tag == "CorralPasto")Vaca.bVacaEnElPasto = true;
			else if(this.gameObject.tag == "CorralTierra")Vaca.bVacaEnElPasto = false;
		}
	}
}
