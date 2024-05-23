using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button _retryBtn;
    [SerializeField] Button _menuBtn;
    [SerializeField] Button _nextBtn;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void F_OnClickMenu()
    {
        //메뉴 창으로 가기
    }
    public void F_OnClickRetry()
    {
        //현재 스테이지 재시작
    }
    public void F_OnClickNext()
    {
        //다음 스테이지 시작
    }
}
