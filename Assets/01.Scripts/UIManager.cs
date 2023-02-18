using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
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
