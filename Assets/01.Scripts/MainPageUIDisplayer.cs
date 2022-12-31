using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPageUIDisplayer : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _displayText_CurrentTime;
    private Transform _logListHolder;

    // From Model
    private string[] _logList;
    public GameObject logPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _displayText_CurrentTime = transform.Find("Text_Today").GetComponent<TMPro.TextMeshProUGUI>();
        UpdateTodayDisplayer();

        _displayText_CurrentTime = transform.Find("Text_CurrentTime").GetComponent<TMPro.TextMeshProUGUI>();

        _logListHolder = transform.Find("Panel_LogDisplayer/LogListHolder");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeDisplayer();
    }

    private void UpdateTodayDisplayer()
    {
        DateTime currentDate = DateTime.Now;

        string displayDate = string.Format("{0:0000}.{1:00}.{2:00}\n({3})",
            currentDate.Year, currentDate.Month, currentDate.Day, currentDate.DayOfWeek);

        _displayText_CurrentTime.text = displayDate;
    }


    private int second = -1;
    private void UpdateTimeDisplayer()
    {
        if (second != DateTime.Now.Second)
        {
            Debug.Log("Tic");
            DateTime CurrentTIme = DateTime.Now;

            string diplayTime = string.Format("{0:00}:{1:00}.{2:00}", 
                CurrentTIme.Hour, CurrentTIme.Minute, CurrentTIme.Second);

            _displayText_CurrentTime.text = diplayTime;

            second = CurrentTIme.Second;
        }
    }

    private void UpdateLogList()
    {
        // Pooling Object
        ClearLogList();

        for (int i = 0; i < _logList.Length; i++)
        {
            Instantiate(logPrefab, _logListHolder);
        }
        // end PoolingObject
    }

    private void ClearLogList()
    {
        int logCount = _logListHolder.childCount;

        if (logCount != 0)
        {
            for (int i = 0; i < logCount; i++)
            {
                Destroy(_logListHolder.GetChild(i));
            }
        }
    }

    public void SetLogList(string[] logList)
    {
        _logList = logList;

        UpdateLogList();
    }
}
