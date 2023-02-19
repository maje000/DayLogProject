using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum PopupType
    { 
        AddTodo = 0,
        END,
    }

    public Transform _popupHolder;

    static UIManager _instance;
    UIManager Instance
    {
        get => _instance;
    }

    public GameObject Popup_AddTodo;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Reset()
    {
        _popupHolder = transform.Find("Popup");
    }

    //public void OpenPopup(PopupType popupType)
    //{
    //    switch (popupType)
    //    {
    //        case PopupType.AddTodo:
    //            Popup_AddTodo?.SetActive(true);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //public void ClosePopup(PopupType popupType)
    //{
    //    switch (popupType)
    //    {
    //        case PopupType.AddTodo:
    //            Popup_AddTodo?.SetActive(false);
    //            break;
    //        default:
    //            break;
    //    }
    //}
}

static public class UIExtention
{
    static public void SetName(this Toggle toggle, string name)
    {
        Transform textT = toggle.transform.Find("Text");
        textT.GetComponent<TextMeshProUGUI>().text = name;
    }

    static public void SetActive(this Toggle toggle, bool isActive)
    {
        toggle.gameObject.SetActive(isActive);
    }
}
