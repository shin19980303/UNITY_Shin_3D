using UnityEngine;
using UnityEngine.Events;

namespace SHIH
{
    /// <summary>
    /// 受傷系統
    /// 處理血量、受傷與死亡
    /// </summary>
    public class HurtSystem : MonoBehaviour
    {
        #region 欄位:公開
        [Header("血量"), Range(0, 5000)]
        public float hp = 100;
        [Header("受傷事件")]
        public UnityEvent onHyrt;
        [Header("死亡事件")]
        public UnityEvent onDead;
        [Header("動畫參數:受傷與死亡")]
        public string parameterHurt = "受傷觸發";
        public string parameterDead = "死亡觸發";

        #endregion

        #region 欄位:私人
        private Animator ani; //元件

        //private   私人 不允許在子類別存取
        //public    公開 允許所有類別存取
        //protected 保護 僅限子類別存取
        protected float hpMax;
 


        #endregion

        #region 事件
        private void Awake()
        {
            ani = GetComponent<Animator>(); //呼叫動畫
            hpMax = hp;
        }
        #endregion

        #region 方法:公開
        
        
        /// <summary>
        /// 受傷
        /// </summary>
        /// <param name="damage">接收到的傷害</param>
        // 成員要被子類別複寫必須加上Virtrual 虛擬
        public virtual bool Hurt (float damage)
        {
            if (ani.GetBool(parameterDead))return true; //如果玩家 死亡參數已勾選就跳出
            
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
        /// 死亡
        /// </summary>
        #region 方法:私人
        public void Dead()
        {
            ani.SetBool(parameterDead, true);
            onDead.Invoke();
        }

        #endregion




    }
}

