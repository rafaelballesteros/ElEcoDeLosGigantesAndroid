using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GM1 : MonoBehaviour {

	//movimiento dialogo

	public GameObject goCanvasDialogo;
	float fPosicionCanvas ;
	public float fVelocidad;
	private float fminimaVelocidad;
	private float fRestaVelocida;
	bool llego =false;
	public bool bMostrarDialogo;

	// texto dialogos

	private string[] list_Text = {"At last, someone came to help \nme! :@ ..."
		,"I need help with my cowrses \nwhat is a cowrse? you say"
		,"well, a cowrse is an animal \nand that's it... I need a favor..."
		,"some of my cowrses have\n can you put them \ninto the barn?"
		,"keep the left button presseed\n and realase it when you reach \nthe barn?"
		,"well, well... it seems the boy \n knowa how to do somenthing, \n at leas! th.. than... thanks."
		, "I need you to do what you\n did, again. A barn is still\n missing "
		,"Make things complete or do \nnothing! Stop being lazy!"
		,"Well, it seems you're good at \nthis. I will recommend you to\n my friends ... Maybe..."
		,"Now go bakc to my brother.I \ndo not want to see you in my\npropiety... Out!"};

	private int countText = 0;
	public Text text;

	// tamaño de las vacas

	public float fTamañoVacasPequeñas;
	public float fTamañoVacasGrandes;
	private GameObject goContenedorVacas;

	// dimensiones del corral

	public float fTamañoCorralMinX;
	public float fTamañoCorralMaxX;
	public float fTamañoCorralMinY;
	public float fTamañoCorralMaxY;

	//tamaño Pasto
	public float fScalaPasto;
	public float fSeparacionPasto;
	public LayerMask lMascaraColision;
	RaycastHit2D rHit2;
	private GameObject goContenedorPasto;


	// mapas del juego vacas
	public GameObject[] goMapas = new GameObject[3];

	GameObject goMapaCreado;
	public bool bSubirDeNivel;
	public int Nivel;



	//variables mapa

	public Vector4[] v4PuntosLimitesPasto;
	public Vector4[] v4PuntosLimitesTierra ;
	public Transform[] tTransformHijo;


	// Use this for initialization
	void Start () {
		Nivel = 0;
		bSubirDeNivel = true;

		GenerarNivel(bSubirDeNivel);

		fminimaVelocidad = fVelocidad / 4;
		fRestaVelocida = fVelocidad / 20;
		fPosicionCanvas = goCanvasDialogo.transform.position.x;
		bMostrarDialogo = true;
		text.text = list_Text[0];

		//generamos el contenedor de todo el pasto


	


	}
	
	// Update is called once per frame
	void Update () {

		//Funcion para Cambiar De Nivel
		GenerarNivel(bSubirDeNivel);


		//funcion para mover el dialogo
		MostrarCanvas (bMostrarDialogo);


	}


	//Boton del  dialogo
	public void BotonUI()
	{
		

		countText++;

		if (countText == 5) bMostrarDialogo = false;
		
		else text.text = list_Text[countText];

	}

	public void MostrarCanvas(bool bMosDial){

		if (bMosDial) {

			if (fPosicionCanvas < 2 && llego == false) {

				fPosicionCanvas = goCanvasDialogo.transform.position.x + fVelocidad;
				goCanvasDialogo.transform.position = new Vector3 (fPosicionCanvas, goCanvasDialogo.transform.position.y, goCanvasDialogo.transform.position.z);
			} else if (fPosicionCanvas >= 0.1) {
				float fVelocidadInterna = 0;
				if (fVelocidad > fminimaVelocidad) {

					fVelocidadInterna = fVelocidad - fRestaVelocida;
				}
				llego = true;
				fPosicionCanvas = goCanvasDialogo.transform.position.x - fVelocidadInterna;
				goCanvasDialogo.transform.position = new Vector3 (fPosicionCanvas, goCanvasDialogo.transform.position.y, goCanvasDialogo.transform.position.z);
			}
		}
		else {

			if (fPosicionCanvas > -20) {
				fPosicionCanvas = goCanvasDialogo.transform.position.x - fVelocidad;
				goCanvasDialogo.transform.position = new Vector3 (fPosicionCanvas, goCanvasDialogo.transform.position.y, goCanvasDialogo.transform.position.z);
			
			} else {
				if (llego) {
					
					text.text = list_Text[countText];
					llego = false;
				}
			}
		}
	
	
	}

	void GenerarVacas(int iCantidadVacas, bool btamañoVaca,bool bsalirseCorral , float fVelocidadVacas, bool bVacaHuyendo )
	{
		if(goContenedorVacas != null)Destroy(goContenedorVacas);
		goContenedorVacas = new GameObject("ContenedorVacas");


		for(int i = 0; i < iCantidadVacas; i++)
		{
			GameObject goVacas;
			goVacas = Instantiate(Resources.Load("Objetos/MJ1/vaca", typeof(GameObject))) as GameObject;
			goVacas.transform.parent = goContenedorVacas.transform;
			//goVacas.transform.position = new Vector3 (Random.Range(fTamañoCorralMinX,fTamañoCorralMaxX),Random.Range(fTamañoCorralMinY,fTamañoCorralMaxY),-1);
			//selecionamos en que parte del pasto queremos que abaresca la vaca
			int iSecionPasto = Random.Range(0,v4PuntosLimitesPasto.Length);
			goVacas.transform.position =  new Vector3 (Random.Range(v4PuntosLimitesPasto[iSecionPasto].x,v4PuntosLimitesPasto[iSecionPasto].z),Random.Range(v4PuntosLimitesPasto[iSecionPasto].y,v4PuntosLimitesPasto[iSecionPasto].w),-1);

			//asignamos tamaño a la vaca
			if(btamañoVaca)
			{
				float random = Random.Range (1,11);
				if (random > 5) {
					random = fTamañoVacasGrandes;
				} else
					random = fTamañoVacasPequeñas;

				goVacas.transform.localScale = new Vector2 (random,random);
			}

			//asignamos la obcion de salirse del corral
			Vacas vacas = goVacas.GetComponent<Vacas>();
			vacas.bSalirmeDelCorral = bsalirseCorral;
			vacas.fMoveSpeed = fVelocidadVacas;
			vacas.bVacaHuir = bVacaHuyendo;
			vacas.fApetitoVaca = Random.Range (20,100);
			vacas.fRapidezAlComer = Random.Range (2,10);
			vacas.fTiempoAmbre = Random.Range (1,20);
			vacas.fPuntosPorAlimento = Random.Range (1, 10);
			vacas.fTamañoCorralMaxX = fTamañoCorralMaxX;
			vacas.fTamañoCorralMinX = fTamañoCorralMinX;
			vacas.fTamañoCorralMaxY = fTamañoCorralMaxY;
			vacas.fTamañoCorralMinY = fTamañoCorralMinY;
			vacas.v4PuntosLimitesPasto = v4PuntosLimitesPasto;
			vacas.v4PuntosLimitesTierra = v4PuntosLimitesTierra;

		}
			
	}

	void GenerarPasto(){

	
		if(goContenedorPasto != null)Destroy(goContenedorPasto);//destruimos el pasta anterior para crear uno nuevo
		goContenedorPasto = new GameObject("ContenedorPasto");	//contenedor de todo el pasto del nivel
		int numeroDePastos = Random.Range (5,50);


		for (int i = 1; i < numeroDePastos; i++) 
		{

			//generamos un cuadro de pasto
			float randomFila = Random.Range (1,5);
			float randomColumna = Random.Range (1,5);

			//int iSecionPasto = Random.Range(0,v4PuntosLimitesPasto.Length);


			float randomX = Random.Range (fTamañoCorralMinX,fTamañoCorralMaxX);
			float randomY = Random.Range (fTamañoCorralMinY,fTamañoCorralMaxY);
			float dato = randomX+(fSeparacionPasto*randomColumna);
			Debug.Log ("randomX" + randomX + " hasta"+ dato +" randomFila " +randomFila +" randomColumna "+randomColumna);

			for (float x = randomX; x < randomX+(fSeparacionPasto*randomColumna); x+=fSeparacionPasto) 
			{

				for (float y = randomY; y < randomY+(fSeparacionPasto*randomFila); y =y+fSeparacionPasto) 
				{

					if (x < fTamañoCorralMaxX && x > fTamañoCorralMinX && y < fTamañoCorralMaxY && y > fTamañoCorralMinY) {
				

						Debug.DrawRay (new Vector3 (x, y,-1), new Vector2 (x, y), Color.red, 2);
						rHit2 = Physics2D.Raycast(new Vector3 (x, y,-1),new Vector2(x,y),30,lMascaraColision);
						if (rHit2.collider != null && rHit2.collider.tag == "Pasto") {
							//Debug.Log ("choco");


						} else {
							GameObject goPasto;
							goPasto = Instantiate (Resources.Load ("Objetos/MJ1/pasto", typeof(GameObject))) as GameObject;
							goPasto.transform.parent = goContenedorPasto.transform;//asemos  goPasto hijo del contenedor de pasto
							goPasto.transform.position = new Vector2 (x, y);
						}
					}

				}
			}



		}

	
	}


	//funciones para los mapas

	void GenerarMapa(){

		if(goMapaCreado != null)Destroy(goMapaCreado);




		int iCantidadDeMapas = goMapas.Length; //miramos la cantidad de mapas ingresados
		int iRandomMapa = Random.Range(0,iCantidadDeMapas);
		Debug.Log("mapa" +iRandomMapa);


		goMapaCreado = Instantiate (goMapas[iRandomMapa], transform.position, transform.rotation) as GameObject;
		goMapaCreado.transform.position = new Vector3(0,0,1);




	}

	void rotarMapa(GameObject mapa){
		if(mapa != null){
			float fPosicionZ = mapa.transform.localEulerAngles.z;
			if(fPosicionZ != 0) fPosicionZ = 0;
			else fPosicionZ = 180;
			mapa.transform.localEulerAngles = new Vector3 (0,0, fPosicionZ);
		}
	}


	void GenerarNivel(bool CambiarNivel){


		if(CambiarNivel){
			Nivel++;

			int iCantidadDeVacas;
			bool btamañoVacas;
			bool bSalirDeCorral;
			float fVelocidadVaca;
			bool bVacasHuyendo;
		// condiciones de cada nivel de juego
			switch(Nivel){
			case 1:
				iCantidadDeVacas = 3;
				btamañoVacas = false;
				bSalirDeCorral = false;
				bVacasHuyendo = false;
				fVelocidadVaca = Random.Range(0.5f,1);

				break;
			case 2:
				iCantidadDeVacas = 4;
				btamañoVacas = true;
				bSalirDeCorral = false;
				bVacasHuyendo = false;
				fVelocidadVaca = Random.Range(0.8f,1);
				break;
			case 3:
				iCantidadDeVacas = 5;
				btamañoVacas = true;
				bSalirDeCorral = true;
				bVacasHuyendo = false;
				fVelocidadVaca = Random.Range(1,1.5f);

				break;
			case 4:
				iCantidadDeVacas = 6;
				btamañoVacas = true;
				bSalirDeCorral = true;
				bVacasHuyendo = false;
				fVelocidadVaca = Random.Range(1.5f,1.9f);

				break;
			case 5:
				iCantidadDeVacas = 6;
				btamañoVacas = true;
				bSalirDeCorral = true;
				bVacasHuyendo = false;
				fVelocidadVaca = Random.Range(1.9f,2.4f);

				break;
			case 6:
				iCantidadDeVacas = 6;
				btamañoVacas = true;
				bSalirDeCorral = true;
				bVacasHuyendo = true;
				fVelocidadVaca = Random.Range(2.4f,2.8f);

				break;
			
			default:
				iCantidadDeVacas = Random.Range(6,15);
				btamañoVacas = true;
				bSalirDeCorral = true;
				bVacasHuyendo = true;
				fVelocidadVaca = Random.Range(2.8f,3.6f);

				break;


			}

		GenerarMapa();

		EscanearMapa();

		GenerarPasto ();

		GenerarVacas (iCantidadDeVacas,btamañoVacas,bSalirDeCorral,fVelocidadVaca,bVacasHuyendo);

		bSubirDeNivel = false;

		}


	}


	void EscanearMapa(){


		//escaneamos "n" hijos del mapa y los guardamos en un vector

		tTransformHijo = new Transform[goMapaCreado.transform.childCount];
		int iContadoPasto = 0,iContadorTierra = 0;


		//Calculamos el tamaño de cada vector pasto y tierra
		int iPas = 0,iTie = 0;
		foreach (Transform child in goMapaCreado.transform) {
			if(child.tag == "CorralPasto")  iPas++;
			else if(child.tag == "CorralTierra") iTie++;
		}
		v4PuntosLimitesPasto = new Vector4[iPas];
		v4PuntosLimitesTierra = new Vector4[iTie];

		foreach (Transform child in goMapaCreado.transform) {


				//tTransformHijo[i++] = child;
			//lemos el componente BoxCollider2D del hijo
			BoxCollider2D bBoxCollider2D = child.GetComponent<BoxCollider2D>();
			//obtenemos los limites de cada hijo 
			Vector4 LimitesObjeto = new Vector4(bBoxCollider2D.bounds.min.x,bBoxCollider2D.bounds.min.y,bBoxCollider2D.bounds.max.x,bBoxCollider2D.bounds.max.y);

			if(child.tag == "CorralPasto"){v4PuntosLimitesPasto[iContadoPasto++] = LimitesObjeto;}
			else if(child.tag == "CorralTierra"){v4PuntosLimitesTierra[iContadorTierra++] = LimitesObjeto;}

			//volvemos los colider triger para que las vacas no colisionen con el piso

			bBoxCollider2D.isTrigger = true;
		

	  }


	
	





		Debug.Log("Mapa Escaneado");
	}





}
