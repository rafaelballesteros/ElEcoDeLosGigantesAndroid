using UnityEngine;
using System.Collections;

public class Frutas : MonoBehaviour {

	public string sTipoOrientacion;
	public GameObject spFruto;
	public GameObject spFlecha;
	public Sprite sTextura;
	public Sprite sTexturaFlecha;
	SpriteRenderer algo;	
	public static int iAciertos;
	public static int iDesaciertos;
	public string sFrutaPicha;

	private float fposicionMouse;
	private Rigidbody2D rbObjeto;
	private bool bToque = false, bCinta = false;

	// Use this for initialization
	void Start () {

		spFruto = GameObject.Find ("TableroFruta");
		spFlecha = GameObject.Find ("Flecha");
		rbObjeto = GetComponent<Rigidbody2D>();
		fposicionMouse = Camera.main.farClipPlane - Camera.main.nearClipPlane;

	}
	
	// Update is called once per frame
	void Update () {
		
		if(bToque && bCinta == false)
		{
			Vector3 vPosicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position =  new Vector3(vPosicionMouse.x,vPosicionMouse.y,transform.position.z);
		}
		if (Input.GetMouseButtonUp (0))
		{
			bToque=false;
		}
	}

	void OnTouchStay(){
		
		//Debug.Log("toco");
		bToque=true;
		if(bToque && bCinta == false){
			spFruto.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
			if(sTipoOrientacion == "Arriba"){spFlecha.gameObject.GetComponent<SpriteRenderer>().sprite = sTexturaFlecha;}
			else
			{
				spFlecha.gameObject.GetComponent<SpriteRenderer>().sprite = sTexturaFlecha;
				spFlecha.transform.Rotate(new Vector3(0,0,-180));
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.name == "basket" && sTipoOrientacion == "Arriba" || other.gameObject.name == "basket (2)" && sTipoOrientacion == "Abajo") {

			Destroy (this.gameObject, .1f);
			iAciertos++;
		} 

		else if (other.gameObject.name == "basket" && sTipoOrientacion == "Abajo" || other.gameObject.name == "basket (2)" && sTipoOrientacion == "Arriba") {

			Destroy (this.gameObject, .1f);
			iDesaciertos++;
		} 

		if ((other.gameObject.name != "PuntoDelMouse"  && sFrutaPicha == "FrutaPicha") ) {

			Destroy (this.gameObject, .1f);
			iAciertos = iAciertos - GM2.iRestarPuntos;
		}

		if (other.gameObject.name == "basket (1)") {

			Debug.Log ("Mal");
			Destroy (this.gameObject, .1f);
		}
		
	}

}
