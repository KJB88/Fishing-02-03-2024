using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerView : MonoBehaviour
{
    [SerializeField] Slider powerBar;
    [SerializeField] TMP_Text powerText;

    public Slider PowerBar => powerBar;
    public TMP_Text PowerText => powerText;
}
