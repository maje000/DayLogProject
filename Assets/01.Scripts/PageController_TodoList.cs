using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageController_TodoList : MonoBehaviour
{
    PageDisplayer_TodoList _displayer;
    List<DataManager.Todo> _todoList;

    private void OnEnable()
    {
        Init();

        _displayer = GetComponent<PageDisplayer_TodoList>();
        _displayer.Init();
        //_displayer.onClick_AddTodo = OnClick_AddTodo;

        _displayer.UpdateTodoList(_todoList.GetEnumerator());
    }

    private void Init()
    {
        if (_todoList == null)
        {
            _todoList = new List<DataManager.Todo>();
        }
    }
}
