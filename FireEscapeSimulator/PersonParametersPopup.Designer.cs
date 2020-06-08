namespace FireEscapeSimulator {
    partial class PersonParametersPopup {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.fireSpreadRateLabel = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.healthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.speedUpDown = new System.Windows.Forms.NumericUpDown();
            this.panicUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.healthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panicUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // fireSpreadRateLabel
            // 
            this.fireSpreadRateLabel.AutoSize = true;
            this.fireSpreadRateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fireSpreadRateLabel.Location = new System.Drawing.Point(37, 12);
            this.fireSpreadRateLabel.Name = "fireSpreadRateLabel";
            this.fireSpreadRateLabel.Size = new System.Drawing.Size(64, 24);
            this.fireSpreadRateLabel.TabIndex = 1;
            this.fireSpreadRateLabel.Text = "Health";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(70, 89);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(142, 31);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "Confirm changes";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // healthUpDown
            // 
            this.healthUpDown.Location = new System.Drawing.Point(165, 12);
            this.healthUpDown.Name = "healthUpDown";
            this.healthUpDown.Size = new System.Drawing.Size(92, 20);
            this.healthUpDown.TabIndex = 4;
            this.healthUpDown.ValueChanged += new System.EventHandler(this.healthUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Panic";
            // 
            // speedUpDown
            // 
            this.speedUpDown.Location = new System.Drawing.Point(165, 38);
            this.speedUpDown.Name = "speedUpDown";
            this.speedUpDown.Size = new System.Drawing.Size(92, 20);
            this.speedUpDown.TabIndex = 7;
            this.speedUpDown.ValueChanged += new System.EventHandler(this.speedUpDown_ValueChanged);
            // 
            // panicUpDown
            // 
            this.panicUpDown.Location = new System.Drawing.Point(165, 62);
            this.panicUpDown.Name = "panicUpDown";
            this.panicUpDown.Size = new System.Drawing.Size(92, 20);
            this.panicUpDown.TabIndex = 8;
            this.panicUpDown.ValueChanged += new System.EventHandler(this.panicUpDown_ValueChanged);
            // 
            // PersonParametersPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 132);
            this.Controls.Add(this.panicUpDown);
            this.Controls.Add(this.speedUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.healthUpDown);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.fireSpreadRateLabel);
            this.Name = "PersonParametersPopup";
            this.Text = "Person Parameters";
            ((System.ComponentModel.ISupportInitialize)(this.healthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panicUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fireSpreadRateLabel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.NumericUpDown healthUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown speedUpDown;
        private System.Windows.Forms.NumericUpDown panicUpDown;
    }
}