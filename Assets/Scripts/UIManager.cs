using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region SingleTon
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
    #endregion
    [SerializeField] GameObject clearUI;
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

    public void F_OnClickMain()
    {
        //메인화면 가기
        SceneManager.LoadScene(0);
        //메인 씬 가기
    }
    public void F_OnClickRetry()
    {
        //현재 스테이지 재시작
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex > 0)
            SceneManager.LoadScene(sceneIndex);
        else
            return;
        //씬 재시작
    }
    public void F_OnClickNext()
    {
        //다음 스테이지 인덱스
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // 씬 개수 가져오기
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex <= sceneCount)
            SceneManager.LoadScene(nextSceneIndex);
        else { }
            //다음 씬이 없다면 없다는 문구 띄우기
    }
}
