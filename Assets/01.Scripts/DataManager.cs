using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager
{
    public enum DayOfWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }



    [System.Serializable]
    public struct Schedule
    {
        public int id;
        public int year;
        public int month;
        public int day;
        public DayOfWeek dayOfWeek;
        public int startTime;
        public int endTime;
        public string contents;
    }

    public struct ScheduleArray
    {
        public Schedule[] schedules;
    }

    static public void SaveSchedule(Schedule[] schedules)
    {
        ScheduleArray scheduleArray = new ScheduleArray();
        scheduleArray.schedules = schedules;

        string saveData = JsonUtility.ToJson(scheduleArray, true);

        string path = Path.Combine(Application.streamingAssetsPath, "SaveData.txt");
        File.WriteAllText(path, saveData);
    }

    static public Schedule[] LoadSchedule()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "SaveData.txt");
        string loadData = File.ReadAllText(path);

        ScheduleArray schedules = JsonUtility.FromJson<ScheduleArray>(loadData);

        return schedules.schedules;
    }

    static public Schedule GetSchedule(int id, Schedule[] schedules)
    {
        int scheduleCount = schedules.Length;

        for(int i = 0; i < scheduleCount; i++)
        {
            if (schedules[i]
                
                .id == id)
            {
                return schedules[i];
            }
        }

        return new Schedule();
    }

    //=====================================================================================================TodoList
    [System.Serializable]
    public struct Todo
    {
        public DayOfWeek dayOfWeek;
        public string contents;
        public bool isDone;
    }

    public struct TodoListArray
    {
        public Todo[] todoList;
    }

    static public void SaveTodoList(Todo[] todoList)
    {
        TodoListArray todoListArray = new TodoListArray();
        todoListArray.todoList = todoList;

        string saveData = JsonUtility.ToJson(todoListArray, true);

        string path = Path.Combine(Application.streamingAssetsPath, "SaveData_TodoList.txt");
        File.WriteAllText(path, saveData);
    }

    static public Todo[] LoadTodoList()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "SaveData_TodoList.txt");
        string loadData = File.ReadAllText(path);

        TodoListArray todoListArray = JsonUtility.FromJson<TodoListArray>(loadData);

        return todoListArray.todoList;
    }
}

static class StaticExtention
{
    static public void AddSchedule(this List<DataManager.Schedule> schedules, DataManager.Schedule newSchedule)
    {
        if (schedules == null)
        {
            schedules = new List<DataManager.Schedule>();
        }

        schedules.Add(newSchedule);
    }

    static public void AddSchedule(this List<DataManager.Schedule> schedules, string contents)
    {
        // Set::Schedule data;
        DataManager.Schedule schedule = new DataManager.Schedule();

        DateTime now = DateTime.Now;
        schedule.year = now.Year;
        schedule.month = now.Month;
        schedule.day = now.Day;
        schedule.dayOfWeek = (DataManager.DayOfWeek)now.DayOfWeek;
        schedule.startTime = now.Hour * 60 * 60 + now.Minute * 60 + now.Second;
        schedule.contents = contents;

        schedules.AddSchedule(schedule);
    }

    static public void AddSchedules(this List<DataManager.Schedule> schedules, DataManager.Schedule[] newSchedule)
    {
        if (schedules == null)
        {
            schedules = new List<DataManager.Schedule>();
        }

        schedules.AddRange(newSchedule);
    }

    static public void AddTodo(this List<DataManager.Todo> TodoList, DataManager.Todo newTodo)
    {
        if (TodoList == null)
        {
            TodoList = new List<DataManager.Todo>();
        }

        TodoList.Add(newTodo);
    }

    static public void AddTodo(this List<DataManager.Todo> todoList, string contents)
    {
        DataManager.Todo todo = new DataManager.Todo();

        DateTime now = DateTime.Now;
        todo.dayOfWeek = (DataManager.DayOfWeek)now.DayOfWeek;
        todo.contents = contents;

        todoList.AddTodo(todo);
    }
}