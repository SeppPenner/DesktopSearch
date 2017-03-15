namespace DesktopSearch
{
    partial class MainView
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.textBox_Search = new System.Windows.Forms.TextBox();
            this.listView_Search = new System.Windows.Forms.ListView();
            this.comboBox_View = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelSmall = new System.Windows.Forms.TableLayoutPanel();
            this.button_Search = new System.Windows.Forms.Button();
            this.comboBox_Language = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelSmall.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_Search
            // 
            this.textBox_Search.AcceptsReturn = true;
            this.textBox_Search.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_Search.Location = new System.Drawing.Point(3, 3);
            this.textBox_Search.Name = "textBox_Search";
            this.textBox_Search.Size = new System.Drawing.Size(563, 20);
            this.textBox_Search.TabIndex = 0;
            this.textBox_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Search_KeyDown);
            // 
            // listView_Search
            // 
            this.listView_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Search.Location = new System.Drawing.Point(3, 33);
            this.listView_Search.MultiSelect = false;
            this.listView_Search.Name = "listView_Search";
            this.listView_Search.Size = new System.Drawing.Size(958, 375);
            this.listView_Search.TabIndex = 2;
            this.listView_Search.UseCompatibleStateImageBehavior = false;
            this.listView_Search.SelectedIndexChanged += new System.EventHandler(this.ListView_Search_SelectedIndexChanged);
            // 
            // comboBox_View
            // 
            this.comboBox_View.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_View.FormattingEnabled = true;
            this.comboBox_View.Items.AddRange(new object[] {
            "LargeIcon",
            "List",
            "SmallIcon",
            "Tile"});
            this.comboBox_View.Location = new System.Drawing.Point(699, 3);
            this.comboBox_View.Name = "comboBox_View";
            this.comboBox_View.Size = new System.Drawing.Size(121, 21);
            this.comboBox_View.Sorted = true;
            this.comboBox_View.TabIndex = 3;
            this.comboBox_View.SelectedIndexChanged += new System.EventHandler(this.ComboBox_View_SelectedIndexChanged);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelSmall, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.listView_Search, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(964, 411);
            this.tableLayoutPanelMain.TabIndex = 4;
            // 
            // tableLayoutPanelSmall
            // 
            this.tableLayoutPanelSmall.ColumnCount = 4;
            this.tableLayoutPanelSmall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSmall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelSmall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelSmall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelSmall.Controls.Add(this.textBox_Search, 0, 0);
            this.tableLayoutPanelSmall.Controls.Add(this.comboBox_View, 2, 0);
            this.tableLayoutPanelSmall.Controls.Add(this.button_Search, 1, 0);
            this.tableLayoutPanelSmall.Controls.Add(this.comboBox_Language, 3, 0);
            this.tableLayoutPanelSmall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSmall.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelSmall.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelSmall.Name = "tableLayoutPanelSmall";
            this.tableLayoutPanelSmall.RowCount = 1;
            this.tableLayoutPanelSmall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSmall.Size = new System.Drawing.Size(964, 30);
            this.tableLayoutPanelSmall.TabIndex = 0;
            // 
            // button_Search
            // 
            this.button_Search.Location = new System.Drawing.Point(572, 2);
            this.button_Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(121, 23);
            this.button_Search.TabIndex = 1;
            this.button_Search.Text = "Search";
            this.button_Search.UseVisualStyleBackColor = true;
            this.button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // comboBox_Language
            // 
            this.comboBox_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Language.FormattingEnabled = true;
            this.comboBox_Language.Location = new System.Drawing.Point(826, 3);
            this.comboBox_Language.Name = "comboBox_Language";
            this.comboBox_Language.Size = new System.Drawing.Size(135, 21);
            this.comboBox_Language.TabIndex = 4;
            this.comboBox_Language.SelectedIndexChanged += new System.EventHandler(this.comboBox_Language_SelectedIndexChanged);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 411);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "Desktop Search";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelSmall.ResumeLayout(false);
            this.tableLayoutPanelSmall.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Search;
        private System.Windows.Forms.ListView listView_Search;
        private System.Windows.Forms.ComboBox comboBox_View;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSmall;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.ComboBox comboBox_Language;
    }
}

