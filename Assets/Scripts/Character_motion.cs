using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animation_Controller))]

public class Character_motion : MonoBehaviour {

	public float moveV = 0f;
	public float moveH = 0f;
	public float speed = 0.5f;
	//public BoxCollider2D bxCollider;

	public float maximoZ;
	public bool RestaurarZ;
	public float TiempoEntreRestauracion;
	public float Zanterior;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		moveV = Input.GetAxis("Vertical");
		moveH = Input.GetAxis("Horizontal");


	
		if (moveH > 0) {


			motion(speed,0);
				}
	
		if (moveH < 0) {

			motion(-speed,0);

		}

		if (moveV > 0) {
			
			motion(0,speed);
			//bxCollider.size = new Vector2(0.34f,0.78f);
		}
		
		if (moveV < 0) {

			motion(0,-speed);
			
		}

		if(RestaurarZ){
			TiempoEntreRestauracion += Time.deltaTime;
			if(TiempoEntreRestauracion >=0.3f){
				this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,maximoZ);
				RestaurarZ=false;
				TiempoEntreRestauracion =0 ;
			}
			
		}
		else{
			TiempoEntreRestauracion = 0;
		}

	}



	void motion(float x ,float y){

		this.transform.Translate (new Vector3 (x*Time.deltaTime, y*Time.deltaTime, 0));

	}

	void OnTriggerStay2D(Collider2D other)
	{
		
		if(other.gameObject.tag == "Objeto" )
		{
			float fNuevaPosicion;
			fNuevaPosicion = other.gameObject.transform.position.z + 0.5f;
			this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,fNuevaPosicion);
			RestaurarZ = false;
			Zanterior = fNuevaPosicion;
		}
		
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Objeto" )
		{
			RestaurarZ = true;
		}
	}
	
}
