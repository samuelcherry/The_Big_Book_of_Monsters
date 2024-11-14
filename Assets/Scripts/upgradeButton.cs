using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public int roleIndex;
    public int upgradeIndex;
    public Upgrades upgradesScript; // Reference to the main Upgrades script

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnUpgradeClicked);
    }

    void OnUpgradeClicked()
    {
        upgradesScript.PurchaseUpgrade(roleIndex, upgradeIndex);
    }
}