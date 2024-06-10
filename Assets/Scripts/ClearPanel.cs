using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPanel : MonoBehaviour
{
    Rigidbody clearRb;

    private void Start()
    {
        clearRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PanelManager.Instance.F_Interaction(other, clearRb); //닿으면 클리어 패널 떨어트리기
        UIManager.Instance.F_OnClearUI(); //ClearUI 켜기
        UIManager.Instance.F_GetData(); //사망, 클리어 UI에 움직인 횟수, 걸린 시간 표시
        //몇 스테이지인지 정보는 씬 인덱스
    }
}
