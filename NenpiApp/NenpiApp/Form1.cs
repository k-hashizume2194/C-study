using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (!string.IsNullOrWhiteSpace(message))
            {
                // ⇒入力チェックの結果、エラーがあれば
                //メッセージをダイアログに出し、給油量テキストボックスにフォーカスし、処理終了
                MessageBox.Show(message);
                this.ActiveControl = this.boxOilingQuantity;
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
            }
            else
            {
                return;
            }

            ///3.燃費データ保存
            ///・内蔵DB(SQLite)のテーブル「t_nenpi」に以下の内容のレコードを追加
            //DBを作成します
            string db_file = "nenpi.db";


            ///給油日付 「給油日入力部品」の入力値をYYYYMMDD形式に変換
            string fuelDay = dateTimePicker.Value.ToString("yyyyMMdd");
            ///給油時総走行距離	「給油時総走行距離」の値
            double d1 = double.Parse(txtCurrentMileage.Text);
            ///区間走行距離 「区間走行距離」の値
            double d2 = double.Parse(txtThisMileage.Text);
            ///区間燃費 「区間燃費」の値
            double d3 = double.Parse(txtFuelConsumption.Text);


            //燃費記録用テーブルがなければ、燃費記録用テーブルを作成する
            if (System.IO.File.Exists(db_file) == false)
            {
                using (SQLiteConnection nenpiData = new SQLiteConnection("Data Source=" + db_file))
                {
                    nenpiData.Open();
                    using (SQLiteCommand command = nenpiData.CreateCommand())
                    {
                        command.CommandText = "CREATE TABLE t_nenpi(id INTEGER  PRIMARY KEY AUTOINCREMENT, refuel_date TEXT, mileage REAL, trip_mileage REAL, fuel_cost REAL)";
                        command.ExecuteNonQuery();
                    }

                }
            }

            //データ保存
            //DBファイルの燃費記録テーブルに対してINSERT文を実行してデータを登録
            using (SQLiteConnection nenpiData = new SQLiteConnection("Data Source=" + db_file))
            {
                nenpiData.Open();

                using (var transaction = nenpiData.BeginTransaction())
                {
                    using (SQLiteCommand command = nenpiData.CreateCommand())
                    {
                        command.CommandText = "insert into t_nenpi(refuel_date, mileage, trip_mileage, fuel_cost) values (@refuel_date, @mileage, @trip_mileage, @fuel_cost)";
                        //command.Parameters.Add(new SQLiteParameter("@ID", 1));
                        command.Parameters.Add(new SQLiteParameter("@refuel_date", fuelDay));
                        command.Parameters.Add(new SQLiteParameter("@mileage", d1));
                        command.Parameters.Add(new SQLiteParameter("@trip_mileage", d2));
                        command.Parameters.Add(new SQLiteParameter("@fuel_cost", d3));

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }


            ///・記録処理完了メッセージの表示
            ///メッセージ：「記録処理が完了しました」													
            ///をダイアログに表示して処理終了
            MessageBox.Show("記録処理が完了しました", "");
            //画面をクリアする
            Clear();
        }

        /// <summary>
        /// 終了ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            //メインフォームを閉じる
            this.Close();
        }

        /// <summary>
        /// 給油時走行距離フォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCurrentMileage_Leave(object sender, EventArgs e)
        {
            //1.給油時総走行距離の入力チェックを行う																																	
            string kyuyuzitMileage = txtCurrentMileage.Text;
            double zenkaiMileage = double.Parse(txtPastMileage.Text);

            //   1 - 1.未入力チェック
            //   ・未入力の場合：以下の処理を実行して処理終了
            if (!string.IsNullOrWhiteSpace(kyuyuzitMileage))
            {
                //nullではなく、かつ空文字列でもなく、かつ空白文字列でもない
                //	・入力がある場合：1 - 2へ
            }
            else
            {
                //「区間距離」テキストボックスに空白を設定
                //「計算」ボタンをクリック不可状態にする
                txtThisMileage.Text = "";
                btnCalculation.Enabled = false;
                return;
            }
            // 0以上の数値、整合性チェック
            string messagekyuyuzi = CheckCurrentMileage(kyuyuzitMileage, zenkaiMileage);

            if (!string.IsNullOrWhiteSpace(messagekyuyuzi))
            {
                //エラーだと給油時走行距離にフォーカスを設定して空白セット、
                //区間距離空白と計算ボタン不可設定してメッセージ表示
                txtThisMileage.Text = "";
                btnCalculation.Enabled = false;
                this.ActiveControl = this.txtCurrentMileage;
                txtCurrentMileage.Text = "";
                // フォーカスイベントなのでメッセージボックスを最後に配置
                MessageBox.Show(messagekyuyuzi);

                return;
            }

            //区間距離を算出して「区間距離」テキストボックスに設定
            double kyuyuzidouble = double.Parse(kyuyuzitMileage);
            double kukankyori = KukanCul(kyuyuzidouble, zenkaiMileage);
            txtThisMileage.Text = kukankyori.ToString();
            //計算ボタンにフォーカス
            this.ActiveControl = this.btnCalculation;

            //2 - 2.計算ボタンをクリック可能にする
            btnCalculation.Enabled = true;
        }

        /// <summary>
        /// テキストボックスフォーカスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Focus(object sender, EventArgs e)
        {
            // イベント発生元(sender)をテキストボックスにキャスト
            TextBox txt = (TextBox)sender;

            // テキストボックスを全選択状態とする
            txt.SelectAll();
        }


        #endregion

        #region privateメソッド

        /// <summary>
        /// 入力値クリアメソッド
        /// </summary>
        private void Clear()
        {
            //給油日: 現在日付
            //給油量:空白
            //前回給油時総走行距離:DBに記録されている最後の給油時走行距離
            //給油時総走行距離:空白
            //区間走行距離:空白
            //区間燃費:空白
            //計算ボタン:クリック不可状態
            dateTimePicker.Value = DateTime.Now;
            boxOilingQuantity.Text = "";
            txtCurrentMileage.Text = "";
            txtThisMileage.Text = "";
            txtFuelConsumption.Text = "";
            btnCalculation.Enabled = false;
            //計算時に変更不可にした給油日、給油量、給油時走行距離を入力可に戻す
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
            //　「前回給油時総走行距離取得メソッド」を実行
            //    前回給油時総走行距離取得メソッド
            //    引数１：なし
            //    戻り値：前回給油時総走行距離(double)																															
            //  ・内蔵DB(SQLite)のテーブル「t_nenpi」から以下の条件でレコード抽出
            //	・条件：給油日が直近(一番最近)
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
            ///戻り値：区間燃費 (double) ※30.5のように小数値で返す ※小数点第二位で四捨五入する
            double nenpiDouble = valThisMileage / oilingdouble;
            Console.WriteLine(nenpiDouble);
            return Math.Round(nenpiDouble, 1, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 給油時走行距離入力チェックメソッド
        /// </summary>
        /// <param name="kyuyuzitMileage">給油時総走行距離</param>
        /// <param name="zenkaiMileage">前回給油時総走行距離</param>
        /// <returns>メッセージ</returns>
        private string CheckCurrentMileage(string kyuyuzitMileage, double zenkaiMileage)
        {
            //   1 - 2.正の数値チェック
            //	「給油時総走行距離」がゼロ以下のの数値以外の場合
            //　※数値以外の文字、マイナスの数値をエラーにする
            double kyuyuzitMileagenumber;
            bool canConvert = double.TryParse(kyuyuzitMileage, out kyuyuzitMileagenumber);
            if (!canConvert || kyuyuzitMileagenumber <= 0)
            {
                return "給油走行距離は0より大きい数値で入力してください";
            }

            ////   1 - 3.整合性チェック
            ////　「給油時総走行距離」＜=「前回給油時総走行距離」の場合
            if (kyuyuzitMileagenumber <= zenkaiMileage)
            {
                return "給油時総走行距離は前回の距離より大きな値で入力してください";
            }
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
            // 区間距離を計算して小数点第二位で四捨五入する
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
                return "給油量を入力してください";
            }

            //数値かどうかチェック
            ///1-2.正の数値チェック
            ///「給油量」が0以下の数値以外の場合エラーにする
            double oilingNumber;
            bool canConvert = double.TryParse(oiling, out oilingNumber);

            if (!canConvert || oilingNumber <= 0)
            {
                return "給油量は0より大きい数値で入力してください";
            }
            return "";
        }


        #endregion

    }
}
