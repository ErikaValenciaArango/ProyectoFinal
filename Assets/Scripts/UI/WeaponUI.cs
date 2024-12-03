using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image Image;
    [SerializeField] private TextMeshProUGUI magazineSizeText;
    [SerializeField] private TextMeshProUGUI magazineCountText;

    public void UpdateInfo(int magazineSize, int magazineCount)
    {
        magazineSizeText.text = magazineSize.ToString();
        int magazineCountAmount = magazineSize * magazineCount;
        magazineCountText.text = magazineCountAmount.ToString();
        
    }

}
