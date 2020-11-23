namespace DesktopSearch2
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
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.listViewSearch = new System.Windows.Forms.ListView();
            this.comboBoxView = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelSmall = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelSmall.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_Search
            // 
            this.textBoxSearch.AcceptsReturn = true;
            this.textBoxSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxSearch.Location = new System.Drawing.Point(3, 3);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(563, 20);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxSearchKeyDown);
            // 
            // listViewSearch
            // 
            this.listViewSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSearch.Location = new System.Drawing.Point(3, 33);
            this.listViewSearch.MultiSelect = false;
            this.listViewSearch.Name = "listViewSearch";
            this.listViewSearch.Size = new System.Drawing.Size(958, 375);
            this.listViewSearch.TabIndex = 2;
            this.listViewSearch.UseCompatibleStateImageBehavior = false;
            this.listViewSearch.SelectedIndexChanged += this.ListViewSearchSelectedIndexChanged;
            // 
            // comboBox_View
            // 
            this.comboBoxView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxView.FormattingEnabled = true;
            this.comboBoxView.Items.AddRange(new object[] {
            "LargeIcon",
            "List",
            "SmallIcon",
            "Tile"});
            this.comboBoxView.Location = new System.Drawing.Point(699, 3);
            this.comboBoxView.Name = "comboBoxView";
            this.comboBoxView.Size = new System.Drawing.Size(121, 21);
            this.comboBoxView.Sorted = true;
            this.comboBoxView.TabIndex = 3;
            this.comboBoxView.SelectedIndexChanged += new System.EventHandler(this.ComboBoxViewSelectedIndexChanged);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelSmall, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.listViewSearch, 0, 1);
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
            this.tableLayoutPanelSmall.Controls.Add(this.textBoxSearch, 0, 0);
            this.tableLayoutPanelSmall.Controls.Add(this.comboBoxView, 2, 0);
            this.tableLayoutPanelSmall.Controls.Add(this.buttonSearch, 1, 0);
            this.tableLayoutPanelSmall.Controls.Add(this.comboBoxLanguage, 3, 0);
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
            this.buttonSearch.Location = new System.Drawing.Point(572, 2);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(121, 23);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearchClick);
            // 
            // comboBox_Language
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(826, 3);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(135, 21);
            this.comboBoxLanguage.TabIndex = 4;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLanguageSelectedIndexChanged);
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
            this.Load += new System.EventHandler(this.MainViewLoad);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelSmall.ResumeLayout(false);
            this.tableLayoutPanelSmall.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ListView listViewSearch;
        private System.Windows.Forms.ComboBox comboBoxView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSmall;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
    }
}

