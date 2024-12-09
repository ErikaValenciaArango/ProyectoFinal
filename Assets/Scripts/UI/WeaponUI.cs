
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image Image;
    [SerializeField] private TextMeshProUGUI magazineSizeText;
    [SerializeField] private TextMeshProUGUI storedAmmoText;

    public void UpdateInfo(int magazineSize, int storedAmmo)
    {
        magazineSizeText.text = magazineSize.ToString();
        storedAmmoText.text = storedAmmo.ToString();
        
    }

    public void UpdateAmmoUI (int magazineSize, int storedAmmo)
    {
        magazineSizeText.text = magazineSize.ToString();
        storedAmmoText.text = storedAmmo.ToString();
    }

}
