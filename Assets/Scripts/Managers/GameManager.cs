using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] stageBtn; //�������� ����â�� �� �������� ��ư ����Ʈ

    #region SingleTon
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ������Ʈ�� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


}
