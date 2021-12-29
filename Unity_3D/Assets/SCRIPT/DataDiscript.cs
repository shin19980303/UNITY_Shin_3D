using UnityEngine;

namespace SHIH.Dialogue
{
    /// <summary>
    /// ��ܨt�Ϊ����
    /// NPC�n��ܪ��T�Ӷ��q���e
    /// �����ȫe�B���i�椤�B��������
    /// </summary>
    //ScriptableObject�~�Ӧ����O�|�ܦ��}������A�i�N���}����Ʒ�����O�s�b�M��project��
    //CreateAssetMenu ���O�ݩ�: �������O�K�Q�M�פ����
    //menuName ���W�١A�i��/���h
    //fileName �ɮצW��
    [CreateAssetMenu(menuName ="SHIH/��ܸ��",fileName ="NPC/��ܸ��")]
    public class DataDiscript : ScriptableObject
    {

        [Header("��ܪ̦W��")]
        public string nameDialogue;
        [Header("���ȫe��ܤ��e"),TextArea(3,7)]
        //�}�C:�O�s�ۦP������������c
        public string[] beforeMission;
        [Header("���ȶi�椤��ܤ��e"), TextArea(3, 7)]
        public string[] missioning;
        [Header("���ȧ������ܤ��e"), TextArea(3, 7)]
        public string[] aftermission;
        [Header("���ȻݨD�ƶq"), Range(0, 100)]
        public int countNeed;
        //�ϥΦC�|:
        //�y�k:�׹��� �C�|�W�� �۩w�q���W��;
        [Header("NPC ���Ȫ��A")]
        public StateNPCMission stateNPCMission = StateNPCMission.BeforeMission;

    }
}

