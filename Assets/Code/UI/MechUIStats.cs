using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MechUIStats : MonoBehaviour
{
    [SerializeField] GameObject _statPanel;
    [SerializeField] TextMeshProUGUI _statTextArmor;
    [SerializeField] TextMeshProUGUI _statTextSpeed;

    [Space(10)]
    [SerializeField] GameObject _mobilityFlagsWindow;
    [SerializeField] TextMeshProUGUI _mobilityFlagsText;


    Mech _displayedMech;

    public void Display(Mech inMech)
    {
        _displayedMech = inMech;

        _statPanel.SetActive(true);
        _mobilityFlagsWindow.SetActive(true);

        _statTextArmor.text = $"Armor: { inMech.healthComponent.currentHealth }";
        _statTextSpeed.text = $"Speed: { inMech.movementComponent.moveSpeed }";
        
        MobilityFlags[] mobilityFlags = inMech.mobilityType.data.GetMobilityFlags();
        if (mobilityFlags.Length > 0)
        {
            _mobilityFlagsWindow.SetActive(true);
            _mobilityFlagsText.text = "";
        
            foreach (var item in mobilityFlags)
                _mobilityFlagsText.text += item.ToString() + "\n";
        }
    }

    public void Hide(Mech inMech)
    {
        if (_displayedMech != inMech)
            return;

        _displayedMech = null;

        _statPanel.SetActive(false);
        _mobilityFlagsWindow.SetActive(false);
    }
}
