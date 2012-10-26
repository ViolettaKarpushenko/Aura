using System.Windows.Forms;

namespace Aura
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Default = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.Mineral1 = new System.Windows.Forms.TabPage();
            this._dgvMineralD = new System.Windows.Forms.DataGridView();
            this.Water1 = new System.Windows.Forms.TabPage();
            this._dgvWaterD = new System.Windows.Forms.DataGridView();
            this.Land1 = new System.Windows.Forms.TabPage();
            this._dgvTerritorialD = new System.Windows.Forms.DataGridView();
            this.Biological1 = new System.Windows.Forms.TabPage();
            this._dgvBiologicalD = new System.Windows.Forms.DataGridView();
            this.Animals1 = new System.Windows.Forms.TabPage();
            this._dgvAnimalsD = new System.Windows.Forms.DataGridView();
            this.Result = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.LA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._miAddRegion = new System.Windows.Forms.ToolStripMenuItem();
            this._miMap = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.Default.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.Mineral1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvMineralD)).BeginInit();
            this.Water1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvWaterD)).BeginInit();
            this.Land1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTerritorialD)).BeginInit();
            this.Biological1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvBiologicalD)).BeginInit();
            this.Animals1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvAnimalsD)).BeginInit();
            this.Result.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Default);
            this.tabControl1.Controls.Add(this.Result);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(947, 412);
            this.tabControl1.TabIndex = 0;
            // 
            // Default
            // 
            this.Default.Controls.Add(this.tabControl2);
            this.Default.Location = new System.Drawing.Point(4, 22);
            this.Default.Name = "Default";
            this.Default.Padding = new System.Windows.Forms.Padding(3);
            this.Default.Size = new System.Drawing.Size(939, 386);
            this.Default.TabIndex = 0;
            this.Default.Text = "Исходные";
            this.Default.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.Mineral1);
            this.tabControl2.Controls.Add(this.Water1);
            this.tabControl2.Controls.Add(this.Land1);
            this.tabControl2.Controls.Add(this.Biological1);
            this.tabControl2.Controls.Add(this.Animals1);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(933, 380);
            this.tabControl2.TabIndex = 0;
            // 
            // Mineral1
            // 
            this.Mineral1.Controls.Add(this._dgvMineralD);
            this.Mineral1.Location = new System.Drawing.Point(4, 22);
            this.Mineral1.Name = "Mineral1";
            this.Mineral1.Padding = new System.Windows.Forms.Padding(3);
            this.Mineral1.Size = new System.Drawing.Size(925, 354);
            this.Mineral1.TabIndex = 0;
            this.Mineral1.Text = "Минеральные";
            this.Mineral1.UseVisualStyleBackColor = true;
            // 
            // _dgvMineralD
            // 
            this._dgvMineralD.AllowUserToAddRows = false;
            this._dgvMineralD.AllowUserToDeleteRows = false;
            this._dgvMineralD.AllowUserToOrderColumns = true;
            this._dgvMineralD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvMineralD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvMineralD.Location = new System.Drawing.Point(3, 3);
            this._dgvMineralD.Name = "_dgvMineralD";
            this._dgvMineralD.ReadOnly = true;
            this._dgvMineralD.Size = new System.Drawing.Size(919, 348);
            this._dgvMineralD.TabIndex = 0;
            // 
            // Water1
            // 
            this.Water1.Controls.Add(this._dgvWaterD);
            this.Water1.Location = new System.Drawing.Point(4, 22);
            this.Water1.Name = "Water1";
            this.Water1.Padding = new System.Windows.Forms.Padding(3);
            this.Water1.Size = new System.Drawing.Size(925, 354);
            this.Water1.TabIndex = 1;
            this.Water1.Text = "Водные";
            this.Water1.UseVisualStyleBackColor = true;
            // 
            // _dgvWaterD
            // 
            this._dgvWaterD.AllowUserToAddRows = false;
            this._dgvWaterD.AllowUserToDeleteRows = false;
            this._dgvWaterD.AllowUserToOrderColumns = true;
            this._dgvWaterD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvWaterD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvWaterD.Location = new System.Drawing.Point(3, 3);
            this._dgvWaterD.Name = "_dgvWaterD";
            this._dgvWaterD.ReadOnly = true;
            this._dgvWaterD.Size = new System.Drawing.Size(919, 348);
            this._dgvWaterD.TabIndex = 1;
            // 
            // Land1
            // 
            this.Land1.Controls.Add(this._dgvTerritorialD);
            this.Land1.Location = new System.Drawing.Point(4, 22);
            this.Land1.Name = "Land1";
            this.Land1.Padding = new System.Windows.Forms.Padding(3);
            this.Land1.Size = new System.Drawing.Size(925, 354);
            this.Land1.TabIndex = 2;
            this.Land1.Text = "Территориальные";
            this.Land1.UseVisualStyleBackColor = true;
            // 
            // _dgvTerritorialD
            // 
            this._dgvTerritorialD.AllowUserToAddRows = false;
            this._dgvTerritorialD.AllowUserToDeleteRows = false;
            this._dgvTerritorialD.AllowUserToOrderColumns = true;
            this._dgvTerritorialD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvTerritorialD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvTerritorialD.Location = new System.Drawing.Point(3, 3);
            this._dgvTerritorialD.Name = "_dgvTerritorialD";
            this._dgvTerritorialD.ReadOnly = true;
            this._dgvTerritorialD.Size = new System.Drawing.Size(919, 348);
            this._dgvTerritorialD.TabIndex = 2;
            // 
            // Biological1
            // 
            this.Biological1.Controls.Add(this._dgvBiologicalD);
            this.Biological1.Location = new System.Drawing.Point(4, 22);
            this.Biological1.Name = "Biological1";
            this.Biological1.Padding = new System.Windows.Forms.Padding(3);
            this.Biological1.Size = new System.Drawing.Size(925, 354);
            this.Biological1.TabIndex = 3;
            this.Biological1.Text = "Растительные";
            this.Biological1.UseVisualStyleBackColor = true;
            // 
            // _dgvBiologicalD
            // 
            this._dgvBiologicalD.AllowUserToAddRows = false;
            this._dgvBiologicalD.AllowUserToDeleteRows = false;
            this._dgvBiologicalD.AllowUserToOrderColumns = true;
            this._dgvBiologicalD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvBiologicalD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvBiologicalD.Location = new System.Drawing.Point(3, 3);
            this._dgvBiologicalD.Name = "_dgvBiologicalD";
            this._dgvBiologicalD.ReadOnly = true;
            this._dgvBiologicalD.Size = new System.Drawing.Size(919, 348);
            this._dgvBiologicalD.TabIndex = 1;
            // 
            // Animals1
            // 
            this.Animals1.Controls.Add(this._dgvAnimalsD);
            this.Animals1.Location = new System.Drawing.Point(4, 22);
            this.Animals1.Name = "Animals1";
            this.Animals1.Padding = new System.Windows.Forms.Padding(3);
            this.Animals1.Size = new System.Drawing.Size(925, 354);
            this.Animals1.TabIndex = 4;
            this.Animals1.Text = "Животные";
            this.Animals1.UseVisualStyleBackColor = true;
            // 
            // _dgvAnimalsD
            // 
            this._dgvAnimalsD.AllowUserToAddRows = false;
            this._dgvAnimalsD.AllowUserToDeleteRows = false;
            this._dgvAnimalsD.AllowUserToOrderColumns = true;
            this._dgvAnimalsD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvAnimalsD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvAnimalsD.Location = new System.Drawing.Point(3, 3);
            this._dgvAnimalsD.Name = "_dgvAnimalsD";
            this._dgvAnimalsD.ReadOnly = true;
            this._dgvAnimalsD.Size = new System.Drawing.Size(919, 348);
            this._dgvAnimalsD.TabIndex = 2;
            // 
            // Result
            // 
            this.Result.Controls.Add(this.dataGridView1);
            this.Result.Controls.Add(this.comboBox1);
            this.Result.Location = new System.Drawing.Point(4, 22);
            this.Result.Name = "Result";
            this.Result.Padding = new System.Windows.Forms.Padding(3);
            this.Result.Size = new System.Drawing.Size(939, 386);
            this.Result.TabIndex = 1;
            this.Result.Text = "Итоговые";
            this.Result.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 34);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(924, 344);
            this.dataGridView1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Минеральные",
            "Водные",
            "Территориальные",
            "Растительные",
            "Животные",
            "Итог"});
            this.comboBox1.Location = new System.Drawing.Point(7, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // LA
            // 
            this.LA.HeaderText = "Озёрные";
            this.LA.Name = "LA";
            this.LA.ReadOnly = true;
            // 
            // AA
            // 
            this.AA.HeaderText = "С/Х";
            this.AA.Name = "AA";
            this.AA.ReadOnly = true;
            // 
            // GA
            // 
            this.GA.HeaderText = "Земельные";
            this.GA.Name = "GA";
            this.GA.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Регион";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._miAddRegion,
            this._miMap});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(947, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // _miAddRegion
            // 
            this._miAddRegion.Name = "_miAddRegion";
            this._miAddRegion.Size = new System.Drawing.Size(102, 20);
            this._miAddRegion.Text = "Добавить район";
            this._miAddRegion.Click += new System.EventHandler(this.AddRegion);
            // 
            // _miMap
            // 
            this._miMap.Name = "_miMap";
            this._miMap.Size = new System.Drawing.Size(50, 20);
            this._miMap.Text = "Карта";
            this._miMap.Click += new System.EventHandler(this.ShowMap);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 436);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Aura 0.1";
            this.Load += new System.EventHandler(this.OnLoad);
            this.tabControl1.ResumeLayout(false);
            this.Default.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.Mineral1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgvMineralD)).EndInit();
            this.Water1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgvWaterD)).EndInit();
            this.Land1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgvTerritorialD)).EndInit();
            this.Biological1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgvBiologicalD)).EndInit();
            this.Animals1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgvAnimalsD)).EndInit();
            this.Result.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Default;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage Water1;
        private System.Windows.Forms.TabPage Result;
        private System.Windows.Forms.DataGridView _dgvWaterD;
        private System.Windows.Forms.TabPage Biological1;
        private System.Windows.Forms.DataGridView _dgvBiologicalD;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private TabPage Animals1;
        private TabPage Mineral1;
        private DataGridView _dgvMineralD;
        private TabPage Land1;
        private DataGridView _dgvTerritorialD;
        private DataGridView _dgvAnimalsD;
        private DataGridViewTextBoxColumn LA;
        private DataGridViewTextBoxColumn AA;
        private DataGridViewTextBoxColumn GA;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem _miAddRegion;
        private ToolStripMenuItem _miMap;
    }
}

