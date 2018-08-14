using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MechUIActions : MonoBehaviour
{
    [SerializeField] GameObject _mechActionsPanel;
    [SerializeField] GameObject _weaponButtonPrefab;

    Mech _displayedMech;


    public void Display(Mech inMech)
    {
        Clear();

        _displayedMech = inMech;
        _mechActionsPanel.SetActive(true);

        foreach (Weapon mechWeapon in inMech.utilityComponent.weapons)
        {
            GameObject newWeaponButton = Instantiate(_weaponButtonPrefab);
            newWeaponButton.transform.SetParent(_mechActionsPanel.transform);
            newWeaponButton.transform.localScale = Vector3.one;

            newWeaponButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mechWeapon.data.name;
        }
    }

    public void Hide(Mech inMech)
    {
        if (_displayedMech != inMech)
            return;

        Clear();
    }

    void Clear()
    {
        _displayedMech = null;
        _mechActionsPanel.SetActive(false);

        foreach (Transform child in _mechActionsPanel.transform)
            Destroy(child.gameObject);
    }
}
