using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UGUIDemo {

public class UIManager : MonoBehaviour
{
    public GameObject defaultSelection;
    
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(defaultSelection);
    }

    public void OnButtonPress(Button target) {
        Debug.LogFormat("OnButtonPress({0})", target.name);
    }
}
}
