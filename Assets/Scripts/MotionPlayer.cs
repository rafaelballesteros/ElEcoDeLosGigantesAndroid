using UnityEngine;
using System.Collections;

public class MotionPlayer : MonoBehaviour {
	
	// Use this for initialization

	public float fMoveSpeed = 1 ;

	public LayerMask layerMask = 1<<7; // mascara con la que hara collision.
	public LayerMask lMascaraColision;

	//private Vector3 pointMouse = new Vector3 (0, 0, 0); //posision del mouse, para almacenar.
	private float fRayCastDisntance = 500.0f; // distancia del rayo.
	private Vector3 v3CurrentMoveToPos; // la posicion del click, a la cual el jugador debe ir.


	
	public float fDistanceError = 0.03f; // Distancia entre el punto y la posicion del player.
	
	
	public float fAnguloDireccion;
	float fProporcionEnX;
	float fProporcionEnY;


	public float fMaximoZ;
	public bool bRestaurarZ;
	public float fTiempoEntreRestauracion;
	public float fZanterior;
	
	
	//animacion
	private Animator animation_Controller;
	int iMoveHInt=0;
	int iMoveVInt=0;

	//proteccion distacia entre el mouse y el player
	float fDistancia,fDistanciaRecorrida = 0,fDistanciaArecorrer = 0;
	Vector3 v3PlayerPosicionInicial;

	//colicion
	private bool bDetenerPorColicion = false;
	private float fDireccionDeColicion ;
	private RaycastHit2D rHit2;

	void Start () {
		
	
		
		animation_Controller = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		


		if ((Input.GetAxis("Fire1")>0 || Input.GetAxis("Fire2")>0) )
		{
			

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit,fRayCastDisntance, layerMask)) 
			{
				v3CurrentMoveToPos = hit.point;			
			}

			//Calculamos el angulo para determinar la direcion del player
			fAnguloDireccion = Mathf.Atan ((v3CurrentMoveToPos.y - this.transform.position.y)  / (v3CurrentMoveToPos.x-this.transform.position.x ))*Mathf.Rad2Deg;
			if(v3CurrentMoveToPos.x < this.transform.position.x) fAnguloDireccion = 180 + fAnguloDireccion;

			fAnguloDireccion = Mathf.Round(fAnguloDireccion);
			if(fAnguloDireccion<0) fAnguloDireccion = 360 + fAnguloDireccion;
			
			//Calculamos la proporciones X y Y para determinar la cantida de movimiento de cada cordenada
			fProporcionEnX = Mathf.Cos (fAnguloDireccion*Mathf.Deg2Rad);
			fProporcionEnY = Mathf.Sin (fAnguloDireccion*Mathf.Deg2Rad);
			
			
			//animacion player;
			tipoDeAnimacion(fAnguloDireccion);

			v3PlayerPosicionInicial = this.transform.position;



			fDistanciaArecorrer = Vector2.Distance (this.transform.position, v3CurrentMoveToPos); 

			rHit2 = Physics2D.Raycast(transform.position,new Vector2(fProporcionEnX,fProporcionEnY),90f,lMascaraColision);

			if(rHit2.collider == null && bDetenerPorColicion)bDetenerPorColicion = false;

			Debug.DrawRay(transform.position,new Vector2(fProporcionEnX,fProporcionEnY),Color.red,2);
		}

			
		//		pointMouse = currentMoveToPos;



		fDistancia = Vector2.Distance (this.transform.position, v3CurrentMoveToPos); 

		fDistanciaRecorrida = Vector2.Distance (this.transform.position, v3PlayerPosicionInicial);
		if(fDistanciaRecorrida>fDistanciaArecorrer) v3CurrentMoveToPos = this.transform.position;

		//Debug.Log("fDistancia "+fDistancia + " fDistanciaAnterior" +fDistanciaRecorrida);

			if (fDistancia> fDistanceError  && bDetenerPorColicion == false) {


			
			//movimiento del player
			motion(fProporcionEnX,fProporcionEnY);

		
			//animacion player
			animation_Controller.SetInteger("player_V",iMoveVInt);
			animation_Controller.SetInteger("player_H",iMoveHInt);
			//Debug.Log("moveVInt "+ moveVInt + " moveHInt " + moveHInt);


		} 


		

		if(bRestaurarZ){
			fTiempoEntreRestauracion += Time.deltaTime;
			if(fTiempoEntreRestauracion >=0.3f){
				this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,fMaximoZ);
				bRestaurarZ=false;
				fTiempoEntreRestauracion =0 ;
			}
			
		}
		else{
			fTiempoEntreRestauracion = 0;
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

	void  OnCollisionStay2D(Collision2D other){
		if(other.gameObject.tag == "Objeto" && bDetenerPorColicion == false)
		{

			rHit2 = Physics2D.Raycast(transform.position,new Vector2(fProporcionEnX,fProporcionEnY),90f,lMascaraColision);

			if(rHit2.collider != null && rHit2.collider.tag == "Objeto"){
				Debug.Log("mi frente");

			
			fDireccionDeColicion = fAnguloDireccion;
			bDetenerPorColicion = true;
					v3CurrentMoveToPos = this.transform.position;

			
			}

		}

	}

	void OnTriggerStay2D(Collider2D other)
	{
		
		if(other.gameObject.tag == "Objeto" )
		{
			float fNuevaPosicion;
			fNuevaPosicion = other.gameObject.transform.position.z + 0.5f;
			this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,fNuevaPosicion);
			bRestaurarZ = false;
			fZanterior = fNuevaPosicion;
		}
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Objeto" )
		{
			bRestaurarZ = true;
		}
	}
}
