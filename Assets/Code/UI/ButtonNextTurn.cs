using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNextTurn : MonoBehaviour
{
    public event Action OnClicked;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => OnClicked?.Invoke());
    }
}
