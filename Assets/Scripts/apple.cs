using UnityEngine;
using System.Collections;

public class apple : MonoBehaviour {

//	public  float fvelocidadMovimiento;
//	public float fvelocidadRotacion;
	public string sTipoOrientacion;
	public GameObject spFruto;
	public Sprite sTextura;
	SpriteRenderer algo;	

	private float fposicionMouse;
	private Rigidbody2D rbObjeto;

	// Use this for initialization
	void Start () {

		rbObjeto = GetComponent<Rigidbody2D>();
		fposicionMouse = Camera.main.farClipPlane - Camera.main.nearClipPlane;

	}
	
	// Update is called once per frame
	void Update () {
		
//		if (bmovimiento) {
//
//			rbObjeto.velocity = new Vector2 (fvelocidadMovimiento,0);
//			transform.Rotate (Vector3.forward * fvelocidadRotacion, Space.World);
//		}


	}

	void OnTouchStay(){

		if (Input.GetMouseButton(0)) {
			Debug.Log("toco");

			Ray theLine = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 newPos = theLine.GetPoint(fposicionMouse);			
			Vector3 temp = transform.position;			
			temp.x = newPos.x;
			temp.y = newPos.y;
			transform.position = temp;


//			Tablero cosa;
//			cosa = spFruto.GetComponent<Tablero> ();
//			cosa.Objetoss =   this.gameObject;
			//tablero.spritek = this.gameObject.GetComponent<SpriteRenderer>().sprite;

			Debug.Log ("cREAR");
			//this.gameObject.GetComponent<SpriteRenderer>().sprite = sTextura; // we are accessing the SpriteRenderer that is attached to the Gameobject
			//spFruto.gameObject.GetComponent<SpriteRenderer>().sprite = sTextura;
			//if (algo.sprite == null) // if the sprite on spriteRenderer is null then
			//algo = sTextura; // set the sprite to sprite1
			
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.name == "basket" && sTipoOrientacion == "Arriba") {

			Debug.Log ("Bien");
			Destroy (this.gameObject, .1f);
		} else if (other.gameObject.name == "basket (1)" && sTipoOrientacion == "Abajo") {

			Debug.Log ("Bien");
			Destroy (this.gameObject, .1f);
		} else {

			Debug.Log ("Mal");
			Destroy (this.gameObject, .1f);
		}
		
	}

}
