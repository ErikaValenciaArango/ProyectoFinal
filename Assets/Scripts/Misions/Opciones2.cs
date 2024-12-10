using UnityEngine;
using System.Collections;

public class Opciones2 : MonoBehaviour
{
    public static int piedras = 0;
    public GUISkin miSkin;

    void OnGUI()
    {
        GUI.skin = miSkin;

        if (Mision2.misionSegunda)
        {
            // Cambi� el tama�o de la fuente a 60 y ajust� el color del texto a blanco
            GUIStyle style = new GUIStyle()
            {
                fontSize = 60,
                normal = new GUIStyleState() { textColor = Color.white } // Establecer el color del texto a blanco
            };
            // Ajuste del rect�ngulo para que el texto no se corte
            GUI.Label(new Rect(Screen.width / 2 - 50, 20, 250, 100), piedras + "/1 Keys", style);
        }
    }
}
