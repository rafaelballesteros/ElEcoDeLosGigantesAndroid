using UnityEngine;
using System.Collections;

public class GM4 : MonoBehaviour {
	public GameObject gCanoa1, gCanoa2;
	GameObject gContenedor,gDestructor;
	public Vector2 vPosicionContenedor = new Vector2(5.4f,0),vPosicionDestructor = new Vector2(-12.5f,0);
	public Vector2 vEscalaContenedor = new Vector2(5.1f,15),vEscalaDestructor = new Vector2(5.1f,15);


	public float fTiempoDeJuego;
	public float fTiempoTrascurrido;


	public float fTiempoAumentoVelocidad;
	private float ftiempoTrascurridoAV;
	public bool  bJuegoTerminado;
	private Contenedor contenedor; 


	public float fVelocidadBarcos = 2;
	public float fTamañoCanoaPequeña,fTamañoCanoaGrande;

	public int iPuntosBuenos = 0,iPuntosMalos = 0;
	public int barcosCreados = 0;


	// Use this for initialization
	void Start () {


		gContenedor = new GameObject("Contenedor");
		gContenedor.tag = "Contenedor";
		gContenedor.transform.position = vPosicionContenedor;
		gContenedor.transform.localScale = vEscalaContenedor;
		BoxCollider2D bBoxCollider = gContenedor.AddComponent<BoxCollider2D>();
		contenedor = gContenedor.AddComponent<Contenedor>();
		contenedor.bContenedorlleno = false;

	
	


//		gDestructor = new GameObject("DestructorBarcos");
//		gDestructor.tag = "Finish";
//		gDestructor.transform.position = vPosicionDestructor;
//		gDestructor.transform.localScale = vEscalaDestructor;
//		BoxCollider2D bBoxColli = gDestructor.AddComponent<BoxCollider2D>();
//		bBoxColli.isTrigger = true;
//
//		gDestructor.AddComponent<DestructorBarcos>();

		//gridPlane = Instantiate(Resources.Load("Terreno/objetos_1", typeof(GameObject))) as GameObject;

		fTiempoTrascurrido = 0;

		ftiempoTrascurridoAV = fTiempoAumentoVelocidad;
		bJuegoTerminado = false;

	}
	
	// Update is called once per frame
	void Update () {

		if(bJuegoTerminado == false){
			fTiempoTrascurrido += Time.deltaTime;

			//condicion para generar un barco 
			if(contenedor.bContenedorlleno == false){

				// generamos el barco
				GameObject gClone ;
				GameObject gObjetoACrear ;

				//elegimos que objeto crear con un random
				int iobjeto = Random.Range(1,11);
				if (iobjeto>5)gObjetoACrear =gCanoa2;
				else gObjetoACrear = gCanoa1;
				//creamos el objeto
				gClone = Instantiate(gObjetoACrear, new Vector3(10,0,0), transform.rotation)as GameObject; 

				//elegimos el tamñano del objeto 1 pequeño 2 grande
				int tamano=0;
				float fEscala = 0;
				int iTamañoObjeto = Random.Range(1,11);
				if (iTamañoObjeto>5){ fEscala = fTamañoCanoaGrande;tamano = 2;}
				else{ fEscala = fTamañoCanoaPequeña; tamano = 1;}
				
				gClone.transform.localScale = new Vector2(fEscala,fEscala);
				
				Barcos movimiento_Canoas;
				movimiento_Canoas = gClone.GetComponent<Barcos>();
				movimiento_Canoas.fMoveSpeed = fVelocidadBarcos;
				movimiento_Canoas.iMitamano = tamano;
 
				barcosCreados++;
				contenedor.bContenedorlleno = true;

			}

			//condicion para aumentar la velocidad de los barcos cada sierto tiempo
			if(fTiempoTrascurrido >= ftiempoTrascurridoAV ){fVelocidadBarcos = GenerarVelocidad(fVelocidadBarcos); ftiempoTrascurridoAV = fTiempoTrascurrido +fTiempoAumentoVelocidad;}
		
		}

	  if(fTiempoTrascurrido >= fTiempoDeJuego){






			bJuegoTerminado = true;

		}


	}

	float GenerarVelocidad(float velocidadActual){
		
		float nuevaVelocidad = Random.Range(velocidadActual,velocidadActual+2);

		return nuevaVelocidad;
	}
}
