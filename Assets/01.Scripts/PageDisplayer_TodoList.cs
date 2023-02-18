using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageDisplayer_TodoList : MonoBehaviour
{
    private Toggle[] toggles;
    [SerializeField] private List<DataManager.Todo> todoList;

    void Start()
    {
        toggles = transform.Find("List/ViewPort/Contents").GetComponentsInChildren<Toggle>();
        toggles[0].SetName("Hellow");
        toggles[1].SetName("World");

        todoList.AddTodo("양치하기");
        todoList.AddTodo("일찍 일어나기");
        todoList.AddTodo("물마시기");
        todoList.AddTodo("헤헤");
        todoList.AddTodo("머리카락 정리하기");
        todoList.AddTodo("밥먹기");

        UpdateTodoList();
    }

    private void UpdateTodoList()
    {
        int count = todoList.Count;

        for(int i = 0; i < toggles.Length; i++)
        {
            if (i < count)
            {
                // Todo 업데이트

                toggles[i].SetName(todoList[i].contents);
                toggles[i].isOn = todoList[i].isDone;
            }
            else
            {
                // 비활성화
                toggles[i].SetActive(false);
            }
        }
    }
}
