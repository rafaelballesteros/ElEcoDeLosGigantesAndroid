using UnityEngine;
using System.Collections;

public class Vacas : MonoBehaviour {

	// Use this for initialization

	public float fMoveSpeed = 1 ;

	public float fApetitoVaca ;
	public float fTiempoAmbre;//tiempo en que le da hambre a la vaca
	private float fTiempoTrascurridoAmbre;

	public float fTamañoCorralMaxX,fTamañoCorralMinX;
	public float fTamañoCorralMaxY,fTamañoCorralMinY;

	//private Vector3 pointMouse = new Vector3 (0, 0, 0); //posision del mouse, para almacenar.

	private Vector2 v2CurrentMoveToPos; // la posicion del click, a la cual el jugador debe ir.

	public float fDistanceError = 0.08f; // Distancia entre el punto y la posicion del player.

	private float fAnguloDireccion;
	float fProporcionEnX;
	float fProporcionEnY;

	//animacion
	private Animator animation_Controller;
	int iMoveHInt=0;
	int iMoveVInt=0;

	//proteccion distacia entre el mouse y el player
	float fDistancia,fDistanciaRecorrida = 0,fDistanciaArecorrer = 0;
	Vector3 v3PlayerPosicionInicial;

	//colicion

	private float fTiempoTranscurridoPuntos;	
	private float fTiempoTranscurrido;

	public float fTiempoPuntos;
	public bool bVacaComiendo;
	public   float TiempoTrascurridoPasto;
	public float fRapidezAlComer;


	private RaycastHit2D rHit2;
	public LayerMask lMascaraColision;


	public bool bSalirmeDelCorral ,bVacaHuir;
	public float fPuntosPorAlimento;

	private bool bLLegoVaca;//cuando llega la vaca al punto aleatorio
	GameObject goPastoComiendo; //pasto que se esta comiendo la vaca

	//variables al tocar la vaca
	private bool bToque = false;

	//limites del mapa
	public Vector4[] v4PuntosLimitesPasto;
	public Vector4[] v4PuntosLimitesTierra ;
	public bool bVacaEnElPasto;


	void Start () {
		bVacaEnElPasto = true;
		fTiempoTrascurridoAmbre = 0;
		fTiempoTranscurridoPuntos = 0;
		TiempoTrascurridoPasto = 0;
		bVacaComiendo = false;
		bLLegoVaca = true;
		v2CurrentMoveToPos = this.transform.position;
		animation_Controller = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

		fTiempoTranscurrido += Time.deltaTime;
		//fTiempoTranscurridoPuntos = fTiempoTranscurrido;
		// = fTiempoTranscurrido;
		float tiempoAmbreVaca = fTiempoTranscurridoPuntos+(fApetitoVaca/fTiempoPuntos);

			if (fTiempoTranscurrido >= tiempoAmbreVaca) {
			if(bVacaComiendo == false && bLLegoVaca)GenerarPunto (v2CurrentMoveToPos,true);
			fTiempoTranscurridoPuntos = fTiempoTranscurrido;
		}


		//aumento de apetito de la vaca
		if (fTiempoTranscurrido >= fTiempoAmbre + fTiempoTrascurridoAmbre  && bVacaComiendo == false) {
			
			if (fApetitoVaca > 1) {
				fApetitoVaca = fApetitoVaca - fPuntosPorAlimento;
				if (fApetitoVaca < 1)
					fApetitoVaca = 1;
			}

			fTiempoTrascurridoAmbre = fTiempoTranscurrido;
		}
			
		fDistancia = Vector2.Distance (this.transform.position, v2CurrentMoveToPos); 

		//fDistanciaRecorrida = Vector2.Distance (this.transform.position, v3PlayerPosicionInicial);
		//if(fDistanciaRecorrida>fDistanciaArecorrer) v2CurrentMoveToPos = this.transform.position;

		//Debug.Log("fDistancia "+fDistancia + " fDistanciaAnterior" +fDistanciaRecorrida);

		if (fDistancia > fDistanceError) {
			if(bLLegoVaca)bLLegoVaca = false;

			//Calculamos el angulo para determinar la direcion del player
			fAnguloDireccion = Mathf.Atan ((v2CurrentMoveToPos.y - this.transform.position.y)  / (v2CurrentMoveToPos.x-this.transform.position.x ))*Mathf.Rad2Deg;
			if(v2CurrentMoveToPos.x < this.transform.position.x) fAnguloDireccion = 180 + fAnguloDireccion;

			fAnguloDireccion = Mathf.Round(fAnguloDireccion);
			if(fAnguloDireccion<0) fAnguloDireccion = 360 + fAnguloDireccion;

			//Calculamos la proporciones X y Y para determinar la cantida de movimiento de cada cordenada
			fProporcionEnX = Mathf.Cos (fAnguloDireccion*Mathf.Deg2Rad);
			fProporcionEnY = Mathf.Sin (fAnguloDireccion*Mathf.Deg2Rad);

			//animacion player;
			tipoDeAnimacion(fAnguloDireccion);
			v3PlayerPosicionInicial = this.transform.position;
			fDistanciaArecorrer = Vector2.Distance (this.transform.position, v2CurrentMoveToPos); 
			//movimiento del player
			motion (fProporcionEnX, fProporcionEnY);
			//animacion player
			animation_Controller.SetInteger ("player_V", iMoveVInt);
			animation_Controller.SetInteger ("player_H", iMoveHInt);
			//Debug.Log("moveVInt "+ moveVInt + " moveHInt " + moveHInt);
		}
		else
		{
			if(bLLegoVaca == false)bLLegoVaca = true;
			iMoveHInt=0;
			iMoveVInt=0;
			animation_Controller.SetInteger("player_V",iMoveVInt);
			animation_Controller.SetInteger("player_H",iMoveHInt);
		}



		//revisamos si el pasto que se esta comiendo la vaca se lo comio otra vaca

		if(goPastoComiendo == null)bVacaComiendo =false;

	
		//revisamos si se esta tocando la vaca con el mouse
		if(bToque)
		{
			Vector3 vPosicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position =  new Vector3(vPosicionMouse.x,vPosicionMouse.y,transform.position.z);
		}
		if (Input.GetMouseButtonUp (0))
		{
			

			
			if(bToque){
				v2CurrentMoveToPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				bVacaComiendo = false;
				bToque = false;}

		}
	}


	void GenerarPunto(Vector2 posicion , bool valor){

		Vector2 v2CurrentToPos;
		// dimenciones del corral
		if (valor) {

			if(bVacaEnElPasto){
				int iSecionPasto = Random.Range(0,v4PuntosLimitesPasto.Length);
				v2CurrentToPos =  new Vector2 (Random.Range(v4PuntosLimitesPasto[iSecionPasto].x,v4PuntosLimitesPasto[iSecionPasto].z),Random.Range(v4PuntosLimitesPasto[iSecionPasto].y,v4PuntosLimitesPasto[iSecionPasto].w));
			}

			else {
				int iSecionTierra = Random.Range(0,v4PuntosLimitesTierra.Length);
				v2CurrentToPos =  new Vector2 (Random.Range(v4PuntosLimitesTierra[iSecionTierra].x,v4PuntosLimitesTierra[iSecionTierra].z),Random.Range(v4PuntosLimitesTierra[iSecionTierra].y,v4PuntosLimitesTierra[iSecionTierra].w));
			}




		
		}
		else v2CurrentToPos = posicion;

	
		v2CurrentMoveToPos = v2CurrentToPos;
	}

	void RevisarMiAlrededor(){

		bVacaComiendo = false;

		for (int i = 0; i <= 360; i++) {
			float dato1, dato2;
			dato1 = Mathf.Cos (i * Mathf.Deg2Rad);
			dato2 = Mathf.Sin (i * Mathf.Deg2Rad);
			Debug.DrawRay (this.transform.position, new Vector2 (dato1, dato2), Color.red, 2);
			rHit2 = Physics2D.Raycast(this.transform.position,new Vector2(dato1,dato2),2f,lMascaraColision);
			if (rHit2.collider != null && rHit2.collider.tag == "Pasto") {
				//Destroy (rHit2.collider.gameObject);
				GenerarPunto (rHit2.collider.gameObject.transform.position, false);
				Debug.Log ("mi frente posision"+rHit2.collider.gameObject.transform.position);
				//Debug.Log (" dato1 " + dato1 + " dato2 " + dato2);
				bVacaComiendo = true;
				break;
			}

		}
	}
	void motion(float x ,float y){

		this.transform.Translate (new Vector3 (x*Time.deltaTime*fMoveSpeed, y*Time.deltaTime*fMoveSpeed, 0));

	}

	void tipoDeAnimacion(float angulo){

		iMoveHInt=0;
		iMoveVInt=0;

		//angulos horizontales angulos verticales 	angulos diagonales
		if(angulo <= 20 || angulo >= 341){iMoveHInt = 1;}
		else if(angulo >= 21 && angulo <= 69 ){iMoveHInt = 1;iMoveVInt = 1;}

		else if(angulo >= 70 && angulo <= 110   ){iMoveVInt = 1;}
		else if(angulo >= 111 && angulo <= 159  ){iMoveHInt = -1;iMoveVInt = 1;}

		else if(angulo >= 160 && angulo <= 200  ){iMoveHInt = -1;}
		else if(angulo >= 201 && angulo <= 249  ){iMoveHInt = -1;iMoveVInt = -1;}

		else if(angulo >= 250 && angulo <= 290  ){iMoveVInt = -1;}	
		else if(angulo >= 291 && angulo <= 340){iMoveHInt = 1;iMoveVInt = -1;}
	}

	void  OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Vaca") {
			v2CurrentMoveToPos = new Vector2 (-v2CurrentMoveToPos.x,this.transform.position.y);
		}

	}

	void OnCollisionStay2D(Collision2D other)
	{


		if (other.gameObject.tag == "Pasto") {
			goPastoComiendo = other.gameObject;
			Debug.Log ("aqui hay pasto");
			GenerarPunto (other.gameObject.transform.position, false);
			bVacaComiendo = true;

			TiempoTrascurridoPasto += Time.deltaTime;

			if (TiempoTrascurridoPasto > fRapidezAlComer) {
				if (fApetitoVaca < 100)
					fApetitoVaca = fApetitoVaca + fPuntosPorAlimento;
				if (fApetitoVaca > 100)
					fApetitoVaca = 100;


				TiempoTrascurridoPasto = 0;
				Destroy (other.gameObject);
				RevisarMiAlrededor();

			}
		}
		

	}

	void OnTriggerStay2D(Collider2D other)
	{
		
	

		if(other.gameObject.tag == "mouse" && bVacaHuir){
			Vector2 worldPos  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float x,y;
			if(this.transform.position.x < worldPos.x)x=this.transform.position.x-4;
			else if(this.transform.position.x > worldPos.x)x= this.transform.position.x+4;
			if(this.transform.position.y < worldPos.y)x=this.transform.position.y-4;
			else if(this.transform.position.y > worldPos.y)x= this.transform.position.y+4;

			v2CurrentMoveToPos = new Vector3(-worldPos.x*10,-worldPos.y*10,0);
			bLLegoVaca = false;
		Debug.Log("algo el mouse");
		}


	}

	//funciones que representa el agarrar o soltar la vaca
	void OnTouchStay(){

	 
		if(bToque == false)bToque=true;
		//Debug.Log("Agarre la vaca");
	
	}

	void OnTouchOver(){


	}
		
}
