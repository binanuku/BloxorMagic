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
        PanelManager.Instance.F_Interaction(other, clearRb); //������ Ŭ���� �г� ����Ʈ����
        UIManager.Instance.F_OnClearUI(); //ClearUI �ѱ�
        UIManager.Instance.F_GetData(); //���, Ŭ���� UI�� ������ Ƚ��, �ɸ� �ð� ǥ��
        //�� ������������ ������ �� �ε���
    }
}
