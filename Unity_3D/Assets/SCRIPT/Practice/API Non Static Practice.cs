using UnityEngine;

public class APINonStaticPractice : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer sprCapsule;
    public Camera camMain;
    public SpriteRenderer spr3;
    public Transform pic1;
    public Rigidbody2D pic2;

    private void Start()
    {
        #region
        print("��v���`��: " + cam.depth);
        print("�Ϥ��C��" + sprCapsule.color);

        camMain.backgroundColor = Random.ColorHSV();
        spr3.flipY = true;
        #endregion

        
    }
    void Update()
    {
        #region
        pic1.Rotate(0, 0, 3);
        pic2.AddForce(new Vector2(0, 10));
        #endregion
    }
}
    
       
    


    

    
    
     
   

