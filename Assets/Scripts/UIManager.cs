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
        //�޴� â���� ����
    }
    public void F_OnClickRetry()
    {
        //���� �������� �����
    }
    public void F_OnClickNext()
    {
        //���� �������� ����
    }
}
