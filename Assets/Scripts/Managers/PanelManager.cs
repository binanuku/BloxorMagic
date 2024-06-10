using UnityEngine;

public class PanelManager : MonoBehaviour
{
    #region SingleTon
    public static PanelManager Instance { get; private set; }

    private void Awake()
    {
        // 중복된 인스턴스가 생성되지 않도록 체크
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void F_Interaction(Collider col, Rigidbody rb) {
        if (col.transform.CompareTag("Interaction"))
        {
            rb.isKinematic = false;
            //패널에 플레이어가 부딪히면 떨어짐
        }
    }
    public void F_GlassActive(Collider col, Rigidbody rb)
    {
        F_Interaction(col, rb);
        StartCoroutine(UIManager.Instance.F_OnDeadUI());
    }

    public void F_CreatePanel(Collision col, Rigidbody rb)
    {
        if (col.transform.CompareTag("Interaction"))
        {

        }
    }
}
