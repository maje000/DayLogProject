using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    TMPro.TextMeshProUGUI Text_HorizontalSlideValue;
    public Transform[] pages;
    public int currentPage;
    public Vector3 _centerPagePos;
    public Vector3 _leftPagePos;
    public Vector3 _rightPagePos;

    private int TotalPageCount
    {
        get
        {
            if (pages != null)
            {
                return pages.Length;
            }

            return 0;
        }
    }

    private void Start()
    {
        Text_HorizontalSlideValue = transform.Find("Text_HorizontalSlideValue").GetComponent<TMPro.TextMeshProUGUI>();

        /** ������ ��ġ ����
         * ���� ������ ��ġ > _centerPagePos
         * ���� �������� ��ġ > _leftPagePos ���� ���������� ��� �� ��ġ��
         * ���� �������� ��ġ > _rightPagePos ���� ���������� ��� �� ��ġ��
         * **/
        //_centerPagePos = pages[0].position;
        _centerPagePos = new Vector3(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, 0);
        
        float pageCenterPosX = _centerPagePos.x;
        _leftPagePos = _centerPagePos - Vector3.right * (pageCenterPosX) * 2;
        _rightPagePos = _centerPagePos + Vector3.right * (pageCenterPosX) * 2;

        currentPage = 0;

        if (TotalPageCount > 1)
        for (int i = 1; i < pages.Length; i++)
        {
            pages[i].position = _rightPagePos;
        }
    }

    private void Update()
    {
        MobileTouchEvent();

        pages[currentPage].transform.position = _centerPagePos + Vector3.right * _horizontalSlideValue;
    }

    #region InputManager
    float _horizontalSlideValue = 0;

    private void MobileTouchEvent()
    {
        /** ����ó��.. **/
        int TouchCount = Input.touchCount;
        if (TouchCount == 1)
        {
            Touch[] touch = Input.touches;

            _horizontalSlideValue += touch[0].deltaPosition.x;

            // TODO:: ���� ������ �Ѿ�� �ٷ� ���� �������� �Ѿ�� ���ϰ� ���ƾ� ��.
            if (_horizontalSlideValue < -300)
            {
                NextPage();
            }
            else if (_horizontalSlideValue > 300)
            {
                PrePage();
            }
        }

        _horizontalSlideValue = Mathf.Lerp(_horizontalSlideValue, 0, 0.2f);

        Text_HorizontalSlideValue.text =string.Format($"{_horizontalSlideValue:0.0}") ;
    }

    private void NextPage()
    {
        if (currentPage >= TotalPageCount - 1)
        {
            // ������ ������. ���� �������� ����.
            return;
        }

        Transform currentPageT = pages[currentPage];
        currentPageT.position = _leftPagePos;
        currentPage++;
    }

    private void PrePage()
    {
        if (currentPage == 0)
        {
            // ù ������. ���� �������� ����.
            return;
        }

        Transform currentPageT = pages[currentPage];
        currentPageT.position = _rightPagePos;
        currentPage--;
    }
    #endregion
}
