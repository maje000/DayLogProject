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

        todoList.AddTodo("��ġ�ϱ�");
        todoList.AddTodo("���� �Ͼ��");
        todoList.AddTodo("�����ñ�");
        todoList.AddTodo("����");
        todoList.AddTodo("�Ӹ�ī�� �����ϱ�");
        todoList.AddTodo("��Ա�");

        UpdateTodoList();
    }

    private void UpdateTodoList()
    {
        int count = todoList.Count;

        for(int i = 0; i < toggles.Length; i++)
        {
            if (i < count)
            {
                // Todo ������Ʈ

                toggles[i].SetName(todoList[i].contents);
                toggles[i].isOn = todoList[i].isDone;
            }
            else
            {
                // ��Ȱ��ȭ
                toggles[i].SetActive(false);
            }
        }
    }
}
