using UnityEngine;

public class PlayerManager : SingleTon<PlayerManager>
{
    public Transform _contactWall; //�÷��̾ ��� ���̰� �浹�� ��
    public Transform _mainCamera; //ī�޶�
    public GameObject InteractionUp; //Panel�� ��ȣ�ۿ��� �ݶ��̴� ������Ʈ
    public GameObject InteractionDown; //Panel�� ��ȣ�ۿ��� �ݶ��̴� ������Ʈ
}
