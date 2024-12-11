using UnityEngine;

public class QuestGUI2 : MonoBehaviour
{
    public bool activarQuest = false;
    public bool mediumQuest = false;
    public bool finishQuest = false;

    public Rect firstQuest;
    public string nomMision = "";
    public string textMisionIncompleta = "Recolecta 1 piedra para completar la misión.";
    public string textMisionCompleta = "Misión completada. Has recolectado todas las piedras.";
    public Texture2D rostroMis;
    public GUISkin miSkin;

    public bool cercaDeMision = false;
    private float tiempoMisionCompletada = 0f;
    public bool missionFinished = false;

    private float tiempoInicioMision = 0f;
    private bool mostrandoInstrucciones = false;

    void Start()
    {
        firstQuest = new Rect(30, 30, 500, 300);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !activarQuest && cercaDeMision)
        {
            activarQuest = true;
            mediumQuest = false;
            finishQuest = false;
            mostrandoInstrucciones = true;
            tiempoInicioMision = Time.time;
        }

        if (mostrandoInstrucciones && Time.time - tiempoInicioMision > 5f)
        {
            mostrandoInstrucciones = false;
            mediumQuest = true;
        }

        if (finishQuest && !missionFinished)
        {
            tiempoMisionCompletada = Time.time;
            missionFinished = true;
        }

        if (missionFinished && Time.time - tiempoMisionCompletada > 5f)
        {
            ResetQuest();
        }
    }

    void OnGUI()
    {
        GUI.skin = miSkin;

        GUIStyle titleStyle = new GUIStyle(GUI.skin.window)
        {
            fontSize = 20,
            alignment = TextAnchor.UpperCenter
        };

        if (activarQuest && mostrandoInstrucciones)
        {
            firstQuest = GUI.Window(0, firstQuest, MostrarInstrucciones, "Misión - " + nomMision, titleStyle);
        }

        if (mediumQuest)
        {
            firstQuest = GUI.Window(0, firstQuest, Quest_Incompleta, "Misión en progreso", titleStyle);
        }

        if (finishQuest)
        {
            firstQuest = GUI.Window(0, firstQuest, Quest_Completa, "Misión completada - " + nomMision, titleStyle);
        }

        if (cercaDeMision && !finishQuest && !activarQuest)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 100),
                "Presiona E para comenzar la misión",
                new GUIStyle()
                {
                    fontSize = 40,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = Color.white }
                });
        }
    }

    void MostrarInstrucciones(int WindowID)
    {
        GUI.Label(new Rect(30, 100, 440, 60), textMisionIncompleta, new GUIStyle("Box") { fontSize = 20, alignment = TextAnchor.MiddleCenter });
        GUI.DrawTexture(new Rect(350, 50, 100, 100), rostroMis);
    }

    void Quest_Incompleta(int WindowID)
    {
        string progreso = $"Buscar palanca: {Opciones2.piedras} / 1";
        GUI.Label(new Rect(30, 100, 440, 60), progreso, new GUIStyle("Box") { fontSize = 20, alignment = TextAnchor.MiddleCenter });
        GUI.DrawTexture(new Rect(350, 50, 100, 100), rostroMis);

        if (Opciones2.piedras >= 1)
        {
            finishQuest = true;
            mediumQuest = false;
        }
    }

    void Quest_Completa(int WindowID)
    {
        GUI.Label(new Rect(30, 100, 440, 60), textMisionCompleta, new GUIStyle("Box") { fontSize = 20, alignment = TextAnchor.MiddleCenter });
        GUI.DrawTexture(new Rect(350, 50, 100, 100), rostroMis);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDeMision = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDeMision = false;
        }
    }

    public void ResetQuest()
    {
        activarQuest = false;
        mediumQuest = false;
        finishQuest = false;
        cercaDeMision = false;
        missionFinished = false;
        mostrandoInstrucciones = false;
        tiempoInicioMision = 0f;
        tiempoMisionCompletada = 0f;
        Opciones2.piedras = 0; // Reinicia el progreso de la misión
    }
}