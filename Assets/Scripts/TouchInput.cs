using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {
	
	public LayerMask touchInputMask;
	private RaycastHit2D hit;
	private bool bClick;//para que solo pueda agarrarse una sola cosa a la ves
	public GameObject PuntoMouse;
	public bool bUnClick;

	void Start(){
		
		bClick = false;

	}
	
	void Update(){
		Vector2 worldPos  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		PuntoMouse.transform.position = worldPos; //ponemos la punta del el objeto en el mouse

		if (Input.GetMouseButton (0) || Input.GetMouseButtonDown (0) || Input.GetMouseButtonUp (0)) {

			Vector3 ray = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
			hit = Physics2D.Raycast (ray, Vector2.one);
		
			if (hit != null && hit.collider != null) {
								
				GameObject recipient = hit.transform.gameObject;
		

				if (Input.GetMouseButton(0)) 
				{
					//Debug.Log("envio");
					if (bUnClick) {
						if (bClick == false) {
							bClick = true;
							recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
						}
					} else {

						recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
			
				}
					
			}
				
		}

		else
		{
			bClick = false;
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			RaycastHit2D hit = Physics2D.Raycast( worldPoint, Vector2.zero );
			if ( hit.collider != null && hit != null)
			{
				GameObject recipient = hit.transform.gameObject;
				recipient.SendMessage ("OnTouchOver", hit.point, SendMessageOptions.DontRequireReceiver);

			}


		}

	}
}
