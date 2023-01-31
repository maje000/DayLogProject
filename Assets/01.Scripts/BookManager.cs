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

        /** 페이지 위치 지정
         * 현재 페이지 위치 > _centerPagePos
         * 이전 페이지들 위치 > _leftPagePos 이전 페이지들은 모두 이 위치에
         * 다음 페이지들 위치 > _rightPagePos 다음 페이지들은 모두 이 위치에
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
        /** 예외처리.. **/
        int TouchCount = Input.touchCount;
        if (TouchCount == 1)
        {
            Touch[] touch = Input.touches;

            _horizontalSlideValue += touch[0].deltaPosition.x;

            // TODO:: 다음 페이지 넘어가고 바로 다음 페이지로 넘어가지 못하게 막아야 함.
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
            // 마지막 페이지. 다음 페이지는 없다.
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
            // 첫 페이지. 이전 페이지는 없다.
            return;
        }

        Transform currentPageT = pages[currentPage];
        currentPageT.position = _rightPagePos;
        currentPage--;
    }
    #endregion
}
