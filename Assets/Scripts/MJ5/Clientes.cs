using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Clientes : MonoBehaviour {

	public float fVelocidadMoviento;
	public float fX;
	public Text tColor;
	public Text tTamaño;
	public static string sColor;
	public static string sTamaño;

	private float fXNormal;

	// Use this for initialization
	void Start () {

		fXNormal = fX;
	
	}
	
	// Update is called once per frame
	void Update () {

		motion (fX,0);

	
	}

	void motion(float x ,float y){

		this.transform.Translate (new Vector3 (x*Time.deltaTime*fVelocidadMoviento, y*Time.deltaTime*fVelocidadMoviento, 0));

	}

	void OnTouchStay()
	{
		Debug.Log ("sii");
		tColor.text = sColor;
		tTamaño.text = sTamaño;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.name == "2")
		{
			fX = 0;
		}
	}
	void OnCollisionExit2D(Collision2D other)
	{
		if(other.gameObject.name == "2")
		{
			fX = fXNormal;
		}
	}
}
