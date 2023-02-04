using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageDisplayer_DayLog : MonoBehaviour
{
    PageController_DayLog _uiController;

    private TextMeshProUGUI Text_Today;
    private TextMeshProUGUI Text_CurrentTIme;
    private Transform LogListHolder;
    private TMP_InputField InputField_AddLog;
    private Button Button_Reset;
    private Button Button_Exit;

    private string[] _logList;
    public GameObject logPrefab;


    /** API��
     * API�� ���� �� �������� ������ ����Ͽ� �� ����Ʈ���� ���� ��Ұ� ���� ����� �� �ְ� �ϴ� ��Ŀ�����̴�.
     * */
    #region API
    public void Init()
    {
        /*�ڵ��� ������..*/
        _uiController = GetComponent<PageController_DayLog>();

        // ��� Text �ڵ�
        Text_Today = transform.Find("Text_Today").GetComponent<TextMeshProUGUI>();
        Text_CurrentTIme = transform.Find("Text_CurrentTime").GetComponent<TextMeshProUGUI>();

        LogListHolder = transform.Find("Panel_LogDisplayer/LogListHolder");

        // ��ǲ�ʵ� �ڵ� �� �̺�Ʈ �߰�
        InputField_AddLog = transform.Find("InputField_AddLog").GetComponent<TMP_InputField>();
        InputField_AddLog.onSubmit.AddListener(OnSubmit_AddLog);

        // ��ư �ڵ�
        Button_Reset = transform.Find("Button_Reset").GetComponent<Button>();
        Button_Exit = transform.Find("Button_Exit").GetComponent<Button>();

        // ��ư �̺�Ʈ �߰�
        Button_Reset.onClick.AddListener(OnClick_Reset); 
        Button_Exit.onClick.AddListener(OnClick_Exit);


        /*�ʱ� ȭ�� ���..*/
        UpdateTodayDisplayer();
    }

    public void UpdateLogList()
    {
        ClearLog();

        for (int i = 0; i < _logList.Length; i++)
        {
            CreateLog(_logList[i]);
        }
    }

    public void SetLog(string[] logList)
    {
        _logList = logList;
    }
    #endregion

    #region OnUIEvent
    public void OnSubmit_AddLog(string contents)
    {
        if (string.IsNullOrEmpty(contents))
        {
            return;
        }

        _uiController.AddSchedule(contents);
        InputField_AddLog.text = "";
        InputField_AddLog.onDeselect.Invoke("");
    }

    public void OnClick_Reset()
    {

    }

    public void OnClick_Exit()
    {
        Application.Quit();
    }
    #endregion

    #region Unity lifecycle
    private void Update()
    {
        UpdateTimeDisplayer();
    }
    #endregion

    #region Private Method
    private void UpdateTodayDisplayer()
    {
        DateTime currentDate = DateTime.Now;

        string displayDate = string.Format("{0:0000}.{1:00}.{2:00}\n({3})",
            currentDate.Year, currentDate.Month, currentDate.Day, currentDate.DayOfWeek);

        Text_Today.text = displayDate;
    }

    private int _secondCount = -1;
    private void UpdateTimeDisplayer()
    {
        if (_secondCount != DateTime.Now.Second)
        {
            Debug.Log("Tic");
            DateTime CurrentTIme = DateTime.Now;

            string diplayTime = string.Format("{0:00}:{1:00}.{2:00}", 
                CurrentTIme.Hour, CurrentTIme.Minute, CurrentTIme.Second);

            Text_CurrentTIme.text = diplayTime;

            _secondCount = CurrentTIme.Second;
        }
    }

    private void CreateLog(string contents)
    {
        GameObject log = Instantiate(logPrefab, LogListHolder);
        log.GetComponent<TextMeshProUGUI>().text = contents;
    }

    private void ClearLog()
    {
        int logCount = LogListHolder.childCount;

        if (logCount != 0)
        {
            for (int i = 0; i < logCount; i++)
            {
                Destroy(LogListHolder.GetChild(i).gameObject);
            }
        }
    }
    #endregion
}
