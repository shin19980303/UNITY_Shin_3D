using UnityEngine;

namespace SHIH.Dialogue
{
    /// <summary>
    /// 對話系統的資料
    /// NPC要對話的三個階段內容
    /// 接任務前、任進行中、完成任務
    /// </summary>
    //ScriptableObject繼承此類別會變成腳本物件，可將此腳本資料當成物件保存在專案project內
    //CreateAssetMenu 類別屬性: 為此類別便利專案內選單
    //menuName 選單名稱，可用/分層
    //fileName 檔案名稱
    [CreateAssetMenu(menuName ="SHIH/對話資料",fileName ="NPC/對話資料")]
    public class DataDiscript : ScriptableObject
    {

        [Header("對話者名稱")]
        public string nameDialogue;
        [Header("任務前對話內容"),TextArea(3,7)]
        //陣列:保存相同資料類型的結構
        public string[] beforeMission;
        [Header("任務進行中對話內容"), TextArea(3, 7)]
        public string[] missioning;
        [Header("任務完成後對話內容"), TextArea(3, 7)]
        public string[] aftermission;
        [Header("任務需求數量"), Range(0, 100)]
        public int countNeed;
        //使用列舉:
        //語法:修飾詞 列舉名稱 自定義欄位名稱;
        [Header("NPC 任務狀態")]
        public StateNPCMission stateNPCMission = StateNPCMission.BeforeMission;

    }
}

