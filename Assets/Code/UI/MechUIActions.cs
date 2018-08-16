using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MechUIActions : MonoBehaviour
{
    [SerializeField] GameObject _mechActionsPanel;
    [SerializeField] GameObject _weaponButtonPrefab;

    Weapon[] _displayedWeapons;

    Mech _displayedMech;

    public event Action<Weapon> OnWeaponButtonClicked;


    public void Display(Mech inMech)
    {
        Debug.Log("Todo: make onWeaponButtonClicked event execute properly");

        Clear();

        _displayedMech = inMech;
        _mechActionsPanel.SetActive(true);

        _displayedWeapons = inMech.utilityComponent.weapons;
        foreach (Weapon displayedWeapon in _displayedWeapons)
            CreateWeaponButton(displayedWeapon);
    }

    void CreateWeaponButton(Weapon inWeaponToDisplay)
    {
        GameObject newWeaponButton = Instantiate(_weaponButtonPrefab);
        newWeaponButton.transform.SetParent(_mechActionsPanel.transform);
        newWeaponButton.transform.localScale = Vector3.one;

        newWeaponButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = inWeaponToDisplay.data.name;

        newWeaponButton.GetComponent<Button>().onClick.AddListener(() => OnWeaponButtonClicked?.Invoke(inWeaponToDisplay));
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
