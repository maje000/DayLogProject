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
        if (schedules == null)
            return;

        ScheduleArray scheduleArray = new ScheduleArray();
        scheduleArray.schedules = schedules;

        string saveData = JsonUtility.ToJson(scheduleArray, true);
        
        string path = Path.Combine(Application.persistentDataPath, "ScheduleData.txt");
        File.WriteAllText(path, saveData);
    }

    static public Schedule[] LoadSchedule()
    {
        string path = Path.Combine(Application.persistentDataPath, "ScheduleData.txt");
        if (File.Exists(path))
        {
            string loadData = File.ReadAllText(path);

            if (!string.IsNullOrEmpty(loadData))
            {
                ScheduleArray schedules = JsonUtility.FromJson<ScheduleArray>(loadData);
                return schedules.schedules;
            }
        }
        
        return null;
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
        if (todoList == null)
            return;

        TodoListArray todoListArray = new TodoListArray();
        todoListArray.todoList = todoList;

        string saveData = JsonUtility.ToJson(todoListArray, true);

        string path = Path.Combine(Application.persistentDataPath, "TodoListData.txt");
        File.WriteAllText(path, saveData);
    }

    static public Todo[] LoadTodoList()
    {
        string path = Path.Combine(Application.persistentDataPath, "TodoListData.txt");
        if (File.Exists(path))
        {
            string loadData = File.ReadAllText(path);

            if (!string.IsNullOrEmpty(loadData))
            {
                TodoListArray todoListArray = JsonUtility.FromJson<TodoListArray>(loadData);

                return todoListArray.todoList;
            }
        }

        return null;
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
        if (schedules == null)
        {
            schedules = new List<DataManager.Schedule>();
        }

        // Set::Schedule data;
        DataManager.Schedule schedule = new DataManager.Schedule();

        DateTime now = DateTime.Now;
        schedule.year = now.Year;
        schedule.month = now.Month;
        schedule.day = now.Day;
        schedule.dayOfWeek = (DataManager.DayOfWeek)now.DayOfWeek;
        schedule.startTime = now.Hour * 60 * 60 + now.Minute * 60 + now.Second;
        schedule.contents = contents;

        schedules.Add(schedule);
    }

    static public void AddSchedules(this List<DataManager.Schedule> schedules, DataManager.Schedule[] newSchedule)
    {
        if (schedules == null)
        {
            schedules = new List<DataManager.Schedule>();
        }

        schedules.AddRange(newSchedule);
    }

    static public void AddTodo(this List<DataManager.Todo> todoList, DataManager.Todo newTodo)
    {
        if (todoList == null)
        {
            todoList = new List<DataManager.Todo>();
        }

        todoList.Add(newTodo);
    }

    static public void AddTodo(this List<DataManager.Todo> todoList, string contents)
    {
        if (todoList == null)
        {
            todoList = new List<DataManager.Todo>();
        }

        DataManager.Todo todo = new DataManager.Todo();

        DateTime now = DateTime.Now;
        todo.dayOfWeek = (DataManager.DayOfWeek)now.DayOfWeek;
        todo.contents = contents;

        todoList.Add(todo);
    }
}