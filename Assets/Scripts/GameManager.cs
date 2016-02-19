using UnityEngine;
using System.Collections;

//http://www.tasharen.com/forum/index.php?topic=5879.0

public class GameManager : MonoBehaviour {

	// declaramos variables

	private DatosJuegoUsuario datosJuegosUsuario = new DatosJuegoUsuario ();
	private bool bVentanaActivaLoginAdulto;
	private bool bVentanaActivaLoginNiño;
	private bool bVentanaTipoUsuario;
	public static bool bGuardar;
	private string password = "", name = "";
	private GameObject goPlayer;
	public Camara cCam;
	//private Grid gTerreno;
	private string sUrl;
	private float X;
	private float Y;
	private GameObject goTarget;

	// Use this for initialization
	void Start () {

		// instancaiamos variables y borramos el los PlayerPrefebs

//		bVentanaActivaLoginAdulto = false;
//		bVentanaActivaLoginNiño = false;
//		bVentanaTipoUsuario = false;
		bGuardar = false;
	    //StartCoroutine(datosJuegosUsuario.obtener_DatosJuegoUsuario (1));
		PlayerPrefs.DeleteKey ("PosX");
		PlayerPrefs.DeleteKey ("PosY");
		cCam.enabled =true;
		goPlayer = Instantiate(Resources.Load("Player/Player_Eco", typeof(GameObject))) as GameObject;
		bGuardar = true;
	}
	
	// Update is called once per frame
	void Update () {

		// tamaño de la pantalla

		Y = Screen.height;
		X = Screen.width;

	}

//	void OnGUI () {
//
//		if(bVentanaTipoUsuario == true)
//		{
//			// Caja contenerdora con medidas en porcentaje de acuerdo con el tamaño de la pantalla
//			
//			GUI.Box(new Rect(X * 0.25f, Y * 0.25f,X * 0.50f, Y * 0.60f), "Login");
//
//			// Botones Tipo Usuario Niño o Adulto
//			
//			if(GUI.Button(new Rect(X * 0.3125f, Y * 0.35f, X * 0.175f, Y * 0.4f), "Adulto")) {
//
//				bVentanaActivaLoginAdulto = true;
//				bVentanaTipoUsuario = false;
//
//			}
//			if(GUI.Button(new Rect(X * 0.5125f, Y * 0.35f, X * 0.175f, Y * 0.4f), "Niño")) {
//				
//				bVentanaActivaLoginNiño = true;
//				bVentanaTipoUsuario = false;
//			}
//		}
//
//		if(bVentanaActivaLoginNiño == true)
//		{
//			// Caja contenerdora con medidas en porcentaje de acuerdo con el tamaño de la pantalla
//			
//			GUI.Box(new Rect(X * 0.25f, Y * 0.25f,X * 0.50f, Y * 0.60f), "Login");
//			// Titulo y caja de texto para ingreso de usuario y contraseña con con medidas en 
//			//porcentaje de acuerdo con el tammaño de la pantalla
//			
//			GUI.Label(new Rect(X * 0.3125f, Y * 0.3125f, X * 0.50f, Y * 0.60f),"Usuario");
//			name = GUI.TextField(new Rect(X * 0.3125f, Y * 0.375f, X * 0.375f, Y * 0.10f),name);
//			GUI.Label(new Rect(X * 0.3125f, Y * 0.4925f, X * 0.50f, Y * 0.60f),"Contraseña");
//			password = GUI.PasswordField(new Rect(X * 0.3125f, Y * 0.5625f, X * 0.375f, Y * 0.10f),password,"*"[0]);
//			
//			//botoner con medidas en porcentaje de acuerdo con el tamaño de la pantalla
//			
//			if(GUI.Button(new Rect(X * 0.3125f, Y * 0.7f, X * 0.175f, Y * 0.08f), "Entrar")) {
//				
//				datosJuegosUsuario = new DatosJuegoUsuario ();
//				datosJuegosUsuario.obtener_DatosJuegoUsuario (1);
//				
//				// preguntamos si los datos pedidos son correctos intanciamos el player
//				
//				if(DatosJuegoUsuario.sMensaje == "correcto")
//				{
//					//gTerreno = GetComponent<Grid>();
//					//gTerreno.enabled = true;
//					cCam.enabled =true;
//					goPlayer = Instantiate(Resources.Load("Player/Player_Eco", typeof(GameObject))) as GameObject;
//					goPlayer.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"),PlayerPrefs.GetFloat("PosY"),0);
//					//gTerreno.transform.position = Vector3.zero;
//					bVentanaActivaLoginNiño = false;
//					bGuardar = true;
//				}
//			}
//			
//			// boton de registro al portal de juego con medidas en porcentaje de acuerdo con el tamaño de la pantalla
//			
//			if(GUI.Button(new Rect(X * 0.5125f, Y * 0.7f, X * 0.175f, Y * 0.08f), "Registrarse")) {
//				
//				sUrl = "http://www.life2play.co";
//				Application.OpenURL(sUrl);
//			}
//		}
//
//		if(bVentanaActivaLoginAdulto == true)
//		{
//			// Caja contenerdora con medidas en porcentaje de acuerdo con el tamaño de la pantalla
//
//			GUI.Box(new Rect(X * 0.25f, Y * 0.25f,X * 0.50f, Y * 0.60f), "Login");
//			// Titulo y caja de texto para ingreso de usuario y contraseña con con medidas en 
//			//porcentaje de acuerdo con el tammaño de la pantalla
//
//			GUI.Label(new Rect(X * 0.3125f, Y * 0.3125f, X * 0.50f, Y * 0.60f),"Usuario");
//			name = GUI.TextField(new Rect(X * 0.3125f, Y * 0.375f, X * 0.375f, Y * 0.10f),name);
//			GUI.Label(new Rect(X * 0.3125f, Y * 0.4925f, X * 0.50f, Y * 0.60f),"Contraseña");
//			password = GUI.PasswordField(new Rect(X * 0.3125f, Y * 0.5625f, X * 0.375f, Y * 0.10f),password,"*"[0]);
//
//			//botoner con medidas en porcentaje de acuerdo con el tamaño de la pantalla
//
//			if(GUI.Button(new Rect(X * 0.3125f, Y * 0.7f, X * 0.175f, Y * 0.08f), "Entrar")) {
//
//				datosJuegosUsuario = new DatosJuegoUsuario ();
//				datosJuegosUsuario.obtener_DatosJuegoUsuario (1);
//
//				 // preguntamos si los datos pedidos son correctos intanciamos el player
//
//				if(DatosJuegoUsuario.sMensaje == "correcto")
//				{
//					//gTerreno = GetComponent<Grid>();
//					//gTerreno.enabled = true;
//					cCam.enabled =true;
//					goPlayer = Instantiate(Resources.Load("Player/Player_Eco", typeof(GameObject))) as GameObject;
//
//					goPlayer.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"),PlayerPrefs.GetFloat("PosY"),0);
//					//gTerreno.transform.position = Vector3.zero;
//					bVentanaActivaLoginAdulto = false;
//					bGuardar = true;
//				}
//			}
//
//			// boton de registro al portal de juego con medidas en porcentaje de acuerdo con el tamaño de la pantalla
//
//			if(GUI.Button(new Rect(X * 0.5125f, Y * 0.7f, X * 0.175f, Y * 0.08f), "Registrarse")) {
//
//				sUrl = "http://www.life2play.co";
//				Application.OpenURL(sUrl);
//			}
//		}
//
//	}
}
