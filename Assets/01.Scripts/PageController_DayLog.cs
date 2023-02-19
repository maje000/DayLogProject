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
        if (_scheduleList == null)
        {
            _scheduleList = new List<DataManager.Schedule>();
        }

        LoadSchedules();

        _uiDisplayer = GetComponent<PageDisplayer_DayLog>();
        _uiDisplayer.Init();
    }

    private void OnDisable()
    {
        SaveSchedules();
    }

    public void OnAddSchedule(string contents)
    {
        _scheduleList.AddSchedule(contents);
        _uiDisplayer.CreateSchedule(contents);

        SaveSchedules();
    }

    public void RemoveSchedules()
    {
        _scheduleList.Clear();
        _uiDisplayer.ClearScheduleDisplayer();

        SaveSchedules();
    }

    public void UpdateScheduiles()
    {
        int scheduleCount = _scheduleList.Count;

        if (scheduleCount > 0)
        {
            _uiDisplayer.ClearScheduleDisplayer();

            for (int i = 0; i < scheduleCount; i++)
            {
                _uiDisplayer.CreateSchedule(_scheduleList[i].contents);
            }
        }
    }

    [ContextMenu("SaveSchedules")]
    private void SaveSchedules()
    {
        if (_scheduleList.Count > 0)
        {
            DataManager.SaveSchedule(_scheduleList.ToArray());
        }
    }

    [ContextMenu("LoadSchedules")]
    private void LoadSchedules()
    {
        DataManager.Schedule[] schedules = DataManager.LoadSchedule();

        if (schedules != null)
        {
            _scheduleList.AddSchedules(schedules);
        }
    }
}