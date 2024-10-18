using UnityEngine;
using UnityEngine.UI;

public class BarUpdater : MonoBehaviour
{
    [SerializeField] private Image _fullBar;

    public void SetAmmount(float current, float max)
    {
        _fullBar.fillAmount = current / max;
    }
}
