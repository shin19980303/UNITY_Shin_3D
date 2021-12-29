using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using SHIH.Dialogue;


namespace SHIH.Enemy
{
    /// <summary>
    /// �ĤH�欰
    /// �ĤH���A: ���ݡB�����B�l�ܡB�����B���ˡB���`
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region ���: ���}
        [Header("���ʳt��"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("�����O"), Range(0, 200)]
        public float attack = 35;
        [Header("�d�� : �l�ܻP����")]
        public float rangeAttack = 5;
        public float rangeTrack = 15;
        [Header("�����H�����")]
        public Vector2 v2RandomWalk = new Vector2(3, 7);
        [Header("�����H�����")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        #endregion

        #region ���: �p�H
        [SerializeField]    // �ǦC����� : ��ܨp�H���
        private StateEnemy state;
        #endregion

        [Header("�����ϰ�첾�P�ؤo")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;
        #region ø�s�ϧ�
        private void OnDrawGizmos()
        {
            #region �����d��B�l�ܽd��P�H���樫�y��
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);

            if (state == StateEnemy.Walk && isWalk)
            {
                Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
                Gizmos.DrawSphere(v3RandomWalkFinal, 0.3f);
            }
            #endregion

            #region �����I���P�w�ϰ�
            Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);
            //ø�s��ΡA�ݭn��۸}�����ɨϥ�matrix ���w�y�Ш��פؤo
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                transform.rotation, transform.localScale);

            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
            #endregion

        }
        #endregion

        #region �ƥ�
        //�Q�쪺NPC�W��
        [Header("NPC �W��")]
        public string nameNPC = "NPC�p��";

        //�쪺���
        private NPC npc;
        private HurtSystem hurtSystem;

        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = speed;
            hurtSystem = GetComponent<HurtSystem>();


            traPlayer = GameObject.Find(namePlayer).transform;
            npc = GameObject.Find(nameNPC).GetComponent<NPC>();

            //���˨t��-���`�ƥ�Ĳ�o�� ��NPC ��s�ƶq
            //AddListener(��k) �K�[��ť��(��k)
            hurtSystem.onDead.AddListener(npc.UpdateMissionCount);

            nma.SetDestination(transform.position);
        }

        private void Update()
        {
            StateManger();
        }
        #endregion

        #region ��k: �p�H

        private Animator ani;
        private NavMeshAgent nma;
        private string parameterIdleWalk = "�����}��";
        /// <summary>
        /// �H���樫�y��
        /// </summary>
        private Vector3 v3RandomWalk
        {
            get => Random.insideUnitSphere * rangeTrack + transform.position;
        }

        private Transform traPlayer;
        private string namePlayer = "kaya";

        private void StateManger()
        {
            switch (state)
            {
                case StateEnemy.Idle:
                    Idle();
                    break;
                case StateEnemy.Walk:
                    Walk();
                    break;
                case StateEnemy.Track:
                    Track();
                    break;
                case StateEnemy.Attack:
                    Attack();
                    break;
                case StateEnemy.Hurt:
                    break;
                case StateEnemy.Dead:
                    break;
                default:
                    break;
            }
        }

        private bool isIdle;


        /// <summary>
        /// ����: �H����ƫ�i�J�������A
        /// </summary>
        private void Idle()
        {
            if (!targetIsDead && playerInTrackRange) state = StateEnemy.Track;  //�p�G ���a�i�J �l�ܽd�� �N�����l�ܪ��A
            #region �i�J����
            if (isIdle) return;

            isIdle = true;
            #endregion
            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(IdleEffect());
        }


        /// <summary>
        /// ���ݮĪG
        /// </summary>
        /// <returns></returns>
        private IEnumerator IdleEffect()
        {
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);

            state = StateEnemy.Walk;
            #region �X�h����
            isIdle = false;
            #endregion
        }

        /// <summary>
        /// �H���樫�y��:�z�LAPI���o���椺�i���쪺��m
        /// </summary>
        private Vector3 v3RandomWalkFinal;


        /// <summary>
        /// �O�_�������A
        /// </summary>
        private bool isWalk;

        /// <summary>
        /// ����:�H����ƫ�i�J���ݪ��A
        /// </summary>
        private void Walk()
        {
            #region �������ϰ�
            if (!targetIsDead && playerInTrackRange) state = StateEnemy.Track;  //�p�G ���a�i�J �l�ܽd�� �N�����l�ܪ��A

            nma.SetDestination(v3RandomWalkFinal);                       //�N�z��.�]�w�ت��a(�y��)
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.1f);//�����ʵe-���ت��a�Z���j��0.1�ɨ���
            #endregion

            #region �i�J����
            if (isWalk) return;
            isWalk = true;
            #endregion

            print("�H���y��: " + v3RandomWalk);

            NavMeshHit hit;                                                             //��������I��-�x�s�����I����T
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack, NavMesh.AllAreas);//��������.���o�y��(�H���y�СB�I����T.�b�|.�ϰ�)-���椺�i�樫���y��
            v3RandomWalkFinal = hit.position;                                           //�̲׮y��=�I����T���y��


            StartCoroutine(WalkEffect());
        }
        private IEnumerator WalkEffect()
        {
            float randomWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            yield return new WaitForSeconds(randomWalk);

            state = StateEnemy.Idle;
            #region ���}����
            isWalk = false;
            #endregion
        }

        /// <summary>
        /// ���a�O�_�b�l�ܽd�򤺡Atrue �O�Afalse�_
        /// </summary>
        private bool playerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }


        private bool isTrack;
        /// <summary>
        /// �l�ܪ��a
        /// </summary>
        private void Track()
        {
            #region �i�J����
            if (!isTrack)
            {
                StopAllCoroutines();
            }
            isTrack = true;
            #endregion

            nma.isStopped = false;                      //������ �]��false�N�O�Ұ�
            nma.SetDestination(traPlayer.position);
            ani.SetBool(parameterIdleWalk, true);

            //�Z���p�󵥩���� �N�i�������A
            if (nma.remainingDistance <= rangeAttack) state = StateEnemy.Attack;

        }

        [Header("�����ɶ�"), Range(0, 5)]
        public float timeAttack = 2.5f;
        private string parameterAttack = "����Ĳ�o";
        private bool isAttack;
        [Header("��������ǰe�ˮ`�ɶ�"), Range(0, 5)]
        public float delaySendDamage = 0.5f;

        /// <summary>
        /// �������a
        /// </summary>
        private void Attack()
        {
            nma.isStopped = true;                      //������ ����
            ani.SetBool(parameterIdleWalk, false);      //�����
            nma.SetDestination(traPlayer.position);
            if (nma.remainingDistance > rangeAttack) state = StateEnemy.Track;
            LookAtPlayer();


            if (isAttack) return;                   //�p�G���b�������N���X9�קK���Ƨ���0
            isAttack = true;                        //���b������
            ani.SetTrigger(parameterAttack);

            StartCoroutine(DelaySendDamageToTarget());      //�Ұʩ���ǰe�ˮ`���ؼШ�{
        }

        private bool targetIsDead;

        /// <summary>
        /// ����ǰe�ˮ`���ؼ�
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelaySendDamageToTarget()
        {
            yield return new WaitForSeconds(delaySendDamage);

            //���z �����I��(�����I.�@�b�ؤo�A�}�B�A�ϼh)
            Collider[] hits = Physics.OverlapBox
                (
                  transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
               v3AttackOffset / 2, Quaternion.identity, 1 << 6);

            //�p�G �I������ƶq�j�� �s�B �ǰe�����O���I�����󪺨��˨t��
            if (hits.Length > 0) targetIsDead = hits[0].GetComponent<HurtSystem>().Hurt(attack);
            if (targetIsDead) TargetDead();

            float waitToNextAttack = timeAttack - delaySendDamage;  //�p��Ѿl�N�o�ɶ�
            yield return new WaitForSeconds(waitToNextAttack);      //����

            isAttack = false;                                       //��_ �������A        
        }

        /// <summary>
        /// �ؼЦ��`
        /// </summary>
        private void TargetDead()
        {
            state = StateEnemy.Walk;
            isIdle = false;
            isWalk = false;
            nma.isStopped = false;
        }
        #endregion 

        [Header("���۪��a�t��"), Range(0, 50)]
        public float speedLookAt = 10;

        /// <summary>
        /// ���V���a
        /// </summary>
        private void LookAtPlayer()
        {
            Quaternion angle = Quaternion.LookRotation(traPlayer.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            ani.SetBool(parameterIdleWalk, transform.rotation != angle); //����ɰʧ@
        }
    }

}






