using UnityEngine;
using System.Collections;

public class EstadosPersonaje : MonoBehaviour {

	public int iEstado ;
	public int iOrientacion,iOrientacionAnterior;
	// Use this for initialization
	void Start () {
	iOrientacionAnterior=0;
	}
	
	// Update is called once per frame
	void Update () {
		if(iOrientacion != iOrientacionAnterior){
			orientacion(iOrientacion);
			iOrientacionAnterior = iOrientacion;
		}
	
	}

	void orientacion(int miOrientacion){

		switch(miOrientacion){
		case 1: 
			this.transform.eulerAngles=new Vector3(0,0,90); break;
		case 2: 
			this.transform.eulerAngles=new Vector3(0,0,45); break;
		case 3: 
			this.transform.eulerAngles=new Vector3(0,0,0); break;
		case 4: 
			this.transform.eulerAngles=new Vector3(0,0,315); break;
		case 5: 
			this.transform.eulerAngles=new Vector3(0,0,270); break;
		case 6: 
			this.transform.eulerAngles=new Vector3(0,0,225); break;
		case 7: 
			this.transform.eulerAngles=new Vector3(0,0,180); break;
		case 8: 
			this.transform.eulerAngles=new Vector3(0,0,135); break;
			}
		
	}
}
