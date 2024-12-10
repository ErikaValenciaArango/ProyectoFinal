using UnityEngine;

public class QuestGUI2 : MonoBehaviour
{
    public static bool activarQuest = false;
    private bool mediumQuest = false;
    private bool finishQuest = false;

    public Rect firstQuest;
    public string nomMision = "";
    public string textMisionIncompleta = "Recolecta 3 piedras para completar la misión.";
    public string textMisionCompleta = "Misión completada. Has recolectado todas las piedras.";
    public Texture2D rostroMis;
    public GUISkin miSkin;

    private bool cercaDeMision = false;
    private float tiempoMisionCompletada = 0f;
    private bool missionFinished = false;

    void Start()
    {
        firstQuest = new Rect(30, 30, 500, 300);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !activarQuest && cercaDeMision)
        {
            activarQuest = true;
            mediumQuest = true;
            finishQuest = false;
        }

        if (finishQuest && !missionFinished)
        {
            tiempoMisionCompletada = Time.time;
            missionFinished = true;
        }

        if (missionFinished && Time.time - tiempoMisionCompletada > 5f)
        {
            finishQuest = false;
            activarQuest = false;
            mediumQuest = false;
            cercaDeMision = false;
        }
    }

    void OnGUI()
    {
        GUI.skin = miSkin;
        GUIStyle style = new GUIStyle(GUI.skin.window) { fontSize = 30 };

        if (activarQuest)
        {
            firstQuest = GUI.Window(0, firstQuest, Quest, "Mision - " + nomMision);
        }

        if (mediumQuest)
        {
            firstQuest = GUI.Window(0, firstQuest, Quest_Incompleta, "Mision en progreso");
        }

        if (finishQuest)
        {
            firstQuest = GUI.Window(0, firstQuest, Quest_Completa, "Mision completada - " + nomMision);
        }

        if (cercaDeMision && !finishQuest)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 100), "Presiona E para comenzar la misión", new GUIStyle() { fontSize = 40, alignment = TextAnchor.MiddleCenter, normal = { textColor = Color.white } });
        }
    }

    void Quest(int WindowID)
    {
        GUI.Label(new Rect(30, 100, 440, 60), textMisionIncompleta, new GUIStyle("Box") { fontSize = 20, alignment = TextAnchor.MiddleCenter });
        GUI.DrawTexture(new Rect(350, 50, 100, 100), rostroMis);
    }

    void Quest_Incompleta(int WindowID)
    {
        string progreso = $"Piedras recolectadas: {Opciones2.piedras} / 3";
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
}
