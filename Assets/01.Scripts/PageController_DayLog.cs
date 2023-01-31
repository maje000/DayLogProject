using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController_DayLog : MonoBehaviour
{
    private PageDisplayer_DayLog _uiDisplayer;
    private List<DataManager.Schedule> _scheduleList;
    

    private void OnEnable()
    {
        _uiDisplayer = GetComponent<PageDisplayer_DayLog>();
        _uiDisplayer.Init();

        LoadSchedules();
    }

    private void OnDisable()
    {
        SaveSchedules();
    }

    public void AddSchedule(string contents)
    {
        _scheduleList.AddSchedule(contents);

        List<string> schedules = new List<string>();

        foreach(DataManager.Schedule schedule in _scheduleList)
        {
            schedules.Add(schedule.contents);
        }

        _uiDisplayer.SetLog(schedules.ToArray());
        _uiDisplayer.UpdateLogList();
    }
    public void RemoveSchedules()
    {
        _scheduleList.Clear();
        _uiDisplayer.UpdateLogList();
    }

    [ContextMenu("SaveSchedules")]
    private void SaveSchedules()
    {
        DataManager.SaveSchedule(_scheduleList.ToArray());
    }

    [ContextMenu("LoadSchedules")]
    private void LoadSchedules()
    {
        _scheduleList.AddSchedules(DataManager.LoadSchedule());
    }
}