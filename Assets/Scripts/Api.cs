using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Api : MonoBehaviour {
	
	[HideInInspector]
	public string sMensaje;
	public static string sError;
	[HideInInspector]
	public string sMensajeDatosGuardados;
	
	private string sLlave;
	private int count;
	
	private string sNombreElemento;
	private float sPosX;
	private float sPosY;
	private int iEstado;
	private int iOrientacion;
	private int iDisponibleTerminar;
	
	
	// Use this for initialization
	void Start () {
		
		PlayerPrefs.DeleteKey("Nivel");
		PlayerPrefs.DeleteKey("PosX");
		PlayerPrefs.DeleteKey("PosY");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// funcion para iniciar sesion
	public IEnumerator iniciarSesion (string sIemail, string sIpassword) {
		
		string url = "http://127.0.0.1/egigantes/api/loginUsuario";
		WWWForm form = new WWWForm();
		JSONObject JSON = new JSONObject(JSONObject.Type.OBJECT);
		JSON.AddField("email", sIemail);
		JSON.AddField("password",sIpassword);
		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("email", sIemail);
		headers.Add("password",sIpassword);
		
		byte[] pData = Encoding.UTF8.GetBytes(JSON.ToString());
		
		WWW www = new WWW(url,pData,headers);
		
		yield return www;
		sMensaje = www.error;
		if (!string.IsNullOrEmpty(www.error)) {
			Debug.Log (www.error);
			sError = www.error;
		} else {
			
			if(www.isDone == true)
			{
				JSONObject JSON2 = new JSONObject (www.text);
				guardar_llave (JSON2);
				//sMensaje = "correcto";
			}
			//Debug.Log(www.text);
		}
		
	}
	
	// funcion para obtener los datos de la tabla DatosJuegoUsuario
	public IEnumerator obtener_DatosJuegoUsuario(string n) {
		
		WWWForm form = new WWWForm();
		string url = "http://127.0.0.1/egigantes/api/obtenerDatosJuegoUsuario";
		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("llave", n);
		WWW w = new WWW(url,null,headers);
		
		yield return w;
		//sMensaje = w.error;
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log (w.error);
			Debug.Log(w.text);
		} else {
			
			if(w.isDone == true)
			{
				JSONObject j = new JSONObject (w.text);
				guardar_DatosJuegoUsuario (j);
				//guardar_ObjetosInteraccion(j);
				//Debug.Log(w.text);
			}
			//Debug.Log(w.text);
		}
		
	}
	//	#if UNITY_WEBGL
	//	// funcion para obtener los datos de la tabla DatosJuegoUsuario
	//	public IEnumerator obtener_DatosJuegoUsuarioWeb() {
	//		
	//		WWWForm form = new WWWForm();
	//		string url = "http://127.0.0.1/egigantes/api/obtenerDatosJuegoUsuario";
	//
	//		WWW w = new WWW(url);
	//		
	//		yield return w;
	//		//sMensaje = w.error;
	//		if (!string.IsNullOrEmpty(w.error)) {
	//			Debug.Log (w.error);
	//			Debug.Log(w.text);
	//		} else {
	//			
	//			if(w.isDone == true)
	//			{
	//				JSONObject j = new JSONObject (w.text);
	//				guardar_DatosJuegoUsuario (j);
	//				//guardar_ObjetosInteraccion(j);
	//				//Debug.Log(w.text);
	//			}
	//			//Debug.Log(w.text);
	//		}
	//	}
	//	#endif
	// funcion para guardar la llave del juego
	public void guardar_llave(JSONObject obj){
		
		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
				//Debug.Log(key);
				guardar_llave (j);
				if(key.ToString() == "llave")
				{
					Debug.Log(key+":"+j.str);
					PlayerPrefs.SetString("llave", j.str);
					sMensajeDatosGuardados = "guardado";
					//sLlave = j.str;
					//txTxt.text = sLlave;
				}
			}
			break;
		case JSONObject.Type.ARRAY:
			
			foreach(JSONObject j in obj.list){
				guardar_llave(j);
			}
			
			break;
		case JSONObject.Type.STRING:
			if(obj.str.ToString() == "llave"){
				Debug.Log(obj.str);
			}
			break;
		}
	}
	
	// funcion para guardar los datos del juego obtenidos
	public void guardar_DatosJuegoUsuario(JSONObject obj){
		
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
		
		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
				guardar_DatosJuegoUsuario(j);

				if(i==1)
				{
					if(key.ToString() == "NombreEscena")
					{
						Debug.Log(key+":"+j.str);
						PlayerPrefs.SetString("Nivel",j.str);
					}
				}
			}
			break;
		case JSONObject.Type.ARRAY:
			
			foreach(JSONObject j in obj.list){
				guardar_DatosJuegoUsuario(j);
			}
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
			
//			Debug.Log(sId_DatosJuegoUsuario);
//			Debug.Log(iId_Usuario);
//			Debug.Log(iId_Nivel);
//			Debug.Log(iId_Escena);
//			Debug.Log(fPosX);
//			Debug.Log(fPosY);
//			Debug.Log(iVida);
//			Debug.Log(iPuntos);
//			Debug.Log(iMinutosJuegosDia);
//			Debug.Log(iMinutosJuegosRestante);
			PlayerPrefs.SetFloat("PosX", fPosX);
			PlayerPrefs.SetFloat("PosY", fPosY);
			sMensaje = "correcto";
			break;
		}
	}
	
}
