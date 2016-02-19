using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class test : MonoBehaviour {


	private GameObject goPadreElementos;
	public float fTamañoGrilla;
	public float fAnchoDeUnCuadro,fAltoDeUnCuadro; //tamaño de un cuadro de la grilla
	private float fPDistanciaEntreRectas;
	private float fProporcionEnX;
	private float fAnguloIsometrico = 35;
	// Use this for initialization
	
	// Declarar y valores de los elementos array establecidos 
	public Vector3 [,] multiDimensionalArray2 ;


	private string sNombreElemento;
	private float sPosX;
	private float sPosY;
	private int iEstado;
	private int iOrientacion;
	private int count;

	public bool bGenerarMapa = false;
	public bool bGenerarObjeto = false;
	public Vector2 CordenadasXY;
	public int selecionarObjeto = 1;

	//public string sNombreElemento2;

	// Use this for initialization
	void Start(){

		StartCoroutine(Sesion("ebf357f1c4a628078c83187135f1e562"));
		goPadreElementos = Instantiate(Resources.Load("Terreno/Terreno", typeof(GameObject))) as GameObject;
		// inicializacion del array
		multiDimensionalArray2 = new Vector3[(int)(fTamañoGrilla*fTamañoGrilla), (int)(fTamañoGrilla*fTamañoGrilla)];
		//GameObject goElementosInte;
		//goElementosInte = Instantiate(Resources.Load("Terreno/"+sNombreElemento2, typeof(GameObject))) as GameObject;
		fProporcionEnX =(fAnchoDeUnCuadro/200);
		fPDistanciaEntreRectas = fAltoDeUnCuadro/100;
		LLenarMatriz ();

	}

	// Update is called once per frame
	void Update () {

		if(bGenerarMapa){
			GameObject gridPlane = null;
			float fNumeroAleatorio;
			for(int j=0 ; j< fTamañoGrilla; j++){
				for(int i=0 ; i< fTamañoGrilla; i++){
					
					fNumeroAleatorio = Random.Range(1,10);
					if(fNumeroAleatorio<=5){
						gridPlane = Instantiate(Resources.Load("Terreno/objetos_1 (1)", typeof(GameObject))) as GameObject;
						gridPlane.transform.parent = goPadreElementos.transform;
					}
					else{
						gridPlane = Instantiate(Resources.Load("Terreno/objetos_1 (1)", typeof(GameObject))) as GameObject;
						gridPlane.transform.parent = goPadreElementos.transform;
					}
					PosicionarObjeto (gridPlane,(int) j, i);
				}
			}
			bGenerarMapa = false;
		}

		if(bGenerarObjeto){
			
			GameObject gridPlane = null;
			if(selecionarObjeto == 1) gridPlane = Instantiate(Resources.Load("Terreno/objetos_1 (1)", typeof(GameObject))) as GameObject;
			else if(selecionarObjeto == 2) gridPlane = Instantiate(Resources.Load("Terreno/objetos_1", typeof(GameObject))) as GameObject;
			else if (selecionarObjeto == 3) gridPlane = Instantiate(Resources.Load("Terreno/isometricop_2", typeof(GameObject))) as GameObject;
			PosicionarObjeto (gridPlane,(int) CordenadasXY.x, (int)CordenadasXY.y);
			bGenerarObjeto = false;
		}
	
	}

	public IEnumerator Sesion (string llave) {

		string url = "http://127.0.0.1/egigantes/api/obtenerDatosJuegoUsuario";

		Dictionary<string, string> openWith = new Dictionary<string, string>();

		openWith.Add("llave", llave);

		WWW www = new WWW(url,null,openWith);
		yield return www;
		JSONObject j = new JSONObject (www.text);
		GuardarDatos (j);
		//Debug.Log(www.text);

	}


	void GuardarDatos(JSONObject JSON)
	{
	
		switch(JSON.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < JSON.list.Count; i++){
				string key = (string)JSON.keys[i];
				JSONObject j = (JSONObject)JSON.list[i];
				//Debug.Log(key);
				GuardarDatos (j);
				if(key.ToString() == "NombreNivel")
				{
					//Debug.Log(key+":"+j.str);
					//string sLlave = j.str;
				}
				if(key.ToString() == "NombreEscena")
				{
					//Debug.Log(key+":"+j.str);
					//string sLlave = j.str;
				}
				if(key.ToString() == "PosX")
				{
					//Debug.Log(key+":"+j.n);
					sPosX = j.n;
				}
				if(key.ToString() == "PosY")
				{
					//Debug.Log(key+":"+j.n);
					sPosY = j.n;
				}
				if(key.ToString() == "Id_Estado")
				{
					//Debug.Log(key+":"+j.i);
					iEstado = (int)j.n;
				}
				if(key.ToString() == "Id_Orientacion")
				{
					//Debug.Log(key+":"+j.i);
					iOrientacion = (int)j.n;
				}

				if(i==1)
				{
					if(key.ToString() == "NombreElemento")
					{
						sNombreElemento = j.str;
						count+=1;
					}
				}
			}
			break;
		case JSONObject.Type.ARRAY:
			
			foreach(JSONObject j in JSON.list)
			{
//				sPosX = JSON.list[0]["PosX"].n;
//				sPosY = JSON.list[0]["PosY"].n;
				GuardarDatos(j);
				if(count > 0)
				{
				//Debug.Log(sPosX+":"+sPosY+":"+sNombreElemento+":"+iEstado+":"+iOrientacion);
			    Intanciamiento(sPosX,sPosY,iOrientacion,iEstado,sNombreElemento);
				}
			}
			
			break;
//		case JSONObject.Type.STRING:
//			if(JSON.str.ToString() == "llave"){
//				Debug.Log(JSON.str);}
//			break;
		}
	}


	void Intanciamiento(float fPosX, float fPosY, int sOrientacion, int sEstado, string sNombreElemento)
	{
		GameObject goElementosInte = null;
		goElementosInte = Instantiate(Resources.Load("Terreno/"+sNombreElemento, typeof(GameObject))) as GameObject;
		goElementosInte.transform.parent = goPadreElementos.transform;
		PosicionarObjeto (goElementosInte,(int) fPosX, (int)fPosY);
//		goElementosInte.AddComponent<EstadosPersonaje>();
//		EstadosPersonaje estadosPersonaje;
//		estadosPersonaje = goElementosInte.GetComponent<EstadosPersonaje>();
//		estadosPersonaje.iEstado = sEstado;
//		estadosPersonaje.iOrientacion = sNombreElemento;
		
	}
	
	void PosicionarObjeto(GameObject objeto,int x,int y){
		
		if (x > (fTamañoGrilla-1) || y > (fTamañoGrilla-1))Debug.Log ("Error En El ingreso De parametros (PosicionarObjeto) ");
		else{
			objeto.transform.position =  multiDimensionalArray2 [x, y];
			Debug.Log("vl "+ multiDimensionalArray2[x,y]);}
	}
	
	void LLenarMatriz(){
		float fPosicionEnY;
		float fPosicionEnX;
		float fPosicionEnZ = -fTamañoGrilla;
		float fDistanciaEntreRectas = 0;
		float fAumentoEnX = 0;
		float fFinAumentoEnX = fTamañoGrilla;
		
		for (int j = 0; j < fTamañoGrilla; j++) {
			int iGuardarDato = (int)(fTamañoGrilla-1);
			for (float i = fAumentoEnX; i < fFinAumentoEnX; i++) {
				
				fPosicionEnX = i*fProporcionEnX;
				fPosicionEnY = (Mathf.Sin(fAnguloIsometrico*Mathf.Deg2Rad) * fPosicionEnX) - fDistanciaEntreRectas;
				multiDimensionalArray2 [j,iGuardarDato] = new Vector3(fPosicionEnX, fPosicionEnY,fPosicionEnZ);
				fPosicionEnZ++;
				iGuardarDato--;
				//Debug.Log ("multifimenionlna "+ j + iGuardarDato + multiDimensionalArray2 [j,iGuardarDato]);
				//Debug.Log ("x " + fPosicionEnX + "  y " + fPosicionEnY + " z "+ fPosicionEnZ);
				//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//gridPlane = Instantiate(Resources.Load("Terreno/objetos_1 (4)", typeof(GameObject))) as GameObject;
				//gridPlane.transform.position = new Vector3(fPosicionEnX, fPosicionEnY, fPosicionEnZ);
			}
			
			fAumentoEnX++;
			fFinAumentoEnX++;
			fPosicionEnZ = -fTamañoGrilla - (j+1);
			fDistanciaEntreRectas = fDistanciaEntreRectas + fPDistanciaEntreRectas;
		}
	}
}
