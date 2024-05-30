using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] stageBtn; //스테이지 선택창의 각 스테이지 버튼 리스트

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


}
