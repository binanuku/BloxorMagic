using UnityEngine;

public class PanelManager : MonoBehaviour
{
    #region SingleTon
    public static PanelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 기존 인스턴스가 없으면 새로운 인스턴스 생성
                _instance = FindObjectOfType<PanelManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PanelManager>();
                    singletonObject.name = typeof(PlayerManager).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }
    #endregion

    private static PanelManager _instance;

    private void Awake()
    {
        // 중복된 인스턴스가 생성되지 않도록 체크
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void F_Interaction(Collider col, Rigidbody rb) {
        if (col.transform.CompareTag("Interaction"))
        {
            rb.isKinematic = false;
            //패널에 플레이어가 부딪히면 떨어짐
        }
    }
}
