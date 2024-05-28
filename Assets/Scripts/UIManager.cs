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

    [SerializeField] GameObject clearUI;
    [SerializeField] GameObject deadUI;
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

    public void F_OnDeadUI() 
    {
        deadUI.SetActive(true);
    }

    public void F_OnClickMain()
    {
        //����ȭ�� ����
        //���� �� ����
    }
    public void F_OnClickRetry()
    {
        //���� �������� �����
        //�� �����
    }
    public void F_OnClickNext()
    {
        //���� �������� ����
        //�� �迭 ����� ���� �� ����
    }
}
