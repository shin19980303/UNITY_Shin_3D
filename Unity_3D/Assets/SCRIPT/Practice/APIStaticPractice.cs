using UnityEngine;
using System.Collections;

/// <summary>
/// 靜態屬性與方法API課堂練習
/// </summary>
public class APIStaticPractice : MonoBehaviour
{

    void Start()
    {
        /*int Totalcam = Camera.allCamerasCount;
        Vector2 gravity2d = Physics2D.gravity;
        float mathpi = Mathf.PI;

        gravity2d.Set(0, -20);

        Time.timeScale = 0.5f;
        Mathf.FloorToInt(9.999f);
        Vector3.Distance(new Vector3(1, 1, 1), new Vector3(22, 22, 22));

        Application.OpenURL("https://unity.com/");

    }


// Update is called once per frame
   void Update()
    {
        bool anykeypress = Input.anyKey;
        bool isspacepress = Input.GetKeyDown(KeyCode.Space);
    } */
        //カメラの数
        int count = Camera.allCamerasCount;
        print("カメラ" + count + "台があります");
        //重力差
        Physics2D.gravity=new Vector2(0,-20);
        
        //円周率
        float pi = Mathf.PI;
        print("円周率は" + pi);
        //重力設定
        Physics2D.gravity = new Vector2(0.0f, -20f);
        print("新たな重力は:" + Physics2D.gravity);
        //時間設定0.5
        Time.timeScale = 0.5f;
        //9.999の小数点を除く
        print("小数点を除いたら: " + Mathf.Round(9.999f));
        //両点間の距離
        Vector3 a = new Vector3(1, 1, 1);
        Vector3 b = new Vector3(22, 22, 22);
        float distance = Vector3.Distance(a, b);
        print("両点間の距離" + distance);
        //ウエブサイトを開く
        Application.OpenURL("https://unity.com/");
    }
        private void Update()
        {
            //
            print("ボタンを押してるかどうか" + Input.anyKey);
            print("経った時間: " + Time.time);
            print("スペースバーを押してるかどうか" + Input.GetKey(KeyCode.Space));
        }
}
