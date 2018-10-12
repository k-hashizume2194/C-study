using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace NenpiApp
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// コンストラクター
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        #region イベント処理

        /// <summary>
        /// 初期表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //   1.画面の内容を初期表示状態に戻す
            //※「入力値クリアメソッド」を実行
            Clear();
        }

        /// <summary>
        /// クリアボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            //   1.画面の内容を初期表示状態に戻す
            //※「入力値クリアメソッド」を実行
            Clear();
        }

        /// <summary>
        /// 計算ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculation_Click(object sender, EventArgs e)
        {
            string oiling = boxOilingQuantity.Text;
            string mileageVal = txtThisMileage.Text;

            ///1.給油量の入力チェックを行う
            // 入力チェック結果を取得
            string message = CheckOilingQuantity(oiling);
            // ⇒入力チェックの結果、エラーがあればメッセージをダイアログに出して処理終了
            if (!string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show(message);
                return;
            } 

            ///2-1.燃費計算
            ///区間燃費 ＝ 区間距離 / 給油量
            double oilingdouble = double.Parse(oiling);
            double valThisMileage = double.Parse(mileageVal);
            double nenpi = Culcnenpi(oilingdouble, valThisMileage);

            // 3.燃費計算結果をテキストボックスにセット
            txtFuelConsumption.Text = nenpi.ToString();

            ///4.「クリア」「記録」「終了」ボタン以外の入力部品を変更不可状態にする。
            dateTimePicker.Enabled = false;
            boxOilingQuantity.Enabled = false;
            txtCurrentMileage.Enabled = false;
            btnCalculation.Enabled = false;
        }

        /// <summary>
        /// 記録ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecord_Click(object sender, EventArgs e)
        {
            ///1.区間燃費算出済チェックを行う
            ///1-1.未算出チェック
            ///「区間燃費」が空白の場合にエラーとする
            ///メッセージ：「記録処理は区間燃費の算出後に実行してください」
            ///をダイアログに表示して処理終了
            ///⇒入力チェックの結果、エラーが無ければ2へ
            if (string.IsNullOrWhiteSpace(txtFuelConsumption.Text))
            {
                MessageBox.Show("記録処理は区間燃費の算出後に実行してください");
                return;
            }

            ///2.記録処理実行確認ダイアログ表示
            ///メッセージ：「記録処理を実行します。よろしいですか？」
            ///の確認ダイアログを表示
            ///→「はい」をクリックした場合、3へ
            ///→「いいえ」をクリックした場合処理終了
            DialogResult result = MessageBox.Show("記録処理を実行します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //TODO:「はい」をクリックした場合、3へ
            } else
            {
                return;
            }

            ///3.燃費データ保存
            ///・内蔵DB(SQLite)のテーブル「t_nenpi」に以下の内容のレコードを追加
            ///給油日付 「給油日入力部品」の入力値をYYYYMMDD形式に変換
            ///給油時総走行距離	「給油時総走行距離」の値
            ///区間走行距離 「区間走行距離」の値
            ///区間燃費 「区間燃費」の値

            string db_file = "nenpi.db";


            using (var nenpiData = new SQLiteConnection("Data Source=" + db_file))
            {
                // TODO :テーブルがなかった場合作成の処理まで
                nenpiData.Open();
                using (SQLiteCommand command = nenpiData.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS t_nenpi(id INTEGER  PRIMARY KEY AUTOINCREMENT, refuel_date TEXT, mileage REAL, trip_mileage REAL, fuel_cost REAL)";

                    command.ExecuteNonQuery();

                    
                }
                nenpiData.Close();
            }






            ///・記録処理完了メッセージの表示
            ///メッセージ：「記録処理が完了しました」													
            ///をダイアログに表示して処理終了
            MessageBox.Show("記録処理が完了しました", "");
        }

        /// <summary>
        /// 終了ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
         ///メインフォームを閉じる
         this.Close();
        }

        /// <summary>
        /// 給油時走行距離フォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCurrentMileage_Leave(object sender, EventArgs e)
        {                                                                  
	　　　//給油時総走行距離入力チェックを実行																																	
            string kyuyuzitMileage = txtCurrentMileage.Text;
            double zenkaiMileage = double.Parse(txtPastMileage.Text);
            string messagekyuyuzi = CheckCurrentMileage(kyuyuzitMileage, zenkaiMileage);
                     
            //⇒入力チェックの結果
            //		・エラーが無い場合：2 - 1へ
            //		・エラーがある場合：「区間距離」テキストボックスに空白を設定

            //区間距離を算出して「区間距離」テキストボックスに設定
            double kyuyuzidouble = double.Parse(kyuyuzitMileage);
            double kukankyori = KukanCul(kyuyuzidouble, zenkaiMileage);
            txtThisMileage.Text = kukankyori.ToString();

            //2 - 2.計算ボタンをクリック可能にする
            btnCalculation.Enabled = true;
        }

        #endregion

        #region privateメソッド

        /// <summary>
        /// 入力値クリアメソッド
        /// </summary>
        private void Clear()
        {
            dateTimePicker.Value = DateTime.Now;
            boxOilingQuantity.Text = "";
            txtCurrentMileage.Text = "";

            //TODO：前回給油時総走行距離 DBに記録されている最後の給油時走行距離を表示させる
            txtCurrentMileage.Text = "";
            txtThisMileage.Text = "";
            txtFuelConsumption.Text = "";	
            btnCalculation.Enabled = false;
            dateTimePicker.Enabled = true;
            boxOilingQuantity.Enabled = true;
            txtCurrentMileage.Enabled = true;
           
            // TODO：まだスタブ状態
            //※「前回給油時総走行距離表示」テキストボックスには、DBに記録されている最後の給油時総走行距離を取得
            //前回給油時総走行距離取得メソッドを実行
            double zenkai = GetzenkaiFromdb();
            txtPastMileage.Text = zenkai.ToString();
        }

        /// <summary>
        /// 前回走行距離取得メソッド
        /// </summary>
        /// <returns>DBから取得した前回走行距離</returns>
        private double GetzenkaiFromdb()
        {
            //			→「前回給油時総走行距離取得メソッド」を実行
            //                      前回給油時総走行距離取得メソッド
            //                                      引数１：なし
            //                                      戻り値：前回給油時総走行距離(double)																															
            //			・内蔵DB(SQLite)のテーブル「t_nenpi」から以下の条件でレコード抽出
            //				・条件：給油日が直近(一番最近)
            return 12500.5;//TODO:スタブなので固定値
        }

        /// <summary>
        /// 燃費計算メソッド
        /// </summary>
        /// <param name="oilingdouble">給油量</param>
        /// <param name="valThisMileage">区間距離</param>
        /// <returns>区間燃費</returns>
        private double Culcnenpi(double oilingdouble, double valThisMileage)
        {
            ///燃費計算メソッド
            ///引数１：給油量 (double)
            ///引数２：区間距離 (doule)
            ///戻り値：区間燃費 (double) ※30.5のように小数値で返す ※小数点第一位で四捨五入する
            double nenpiDouble = valThisMileage / oilingdouble;
            Console.WriteLine(nenpiDouble);
            return Math.Round(nenpiDouble, 1, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 給油時走行距離入力チェックメソッド
        /// </summary>
        /// <param name="kyuyuzitMileage">給油時総走行距離</param>
        /// <param name="zenkaiMileage">前回給油時総走行距離</param>
        /// <returns>なし</returns>
        private string CheckCurrentMileage(string kyuyuzitMileage, double zenkaiMileage)
        {

            /// 1.給油時総走行距離の入力チェックを行う

            //   1 - 1.未入力チェック
            //	・未入力の場合：以下の処理を実行して処理終了
            //		「区間距離」テキストボックスに空白を設定
            //		「計算」ボタンをクリック不可状態にする

            //	・入力がある場合：1 - 2へ

            //   1 - 2.正の数値チェック
            //	「給油時総走行距離」が正の数値以外の場合
            //			※数値以外の文字、マイナスの数値をエラーにする
            //               メッセージ：「給油時走行距離は正の数値で入力してください」																																
            //							をダイアログに表示

            //   1 - 3.整合性チェック
            //	「給油時総走行距離」＜「前回給油時総走行距離」の場合
            //               メッセージ：「給油時総走行距離は前回の距離より大きな値で入力してください」																																
            //							をダイアログに表示
            //	・「給油時総走行距離入力チェックメソッド」を作成
            //			→メッセージダイアログ表示処理は1か所記述するのみ
            //               給油時総走行距離入力チェックメソッド

            //                               引数１：給油時総走行距離(string)                                                                           nullが渡る場合もあり
            //                              引数２：前回給油時総走行距離(double)

            //                               戻り値：メッセージ(string)
            return "";
        }

        /// <summary>
        /// 区間距離計算メソッド
        /// </summary>
        /// <param name="kyuyuzitMileage">給油時総走行距離</param>
        /// <param name="zenkaiMileage">前回給油時総走行距離</param>
        /// <returns>区間距離</returns>
        private double KukanCul(double kyuyuzitMileage, double zenkaiMileage)
        {
            // 区間距離を計算して小数点第二位四捨五入する
            double kukanMileage = kyuyuzitMileage - zenkaiMileage;
            Console.WriteLine(kukanMileage);
            return Math.Round(kukanMileage, 1, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 給油量未入力チェックメソッド
        /// </summary>
        /// <param name="oiling">給油量</param>
        /// <returns>メッセージ</returns>
        private string CheckOilingQuantity(string oiling)
        {
            ///※給油時総走行距離のチェックがOKの場合のみ計算ボタンはクリックできる
            ///1-1.未入力チェック
            ///※空白、ゼロの場合にエラーとする
            if (!string.IsNullOrWhiteSpace(oiling))
            {
                //nullではなく、かつ空文字列でもなく、かつ空白文字列でもない
            }
            else 
            {
                // null、もしくは空文字列、もしくは空白文字列
                // メッセージ：「給油量を入力してください」をダイアログに表示して処理終了
                return "給油量を入力してください";
            }

            //数値かどうかチェック
            ///1-2.正の数値チェック
            ///「給油量」が0以下の数値以外の場合エラーにする
            ///メッセージ：「給油量は正の数値で入力してください」
            ///をダイアログに表示して処理終了
            double oilingNumber;
            bool canConvert = double.TryParse(oiling, out oilingNumber);

            if (!canConvert || oilingNumber <= 0)
            {
                return "給油量は０より大きい数値で入力してください";
            }
            return "";
        }


        #endregion

    }
}
