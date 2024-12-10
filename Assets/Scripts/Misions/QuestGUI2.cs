using UnityEngine;

public class QuestGUI2 : MonoBehaviour
{
    public static bool activarQuest = false;
    private bool mediumQuest = false;
    private bool finishQuest = false;

    public Rect firstQuest; // Inicialización dinámica
    public string nomMision = "";
    public string textMisionIncompleta = "";
    public string textMisionCompleta = "";
    public string textMision = "";
    public Texture2D rostroMis;

    public GUISkin miSkin;

    void Start()
    {
        // Inicialización de la ventana con tamaño ajustado
        firstQuest = new Rect(30, 30, 500, 300); // Se ajusta la altura y el ancho
    }

    void OnGUI()
    {
        GUI.skin = miSkin;
        GUIStyle style = new GUIStyle(GUI.skin.window);
        style.fontSize = 30;

        // Ventanas de la misión con el estilo aplicado
        if (activarQuest)
        {
            firstQuest = GUI.Window(0, firstQuest, Quest, "Mision - " + nomMision);
        }

        if (mediumQuest)
        {
            firstQuest = GUI.Window(0, firstQuest, Quest_Incompleta, "Mision in progress");
        }

        if (finishQuest)
        {
            firstQuest = GUI.Window(0, firstQuest, Quest_Completa, "Mision completed - " + nomMision);
        }
    }

    void Quest(int WindowID)
    {
        // Aumentamos el alto de la ventana y ajustamos la disposición
        GUI.Label(new Rect(30, 100, 440, 60), textMision, new GUIStyle("Box") { fontSize = 20, alignment = TextAnchor.MiddleCenter });
        GUI.DrawTexture(new Rect(350, 50, 100, 100), rostroMis);

        // Ajustamos el botón de continuar, centrado y con un espaciado adecuado
        if (GUI.Button(new Rect(firstQuest.width / 2 - 50, firstQuest.height - 90, 100, 40), "Continue"))
        {
            activarQuest = true;
            mediumQuest = false;
            finishQuest = false;
            Mision2.misionSegunda = true;
        }
    }

    void Quest_Incompleta(int WindowID)
    {
        GUI.Label(new Rect(30, 100, 440, 60), textMisionIncompleta, new GUIStyle("Box") { fontSize = 20, alignment = TextAnchor.MiddleCenter });
        GUI.DrawTexture(new Rect(350, 50, 100, 100), rostroMis);

        // Ajustamos el botón de continuar, centrado y con un espaciado adecuado
        if (GUI.Button(new Rect(firstQuest.width / 2 - 50, firstQuest.height - 90, 100, 40), "Continue"))
        {
            activarQuest = false;
            mediumQuest = true;
            finishQuest = false;
            Mision2.misionSegunda = true;
        }
    }

    void Quest_Completa(int WindowID)
    {
        GUI.Label(new Rect(30, 100, 440, 60), textMisionCompleta, new GUIStyle("Box") { fontSize = 20, alignment = TextAnchor.MiddleCenter });
        GUI.DrawTexture(new Rect(350, 50, 100, 100), rostroMis);

        // Ajustamos el botón de continuar, centrado y con un espaciado adecuado
        if (GUI.Button(new Rect(firstQuest.width / 2 - 50, firstQuest.height - 90, 100, 40), "Continue"))
        {
            activarQuest = false;
            mediumQuest = false;
            finishQuest = true;
            Mision2.misionSegunda = false;
        }
    }

    void OnTriggerStay()
    {
        activarQuest = true;

        if (Mision2.misionSegunda && Opciones2.piedras < 3)
        {
            finishQuest = false;
            activarQuest = false;
            mediumQuest = true;
            Mision2.misionSegunda = true;
        }

        if (Mision2.misionSegunda && Opciones2.piedras == 3)
        {
            finishQuest = true;
            activarQuest = false;
            mediumQuest = false;
            Mision2.misionSegunda = false;
        }
        if (Mision2.misionSegunda && Opciones2.piedras > 3)
        {
            finishQuest = true;
            activarQuest = false;
            mediumQuest = false;
            Mision2.misionSegunda = false;
        }
    }

    void OnTriggerExit()
    {
        finishQuest = false;
        activarQuest = false;
        mediumQuest = false;
    }
}
