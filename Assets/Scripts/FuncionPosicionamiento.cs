using UnityEngine;
using System.Collections;

public class FuncionPosicionamiento : MonoBehaviour {


	public float fTamañoGrilla;
	public float fAnchoDeUnCuadro,fAltoDeUnCuadro; //tamaño de un cuadro de la grilla
	private float fPDistanciaEntreRectas;
	private float fProporcionEnX;
	public float fAnguloIsometrico;
	// Use this for initialization

	// Declarar y valores de los elementos array establecidos 
	public Vector3 [,] multiDimensionalArray2 ;


	public bool bGenerarMapa = false;
	public bool bGenerarObjeto = false;
	public Vector2 CordenadasXY;
	public int selecionarObjeto = 1;
	void Start () {
		// inicializacion del array

		multiDimensionalArray2 = new Vector3[(int)(fTamañoGrilla*fTamañoGrilla), (int)(fTamañoGrilla*fTamañoGrilla)];


		fProporcionEnX =(fAnchoDeUnCuadro/200);
		fPDistanciaEntreRectas = fAltoDeUnCuadro/100;
		LLenarMatriz ();

	}
	
	// Update is called once per frame
	void Update () {
		
	//	PosicionObjeto (gridPlane,0,0);

		// generadores de prueba
		if(bGenerarObjeto){
			
			GameObject gridPlane = null;
			if(selecionarObjeto == 1) gridPlane = Instantiate(Resources.Load("Terreno/objetos_1", typeof(GameObject))) as GameObject;
			else if(selecionarObjeto == 2) gridPlane = Instantiate(Resources.Load("Terreno/objetos_1 (1)", typeof(GameObject))) as GameObject;
			else if (selecionarObjeto == 3) gridPlane = Instantiate(Resources.Load("Terreno/objetos_3", typeof(GameObject))) as GameObject;


			PosicionarObjeto (gridPlane,(int) CordenadasXY.x, (int)CordenadasXY.y);
			//asignamos estado y orientacion al personaje
// 			gridPlane.AddComponent<EstadosPersonaje>();
//			EstadosPersonaje estadosPersonaje;
//			estadosPersonaje = gridPlane.GetComponent<EstadosPersonaje>();
//			estadosPersonaje.iEstado = 1;
//			estadosPersonaje.iOrientacion =4;




			bGenerarObjeto = false;
		}

		if(bGenerarMapa){
			GameObject gridPlane = null;
			float fNumeroAleatorio;
			for(int j=0 ; j< fTamañoGrilla; j++){

			for(int i=0 ; i< fTamañoGrilla; i++){

					fNumeroAleatorio = Random.Range(1,10);
					if(fNumeroAleatorio<=5){

						gridPlane = Instantiate(Resources.Load("Terreno/isometrico300", typeof(GameObject))) as GameObject;

					}
					else{

						gridPlane = Instantiate(Resources.Load("Terreno/isometrico300", typeof(GameObject))) as GameObject;
					}

					PosicionarObjeto (gridPlane,(int) j, i);
			
			}

			
		}
			bGenerarMapa = false;
		}




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
				//Debug.Log ("multifimenionlna "+ j + iGuardarDato + multiDimensionalArray2 [j,iGuardarDato]);
				iGuardarDato--;
				//Debug.Log ("x " + fPosicionEnX + "  y " + fPosicionEnY + " z "+ fPosicionEnZ);
				//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//				gridPlane = Instantiate(Resources.Load("Terreno/objetos_1 (4)", typeof(GameObject))) as GameObject;
				//				gridPlane.transform.position = new Vector3(fPosicionEnX, fPosicionEnY, fPosicionEnZ);
			}

			fAumentoEnX++;
			fFinAumentoEnX++;
			fPosicionEnZ = -fTamañoGrilla - (j+1);
			fDistanciaEntreRectas = fDistanciaEntreRectas + fPDistanciaEntreRectas;
		}
	}



}
