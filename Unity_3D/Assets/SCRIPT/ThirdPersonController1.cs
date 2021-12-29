using UnityEngine;   //�ޥ�Unity API (�ܮw ��ƻP�\��)
using UnityEngine.Video;


    //MonBahaviour:�����O
    /// <summary>
    /// {�K�n}
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region ���Field
        //�x�s�C����ơA�Ҧp:���ʳt�סB���D���׵���...
        //�`�Υ|�j����:��ơB���I�ơB�r��B���L��
        //���y�k:�׹��� ������� ���W�� (���w �w�]��) ����
        //�׹���:
        //1.���}public - ���\��L�����s�� - ��ܦb�ݩʭ��O - �ݭn�վ㪺��Ƴ]�w�����}
        //2.�p�Hprivate - �T���L�����s�� - ���æb�ݩʭ��O - �w�]��
        //*Unity �H�ݩʭ��O��Ƭ��D
        //��_�{���w�]�ȽЫ�...Reset
        //����ݩ�Attribute :���U�����
        //����ݩʻy�k:[�ݩʦW��(�ݩʭ�)]
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


        
        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;
        //�P�|ctrl+M O
        //�i�}ctrl+M L

        /// <summary>
        /// ���ʫ����J
        /// </summary>
        /// <param name="move">�n���o�b�V�W��</param>
        private void movement(float move)
        {
            //����.�[�t�� = �T��V�q - �[�t�ץΨӱ������T�Ӷb�V���B�ʳt��
            //�e��*��J��*���ʳt��
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
            //print("�y��I�쪺�Ĥ@�Ӫ��� : " + hits[0].name);

            //�Ǧ^ �I���}�C�ƶq>0 - �u�n�I����w�ϼh����N�N��b�a���W
            isGrounded = hits.Length > 0;
            return hits.Length > 0;
            //  if (!isGrounded && hits.Length>0) aud.PlayOneShot(jump_sound, volumeRandom);

        }
        private void Jump()
        {
            print("�O�_�b�a���W: " + groundcheck());

            //�åB&&
            //�p�G �b�a���W �åB ���U�ť��� �N ���D
            if (groundcheck() && keyJump)
            {
                //����A�K�[���O[������W��*���D]
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
            #region �m��
            //�w�����G:
            //���U�e�Ϋ�� �N���L�ȳ]��true
            //�S������ �N���L�ȳ]��false
            //Input
            //if (��ܱ���)
            //!= ,==����B��l(��ܱ���)
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
        [Header("���۳t��"), Range(0, 50)]
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

        #region �ݩ�Property

        //�ݩʤ��|��ܦb���O�W
        //�x�s��ơA�P���ۦP
        //�t���b��:�i�H�]�w�s������ Get Set
        //�ݩʻy�k:�׹��� ������� �ݩʦW�� {��;�s;}
        public int readAndWrite { get; set; }
        //��Ū�ݩ�:�u����oGet
        public int read { get; }
        //��Ū�ݩ�: �z�Lget�]�w�w�]�ȡA����rreturn���Ǧ^��
        public int readValue
        {
            get
            {
                return 77;
            }
        }
        //�߼g�ݩʬO�T��A�����n��get
        //public int write { set; }
        //value �����O���w����
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
        //C#7.0 �s���F �i�H�ϥ�Lamda =>�B��l 
        //�y�k: get => {�{���ϰ�} - ���i�ٲ��j�A��
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
        #region ��k Method
        //�w�q�P��@�������{�����϶��A�\��
        //��k�y�k:�׹��� �Ǧ^������� ��k�W��(�Ѽ�1,....�Ѽ�N){�{���϶�}
        //�`�ζǦ^����: �L�Ǧ^void - ����k�S���Ǧ^���
        //�榡��:ctrl+K�BD
        //�ۭq��k:
        //�ۭq��k�ݭn�Q�I�s�~�|�����k�����{��
        //�W���C�⬰�H���� - �S���Q�I�s
        //�W���C�⬰�G���� - ���Q�I�s




        private void Test()
        {
            print("�ڬO�ۭq��k");
        }

        private int ReturnJump()
        {
            return 999;
        }
        //��񦡰Ѽƥu���b()�k��
        //�Ѽƻy�k:������� �ѼƦW��
        //���w�]�Ȫ��Ѽƥi�H����J�޼ơA��񦡰Ѽ�
        private void Skill(int damage, string effect = "�ǹЯS��", string sound = "������")
        {
            print("�Ѽƪ��� - �ˮ`��:" + damage);
            print("�Ѽƪ���-�ޯ�S��:" + effect);
            print("�Ѽƪ��� - ����:" + sound);
        }

        //��Ӳ�:���ϥΰѼ�
        //���C���@�X�R��
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

        //*�D���n���ܭ��n
        //BMI = �魫/����*����(����)
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
        //�S�w�ɶ��I�|���檺��k�A�{�����J�fstart����Console Main
        //�}�l�ƥ�:�C���}�l�ɰ���@���A�B�z��l�ơA���o��Ƶ���
        public GameObject playerObject;
        /// <summary>
        /// ��v�����O
        /// </summary>

        private void Start()
        {
            print(BMI(61, 1.71f, "SEKI"));


            Skill100();
            Skill200();
            //�I�s���ѼƤ�k�ɡA������J�������޼�
            Skill(300);
            Skill(999, "�z���S��");
            //�ݨD:�ˮ`��500�A�S�ĥιw�]�ȡA���Ĵ���������
            //���h�ӿ�񦡰Ѽƥi�ϥΫ��W�Ѽƻy�k;�ѼƦW��:��
            Skill(500, sound: "������");

            #region ��X ��k
            /*
            print("���o");

            Debug.Log("�@��T��");
            Debug.LogWarning("ĵ�i�T��");
            Debug.LogError("���~�T��");
            */
            #endregion

            #region �ݩʽm��
            /** �ݩʽm��
            //���P�ݩ� ���oGt�A�]�wSet
            print("����� - ���ʳt��:" + speed);
            print("�ݩʸ�� - Ū�g�ݩ�:" + readAndWrite);
            speed = 20.5f;
            readAndWrite = 90;
            print("�ק�᪺���");
            print("����� - ���ʳt��:" + speed);
            print("�ݩʸ�� - Ū�g�ݩ�:" + readAndWrite);
            //��Ū�ݩ�
            //read = 7  //��Ū�ݩʤ���]�wset
            print("��Ū�ݩ�:" + read);
            print("��Ū�ݩʡA���w�]��:" + readValue);

            //�ݩʦs���m��
            print("HP :" + hp);
            hp = 100;
            print("HP :" + hp);
            /**/
            #endregion
            //�I�s�ۭq��k�y�k:��k�W��();
            Test();
            //�I�s���Ǧ^�Ȫ���k
            //1.�ϰ��ܼƫ��w�Ǧ^�� - �ϰ��ܼƶȯ�b�����c(�j�A��)���s��
            int j = ReturnJump();
            print("���D��:" + j);
            //2.�N�Ǧ^��k���Ȩϥ�
            print("���D�ȡA��Ȩϥ�:" + (ReturnJump() + 1));

            //�n���o�}�����C������i�H�ϥ�����r gameObject
            //���o���󪺤�k
            //1.�������W�١A���o���(����(��������)) ��@��������;
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            //2.���}���C������A���o����<�x��>();
            rig = gameObject.GetComponent<Rigidbody>();
            //3.���o����<�x��>();
            //���O�i�H�ϥ��~�����O(�����O)�������B���}�ΫO�@ ���B�ݩʻP��k
            ani = GetComponent<Animator>();

            
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
        //ø�s�ϥܨƥ�
        //�bUnity Editor��ø�s�ϥܻ��U�}�o�A�o����|�۰�����
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







