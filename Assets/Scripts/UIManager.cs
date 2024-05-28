using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 기존 인스턴스가 없으면 새로운 인스턴스 생성
                _instance = FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<UIManager>();
                    singletonObject.name = typeof(UIManager).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    private static UIManager _instance;

    [SerializeField] GameObject clearUI;
    [SerializeField] GameObject deadUI;
    void Awake()
    {
        // 중복된 인스턴스가 생성되지 않도록 체크
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void F_OnClearUI()
    {
        clearUI.SetActive(true);
    }

    public void F_OnDeadUI() 
    {
        deadUI.SetActive(true);
    }

    public void F_OnClickMain()
    {
        //메인화면 가기
        //메인 씬 가기
    }
    public void F_OnClickRetry()
    {
        //현재 스테이지 재시작
        //씬 재시작
    }
    public void F_OnClickNext()
    {
        //다음 스테이지 시작
        //씬 배열 만들고 다음 씬 시작
    }
}
