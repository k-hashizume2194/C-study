namespace NenpiApp
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.labelMainForm = new System.Windows.Forms.Label();
            this.boxOilingQuantity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPastMileage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurrentMileage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtThisMileage = new System.Windows.Forms.TextBox();
            this.btnCalculation = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFuelConsumption = new System.Windows.Forms.TextBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnClear.Location = new System.Drawing.Point(360, 61);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(108, 37);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "クリア";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "給油量：";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(207, 107);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dateTimePicker.Size = new System.Drawing.Size(134, 22);
            this.dateTimePicker.TabIndex = 2;
            // 
            // labelMainForm
            // 
            this.labelMainForm.AutoSize = true;
            this.labelMainForm.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelMainForm.Location = new System.Drawing.Point(203, 21);
            this.labelMainForm.Name = "labelMainForm";
            this.labelMainForm.Size = new System.Drawing.Size(138, 20);
            this.labelMainForm.TabIndex = 3;
            this.labelMainForm.Text = "燃費計算アプリ";
            // 
            // boxOilingQuantity
            // 
            this.boxOilingQuantity.Location = new System.Drawing.Point(207, 147);
            this.boxOilingQuantity.Name = "boxOilingQuantity";
            this.boxOilingQuantity.Size = new System.Drawing.Size(116, 22);
            this.boxOilingQuantity.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "前回給油時総走行距離：";
            // 
            // txtPastMileage
            // 
            this.txtPastMileage.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPastMileage.Location = new System.Drawing.Point(207, 187);
            this.txtPastMileage.Name = "txtPastMileage";
            this.txtPastMileage.ReadOnly = true;
            this.txtPastMileage.Size = new System.Drawing.Size(116, 22);
            this.txtPastMileage.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "給油時走行距離：";
            // 
            // txtCurrentMileage
            // 
            this.txtCurrentMileage.Location = new System.Drawing.Point(207, 227);
            this.txtCurrentMileage.Name = "txtCurrentMileage";
            this.txtCurrentMileage.Size = new System.Drawing.Size(116, 22);
            this.txtCurrentMileage.TabIndex = 8;
            this.txtCurrentMileage.Leave += new System.EventHandler(this.txtCurrentMileage_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "給油日：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(86, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "区間走行距離：";
            // 
            // txtThisMileage
            // 
            this.txtThisMileage.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtThisMileage.Location = new System.Drawing.Point(207, 267);
            this.txtThisMileage.Name = "txtThisMileage";
            this.txtThisMileage.ReadOnly = true;
            this.txtThisMileage.Size = new System.Drawing.Size(116, 22);
            this.txtThisMileage.TabIndex = 11;
            // 
            // btnCalculation
            // 
            this.btnCalculation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCalculation.Location = new System.Drawing.Point(77, 324);
            this.btnCalculation.Name = "btnCalculation";
            this.btnCalculation.Size = new System.Drawing.Size(390, 38);
            this.btnCalculation.TabIndex = 12;
            this.btnCalculation.Text = "計算";
            this.btnCalculation.UseVisualStyleBackColor = false;
            this.btnCalculation.Click += new System.EventHandler(this.btnCalculation_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 385);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "区間燃費：";
            // 
            // txtFuelConsumption
            // 
            this.txtFuelConsumption.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtFuelConsumption.Location = new System.Drawing.Point(207, 382);
            this.txtFuelConsumption.Name = "txtFuelConsumption";
            this.txtFuelConsumption.ReadOnly = true;
            this.txtFuelConsumption.Size = new System.Drawing.Size(116, 22);
            this.txtFuelConsumption.TabIndex = 14;
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.Red;
            this.btnRecord.Location = new System.Drawing.Point(65, 433);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(108, 37);
            this.btnRecord.TabIndex = 15;
            this.btnRecord.Text = "記録";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnEnd.Location = new System.Drawing.Point(360, 433);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(108, 37);
            this.btnEnd.TabIndex = 16;
            this.btnEnd.Text = "終了";
            this.btnEnd.UseVisualStyleBackColor = false;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(339, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "Ⅼ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(337, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "km";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(337, 230);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 15);
            this.label9.TabIndex = 19;
            this.label9.Text = "km";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(337, 270);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 15);
            this.label10.TabIndex = 20;
            this.label10.Text = "km";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(328, 384);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 21;
            this.label11.Text = "km/L";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 550);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelMainForm);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.txtFuelConsumption);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCalculation);
            this.Controls.Add(this.txtThisMileage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCurrentMileage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPastMileage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.boxOilingQuantity);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMainForm;
        private System.Windows.Forms.TextBox boxOilingQuantity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPastMileage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurrentMileage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtThisMileage;
        private System.Windows.Forms.Button btnCalculation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFuelConsumption;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}

