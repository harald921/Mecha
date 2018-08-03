using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public partial class MechGUI : MonoBehaviour
{
    [Space(5)]
    [SerializeField] GameObject _actionPanel;
    [SerializeField] ActionPanelButtons _actionPanelButtons;

    [Space(5)]
    [SerializeField] GameObject _statPanel;
    [SerializeField] StatPanelTexts _statPanelTexts;

    [Space(5)]
    [SerializeField] GameObject _mechMobilityFlagsPanel;
    [SerializeField] TextMeshProUGUI _mechMobilityFlagsPanelText;


    Mech _displayedMech;

    public event Action OnMoveButtonPressed;
    public event Action OnAttackButtonPressed;


    void Awake()
    {
        _actionPanelButtons.buttonMove.onClick.AddListener(() => OnMoveButtonPressed?.Invoke());
        _actionPanelButtons.buttonAttack.onClick.AddListener(() => OnAttackButtonPressed?.Invoke());
    }

    public void Display(Mech inMech)
    {
        _displayedMech = inMech;

        _actionPanel.SetActive(true);
        _statPanel.SetActive(true);
        _mechMobilityFlagsPanel.SetActive(false);

        _statPanelTexts.armor.text = $"Armor: { inMech.healthComponent.currentHealth }";
        _statPanelTexts.speed.text = $"Speed: { inMech.movementComponent.moveSpeed }";

        MobilityFlags[] mobilityFlags = inMech.mobilityType.data.GetMobilityFlags();
        if (mobilityFlags.Length > 0)
        {
            _mechMobilityFlagsPanel.SetActive(true);
            _mechMobilityFlagsPanelText.text = "";

            foreach (var item in mobilityFlags)
                _mechMobilityFlagsPanelText.text += item.ToString() + "\n";
        }
    }

    public void Hide(Mech inMech)
    {
        if (_displayedMech != inMech)
            return;

        _displayedMech = null;

        _actionPanel.SetActive(false);
        _statPanel.SetActive(false);
        _mechMobilityFlagsPanel.SetActive(false);
    }


    [System.Serializable]
    struct StatPanelTexts
    {
        public TextMeshProUGUI armor;
        public TextMeshProUGUI speed;
    }

    [System.Serializable]
    struct ActionPanelButtons
    {
        public Button buttonMove;
        public Button buttonAttack;
    }
}

