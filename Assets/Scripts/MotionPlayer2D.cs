using UnityEngine;
using System.Collections;

public class MotionPlayer2D : MonoBehaviour {

	// Use this for initialization
	private float rayCastDisntance = 500.0f; // distancia del rayo.
	public LayerMask layerMask = 1<<7; // mascara con la que hara collision.
	private Vector3 currentMoveToPos; // la posicion del click, a la cual el jugador debe ir.

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if ((Input.GetAxis("Fire1")>0 || Input.GetAxis("Fire2")>0) )
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit,rayCastDisntance, layerMask)) 
			{
				currentMoveToPos = hit.point;			
			}
			//buttonDown = true;
		}
	
	}
}
