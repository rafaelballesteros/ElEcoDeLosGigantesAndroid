using UnityEngine;
using System.Collections;

public class AnimatorCargador : MonoBehaviour {

	private Animator aCargador;
	public static string sCompleteCargador;

	// Use this for initialization
	void Start () {

		aCargador = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(aCargador.GetCurrentAnimatorStateInfo(0).IsName("cargadorDefault"))
		{
			aCargador.SetBool("loader",false);
			sCompleteCargador = "completado";
		}
	
	}
}
