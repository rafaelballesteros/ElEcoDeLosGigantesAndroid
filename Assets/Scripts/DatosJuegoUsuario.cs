using UnityEngine;
using System.Collections;

public class DatosJuegoUsuario : MonoBehaviour {

	public static string sMensaje;
	private float X;
	private float Y;
	private Vector3 newpos;
	// Use this for initialization
	void Start () {

		StartCoroutine (obtener_DatosJuegoUsuario(2));
		//StartCoroutine (actualizar_DatosJuegoUsuario());
	}
	
	// Update is called once per frame
	void Update () {

		Y = Screen.height;
		X = Screen.width;
	}


	void OnGUI()
	{
		if(GameManager.bGuardar == true){

			if(GUI.Button(new Rect(X * 0.5125f, Y * 0.7f, X * 0.175f, Y * 0.08f), "Guardar")) {
				StartCoroutine(actualizar_DatosJuegoUsuario(1,1,1,1,0.8f,0.8f,3,3,10,480));
				GameManager.bGuardar = false;
			}
		}
	}
	// funcion para obtener los datos de la tabla DatosJuegoUsuario
	public IEnumerator obtener_DatosJuegoUsuario(int n) {
		
		string url = "localhost/game/endpointbuscarDatosUsuario.php?Id_Usuario="+n;
		WWW w = new WWW(url);
		yield return w;
		//sMensaje = w.error;
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log (w.error);
		} else {

			if(w.isDone == true)
			{
				JSONObject j = new JSONObject (w.text);
				guardar_DatosJuegoUsuario (j);
			}
			//Debug.Log(w.text);
		}
	}
	
	// funcion para actualizar los datos de la tabla DatosJuegoUsuario
	public IEnumerator actualizar_DatosJuegoUsuario(int Id_datosJuegosUsuario, int Id_Usuario, int Id_Nivel, int Id_Escena, float PosX, float PosY,
	                                                int Puntos, int Vida, int MinutosJuegoDia, int MinutosJuegoRestante) {
		int iId_datosJuegosUsuario= Id_datosJuegosUsuario;
		int iId_Usuario = Id_Usuario;
		int iId_Nivel = Id_Nivel;
		int iId_Escena = Id_Escena;
		float fPosX = PosX;
		float fPosY= PosY;
		int iPuntos = Puntos;
		int iVida = Vida;
		int iMinutosJuegoDia = MinutosJuegoDia;
		int iMinutosJuegoRestante = MinutosJuegoRestante;
		
		WWWForm form = new WWWForm();
		//form.AddField("id",1);
		string url = "localhost/game/endpointguardarDatosUsuario.php?Id_Usuario="+iId_Usuario+"&"+"Id_Nivel="+iId_Nivel+"&"+"Id_Escena="+iId_Escena+"&"+"PosX="+fPosX+"&"+"PosY="+fPosY
			+"&"+"Puntos="+iPuntos+"&"+"Vida="+iVida+"&"+"MinutosJuegosDia="+iMinutosJuegoDia+"&"+"MinutosJuegosRestante="+iMinutosJuegoRestante+"&"+"Id_DatosJuegosUsuario="+iId_datosJuegosUsuario;
		WWW w = new WWW(url,form);
		yield return w;
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log (w.error);
		} else {
			Debug.Log (w.text);
		}
	}
	
	// funcion para guardar los datos en variables del juego obtenidos
	public void guardar_DatosJuegoUsuario(JSONObject obj){

		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
				guardar_DatosJuegoUsuario(j);
			}
			break;
		case JSONObject.Type.ARRAY:

			int iId_DatosJuegoUsuario;
			int iId_Usuario;
			int iId_Nivel;
			int iId_Escena;
			float fPosX;
			float fPosY;
			int iVida;
			int iPuntos;
			int iMinutosJuegosDia;
			int iMinutosJuegosRestante;

			string sId_DatosJuegoUsuario = obj.list[0]["Id_DatosJuegoUsuario"].ToString();
			int.TryParse(sId_DatosJuegoUsuario,out iId_DatosJuegoUsuario);

			string sId_Usuario = obj.list[0]["Id_Usuario"].ToString();
			int.TryParse(sId_Usuario,out iId_Usuario);

			string sId_Nivel = obj.list[0]["Id_Nivel"].ToString();
			int.TryParse(sId_Nivel,out iId_Nivel);

			string sId_Escena = obj.list[0]["Id_Escena"].ToString();
			int.TryParse(sId_Escena,out iId_Escena);

			fPosX = obj.list[0]["PosX"].n;

			fPosY = obj.list[0]["PosY"].n;

			string sVida = obj.list[0]["Vida"].ToString();
			int.TryParse(sVida,out iVida);

			string sPuntos = obj.list[0]["Puntos"].ToString();
			int.TryParse(sPuntos,out iPuntos);

			string sMinutosJuegosDia = obj.list[0]["MinutosJuegosDia"].ToString();
			int.TryParse(sMinutosJuegosDia, out iMinutosJuegosDia);

			string sMinutosJuegosRestante = obj.list[0]["MinutosJuegoRestante"].ToString();
			int.TryParse(sMinutosJuegosRestante, out iMinutosJuegosRestante);
	
			Debug.Log(iId_DatosJuegoUsuario);
			Debug.Log(iId_Usuario);
			Debug.Log(iId_Nivel);
			Debug.Log(iId_Escena);
			Debug.Log(fPosX);
			PlayerPrefs.SetFloat("PosX", fPosX);
			PlayerPrefs.SetFloat("PosY", fPosY);
			Debug.Log(fPosY);
			Debug.Log(iVida);
			Debug.Log(iPuntos);
			Debug.Log(iMinutosJuegosDia);
			Debug.Log(iMinutosJuegosRestante);
			sMensaje = "correcto";
			break;
		}
	}
	
}
