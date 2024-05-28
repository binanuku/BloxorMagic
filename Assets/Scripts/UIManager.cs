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
                // ���� �ν��Ͻ��� ������ ���ο� �ν��Ͻ� ����
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
        // �ߺ��� �ν��Ͻ��� �������� �ʵ��� üũ
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
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
        //����ȭ�� ����
        SceneManager.LoadScene(0);
        //���� �� ����
    }
    public void F_OnClickRetry()
    {
        //���� �������� �����
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex > 0)
            SceneManager.LoadScene(sceneIndex);
        else
            return;
        //�� �����
    }
    public void F_OnClickNext()
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
}
