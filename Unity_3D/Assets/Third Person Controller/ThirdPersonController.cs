using UnityEngine;   //�ޥ�Unity API (�ܮw ��ƻP�\��)
using UnityEngine.Video;
namespace Shih
{
    //MonBahaviour:�����O
    /// <summary>
    /// {�K�n}
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region ���Field
        [Header("���ʳt��"), Tooltip("�Ψӽվ�}�Ⲿ�ʳt��"), Range(1, 500)]
        public float speed = 10.5f;
        [Header("���D����"), Tooltip("�Ψӽվ�}����D����"), Range(1, 1000)]
        public int jump = 100;

        [Header("�ˬd�a�����")]
        [Tooltip("�Ψ��ˬd�}��O�_�b�a���W")]
        public bool isGrounded;
        public float groundHannkei = 0.2f;
        public Vector3 v3CheckGroundOffset;
        [Range(0, 3)]
        public float checkGroundRadius = 0.2f;

        [Header("�����ɮ�")]
        public AudioClip jump_sound;
        public AudioClip landing_sound;

        [Header("�ʵe�Ѽ�")]
        public string animatorWalk = "�����}��";
        public string animatorRun = "�]�B�}��";
        public string animatorHurt = "���˶}��";
        public string animatorDeath = "���`�}��";
        public string animatorJump = "���DĲ�o";
        public string animatorIsGrounded = "�O�_�b�a�W";


        private ThirdPersonCamera thirdPersonCamera;
        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;
        /// <summary>
        /// ���ʫ����J
        /// </summary>
        /// <param name="move">�n���o�b�V�W��</param>
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
        /// �ˬd�a��
        /// </summary>
        /// <returns>�O�_�I��a�O</returns>
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
            print("�O�_�b�a���W: " + groundcheck());
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
        [Header("���۳t��"), Range(0, 50)]
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

        #region �ݩ�Property
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
        #region ��k Method




        private void Test()
        {
            print("�ڬO�ۭq��k");
        }

        private int ReturnJump()
        {
            return 999;
        }
        private void Skill(int damage, string effect = "�ǹЯS��", string sound = "������")
        {
            print("�Ѽƪ��� - �ˮ`��:" + damage);
            print("�Ѽƪ���-�ޯ�S��:" + effect);
            print("�Ѽƪ��� - ����:" + sound);
        }
        private void Skill100()
        {
            print("�ˮ`��:" + 100);
            print("�ޯ�S��");
        }
        private void Skill200()
        {
            print("�ˮ`��:" + 200);
            print("�ޯ�S��");
        }
        private void Skill300()
        {
            print("�ˮ`��:" + 300);
            print("�ޯ�S��");
        }


        /// <summary>
        /// �p��BMI��k
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private float BMI(float weight, float height, string name = "����")
        {
            print(name + "��BMI");
            return weight / (height * height);
        }

        #endregion

        #region �ƥ� Event
        public GameObject playerObject;
        /// <summary>
        /// ��v�����O
        /// </summary>

        private void Start()
        {
            print(BMI(61, 1.71f, "SEKI"));


            Skill100();
            Skill200();
            Skill(300);
            Skill(999, "�z���S��");
            Skill(500, sound: "������");

            #region ��X ��k
            /*
            print("���o");

            Debug.Log("�@��T��");
            Debug.LogWarning("ĵ�i�T��");
            Debug.LogError("���~�T��");
            */
            #endregion



            Test();

            int j = ReturnJump();
            print("���D��:" + j);
            print("���D�ȡA��Ȩϥ�:" + (ReturnJump() + 1));
            //1.�������W�١A���o���(����(��������)) ��@��������;
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            //2.���}���C������A���o����<�x��>();
            rig = gameObject.GetComponent<Rigidbody>();
            //3.���o����<�x��>();
            //���O�i�H�ϥ��~�����O(�����O)�������B���}�ΫO�@ ���B�ݩʻP��k
            ani = GetComponent<Animator>();

            thirdPersonCamera = FindObjectOfType <ThirdPersonCamera>();
        }
        //��s�ƥ�:�@�����60��.60FPS - Frame Per Second
        //�B�z����ʪ��B�ʡA���ʪ���A��ť���a��J����
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
            //1.���w�C��
            //2.ø�s�ϧ�
            Gizmos.color = new Color(1, 0, 0.3f, 0.3f);


            //transform �P���}���b�P���h��transform����
            Gizmos.DrawSphere(
                transform.position +
                transform.right * v3CheckGroundOffset.x +
                transform.up * v3CheckGroundOffset.y +
                transform.forward * v3CheckGroundOffset.z, checkGroundRadius);
            #endregion
        }
    }
}






