using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class UIManager : MonoBehaviour
{


    [Header("DefaultUI")]
    [SerializeField] GameObject defaultUI; //��� ǥ�� UI
    [SerializeField] Text currentTimeTxt; //�ǽð� ����ð�
    [SerializeField] Text moveCountTxt; //�ǽð� ������ Ƚ��

    [Header("ClearUI")]
    [SerializeField] GameObject clearUI; //Ŭ���� �� ǥ�� UI
    [SerializeField] Text clearPlayTimeTxt; //ClearUI�� ǥ�õ� �ɸ� �ð�
    [SerializeField] Text clearMoveCountTxt;//ClearUI�� ǥ�õ� ������ ���� Ƚ��

    [Header("DeadUI")]
    [SerializeField] GameObject deadUI; //��� �� ǥ�� UI
    [SerializeField] Text deadPlayTimeTxt; //DeadUI�� ǥ�õ� �ɸ� �ð�
    [SerializeField] Text deadMoveCountTxt;//DeadUI�� ǥ�õ� ������ ���� Ƚ��

    [Header("SettingUI")]
    [SerializeField] GameObject settingUI; //���� ȭ�� UI


    //[Header("Main Object")]

    #region SingleTon
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // �ߺ��� �ν��Ͻ��� �������� �ʵ��� üũ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
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

    public void F_OnClickSettingUI()//SettingUI ���� �ѱ�
    {
        if (settingUI.activeSelf)
            settingUI.SetActive(false);
        else
            settingUI.SetActive(true);
    }

    public void F_OnClickMain() //�������� ���� ȭ��
    {
        //����ȭ�� ����
        SceneManager.LoadScene(0);
        //���� �� ����
    }

    public void F_OnClickRetry() //���� �������� �����
    {

        //���� �������� �����
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex > 0)
            SceneManager.LoadScene(sceneIndex);
        else
            return;
        //�� �����
    }

    public void F_OnClickNext() //���� ��������
    {
        //���� �������� �ε���
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // �� ���� ��������
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex <= sceneCount)
            SceneManager.LoadScene(nextSceneIndex);
        else { }
        //���� ���� ���ٸ� ���ٴ� ���� ����
    }

    public void F_GetCurrentTime(int currentTime) //���� �ð��� �⺻ ȭ�鿡 ����
    {
        // �� ���� �ð��� �޾� ��,�ʷ� ��ȯ
        int minutes = currentTime / 60;
        int seconds = currentTime % 60;

        // ���ڿ� ������ ����Ͽ� "MM:SS" �������� ��ȯ
        string timeString = string.Format("{00}:{01:D2}:{02:D2}", minutes / 60, minutes % 60, seconds);

        currentTimeTxt.text = timeString;
    }

    public void F_GetMoveCount(string moveCount) //������ Ƚ�� �⺻ ȭ�鿡 ����
    {
        moveCountTxt.text = moveCount;
    }

    public void F_GetData() //���, Ŭ���� UI�� ������ Ƚ��, �ɸ� �ð� ǥ��
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
    //Ʃ�丮�� â �ѱ� �Լ�

    //���� ���� �Ѵ� �Լ�

    //���� ���� �Լ�

    //���� �Լ�
    #endregion
}
