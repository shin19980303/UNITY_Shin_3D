using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SHIH
{
    /// <summary>
    /// 遊戲管理器
    /// 結束處理
    /// 1.任務完成
    /// 2.玩家死亡
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region 欄位
        [Header("群組物件")]
        public CanvasGroup groupFinal;
        [Header("結束畫面標題")]
        public Text textTitle;

        private string titleWin = "You Win";
        private string titlelose = "You Failed";
        #endregion


        /// <summary>
        /// 開始淡入最後畫面
        /// </summary>
        /// <param name="win">是否獲勝</param>
        #region 方法:公開
        public void StateFadeFinalUI(bool win)
        {
            StartCoroutine(FadeFinalUI(win ? titleWin : titlelose));
        }
        #endregion

        /// <summary>
        /// 淡入結束畫面
        /// </summary>
        /// <param name="title">標題</param>
        #region 方法:私人
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
