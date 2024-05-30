using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class UIManager : MonoBehaviour
{


    [Header("DefaultUI")]
    [SerializeField] GameObject defaultUI; //상시 표시 UI
    [SerializeField] Text currentTimeTxt; //실시간 현재시간
    [SerializeField] Text moveCountTxt; //실시간 움직인 횟수

    [Header("ClearUI")]
    [SerializeField] GameObject clearUI; //클리어 후 표시 UI
    [SerializeField] Text clearPlayTimeTxt; //ClearUI에 표시될 걸린 시간
    [SerializeField] Text clearMoveCountTxt;//ClearUI에 표시될 움직인 최종 횟수

    [Header("DeadUI")]
    [SerializeField] GameObject deadUI; //사망 후 표시 UI
    [SerializeField] Text deadPlayTimeTxt; //DeadUI에 표시될 걸린 시간
    [SerializeField] Text deadMoveCountTxt;//DeadUI에 표시될 움직인 최종 횟수

    [Header("SettingUI")]
    [SerializeField] GameObject settingUI; //설정 화면 UI


    //[Header("Main Object")]

    #region SingleTon
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // 중복된 인스턴스가 생성되지 않도록 체크
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void F_OnClearUI()
    {
        clearUI.SetActive(true);
    }

    public void F_OnDeadUI()
    {
        deadUI.SetActive(true);
    }

    public void F_OnClickSettingUI()//SettingUI 끄고 켜기
    {
        if (settingUI.activeSelf)
            settingUI.SetActive(false);
        else
            settingUI.SetActive(true);
    }

    public void F_OnClickMain() //스테이지 선택 화면
    {
        //메인화면 가기
        SceneManager.LoadScene(0);
        //메인 씬 가기
    }

    public void F_OnClickRetry() //현재 스테이지 재시작
    {

        //현재 스테이지 재시작
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex > 0)
            SceneManager.LoadScene(sceneIndex);
        else
            return;
        //씬 재시작
    }

    public void F_OnClickNext() //다음 스테이지
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

    public void F_GetCurrentTime(int currentTime) //현재 시간을 기본 화면에 적용
    {
        // 초 단위 시간을 받아 분,초로 변환
        int minutes = currentTime / 60;
        int seconds = currentTime % 60;

        // 문자열 형식을 사용하여 "MM:SS" 형식으로 변환
        string timeString = string.Format("{00}:{01:D2}:{02:D2}", minutes / 60, minutes % 60, seconds);

        currentTimeTxt.text = timeString;
    }

    public void F_GetMoveCount(string moveCount) //움직인 횟수 기본 화면에 적용
    {
        moveCountTxt.text = moveCount;
    }

    public void F_GetData() //사망, 클리어 UI에 움직인 횟수, 걸린 시간 표시
    {
        clearMoveCountTxt.text = "Move Count : " + moveCountTxt.text;
        deadMoveCountTxt.text = "Move Count : " + moveCountTxt.text;
        
        clearPlayTimeTxt.text = "PlayTime : " + currentTimeTxt.text;
        deadPlayTimeTxt.text = "PlayTime : " + currentTimeTxt.text;
    }

    public void F_GetClear(int stageIdx)
    {
        GameManager.Instance._isClear[stageIdx] = true;
    }

    #region SettingUI
    //튜토리얼 창 켜기 함수

    //사운드 끄고 켜는 함수

    //음량 조절 함수

    //저장 함수
    #endregion
}
