using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageDisplayer_DayLog : MonoBehaviour
{
    PageController_DayLog _pageController;

    // Resource
    public GameObject prefab_Schedule;

    // UI
    private TextMeshProUGUI Displayer_Today;
    private TextMeshProUGUI Displayer_Time;
    private Transform SchduleListHolder;
    private TMP_InputField InputField_AddSchedule;
    private Button Button_Reset;
    private Button Button_Exit;

    /** API��
     * API�� ���� �� �������� ������ ����Ͽ� �� ����Ʈ���� ���� ��Ұ� ���� ����� �� �ְ� �ϴ� ��Ŀ�����̴�.
     * */
    #region API
    public void Init()
    {
        /*�ڵ��� ������..*/
        _pageController = GetComponent<PageController_DayLog>();

        // ��� Text �ڵ�
        Displayer_Today = transform.Find("Displayer_Today").GetComponent<TextMeshProUGUI>();
        Displayer_Time = transform.Find("Displayer_Time").GetComponent<TextMeshProUGUI>();

        SchduleListHolder = transform.Find("Displayer_Schedule/ScheduleListHolder");

        // ��ǲ�ʵ� �ڵ� �� �̺�Ʈ �߰�
        InputField_AddSchedule = transform.Find("InputField_AddSchedule").GetComponent<TMP_InputField>();
        InputField_AddSchedule.onSubmit.AddListener(OnSubmit_AddSchedule);

        // ��ư �ڵ�
        Button_Reset = transform.Find("Button_Reset").GetComponent<Button>();
        Button_Exit = transform.Find("Button_Exit").GetComponent<Button>();

        // ��ư �̺�Ʈ �߰�
        Button_Reset.onClick.AddListener(OnClick_Reset);
        Button_Exit.onClick.AddListener(OnClick_Exit);


        /*�ʱ� ȭ�� ���..*/
        UpdateTodayDisplayer();
        _pageController.UpdateScheduiles();
    }

    public void CreateSchedule(string content)
    {
        GameObject schedule = Instantiate(prefab_Schedule, SchduleListHolder);

        schedule.GetComponent<TextMeshProUGUI>().text = content;
    }

    public void ClearScheduleDisplayer()
    {
        int scheduleCount = SchduleListHolder.childCount;

        if (scheduleCount != 0)
        {
            for (int i = 0; i < scheduleCount; i++)
            {
                Destroy(SchduleListHolder.GetChild(i).gameObject);
            }
        }
    }
    #endregion

    #region OnUIEvent
    /// <summary>
    /// InputField�� �Էµ� ������ Submit �Ǿ��� �� ����
    /// </summary>
    /// <param name="contents"></param>
    public void OnSubmit_AddSchedule(string contents)
    {
        // ���ڿ� �� üũ
        if (string.IsNullOrEmpty(contents))
        {
            return;
        }

        // ��Ʈ�ѷ����� �̺�Ʈ�� �߻����� ����
        _pageController.OnAddSchedule(contents);

        // �츮�� �ʱ�ȭ
        InputField_AddSchedule.text = "";
        InputField_AddSchedule.onDeselect.Invoke("");
    }

    public void OnClick_Reset()
    {
        _pageController.RemoveSchedules();
    }

    public void OnClick_Exit()
    {
        // ���� ����...
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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

        Displayer_Today.text = displayDate;
    }

    private int _secondCount = -1;
    private void UpdateTimeDisplayer()
    {
        if (_secondCount != DateTime.Now.Second)
        {
            //Debug.Log("Tic");
            DateTime CurrentTIme = DateTime.Now;

            string diplayTime = string.Format("{0:00}:{1:00}.{2:00}",
                CurrentTIme.Hour, CurrentTIme.Minute, CurrentTIme.Second);

            Displayer_Time.text = diplayTime;

            _secondCount = CurrentTIme.Second;
        }
    }

    private void OpenPopup_ScheduleData(int id)
    {

    }
    #endregion
}
