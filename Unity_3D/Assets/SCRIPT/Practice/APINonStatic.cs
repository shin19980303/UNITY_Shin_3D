using UnityEngine;

/// <summary>
/// �{��API:�D�R�A Non Static
/// </summary>
public class APINonStatic : MonoBehaviour
{
    public Transform tra1;//�׹��� �n�s���D�R�A�����O ���W��
    public Camera cam;
    public Light lig;
    
    private void Start()
    {
        #region �D�R�A�ݩ� 
        // �P�R�A�t��
        //1.�ݭn���骫��
        //2.���o���骫��A�w�q���ñN�n�s��������s�J���
        //3.�C������B���� �����s�b������
        //�y�k:���W�١A�D�R�A�ݩ�
        print("��v�����y��: " + tra1.position);
        print("��v�����`��: " + cam.depth);

        tra1.position = new Vector3(99, 99, 99);
        cam.depth = 7;
        #endregion

        #region �D�R�A��k
        lig.Reset();
        #endregion
    }


}
