using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingleTon<GameManager>
{
    public Transform[] stageList;
    public bool[] _isClear;

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
