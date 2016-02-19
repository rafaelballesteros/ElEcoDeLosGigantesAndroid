using UnityEngine;
using System.Collections;

public class Barcos : MonoBehaviour {

	private float fRayCastDisntance = 500.0f; // distancia del rayo.
	public LayerMask layerMask ; // mascara con la que hara collision.
//	public GameObject objetoArriba,objetoAbajo;
	public float fMoveSpeed ;


	// Use this for initialization
	//float x=0, y;
	float fposicionMaxima,fposicionMinima;

	public Vector3 ObjetoSelecionado;
	public Vector3 marcandoRecorrido;
	float x=0,h,y,k,radio;///(h,k)centro circunferencia


	public bool bClickEnObjero = false;
	public int maximaRotaconBolante = 3;

	private bool bCanoaEnContenedor = false;
	public int iMitamano;
	float dato=0;

	public bool bPuertoCorrecto = false;



	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {




		float fDistancia = Vector2.Distance (this.transform.position, marcandoRecorrido);
		if(fDistancia <= 0.3)marcandoRecorrido = new Vector3(ObjetoSelecionado.x-10,ObjetoSelecionado.y,0);


		MoverBarco();//movemos el varco

		   
	
		
		RotacionBarco(marcandoRecorrido);//rotamos el varco



		// miramos si se ase click sobre el objeto arriba o abajo 

		if(bCanoaEnContenedor){

			Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (Input.GetMouseButtonDown(0) && bClickEnObjero == false)
			{
				//RaycastHit2D hit = new RaycastHit2D();
				//Ray2D ray2D = new Ray2D(new Vector2(p.x,p.y), Vector3.down );
				//Ray2D ray = new Ray(transform.position,new Vector3(p.x,p.y,p.z));
				RaycastHit2D hit =Physics2D.Raycast(new Vector2(p.x,p.y),Vector2.zero,5.0f,layerMask);
				if (hit.collider != null && (hit.collider.tag == "Arriba" || hit.collider.tag == "Abajo") )
				{
					ObjetoSelecionado = hit.collider.gameObject.transform.position;	
					marcandoRecorrido = new Vector3(this.transform.position.x-5,ObjetoSelecionado.y,0);
					bClickEnObjero = true ;
				}
			}

	

		}
	    

	}


	void MoverBarco(){
		

		this.transform.Translate (new Vector3 (-Time.deltaTime*fMoveSpeed, 0, 0));
	

	}

	void RotacionBarco(Vector3 puntoAseguir){
		
		float angulo = Mathf.Atan ((puntoAseguir.y- this.transform.position.y)  / (puntoAseguir.x-this.transform.position.x ))*Mathf.Rad2Deg;
	
		if(puntoAseguir.x > this.transform.position.x) angulo = 180 + angulo;
	
//
		if( dato< angulo ){

			dato = dato +(fMoveSpeed/2);
		

		}
		else if( dato > angulo ){

			dato = dato - (fMoveSpeed/2);
		

		}

		this.transform.eulerAngles = new Vector3(0,0,dato);
	

	

		
	}


	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Contenedor" ){
			if(bCanoaEnContenedor == false)bCanoaEnContenedor = true;
			//Vector3 m = Input.mousePosition;
			//m = new Vector3(m.x, m.y, 0);
//			Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//
//			if (Input.GetMouseButtonDown(0))
//			{
//				//RaycastHit2D hit = new RaycastHit2D();
//				//Ray2D ray2D = new Ray2D(new Vector2(p.x,p.y), Vector3.down );
//				//Ray2D ray = new Ray(transform.position,new Vector3(p.x,p.y,p.z));
//				RaycastHit2D hit =Physics2D.Raycast(new Vector2(p.x,p.y),Vector2.zero,5.0f,layerMask);
//				if (hit.collider != null && hit.collider.tag == "Objeto" )
//				{
//					ObjetoSelecionado = hit.collider.gameObject.transform.position;	
//					marcandoRecorrido = new Vector3(this.transform.position.x-5,ObjetoSelecionado.y,0);
//					bClickEnObjero = true ;
//				}
//			}



		}
//		Debug.Log("ay cramba");
//		if(other.gameObject.tag == "Contenedor" )
//		{
//			// revisamos si damos click
//			if ((Input.GetAxis("Fire1")>0 || Input.GetAxis("Fire2")>0) && bClickEnObjero == false )
//			{
//
//
//				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//				RaycastHit hit;
//				if (Physics.Raycast(ray,out hit,fRayCastDisntance, layerMask)) 
//				{
//
//					if (hit.collider.tag == "Objeto"){ObjetoSelecionado = hit.collider.gameObject.transform.position;	
//						marcandoRecorrido = new Vector3(this.transform.position.x-5,ObjetoSelecionado.y,0);
//						bClickEnObjero = true ;	}
//				}
//			}
//		}
//			
	

	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Contenedor" )
		{

			if(bCanoaEnContenedor)bCanoaEnContenedor = false;

		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
//		if(other.gameObject.tag == "Finish" )
//		{
//			
//
//			
//
//
//		}
//
		if(other.gameObject.tag == "Arriba" && iMitamano == 2){
			bPuertoCorrecto = true;
			
		}

		if(other.gameObject.tag == "Abajo" && iMitamano == 1){
			bPuertoCorrecto = true;
		}
	}

}
