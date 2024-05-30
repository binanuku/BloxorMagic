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
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 오브젝트가 파괴되지 않도록 설정
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

    public void F_GetStage(GameObject clickedBtn) //스테이지 선택 버튼
    {
        //클릭한 스테이지 이름을 int형으로 변환
        int stage = Convert.ToInt32(clickedBtn.name);
        SceneManager.LoadScene(stage); //해당 스테이지 시작
    }
}
