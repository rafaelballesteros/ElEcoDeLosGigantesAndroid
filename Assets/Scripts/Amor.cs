using UnityEngine;
using System.Collections;

public class Amor : MonoBehaviour {

	private bool bAmor;
	private bool bCircuntancia;
	private bool bDistancia;
	private bool bJuntos;

	void Juntos () {

		if (bAmor == true) {

			bDistancia = false;
			bCircuntancia = false;

			if(bDistancia == false && bCircuntancia == false)
			{
				bJuntos = true;
			}
		}

		else
		{
			bJuntos = false;
		}
	
	}

}
