using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] GameObject _rollPoint; ȸ���� �� ������ ǥ��
    [SerializeField] Transform _contactWall; //�÷��̾ ��� ���̰� �浹�� ��
    [SerializeField] Transform _mainCamera; //ī�޶�
    Rigidbody _playerRb;

    private float _rollSpeed = 3; //ȸ�� �ӵ�
    private bool _isMooving; //ȸ���ϴµ��� �߰� ȸ���� �ȵǰ� ��
    private bool _isOut; //����ϸ� ������ �ȵǰ� ��

    Vector3 _center;
    Vector3 _axis;

    RaycastHit _hit;
    [SerializeField] LayerMask _contactWallLayer;

    private void Start()
    {
        _playerRb = transform.GetComponent<Rigidbody>();
        F_Initialize();
    }

    private void Update()
    {
        F_Move();
        Debug.DrawRay(transform.position, transform.up * 2f, Color.red);
        _contactWall.position = transform.position; //���̿� �´� �ݶ��̴��� �÷��̾�� ���� ����������
        _mainCamera.position = transform.position + new Vector3(0, 4, -5.5f);//�÷��̾ ���� �����̴� ī�޶�
    }

    public void F_Initialize()
    {
        _isMooving = false;
        _isOut = false;
        transform.position = new Vector3(0, 2.5f, 0); //���������� �ٲ�鼭 �÷��̾� ��ġ �ʱ�ȭ
        _contactWall.position = transform.position;
    }

    void F_Move() //W A S D Ű�� �����ϴ� �Լ�
    {
        if (_isMooving || _isOut) return;

        if      (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) F_GetRollPoint(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) F_GetRollPoint(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) F_GetRollPoint(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) F_GetRollPoint(Vector3.back);
    }


    void F_GetRollPoint(Vector3 dir) //����Ű�� ������ �÷��̾� ��ġ�� ���� ȸ�� �������� �ٲ�
    {
        if (Physics.Raycast(transform.position, transform.up, out _hit, 5f, _contactWallLayer)) //Player ��ܿ� ray ���
        {
            switch (_hit.collider.name) //ray�� ���� ��ü �̸�
            {
                case "Y":
                    _center = transform.position + Vector3.down + dir * 0.5f;
                    break;
                case "Z":
                    if (dir == Vector3.left || dir == Vector3.right)
                        _center = transform.position + Vector3.down * 0.5f + dir * 0.5f;
                    else
                        _center = transform.position + dir + (Vector3.down * 0.5f);
                    break;
                case "X":
                    if(dir == Vector3.left || dir == Vector3.right)
                        _center = transform.position + dir + (Vector3.down * 0.5f);
                    else
                        _center = transform.position + Vector3.down * 0.5f + dir * 0.5f;
                    break;
            }
        }

        //_rollPoint.transform.position = _center;
        _axis = Vector3.Cross(Vector3.up, dir); //�� ������ ���� ���� ��ȯ
        StartCoroutine(F_Rolling(_center, _axis)); //�߽����� ���� ���͸� �־� ȸ��
    }

    IEnumerator F_Rolling(Vector3 v_center, Vector3 v_axis)
    {
        _playerRb.constraints = RigidbodyConstraints.FreezeAll; //ȸ���߿� �󸮱�
        _isMooving = true;

        for (int i = 0; i < (90 / _rollSpeed); i++)
        {
            transform.RotateAround(v_center, v_axis, _rollSpeed); //ȸ�� ������:center ȸ�� ����:axis
            yield return new WaitForSeconds(0.01f);
        }

        _playerRb.constraints &= ~RigidbodyConstraints.FreezePositionY; //YPosition�� Ǯ��
        _isMooving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("DeadPanel"))
        {
            _isOut = true; // ���� �ȵǰ�
        }
    }
}
