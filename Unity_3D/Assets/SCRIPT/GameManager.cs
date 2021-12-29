using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SHIH
{
    /// <summary>
    /// �C���޲z��
    /// �����B�z
    /// 1.���ȧ���
    /// 2.���a���`
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region ���
        [Header("�s�ժ���")]
        public CanvasGroup groupFinal;
        [Header("�����e�����D")]
        public Text textTitle;

        private string titleWin = "You Win";
        private string titlelose = "You Failed";
        #endregion


        /// <summary>
        /// �}�l�H�J�̫�e��
        /// </summary>
        /// <param name="win">�O�_���</param>
        #region ��k:���}
        public void StateFadeFinalUI(bool win)
        {
            StartCoroutine(FadeFinalUI(win ? titleWin : titlelose));
        }
        #endregion

        /// <summary>
        /// �H�J�����e��
        /// </summary>
        /// <param name="title">���D</param>
        #region ��k:�p�H
        private IEnumerator FadeFinalUI(string title)
        {
            
            textTitle.text = title;
            groupFinal.interactable = true;
            groupFinal.blocksRaycasts = true;
            for (int i = 0; i < 10; i++)
            {
                groupFinal.alpha += 0.1f;
                yield return new WaitForSeconds(0.02f);
            }
        }
        #endregion


    }

}
