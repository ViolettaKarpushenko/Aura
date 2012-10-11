namespace Aura
{
    partial class AddRegionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._btnNext = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._tbValue = new System.Windows.Forms.TextBox();
            this._lbCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _btnNext
            // 
            this._btnNext.Location = new System.Drawing.Point(262, 39);
            this._btnNext.Name = "_btnNext";
            this._btnNext.Size = new System.Drawing.Size(75, 23);
            this._btnNext.TabIndex = 1;
            this._btnNext.Text = "Дальше";
            this._btnNext.UseVisualStyleBackColor = true;
            this._btnNext.Click += new System.EventHandler(this.OnNext);
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(181, 39);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 2;
            this._btnCancel.Text = "Отмена";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // _tbValue
            // 
            this._tbValue.Location = new System.Drawing.Point(181, 13);
            this._tbValue.Name = "_tbValue";
            this._tbValue.Size = new System.Drawing.Size(156, 20);
            this._tbValue.TabIndex = 0;
            // 
            // _lbCaption
            // 
            this._lbCaption.AutoSize = true;
            this._lbCaption.Location = new System.Drawing.Point(13, 19);
            this._lbCaption.Name = "_lbCaption";
            this._lbCaption.Size = new System.Drawing.Size(35, 13);
            this._lbCaption.TabIndex = 3;
            this._lbCaption.Text = "label1";
            // 
            // AddRegionForm
            // 
            this.AcceptButton = this._btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(349, 70);
            this.Controls.Add(this._lbCaption);
            this.Controls.Add(this._tbValue);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnNext);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRegionForm";
            this.Text = "AddRegionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnNext;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.TextBox _tbValue;
        private System.Windows.Forms.Label _lbCaption;

    }
}