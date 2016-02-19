using UnityEngine;
using System.Collections;

public class Alfa : MonoBehaviour {

		
	private SpriteRenderer spr;
//	public GameObject goPlayer;
//	public float maximoZ;

	// Use this for initialization
	void Start () {
	
		spr = GetComponent<SpriteRenderer> ();
//		goPlayer = null;
//		goPlayer = GameObject.Find ("Player_Eco");
	}
	
	// Update is called once per frame
	void Update () {
		
//		goPlayer = null;
//		goPlayer = GameObject.Find ("Player_Eco");
	}
		
	void OnTriggerStay2D(Collider2D other)

	{
		if(other.gameObject.tag == "Player" )
		{
			float fNuevaPosicion;
			fNuevaPosicion = this.transform.position.z + 0.5f;
			spr.color = new Color (1f, 1f, 1f, 0.5f);
			//goPlayer.transform.position = new Vector3(goPlayer.transform.position.x,goPlayer.transform.position.y,fNuevaPosicion);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" )
		{
			spr.color = new Color (1f, 1f, 1f, 1f);
			//goPlayer.transform.position = new Vector3(goPlayer.transform.position.x,goPlayer.transform.position.y,maximoZ);
		}
	}
}
