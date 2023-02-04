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


    /** API란
     * API는 정의 및 프로토콜 집합을 사용하여 두 소프트웨어 구성 요소가 서로 통신할 수 있게 하는 메커니즘이다.
     * */
    #region API
    public void Init()
    {
        /*핸들을 가져옴..*/
        _uiController = GetComponent<PageController_DayLog>();

        // 출력 Text 핸들
        Text_Today = transform.Find("Text_Today").GetComponent<TextMeshProUGUI>();
        Text_CurrentTIme = transform.Find("Text_CurrentTime").GetComponent<TextMeshProUGUI>();

        LogListHolder = transform.Find("Panel_LogDisplayer/LogListHolder");

        // 인풋필드 핸들 및 이벤트 추가
        InputField_AddLog = transform.Find("InputField_AddLog").GetComponent<TMP_InputField>();
        InputField_AddLog.onSubmit.AddListener(OnSubmit_AddLog);

        // 버튼 핸들
        Button_Reset = transform.Find("Button_Reset").GetComponent<Button>();
        Button_Exit = transform.Find("Button_Exit").GetComponent<Button>();

        // 버튼 이벤트 추가
        Button_Reset.onClick.AddListener(OnClick_Reset); 
        Button_Exit.onClick.AddListener(OnClick_Exit);


        /*초기 화면 출력..*/
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
