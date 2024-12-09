
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MaxCurrentHealt;
    [SerializeField] private TextMeshProUGUI CurrenHealth;
    // Start is called before the first frame update
    public void ChekHealth(int currentHealth, int maxHealth)
    {
        //Aca se manipula la vida del personaje
        CurrenHealth.text = currentHealth.ToString();
        MaxCurrentHealt.text = maxHealth.ToString();
    }
}
