using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPaner : MonoBehaviour
{
    Rigidbody clearRb;

    private void Start()
    {
        clearRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PanelManager.Instance.F_Interaction(other, clearRb);
        UIManager.Instance.F_OnClearUI();
        UIManager.Instance.F_GetData();
        //UIManager.Instance.F_GetStar(SceneManager.GetActiveScene().buildIndex);
        //�� ������������ ������ �� �ε���
    }
}
