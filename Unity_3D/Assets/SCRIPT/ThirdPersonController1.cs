using UnityEngine;   //引用Unity API (倉庫 資料與功能)
using UnityEngine.Video;


    //MonBahaviour:基底類別
    /// <summary>
    /// {摘要}
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region 欄位Field
        //儲存遊戲資料，例如:移動速度、跳躍高度等等...
        //常用四大類型:整數、福點數、字串、布林值
        //欄位語法:修飾詞 資料類型 欄位名稱 (指定 預設值) 結尾
        //修飾詞:
        //1.公開public - 允許其他類型存取 - 顯示在屬性面板 - 需要調整的資料設定為公開
        //2.私人private - 禁止其他類型存取 - 隱藏在屬性面板 - 預設值
        //*Unity 以屬性面板資料為主
        //恢復程式預設值請按...Reset
        //欄位屬性Attribute :輔助欄位資料
        //欄位屬性語法:[屬性名稱(屬性值)]
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


        
        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;
        //摺疊ctrl+M O
        //展開ctrl+M L

        /// <summary>
        /// 移動按鍵輸入
        /// </summary>
        /// <param name="move">要取得軸向名稱</param>
        private void movement(float move)
        {
            //鋼體.加速度 = 三圍向量 - 加速度用來控制缸體三個軸向的運動速度
            //前方*輸入值*移動速度
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
            //print("球體碰到的第一個物件 : " + hits[0].name);

            //傳回 碰撞陣列數量>0 - 只要碰到指定圖層物件就代表在地面上
            isGrounded = hits.Length > 0;
            return hits.Length > 0;
            //  if (!isGrounded && hits.Length>0) aud.PlayOneShot(jump_sound, volumeRandom);

        }
        private void Jump()
        {
            print("是否在地面上: " + groundcheck());

            //並且&&
            //如果 在地面上 並且 按下空白鍵 就 跳躍
            if (groundcheck() && keyJump)
            {
                //鋼體，添加推力[此物件上方*跳躍]
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
            #region 練習
            //預測成果:
            //按下前或後時 將布林值設為true
            //沒有按時 將布林值設為false
            //Input
            //if (選擇條件)
            //!= ,==比較運算子(選擇條件)
            // ani.SetBool(animatorWalk, movement("Vertical") != 0 | movement("Horizontal") != 0);

            //ani.SetBool(animatorWalk, true);
            #endregion

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
               // Quaternion angle = Quaternion.LookRotation(thirdPersonCamera.posForward - transform.position);
               // transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            }
        }

        #endregion

        #region 屬性Property

        //屬性不會顯示在面板上
        //儲存資料，與欄位相同
        //差異在於:可以設定存取全縣 Get Set
        //屬性語法:修飾詞 資料類型 屬性名稱 {取;存;}
        public int readAndWrite { get; set; }
        //唯讀屬性:只能取得Get
        public int read { get; }
        //唯讀屬性: 透過get設定預設值，關鍵字return為傳回值
        public int readValue
        {
            get
            {
                return 77;
            }
        }
        //唯寫屬性是禁止的，必須要有get
        //public int write { set; }
        //value 指的是指定的值
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
        //C#7.0 存取了 可以使用Lamda =>運算子 
        //語法: get => {程式區域} - 單行可省略大括號
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
        #region 方法 Method
        //定義與實作較複雜程式的區塊，功能
        //方法語法:修飾詞 傳回資料類型 方法名稱(參數1,....參數N){程式區塊}
        //常用傳回類型: 無傳回void - 此方法沒有傳回資料
        //格式化:ctrl+K、D
        //自訂方法:
        //自訂方法需要被呼叫才會執行方法內的程式
        //名稱顏色為淡黃色 - 沒有被呼叫
        //名稱顏色為亮黃色 - 有被呼叫




        private void Test()
        {
            print("我是自訂方法");
        }

        private int ReturnJump()
        {
            return 999;
        }
        //選填式參數只能放在()右邊
        //參數語法:資料類型 參數名稱
        //有預設值的參數可以不輸入引數，選填式參數
        private void Skill(int damage, string effect = "灰塵特效", string sound = "ㄚㄚㄚ")
        {
            print("參數版本 - 傷害值:" + damage);
            print("參數版本-技能特效:" + effect);
            print("參數版本 - 音效:" + sound);
        }

        //對照組:不使用參數
        //降低維護擴充性
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

        //*非必要但很重要
        //BMI = 體重/身高*身高(公尺)
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
        //特定時間點會執行的方法，程式的入口start等於Console Main
        //開始事件:遊戲開始時執行一次，處理初始化，取得資料等等
        public GameObject playerObject;
        /// <summary>
        /// 攝影機類別
        /// </summary>

        private void Start()
        {
            print(BMI(61, 1.71f, "SEKI"));


            Skill100();
            Skill200();
            //呼叫有參數方法時，必須輸入對應的引數
            Skill(300);
            Skill(999, "爆炸特效");
            //需求:傷害值500，特效用預設值，音效換成咻咻咻
            //有多個選填式參數可使用指名參數語法;參數名稱:值
            Skill(500, sound: "咻咻咻");

            #region 輸出 方法
            /*
            print("哈囉");

            Debug.Log("一般訊息");
            Debug.LogWarning("警告訊息");
            Debug.LogError("錯誤訊息");
            */
            #endregion

            #region 屬性練習
            /** 屬性練習
            //欄位與屬性 取得Gt，設定Set
            print("欄位資料 - 移動速度:" + speed);
            print("屬性資料 - 讀寫屬性:" + readAndWrite);
            speed = 20.5f;
            readAndWrite = 90;
            print("修改後的資料");
            print("欄位資料 - 移動速度:" + speed);
            print("屬性資料 - 讀寫屬性:" + readAndWrite);
            //唯讀屬性
            //read = 7  //唯讀屬性不能設定set
            print("唯讀屬性:" + read);
            print("唯讀屬性，有預設值:" + readValue);

            //屬性存取練習
            print("HP :" + hp);
            hp = 100;
            print("HP :" + hp);
            /**/
            #endregion
            //呼叫自訂方法語法:方法名稱();
            Test();
            //呼叫有傳回值的方法
            //1.區域變數指定傳回值 - 區域變數僅能在此結構(大括號)內存取
            int j = ReturnJump();
            print("跳躍值:" + j);
            //2.將傳回方法當成值使用
            print("跳躍值，當值使用:" + (ReturnJump() + 1));

            //要取得腳本的遊戲物件可以使用關鍵字 gameObject
            //取得元件的方法
            //1.物件欄位名稱，取得原件(類型(元件類型)) 當作元件類型;
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            //2.此腳本遊戲物件，取得元件<泛型>();
            rig = gameObject.GetComponent<Rigidbody>();
            //3.取得元件<泛型>();
            //類別可以使用繼承類別(父類別)的成員、公開或保護 欄位、屬性與方法
            ani = GetComponent<Animator>();

            
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
        //繪製圖示事件
        //在Unity Editor內繪製圖示輔助開發，發布後會自動隱藏
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







