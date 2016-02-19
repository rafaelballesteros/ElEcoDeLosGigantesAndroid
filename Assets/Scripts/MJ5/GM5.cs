using UnityEngine;
using System.Collections;

public class GM5 : MonoBehaviour {

	public float fVelocidadInst;
	public int numeroDeClientes;
	public int count = 0;

	private bool bInstanciar = true, b = false;
	private float fTiempoInstanciar;
	private float fTiempoJuego;
	private int iTamañoTela;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		fTiempoJuego += Time.deltaTime;
		fTiempoInstanciar += Time.deltaTime;

		if(fTiempoInstanciar > fVelocidadInst)
		{
			if (bInstanciar)
			{
				int inombreFruto;
				inombreFruto = Random.Range (1,numeroDeClientes);
				Cliente (inombreFruto,TamañoTela());
				fTiempoJuego = fTiempoJuego;

				if(fTiempoJuego >= 32)
				{
					fTiempoJuego = 0; 
					fVelocidadInst--;
					count++;
				}
				//Debug.Log (fTiempoJuego);
				fTiempoInstanciar = 0;
			}
		}
		if(count == 5)
		{
			bInstanciar = false;
			b = true;
			count = 6;
		}
	
	}


	void Cliente(int fsNombreFruto, float ffTamaño)
	{
		GameObject goCliente = null;
		goCliente = Instantiate(Resources.Load("Objetos/Clientes/"+"cliente"+fsNombreFruto.ToString(), typeof(GameObject))) as GameObject;
		goCliente.transform.localScale =  new Vector3(ffTamaño,ffTamaño,ffTamaño);
		TamañoTela ();
		Clientes cClientes;
		cClientes = goCliente.GetComponent<Clientes> ();
	}

	void ColorTela()
	{
		int iColorTela;

		iColorTela = Random.Range (1, 21);
		if(iColorTela > 5) iColorTela = 1;
		else if(iColorTela <= 5)iColorTela = 2;
		else if(iColorTela >= 10)iColorTela = 3;
		else if(iColorTela < 10)iColorTela = 4;
	}

	private float TamañoTela()
	{
		float fTamañoTela;

		fTamañoTela = Random.Range (1, 11);
		if(fTamañoTela > 5)fTamañoTela = iTamañoTela;
		else if(fTamañoTela <= 5)fTamañoTela = iTamañoTela;

		return fTamañoTela;
	}

}
