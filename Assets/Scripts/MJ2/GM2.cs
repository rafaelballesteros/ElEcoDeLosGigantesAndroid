using UnityEngine;
using System.Collections;

public class GM2 : MonoBehaviour {

	private float fTamañoFruto = 0.25f;
	private bool bInstanciar = true, b = false;
	private float fTiempoInstanciar;
	private float fTiempoJuego;
	private string sNombreFruto;
	private string sOrientacionFruto;
	private GameObject sfBandaDes;
	private float X;
	private float Y;
	public int count = 0;

	public float fVelocidadInst;
	public float tamañoFrutaPeque;
	public float tamañoFrutaGrande;
	public int numeroDeFrutas;

	public int iAciertos;
	public int iDesaciertos;
	public int iRestaPuntos;
	public static int iRestarPuntos;
	// Use this for initialization
	void Start () {

		//fVelocidadInst = 2;
		sfBandaDes = GameObject.Find("sintaTransportadora 2");
		Y = Screen.height;
		X = Screen.width;
	
	}
	
	// Update is called once per frame
	void Update () {


		iRestarPuntos = iRestaPuntos;
		iAciertos = Frutas.iAciertos;
		iDesaciertos = Frutas.iDesaciertos;

		fTiempoJuego += Time.deltaTime;
		fTiempoInstanciar += Time.deltaTime;

		if(fTiempoInstanciar > fVelocidadInst)
		{
			if (bInstanciar)
			{
				int inombreFruto;
				inombreFruto = Random.Range (1,numeroDeFrutas);
				Frutos (inombreFruto,TamañoFruto());
				fTiempoJuego = fTiempoJuego;

				if(fTiempoJuego >= 32)
				{
					fTiempoJuego = 0; 
					fVelocidadInst--;
					sfBandaDes.gameObject.GetComponent<SurfaceEffector2D>().speed = sfBandaDes.gameObject.GetComponent<SurfaceEffector2D>().speed + 0.12f;
					count++;
				}
				Debug.Log (fTiempoJuego);
				fTiempoInstanciar = 0;
			}
		}
		if(count == 5)
		{
			bInstanciar = false;
			b = true;
			count = 6;
		}

	}

	void Frutos(int fsNombreFruto, float ffTamaño){

		GameObject goFruto = null;
		goFruto = Instantiate(Resources.Load("Objetos/Frutas/"+"fruta"+fsNombreFruto.ToString(), typeof(GameObject))) as GameObject;
		goFruto.transform.localScale =  new Vector3(ffTamaño,ffTamaño,ffTamaño);
		OrientacionFruto ();
		Frutas orientacion;
		orientacion = goFruto.GetComponent<Frutas> ();
		if (fsNombreFruto == 11) {
			orientacion.sFrutaPicha = "FrutaPicha";
			orientacion.sTipoOrientacion = "";
		}
		else orientacion.sTipoOrientacion = sOrientacionFruto;

	}

	void OrientacionFruto()
	{
		int OrientacionFruto;

		OrientacionFruto = Random.Range (1, 11);
		if(OrientacionFruto > 5) sOrientacionFruto = "Arriba";
		else if(OrientacionFruto <= 5)sOrientacionFruto = "Abajo";
	}

	private float TamañoFruto()
	{
		float iTamañoFruto;

		iTamañoFruto = Random.Range (1, 11);
		if(iTamañoFruto > 5)iTamañoFruto = tamañoFrutaPeque;
		else if(iTamañoFruto <= 5)iTamañoFruto = tamañoFrutaGrande;

		return iTamañoFruto;
	}

	void OnGUI()
	{
		if (b) 
		{
			GUI.Box(new Rect(X * 0.25f, Y * 0.25f,X * 0.50f, Y * 0.60f), "Login");

			if(GUI.Button(new Rect(X * 0.3125f, Y * 0.35f, X * 0.175f, Y * 0.4f), "Reanudar")) {

				count = 0;
				fVelocidadInst = 5;
				sfBandaDes.gameObject.GetComponent<SurfaceEffector2D> ().speed = 1;
				b = false;
				bInstanciar = true;

			}
			if(GUI.Button(new Rect(X * 0.5125f, Y * 0.35f, X * 0.175f, Y * 0.4f), "Volver")) {


				b = false;
			}
		}
	}
}
