using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageDisplayer_TodoList : MonoBehaviour
{
    private Toggle[] toggles;
    private GameObject _popup_AddTodo;
    //public delegate void OnClickEvent();
    

    /// <summary>
    /// ȭ�鿡 ǥ�õǴ� Toggle���� �ڵ��� ��������, �� UI Event���� ��������
    /// </summary>
    public void Init()
    {
        toggles = transform.Find("List/ViewPort/Contents").GetComponentsInChildren<Toggle>();

        Button button_AddTodo = transform.Find("Button_AddTodo").GetComponent<Button>();
        button_AddTodo.onClick.AddListener(OnClick_AddTodo);


        // Popup Init
        Transform Popup_AddTodoT = transform.Find("Popup/Popup_AddTodo");
        if (Popup_AddTodoT != null)
        {
            Transform Button_DoneT = Popup_AddTodoT.Find("Button_Done");
            if (Button_DoneT != null)
            {
                Button Button_Done = Button_DoneT.GetComponent<Button>();
                Button_Done.onClick.RemoveAllListeners();
                Button_Done.onClick.AddListener(OnClick_Done);
            }

            Transform Button_CloseT = Popup_AddTodoT.Find("Button_Close");
            if (Button_CloseT != null)
            {
                Button Button_Close = Button_CloseT.GetComponent<Button>();
                Button_Close.onClick.RemoveAllListeners();
                Button_Close.onClick.AddListener(OnClick_Close);
            }

            _popup_AddTodo = Popup_AddTodoT.gameObject;
            _popup_AddTodo.SetActive(false);
        }
    }

    /// <summary>
    /// ���� ���� todoList�� ȭ�鿡 ǥ�õǴ� Toggle���� ���ؼ� ����ڿ��� ����
    /// </summary>
    /// <param name="todoList">TodoList�� Enumerator - Read Only </param>
    public void UpdateTodoList(IEnumerator<DataManager.Todo> todoList)
    {
        if (todoList == null)
        {
            // ���� ���� ��� ��� ����� ��Ȱ��ȭ
            foreach(Toggle toggle in toggles)
            {
                toggle.SetActive(false);
            }

            return;
        }

        for(int i = 0; i < toggles.Length; i++)
        {
            if (todoList.MoveNext())
            {
                // Todo ������Ʈ
                toggles[i].SetName(todoList.Current.contents);
                toggles[i].isOn = todoList.Current.isDone;
            }
            else
            {
                // �������� ��� ��Ȱ��ȭ
                toggles[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// AddTodo ��ư�� Event
    /// </summary>
    private void OnClick_AddTodo()
    {
        OpenPopup_AddTodo();
    }

    private void OnClick_Done()
    {
        ClosePopup_AddTodo();
    }

    private void OnClick_Close()
    {
        ClosePopup_AddTodo();
    }

    public void OpenPopup_AddTodo()
    {
        _popup_AddTodo.SetActive(true);
    }
    public void ClosePopup_AddTodo()
    {
        // �˾��� ������ �ʱ�ȭ
        _popup_AddTodo.transform.Find("Dropdown").GetComponent<TMPro.TMP_Dropdown>().value = 0;
        _popup_AddTodo.transform.Find("InputField (TMP)").GetComponent<TMPro.TMP_InputField>().text = "";
        // �˾��� ����
        _popup_AddTodo.SetActive(false);
    }
}
