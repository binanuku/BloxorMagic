using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform[] stageList;
    public bool[] _isClear;

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
    private void Start()
    {
        _isClear = new bool[stageList.Length];

        for (int i = 0; i < _isClear.Length; i++)
        {
            _isClear[i] = false;
        }
    }

    public void F_GetStage(GameObject clickedBtn) //�������� ���� ��ư
    {
        //Ŭ���� �������� �̸��� int������ ��ȯ
        int stage = Convert.ToInt32(clickedBtn.name);
        SceneManager.LoadScene(stage); //�ش� �������� ����
    }
}
