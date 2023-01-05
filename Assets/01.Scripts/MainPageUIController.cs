using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageUIController : MonoBehaviour
{
    MainPageUIDisplayer _uiDisplayer;
    List<string> _logList;


    private void OnEnable()
    {
        _uiDisplayer = GetComponent<MainPageUIDisplayer>();
        _uiDisplayer.Init();

        _logList = new List<string>();
        LoadData();
    }

    private void OnDisable()
    {
        SaveData();
    }

    public void AddLog(string contents)
    {
        DateTime now = DateTime.Now;

        string outputTextFormat = string.Format($"{now.Hour:00}Ω√ {now.Minute:00}∫– {now.Second:00}√ :: {contents}");

        _logList.Add(outputTextFormat);

        _uiDisplayer.SetLog(_logList.ToArray());
        _uiDisplayer.UpdateLogList();
    }

    private void LoadData()
    {
        _logList.Clear();

        int count = PlayerPrefs.GetInt("Count");

        for (int i = 0; i < count; i++)
        {
            _logList.Add(PlayerPrefs.GetString($"Contents_{i}"));
        }

        _uiDisplayer.SetLog(_logList.ToArray());
        _uiDisplayer.UpdateLogList();
    }

    private void SaveData()
    {
        int count = _logList.Count;
        PlayerPrefs.SetInt("Count", count);
        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.SetString($"Contents_{i}", _logList[i]);
        }
    }
    
    public void RemoveData()
    {
        PlayerPrefs.DeleteAll();

        _logList.Clear();

        _uiDisplayer.SetLog(_logList.ToArray());
        _uiDisplayer.UpdateLogList();
    }
}
