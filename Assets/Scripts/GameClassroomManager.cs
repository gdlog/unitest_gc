using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClassroomManager : MonoBehaviour {

    //---------------------------------------
    // 目次 ----------------------
    //     定数・変数宣言(画面順)
    //     Start & Update
    //     各関数(画面順)
    // --------------------------
    //
    //(画面順) 
    // 1.正面(黒板)
    // 2.右(扉と机)
    // 3.左(窓とロッカー)
    // 4.ズーム：A子机＋チョコ 
    // 5.ズーム：C子ロッカー 
    // 6.ズーム：ペンケース 
    // 7.ズーム：B男机 
    // + UI設定
    //---------------------------------------


    //------------------------------------------------------------------------------
    // 定数・変数宣言(画面順) --------------------------------------------
    //------------------------------------------------------------------------------

    //--------------------------------------------------
    // 1.正面(黒板)
    //　　　ロジック:座席表をタップで見ることができる
    //           追加：針金ゲット
    //--------------------------------------------------
    //座席表
    public GameObject buttonSeatinglist; //座席表：ポップアップ
	public GameObject buttonSeatinglistOfBoard; //座席表：黒板上に設置
    public GameObject buttonWireOfBoard;  //針金：黒板上に設置


    //--------------------------------------------------
    // 3.2.右(扉と机)
    //　　　ロジック:扉開ける処理
    //--------------------------------------------------
    //扉開いた時用(ボタンからイメージに切り替え)
    public GameObject buttonLockDoor; //ロック状態
	public GameObject imageOpenDoor; //オープン状態

	//--------------------------------------------------
	// 3.右(扉と机)
	//　　　ロジック:ポスター貼り直す
	//          カーテンを開けてボトルGET&チョコを溶かす
	//          ロッカーズーム
	//--------------------------------------------------
	//ポスター
	public GameObject buttonPoster; //ポスター
	public Sprite PicturePoster; //貼り直したポスターの絵

	//カーテン
	public GameObject buttonCurtain; //カーテンをしている状態
	public GameObject buttonCurtainOpen; //カーテンをあけた状態
	private bool isCurtainOpen; //カーテン開いてるかどうか

	//窓際のリムーバー
	public GameObject buttonBottleOfCurtain;


	//--------------------------------------------------
	// 4.弁当
	//　ロジック:徐行ペンで開けると鍵でてくる
    //　アイコンから飛んでくる
	//--------------------------------------------------
	public GameObject buttonBentoOfPanel; //弁当箱
    public Sprite PictureBentoOpen; //開いた弁当箱の絵



	//--------------------------------------------------
	//5.ロッカー設定
	//　　　ロジック:暗証番号4桁が合っていれば鍵の半分が手に入る
	//--------------------------------------------------
	//定数：暗証番号
	public const int NLock0 = 0;
	public const int NLock1 = 1;
	public const int NLock2 = 2;
	public const int NLock3 = 3;
	public const int NLock4 = 4;
	public const int NLock5 = 5;
	public const int NLock6 = 6;
	public const int NLock7 = 7;
	public const int NLock8 = 8;
	public const int NLock9 = 9;
	//ダイヤル桁数(4) 画像表示用
	public GameObject[] buttonNLock = new GameObject[4];
	//ダイヤル数字(1~9) 画像表示用
	public Sprite[] NLockPicture = new Sprite[10];
	//ダイヤル数字 今の値の格納用
	private int[] buttonNLockNowNo = new int[4];


	//ロッカー開いた状態に
	public GameObject imageLockerOpen; 


	//--------------------------------------------------



	//--------------------------------------------------
	//6:ペンケース内部設定
	//　　　ロジック:接着剤が手に入る・リムーバーで落書きを消せる
	//--------------------------------------------------
	//落書き
	public GameObject buttonBoxOfScribble;
	//接着剤
	public GameObject buttonSheelOfBox;

    public Sprite PicturePencaseCleaned;
    public Sprite PictureIsSelectedPencaseCleaned;
    private bool cleanedScribble; //汚れを消したかどうか
    //--------------------------------------------------

    //--------------------------------------------------
    //7:机複数
    //　ロジック:
    //　　・ペンケースが手に入る
    //　　・ペンが手に入る
    //　　・カバンを開閉して弁当箱が手に入る
    //--------------------------------------------------
    //ペンケース
    public GameObject buttonPencaseOfDesk;
    //ペン
    public GameObject buttonPenOfDesk;
    //バッグ
    public GameObject buttonBagOfDesk;
    public Sprite PictureBagOpen; //開いたバッグの絵


    //--------------------------------------------------
    //8:ペン
    public GameObject buttonPenOfPanel;


    //--------------------------------------------------
    //UI設定
    //・ポップアップとアイコン設定まとめ
    //・メッセージ
    //・壁の向き・カメラアングル設定
    //--------------------------------------------------
    //--------------------------------------------------
    // UI設定 > ポップアップとアイコン設定まとめ
    // 
    //--------------------------------------------------
    //アイコンの選択・解除用カラー<Image>
    Color ColorDefaultIcon = new Color(255.0f,255.0f,255.0f,255.0f);
    Color ColorSelectedIcon = new Color(120.0f / 255.0f, 150.0f / 255.0f, 150.0f / 255.0f, 200.0f / 255.0f);

    //鍵 修復後
    public GameObject buttonKey; //鍵：ポップアップ
	public GameObject buttonIconKey; //鍵：アイコン
	public Sprite PictureKey; //鍵の絵
	private bool doesHaveKey; //鍵を持っているかどうか
    private bool isSelectedKey; //鍵を選択しているかどうか

    //鍵　右半分
    public GameObject buttonHalfkeyRight; //鍵右半分：ポップアップ
	public GameObject buttonIconHalfkeyRight; //鍵右半分：アイコン
	public Sprite PictureHalfkeyRight; //鍵右半分の絵
	private bool doesHaveHalfkeyRight; //鍵右半分を持っているかどうか 
    private bool isSelectedHalfkeyRight; //鍵右半分を選択しているかどうか

    //鍵　左半分
    public GameObject buttonHalfkeyLeft; //鍵左半分：ポップアップ
    public GameObject buttonIconHalfkeyLeft; //鍵左半分：アイコン
    public Sprite PictureHalfkeyLeft; //鍵左半分の絵
    private bool doesHaveHalfkeyLeft; //鍵左半分を持っているかどうか
    private bool isSelectedHalfkeyLeft; //鍵左半分を選択しているかどうか

    //接着剤・落書き
    public GameObject buttonSheel; //接着剤：ポップアップ
	public GameObject buttonIconSheel; //接着剤：アイコン
	public Sprite PictureSheel; //接着剤の絵
	private bool doesHaveSheel; //接着剤を持っているかどうか
    private bool isSelectedSheel; //接着剤を選択しているかどうか
    

    //リムーバーのボトル
    public GameObject buttonBottle; //リムーバー：ポップアップ
	public GameObject buttonIconBottle; //リムーバー：アイコン
	public Sprite PictureBottle; //リムーバーの絵
	private bool doesHaveBottle; //リムーバーを持っているかどうか
    private bool isSelectedBottle; //リムーバーを選択しているかどうか
    

    //ペンケース
    public GameObject buttonPencase; //ペンケース：ポップアップ
	public GameObject buttonIconPencase; //ペンケース：アイコン
	public Sprite PicturePencase; //ペンケースの絵
	private bool doesHavePencase; //ペンケースを持っているかどうか
    private bool isSelectedPencase; //ペンケースを選択しているかどうか
    

    //ペン
    public GameObject buttonPen; //ペン：ポップアップ
    public GameObject buttonIconPen; //ペン：アイコン
    public Sprite PicturePen; //ペンの絵
    private bool doesHavePen; //ペンを持っているかどうか
    private bool isSelectedPen; //ペンを選択しているかどうか
    public Sprite PictureEmptyPen; //絵：空のペン
    public Sprite PictureRemoverPen;//絵：除光ペン
    private bool doesHaveRemoverPen; //除光ペンになったかどうか

    //針金
    public GameObject buttonWire; //針金：ポップアップ
    public GameObject buttonIconWire; //針金：アイコン
    public Sprite PictureWire; //針金の絵
    private bool doesHaveWire; //針金を持っているかどうか
    private bool isSelectedWire; //針金を選択しているかどうか

    //弁当箱
    public GameObject buttonBento; //弁当箱：ポップアップ
    public GameObject buttonIconBento; //弁当箱：アイコン
    public Sprite PictureBento; //弁当箱の絵
    private bool doesHaveBento; //弁当箱を持っているかどうか
    private bool isSelectedBento; //弁当箱を選択しているかどうか




    /*
    //アイコン配列
    public GameObject[] buttonPopIconItem;//アイコンポップアップ
    public GameObject[] buttonIconItem; //アイコン
    public Sprite[] PictureIconItem; //アイコンの絵
    private bool[] doesHaveIconItem; //アイコンを持っているかどうか
    private bool[] isSelectedIconItem; //アイコンを選択しているかどうか
    public Sprite[] PictureIsSelectedIconItem; //アイコン（選択状態）


    0 本
    1 弁当
    2 ペン
    */




    //--------------------------------------------------
    // UI設定 > メッセージ設定
    //--------------------------------------------------
    //メッセージウィンドウ
    public GameObject buttonMessage; //ボタン：メッセージ
	public GameObject buttonMessageText; //メッセージテキスト
	public GameObject buttonGoalMessage; //ゴールメッセージ


	//--------------------------------------------------
	//UI設定 > 壁の向き・カメラアングル設定
	//--------------------------------------------------

	public GameObject buttonLeft;
	public GameObject buttonRight;
	public GameObject buttonBack;

	//定数：壁方向
	public const int WALL_FRONT = 1; //前:黒板
	public const int WALL_RIGHT = 2; //右:扉
	public const int WALL_LEFT = 3; //左:窓
	//小物ズーム
	public const int WALL_DESK = 4; //弁当箱
	public const int WALL_LOCKER = 5; //ロッカー
	public const int WALL_PENCASE = 6; //ペンケース
	public const int WALL_DESKB = 7; //机ズーム

	public GameObject panelWalls; //方向転換まとめ
	private int wallNo; //現在の向いている方向
	private int backNo; //戻る方向







	//------------------------------------------------------------------------------
	// Start() & Update()  ---------------------------------------------------------
	//------------------------------------------------------------------------------



	// Use this for initialization
	void Start () {

		//スタート時は正面(黒板)画面を表示
		wallNo = WALL_FRONT;
		backNo = 1;

        //各アイテムを持ってない状態に
        doesHaveKey = false;
		doesHaveHalfkeyRight = false;
		doesHaveHalfkeyLeft = false;
		doesHaveSheel = false;
		doesHaveBottle = false;
		doesHavePencase = false;
        doesHaveWire = false;
        doesHavePen = false;
        doesHaveBento = false;


        //各アイテムアイコンを選択していない状態に
        isSelectedKey = false;
        isSelectedHalfkeyRight = false;
        isSelectedHalfkeyLeft = false;
        isSelectedSheel = false;
        isSelectedBottle = false;
        isSelectedPencase = false;
        isSelectedWire = false;
        isSelectedPen = false;
        isSelectedBento = false;

        //カーテン閉じてる状態
        isCurtainOpen = false;
        //ペンケースの落書きがある状態
        cleanedScribble = false;
        //除光ペンは持っていない状態
        doesHaveRemoverPen = false;

        //ロッカー4桁の暗証番号:初期値0000
        buttonNLockNowNo [0] = NLock0;
		buttonNLockNowNo [1] = NLock0;
		buttonNLockNowNo [2] = NLock0;
		buttonNLockNowNo [3] = NLock0;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}




	//------------------------------------------------------------------------------
	// 各関数(画面順)        ---------------------------------------------------------
	//------------------------------------------------------------------------------

	//--------------------------------------------------
	// 各関数 > 1.正面(黒板)
	//--------------------------------------------------
	//座席表を表示
	public void PushButtonSeatinglistOfBoard(){
		buttonSeatinglist.SetActive (true);
	}
	//座席表を非表示
	public void PushButtonSeatinglist(){
		buttonSeatinglist.SetActive (false);
	}

    //針金をGET
    public void PushButtonWireOfBoard()
    {
        //まだ針金持ってない？
        if (doesHaveWire == false && isCurtainOpen == true)
        {

            //針金ポップアップ表示
            DisplayMessage("針金を手に入れた");
            buttonWire.SetActive(true);

            //アイコン所持表示&アイテムを所持状態に
            buttonIconWire.GetComponent<Image>().sprite = PictureWire;
            doesHaveWire = true; //針金を所持状態に

            //黒板上の針金を非表示に
            buttonWireOfBoard.SetActive(false);

        }
    }

    //--------------------------------------------------
    // 各関数 > 2.右(扉と机)
    //--------------------------------------------------
    public void PushButtonLockDoor(){
		if (isSelectedKey==true) {

			//扉を開けた状態に切り替え
			buttonLockDoor.SetActive (false);
			imageOpenDoor.SetActive (true);

			//ステージクリア表示
			DisplayGoal ();

		} else {
			DisplayMessage ("鍵がかかっている");
		}
	}

	//A子机にズーム 一旦使いません
	public void PushButtonZoomDeskA(){
		backNo = wallNo;
		buttonBack.SetActive (true);
		buttonLeft.SetActive (false);
		buttonRight.SetActive (false);		
		panelWalls.transform.localPosition = new Vector3(-4500.0f,0.0f,0.0f);
	}

	//机sにズーム
	public void PushButtonZoomDeskB(){
		backNo = wallNo;
		buttonBack.SetActive (true);
		buttonLeft.SetActive (false);
		buttonRight.SetActive (false);		
		panelWalls.transform.localPosition = new Vector3(-9000.0f,0.0f,0.0f);
	}

	//--------------------------------------------------
	// 各関数 > 3.左(窓とロッカー)
	//--------------------------------------------------
	//ポスター張り替え
	public void PushButtonPoster(){
		buttonPoster.GetComponent<Image>().sprite = PicturePoster;
	}
	//カーテンをオープン
	public void PushButtonCurtain(){
		buttonCurtain.SetActive (false);
		buttonCurtainOpen.SetActive (true);
        //wire
        buttonWireOfBoard.SetActive(true);
        isCurtainOpen = true;

	}
	//リムーバーをGET
	public void PushButtonBottleOfCurtain(){
		//まだリムーバー持ってない？
		if(doesHaveBottle == false){

			//リムーバーポップアップ表示
			DisplayMessage ("リムーバーを手に入れた");
			buttonBottle.SetActive (true);

			//アイコン所持表示&アイテムを所持状態に
			buttonIconBottle.GetComponent<Image>().sprite = PictureBottle;
			doesHaveBottle = true; //接着剤を所持状態に

			//窓際のリムーバーを非表示に
			buttonBottleOfCurtain.SetActive (false);

		}	
	}
	//Cロッカー
	public void PushButtonLockLocker(){
		backNo = wallNo;
		buttonBack.SetActive (true);
		buttonLeft.SetActive (false);
		buttonRight.SetActive (false);		
		panelWalls.transform.localPosition = new Vector3(-6000.0f,0.0f,0.0f);
	}


	//--------------------------------------------------
	// 各関数 > 4.弁当箱
	//--------------------------------------------------
	public void PushButtonBentoOfPanel(){
		//まだ鍵持ってない？
		if (doesHaveHalfkeyLeft == false)
        {
            if (doesHaveRemoverPen == true && isSelectedPen == true)
            {

                //鍵ポップアップ表示
                DisplayMessage("弁当箱の中から鍵を手に入れた");
                buttonHalfkeyLeft.SetActive(true);

                //アイコン所持表示&アイテムを所持状態に
                buttonIconHalfkeyLeft.GetComponent<Image>().sprite = PictureHalfkeyLeft;
                doesHaveHalfkeyLeft = true;//鍵を所持状態に

                //弁当箱を開けた状態に変える
                buttonBentoOfPanel.GetComponent<Image>().sprite = PictureBentoOpen;
                buttonIconBento.GetComponent<Image>().sprite = PictureBentoOpen;

                //penを元に戻す
                isSelectedPen = false;
                buttonIconPen.GetComponent<Image>().color = ColorDefaultIcon;
            }
            else
            {
                DisplayMessage("接着剤で固く閉じられている");
            }

        } 
	}





	//--------------------------------------------------
	// 各関数 > 5.ロッカー
	//--------------------------------------------------
	//ロッカー 
	//4桁数値変更-----------------------
	public void PushButtonNLockDigit1(){
		ChangeButtonNLockEachNo (0);
	}
	public void PushButtonNLockDigit2(){
		ChangeButtonNLockEachNo (1);
	}
	public void PushButtonNLockDigit3(){
		ChangeButtonNLockEachNo (2);
	}
	public void PushButtonNLockDigit4(){
		ChangeButtonNLockEachNo (3);
	}
	//数値変更
	void ChangeButtonNLockEachNo(int buttonNo){

		//次の数値に変更
		buttonNLockNowNo [buttonNo]++; //現在の値に+1
		if(buttonNLockNowNo [buttonNo] > 9){
			buttonNLockNowNo [buttonNo] = 0; //9の場合は0に戻す			
		}

		//ボタンの画像も変更
		buttonNLock [buttonNo].GetComponent<Image> ().sprite = NLockPicture [buttonNLockNowNo [buttonNo]];


		// ロック解除判定
		if(
			buttonNLockNowNo [0] == 0 &&
			buttonNLockNowNo [1] == 5 &&
			buttonNLockNowNo [2] == 2 &&
			buttonNLockNowNo [3] == 9
		){
			//まだ鍵持ってない？
			if(doesHaveHalfkeyRight == false){
				
				//鍵ポップアップ表示
				DisplayMessage ("金庫の中に鍵が入っていた");
				buttonHalfkeyRight.SetActive (true);

				//アイコン所持表示&アイテムを所持状態に
				buttonIconHalfkeyRight.GetComponent<Image>().sprite = PictureHalfkeyRight;
				doesHaveHalfkeyRight = true;//鍵を所持状態に

				//ロッカーを開いた表示に
				imageLockerOpen.SetActive (true);
				
			}
			
			
		}



	}

	//--------------------------------------------------
	// 各関数 > 6.ペンケース
	//--------------------------------------------------
	//接着剤をゲット
	public void PushButtonSheelOfBox(){
		//まだ接着剤持ってない？
		if(doesHaveSheel == false){
			
			//接着剤ポップアップ表示
			DisplayMessage ("接着剤を手に入れた");
			buttonSheel.SetActive (true);

			//アイコン所持表示&アイテムを所持状態に
			buttonIconSheel.GetComponent<Image>().sprite = PictureSheel;
			doesHaveSheel = true; //接着剤を所持状態に

			//筆箱内の接着剤を非表示に
			buttonSheelOfBox.SetActive (false);

		}		
	}
	//落書きを消す
	public void PushButtonBoxOfScribble(){
		//リムーバーペンを持っているか？
		if (cleanedScribble == false && isSelectedPen==true && doesHaveRemoverPen==true) {
			//落書きを消す
			buttonBoxOfScribble.SetActive (false);
            cleanedScribble = true;

            buttonIconPencase.GetComponent<Image>().sprite = PicturePencaseCleaned;
            DisplayMessage ("除光液で塗りつぶしが消えた");
            //選択状態を外す
            isSelectedPen = false;
            buttonIconPen.GetComponent<Image>().color = ColorDefaultIcon;

        } else {
			DisplayMessage ("油性のペンで塗りつぶされている");
		}
	}

    //--------------------------------------------------
    // 各関数 > 7.机　
    //--------------------------------------------------
    public void PushButtonBagOfDesk()
    {

        if (doesHaveBento == false)
        {
            if (isSelectedWire == true)
            {
                //弁当ポップアップ表示
                DisplayMessage("弁当箱を手に入れた");
                buttonBento.SetActive(true);

                //アイコン所持表示&アイテムを所持状態に
                buttonIconBento.GetComponent<Image>().sprite = PictureBento;
                doesHaveBento = true; //弁当箱を所持状態に

                //カバンを開いた表示に
                buttonBagOfDesk.GetComponent<Image>().sprite = PictureBagOpen;

                //選択状態を外す
                isSelectedWire = false;
                buttonIconWire.GetComponent<Image>().color = ColorDefaultIcon;
            }
            else
            {
                DisplayMessage("鍵がかかっている");
            }
        }

    }

    public void PushButtonPencaseOfDesk(){

		if (doesHavePencase == false) {
			//ペンケースポップアップ表示
			DisplayMessage ("ペンケースを手に入れた");
			buttonPencase.SetActive (true);

			//アイコン所持表示&アイテムを所持状態に
			buttonIconPencase.GetComponent<Image> ().sprite = PicturePencase;
			doesHavePencase = true; //ペンケースを所持状態に

			//机内のペンケースを非表示に
			buttonPencaseOfDesk.SetActive (false);
		}

	}

    public void PushButtonPenOfDesk()
    {

        if (doesHavePen == false)
        {
            //ペンケースポップアップ表示
            DisplayMessage("ペンを手に入れた");
            buttonPen.SetActive(true);

            //アイコン所持表示&アイテムを所持状態に
            buttonIconPen.GetComponent<Image>().sprite = PicturePen;
            doesHavePen = true; //ペンを所持状態に

            //机内のペンを非表示に
            buttonPenOfDesk.SetActive(false);


        }
    }


    //--------------------------------------------------
    // 各関数 > 8.ペン
    //--------------------------------------------------
    public void PushButtonPenOfPanel()
    {
        //まだリムーバーペン持ってない？
        if (doesHaveRemoverPen == false)
        {
            //除光液を選択しているか
            if (isSelectedBottle==true)
            {
                //ペンを変化
                DisplayMessage("消せる！溶かせる！除光液ペンを作った");
                buttonIconPen.GetComponent<Image>().sprite = PictureRemoverPen;
                buttonPenOfPanel.GetComponent<Image>().sprite = PictureRemoverPen;
                doesHaveRemoverPen = true;//リムーバーペンを所持状態に
                
                //除光液を元に戻す
                isSelectedBottle = false;
                buttonIconBottle.GetComponent<Image>().color = ColorDefaultIcon;

            } else {
                //ペンを変化
                DisplayMessage("中身は空のようだ");
                buttonIconPen.GetComponent<Image>().sprite = PictureEmptyPen;
                buttonPenOfPanel.GetComponent<Image>().sprite = PictureEmptyPen;
            }

        }
    }





    //--------------------------------------------------
    // 各関数 > UI設定 > アイテム操作
    //--------------------------------------------------
    /*
     関数化したい
        void PushButtonIcon(int index)
        {
            isSelected[index] = true;
            buttonIcon[index].GetComponent<Image>().sprite = PictureIsSelected[index];
        }   
    */

    //弁当箱アイコン操作
    public void PushButtonIconBento()
    {
        if (doesHaveBento == true)
        {
            if (isSelectedBento == false)
            {
                backNo = wallNo;
                buttonBack.SetActive(true);
                buttonLeft.SetActive(false);
                buttonRight.SetActive(false);
                panelWalls.transform.localPosition = new Vector3(-4500.0f, 0.0f, 0.0f);


                //選択状態にする
                isSelectedBento = true;
                buttonIconPencase.GetComponent<Image>().color = ColorSelectedIcon;


            }
        }
    }

    //ペンアイコン操作
    public void PushButtonIconPen()
    {
        if (doesHavePen == true)
        {
            if (isSelectedPen == false)
            {
                if (doesHaveRemoverPen == false)
                {
                    backNo = wallNo;
                    buttonBack.SetActive(true);
                    buttonLeft.SetActive(false);
                    buttonRight.SetActive(false);
                    panelWalls.transform.localPosition = new Vector3(-10500.0f, 0.0f, 0.0f);
                }

                //選択状態にする
                isSelectedPen = true;
                buttonIconPen.GetComponent<Image>().color = ColorSelectedIcon;
                

            }
            else
            {
                //選択状態を外す
                isSelectedPen = false;
                buttonIconPen.GetComponent<Image>().color = ColorDefaultIcon;

            }
        }
    }


    //ペンケースアイコン操作
    public void PushButtonIconPencase(){
        if (doesHavePencase == true)
        {
            if (isSelectedPencase == false)
            {
                backNo = wallNo;
                buttonBack.SetActive(true);
                buttonLeft.SetActive(false);
                buttonRight.SetActive(false);
                panelWalls.transform.localPosition = new Vector3(-7500.0f, 0.0f, 0.0f);


                //選択状態にする
                isSelectedPencase = true;
                buttonIconPencase.GetComponent<Image>().color = ColorSelectedIcon;
                
            }
        }
    }



    //除光液アイコン操作
    public void PushButtonIconBottle()
    {
        if (doesHaveBottle == true)
        {
            if (isSelectedBottle == false)
            {
                //選択状態にする
                isSelectedBottle = true;
                buttonIconBottle.GetComponent<Image>().color = ColorSelectedIcon;
            }
            else
            {
                //選択状態を外す
                isSelectedBottle = false;
                buttonIconBottle.GetComponent<Image>().color = ColorDefaultIcon;

            }
        }
    }
    //接着剤アイコン操作
    public void PushButtonIconSheel()
    {
        if (doesHaveSheel == true)
        {
            if (isSelectedSheel == false)
            {
                //選択状態にする
                isSelectedSheel = true;
                buttonIconSheel.GetComponent<Image>().color = ColorSelectedIcon;
                CheckGetKey();
            }
            else
            {
                //選択状態を外す
                isSelectedSheel = false;
                buttonIconSheel.GetComponent<Image>().color = ColorDefaultIcon;
            }
        }
    }
    //鍵の半分（左）アイコン操作
    public void PushButtonIconHalfkeyLeft()
    {
        if (doesHaveHalfkeyLeft == true)
        {
            if (isSelectedHalfkeyLeft == false)
            {
                //選択状態にする
                isSelectedHalfkeyLeft = true;
                buttonIconHalfkeyLeft.GetComponent<Image>().color = ColorSelectedIcon;
                CheckGetKey();
            }
            else
            {
                //選択状態を外す
                isSelectedHalfkeyLeft = false;
                buttonIconHalfkeyLeft.GetComponent<Image>().color = ColorDefaultIcon;
            }
        }
    }



    //鍵の半分アイコン操作
    public void PushButtonIconHalfkeyRight()
    {
        if (doesHaveHalfkeyRight == true)
        {
            if (isSelectedHalfkeyRight == false)
            {
                //選択状態にする
                isSelectedHalfkeyRight = true;
                buttonIconHalfkeyRight.GetComponent<Image>().color = ColorSelectedIcon;
                CheckGetKey();
            }
            else
            {
                //選択状態を外す
                isSelectedHalfkeyRight = false;
                buttonIconHalfkeyRight.GetComponent<Image>().color = ColorDefaultIcon;
            }
        }
    }

    //鍵のアイコン操作
    public void PushButtonIconKey()
    {
        if (doesHaveKey == true)
        {
            if (isSelectedKey == false)
            {
                //選択状態にする
                isSelectedKey = true;
                buttonIconKey.GetComponent<Image>().color = ColorSelectedIcon;
            }
            else{
                //選択状態を外す
                isSelectedKey = false;
                buttonIconKey.GetComponent<Image>().color = ColorDefaultIcon;
            }
        }
    }


    void CheckGetKey()
    {
        if (isSelectedHalfkeyLeft == true && isSelectedHalfkeyRight == true && isSelectedSheel == true && doesHaveKey==false)
            {
            //鍵ポップアップ表示
            DisplayMessage("鍵を手に入れた");
            buttonKey.SetActive(true);

            //アイコン所持表示&アイテムを所持状態に
            buttonIconKey.GetComponent<Image>().sprite = PictureKey;
            doesHaveKey = true; //ペンケースを所持状態に

            //選択解除
            isSelectedHalfkeyLeft = false;
            buttonIconHalfkeyLeft.GetComponent<Image>().color = ColorDefaultIcon;
            isSelectedHalfkeyRight = false;
            buttonIconHalfkeyRight.GetComponent<Image>().color = ColorDefaultIcon;
            isSelectedSheel = false;
            buttonIconSheel.GetComponent<Image>().color = ColorDefaultIcon;

            buttonIconHalfkeyLeft.SetActive(false);
            buttonIconHalfkeyRight.SetActive(false);

        }
    }

    //針金アイコン操作
    public void PushButtonIconWire()
    {
        if (doesHaveWire == true)
        {
            if (isSelectedWire == false)
            {
                //選択状態にする
                isSelectedWire = true;
                buttonIconWire.GetComponent<Image>().color = ColorSelectedIcon;

            }
            else
            {
                //選択状態を外す
                isSelectedWire = false;
                buttonIconWire.GetComponent<Image>().color = ColorDefaultIcon;
            }
        }
    }

    //--------------------------------------------------
    // 各関数 > UI設定 > ポップアップ非表示
    //--------------------------------------------------
    public void PushButtonPopup()
    {
        ClearDisplays();
    }

    //--------------------------------------------------
    // 各関数 > UI設定 > メッセージ表示・非表示
    //--------------------------------------------------

    //メッセージを表示
    void DisplayMessage(string mes){
		buttonMessage.SetActive (true);
		buttonMessageText.GetComponent<Text> ().text = mes;
		
	}

	//クリア表示
	void DisplayGoal(){
        ClearDisplays(); //いらない表示を消す
        buttonGoalMessage.SetActive (true);
	}


	//--------------------------------------------------
	// 各関数 > UI設定 > 壁向き・カメラアングル設定
	//--------------------------------------------------

	//右>ボタンを押した時
	public void PushButtonRight(){
		wallNo++; //方向１つ右に
		//左の１つ右は前
		if(wallNo>WALL_LEFT){
			wallNo = WALL_FRONT;
		}
		DisplayWall (); //カメラアングル更新
		ClearDisplays(); //いらない表示を消す
	}

	//<左ボタンを押した時
	public void PushButtonLeft(){
		wallNo--; //方向１つ左に
		//前の１つ左は左
		if(wallNo<WALL_FRONT){
			wallNo = WALL_LEFT;
		}
		DisplayWall (); //カメラアングル更新
		ClearDisplays(); //いらない表示を消す
	}
	//ズームアップ時に戻るボタン押した時の挙動
	public void PushButtonBack(){
		wallNo = backNo;
		backNo = 1;
		buttonBack.SetActive (false);
		buttonLeft.SetActive (true);
		buttonRight.SetActive (true);
		DisplayWall (); //カメラアングル更新
		ClearDisplays(); //いらない表示を消す
       
            isSelectedPencase = false;
            buttonIconPencase.GetComponent<Image>().color = ColorDefaultIcon;
            isSelectedPen = false;
            buttonIconPen.GetComponent<Image>().color = ColorDefaultIcon;
            isSelectedBento = false;
            buttonIconBento.GetComponent<Image>().color = ColorDefaultIcon;
 
    }

	void ClearDisplays(){
		buttonMessage.SetActive (false); //メッセージを消す
		//座席表やアイテム6つのウィンドウも表示されていたら消す
		buttonSeatinglist.SetActive (false);
		buttonKey.SetActive (false);
		buttonHalfkeyRight.SetActive (false);
		buttonHalfkeyLeft.SetActive (false);
		buttonSheel.SetActive (false);
		buttonBottle.SetActive (false);
		buttonPencase.SetActive (false);
        buttonBento.SetActive(false);
        buttonPen.SetActive(false);
        buttonWire.SetActive(false);

    }

	//カメラアングル変更
	void DisplayWall(){
		switch (wallNo) {
		case WALL_FRONT: //1.正面(黒板)
			panelWalls.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
			break;
		case WALL_RIGHT: //2.扉
			panelWalls.transform.localPosition = new Vector3(-1500.0f,0.0f,0.0f);
			break;
		case WALL_LEFT: //3.窓とロッカー
			panelWalls.transform.localPosition = new Vector3(-3000.0f,0.0f,0.0f);
			break;

		}
	}

}
