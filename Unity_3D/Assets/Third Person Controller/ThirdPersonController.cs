using UnityEngine;   //引用Unity API (倉庫 資料與功能)
using UnityEngine.Video;
namespace Shih
{
    //MonBahaviour:基底類別
    /// <summary>
    /// {摘要}
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region 欄位Field
        [Header("移動速度"), Tooltip("用來調整腳色移動速度"), Range(1, 500)]
        public float speed = 10.5f;
        [Header("跳躍高度"), Tooltip("用來調整腳色跳躍高度"), Range(1, 1000)]
        public int jump = 100;

        [Header("檢查地面資料")]
        [Tooltip("用來檢查腳色是否在地面上")]
        public bool isGrounded;
        public float groundHannkei = 0.2f;
        public Vector3 v3CheckGroundOffset;
        [Range(0, 3)]
        public float checkGroundRadius = 0.2f;

        [Header("音效檔案")]
        public AudioClip jump_sound;
        public AudioClip landing_sound;

        [Header("動畫參數")]
        public string animatorWalk = "走路開關";
        public string animatorRun = "跑步開關";
        public string animatorHurt = "受傷開關";
        public string animatorDeath = "死亡開關";
        public string animatorJump = "跳躍觸發";
        public string animatorIsGrounded = "是否在地上";


        private ThirdPersonCamera thirdPersonCamera;
        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;
        /// <summary>
        /// 移動按鍵輸入
        /// </summary>
        /// <param name="move">要取得軸向名稱</param>
        private void movement(float move)
        {
                rig.velocity =
                transform.forward * Movebutton("Vertical") * move +
                transform.right * Movebutton("Horizontal") * move +
                Vector3.up * rig.velocity.y;
        }

        private float Movebutton(string axisName)
        {
            return Input.GetAxis(axisName);
        }

        /// <summary>
        /// 檢查地版
        /// </summary>
        /// <returns>是否碰到地板</returns>
        private bool groundcheck()
        {
            Collider[] hits = Physics.OverlapSphere
                (transform.position +
                transform.right * v3CheckGroundOffset.x +
                transform.up * v3CheckGroundOffset.y +
                transform.forward * v3CheckGroundOffset.z,
                checkGroundRadius, 1 << 3);
            isGrounded = hits.Length > 0;
            return hits.Length > 0;
            //  if (!isGrounded && hits.Length>0) aud.PlayOneShot(jump_sound, volumeRandom);

        }
        private void Jump()
        {
            print("是否在地面上: " + groundcheck());
            if (groundcheck() && keyJump)
            {
                rig.AddForce(transform.up * jump);
                aud.PlayOneShot(jump_sound, volumeRandom);
            }
        }
        private bool KeyUp { get => Input.GetKey(KeyCode.UpArrow); }
        private bool KeyDown { get => Input.GetKey(KeyCode.DownArrow); }
        private bool KeyRight { get => Input.GetKey(KeyCode.RightArrow); }
        private bool KeyLeft { get => Input.GetKey(KeyCode.LeftArrow); }
        private void UpdateAnimation()

        {
            if (KeyUp | KeyDown | KeyRight | KeyLeft)
            {
                ani.SetBool(animatorWalk, true);
            }
            else
            {
                ani.SetBool(animatorWalk, false);
            }

            ani.SetBool(animatorIsGrounded, isGrounded);
            if (keyJump) ani.SetTrigger(animatorJump);

        }
        [Header("面相速度"), Range(0, 50)]
        public float speedLookAt = 2;
        private void LookAtForward()
        {
            if (Mathf.Abs(Movebutton("Vertical")) > 0.1f)
            {
                Quaternion angle = Quaternion.LookRotation(thirdPersonCamera.posForward - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            }
        }
        
        #endregion

        #region 屬性Property
        public int readAndWrite { get; set; }
        public int read { get; }
        public int readValue
        {
            get
            {
                return 77;
            }
        }
        private int _hp;
        public int hp
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
            }

        }
        #endregion
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
        #region 方法 Method




        private void Test()
        {
            print("我是自訂方法");
        }

        private int ReturnJump()
        {
            return 999;
        }
        private void Skill(int damage, string effect = "灰塵特效", string sound = "ㄚㄚㄚ")
        {
            print("參數版本 - 傷害值:" + damage);
            print("參數版本-技能特效:" + effect);
            print("參數版本 - 音效:" + sound);
        }
        private void Skill100()
        {
            print("傷害值:" + 100);
            print("技能特效");
        }
        private void Skill200()
        {
            print("傷害值:" + 200);
            print("技能特效");
        }
        private void Skill300()
        {
            print("傷害值:" + 300);
            print("技能特效");
        }


        /// <summary>
        /// 計算BMI方法
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private float BMI(float weight, float height, string name = "測試")
        {
            print(name + "的BMI");
            return weight / (height * height);
        }

        #endregion

        #region 事件 Event
        public GameObject playerObject;
        /// <summary>
        /// 攝影機類別
        /// </summary>

        private void Start()
        {
            print(BMI(61, 1.71f, "SEKI"));


            Skill100();
            Skill200();
            Skill(300);
            Skill(999, "爆炸特效");
            Skill(500, sound: "咻咻咻");

            #region 輸出 方法
            /*
            print("哈囉");

            Debug.Log("一般訊息");
            Debug.LogWarning("警告訊息");
            Debug.LogError("錯誤訊息");
            */
            #endregion



            Test();

            int j = ReturnJump();
            print("跳躍值:" + j);
            print("跳躍值，當值使用:" + (ReturnJump() + 1));
            //1.物件欄位名稱，取得原件(類型(元件類型)) 當作元件類型;
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            //2.此腳本遊戲物件，取得元件<泛型>();
            rig = gameObject.GetComponent<Rigidbody>();
            //3.取得元件<泛型>();
            //類別可以使用繼承類別(父類別)的成員、公開或保護 欄位、屬性與方法
            ani = GetComponent<Animator>();

            thirdPersonCamera = FindObjectOfType <ThirdPersonCamera>();
        }
        //更新事件:一秒執行60次.60FPS - Frame Per Second
        //處理持續性的運動，移動物件，監聽玩家輸入按鍵
        private void Update()
        {
            Jump();
            UpdateAnimation();
            LookAtForward();
        }

        private void FixedUpdate()
        {
            movement(speed);
        }
        private void OnDrawGizmos()
        {
            //1.指定顏色
            //2.繪製圖形
            Gizmos.color = new Color(1, 0, 0.3f, 0.3f);


            //transform 與此腳本在同階層的transform元件
            Gizmos.DrawSphere(
                transform.position +
                transform.right * v3CheckGroundOffset.x +
                transform.up * v3CheckGroundOffset.y +
                transform.forward * v3CheckGroundOffset.z, checkGroundRadius);
            #endregion
        }
    }
}






