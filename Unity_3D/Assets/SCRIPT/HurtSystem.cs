using UnityEngine;
using UnityEngine.Events;

namespace SHIH
{
    /// <summary>
    /// ���˨t��
    /// �B�z��q�B���˻P���`
    /// </summary>
    public class HurtSystem : MonoBehaviour
    {
        #region ���:���}
        [Header("��q"), Range(0, 5000)]
        public float hp = 100;
        [Header("���˨ƥ�")]
        public UnityEvent onHyrt;
        [Header("���`�ƥ�")]
        public UnityEvent onDead;
        [Header("�ʵe�Ѽ�:���˻P���`")]
        public string parameterHurt = "����Ĳ�o";
        public string parameterDead = "���`Ĳ�o";

        #endregion

        #region ���:�p�H
        private Animator ani; //����

        //private   �p�H �����\�b�l���O�s��
        //public    ���} ���\�Ҧ����O�s��
        //protected �O�@ �ȭ��l���O�s��
        protected float hpMax;
 


        #endregion

        #region �ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>(); //�I�s�ʵe
            hpMax = hp;
        }
        #endregion

        #region ��k:���}
        
        
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="damage">�����쪺�ˮ`</param>
        // �����n�Q�l���O�Ƽg�����[�WVirtrual ����
        public virtual bool Hurt (float damage)
        {
            if (ani.GetBool(parameterDead))return true; //�p�G���a ���`�ѼƤw�Ŀ�N���X
            
            hp -= damage;
            ani.SetTrigger(parameterHurt);
            onHyrt.Invoke();
            if (hp <= 0)
            {
                Dead();
                return true;
            }
            else return false;

          
        }
        #endregion

        /// <summary>
        /// ���`
        /// </summary>
        #region ��k:�p�H
        public void Dead()
        {
            ani.SetBool(parameterDead, true);
            onDead.Invoke();
        }

        #endregion




    }
}

