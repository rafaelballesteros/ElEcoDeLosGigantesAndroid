using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class Login : MonoBehaviour {

	private float X;
	private float Y;
	private bool bVentanaActivaLoginAdulto;
	private bool bVentanaActivaLoginNiño;
	private bool bVentanaTipoUsuario;
	public static bool bGuardar;
	private string sPassword = "", sEmail = "";
	private string sUrl;
	private string sLlave;
	private DatosJuegoUsuario duDatosUsuario;
	private Api api;
	private GameObject goCargador;
	
	public TextMesh txTxt;
	public static string sError;
	private bool bInstancia;
	public static string sMensaje;
	public GameObject goCanvasJugar;
	public GameObject goCanvasLogin;
	public GUIStyle gsStyle;

	private string sMensajeDatosGuardados;

	// Use this for initialization
	void Start()
	{
		api = GetComponent<Api>();
		PlayerPrefs.DeleteKey("llave");
		bInstancia = true;
	}

	// Update is called once per frame
	void Update () {
		
		Y = Screen.height;
		X = Screen.width;

		
		if(api.sMensajeDatosGuardados == "guardado")
		{
			goCanvasLogin.SetActive (false);
			bVentanaActivaLoginNiño = false;

			StartCoroutine(api.obtener_DatosJuegoUsuario(PlayerPrefs.GetString("llave")));
			
			if(api.sMensaje == "correcto")
			{
//				if(bInstancia == true)
//				{
//					goCargador = Instantiate(Resources.Load("Cargador/loader-sprite_0", typeof(GameObject))) as GameObject;
//					bInstancia = false;
//				}
				if(AnimatorCargador.sCompleteCargador == "completado")
				{
					Application.LoadLevel(PlayerPrefs.GetString("Nivel"));
				}
			}
		}
	}

	void OnGUI () {

		if(bVentanaActivaLoginNiño == true)
		{
			// Caja de texto con medidas en porcentaje de acuerdo con el tamaño de la pantalla

			sEmail = GUI.TextField(new Rect(X * 0.36f, Y * 0.3f, X * 0.3f, Y * 0.085f),sEmail,100,gsStyle);
			sPassword = GUI.PasswordField(new Rect(X * 0.36f, Y * 0.45f, X * 0.3f, Y * 0.085f),sPassword,"*"[0],100,gsStyle);

		}
	}


	public void Jugar()
	{
		goCanvasJugar.SetActive (false);
		goCanvasLogin.SetActive(true);
		bVentanaActivaLoginNiño = true;
	}

	public void IniciarSesion()
	{
		StartCoroutine(api.iniciarSesion(sEmail,sPassword));
	}

	public void Registrarme()
	{
		sUrl = "http://www.life2play.co";
		Application.OpenURL(sUrl);
	}
	
}
