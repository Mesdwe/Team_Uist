using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeButton : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

    }

    public void UpdateButtonState(bool interactable)
    {
        if (button != null)
            button.interactable = interactable;
    }
}
