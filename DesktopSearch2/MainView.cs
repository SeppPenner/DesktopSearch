using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using IWshRuntimeLibrary;
using Languages.Implementation;
using Languages.Interfaces;
using File = System.IO.File;

namespace DesktopSearch
{
    public partial class MainView : Form
    {
        private readonly BackgroundWorker _backgroundworker = new BackgroundWorker();
        private readonly Config _config;
        private readonly ILanguageManager _lm = new LanguageManager();
        private int _counter;
        private ImageList _il = new ImageList();
        private ILanguage _lang;

        public MainView()
        {
            InitializeComponent();
            InitializeBakgroundWorker();
            InitializeLanguageManager();
            LoadLanguagesToCombo();
            var location = Assembly.GetExecutingAssembly().Location;
            if (location != null)
                _config = InitConfiguration(Path.Combine(Directory.GetParent(location).FullName, "Config.xml"));
            InitViewType();
            textBox_Search.Focus();
        }

        private void InitializeBakgroundWorker()
        {
            _backgroundworker.WorkerSupportsCancellation = true;
            _backgroundworker.WorkerReportsProgress = true;
            _backgroundworker.DoWork += Backgroundworker_DoWork;
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName + @" " + Application.ProductVersion;
        }

        private void InitializeLanguageManager()
        {
            _lm.SetCurrentLanguage("de-DE");
            _lm.OnLanguageChanged += OnLanguageChanged;
        }

        private void LoadLanguagesToCombo()
        {
            foreach (var lang in _lm.GetLanguages())
                comboBox_Language.Items.Add(lang.Name);
            comboBox_Language.SelectedIndex = 0;
        }

        private void comboBox_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lm.SetCurrentLanguageFromName(comboBox_Language.SelectedItem.ToString());
        }

        private void OnLanguageChanged(object sender, EventArgs eventArgs)
        {
            _lang = _lm.GetCurrentLanguage();
            button_Search.Text = _lang.GetWord("Search");
        }

        private void InitViewType()
        {
            try
            {
                var index = comboBox_View.FindString(_config.ItemView);
                comboBox_View.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                comboBox_View.SelectedIndex = 0;
            }
        }

        private Config InitConfiguration(string filename)
        {
            try
            {
                var xDocument = XDocument.Load(filename);
                return CreateObjectsFromString<Config>(xDocument);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private T CreateObjectsFromString<T>(XDocument xDocument)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T) xmlSerializer.Deserialize(new StringReader(xDocument.ToString()));
        }

        private void Backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            if (worker != null && worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                ClearListViewItems(listView_Search);
                _counter = 0;
                _il = new ImageList();
                SearchDirectory(Path.Combine(GetCurrentUserPath(), "Desktop"), textBox_Search.Text);
                SetButtonEnable(button_Search, true);
            }
        }

        private void TextBox_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Button_Search_Click(sender, e);
        }

        private void ComboBox_View_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView_Search.View = (View) Enum.Parse(typeof(View), comboBox_View.SelectedItem.ToString());
        }

        private void ListView_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var lvic = listView_Search.SelectedItems;
                foreach (ListViewItem lvi in lvic)
                    if (lvi.Name.EndsWith(".lnk"))
                    {
                        IWshShell shell = new WshShell();
                        var shortcut = (IWshShortcut) shell.CreateShortcut(lvi.Tag.ToString());
                        if (shortcut.TargetPath.Contains("."))
                            if (File.Exists(shortcut.TargetPath))
                            {
                                Process.Start(shortcut.TargetPath);
                                break;
                            }
                            else
                            {
                                var tempTargetPath = shortcut.TargetPath.Replace(" (x86)", "");
                                if (File.Exists(tempTargetPath))
                                {
                                    Process.Start(tempTargetPath);
                                    break;
                                }
                                throw new FileNotFoundException(".lnk referres to an invalid file");
                            }
                        //Directory
                        if (Directory.Exists(shortcut.TargetPath))
                        {
                            Process.Start(shortcut.TargetPath);
                            break;
                        }
                        throw new DirectoryNotFoundException(".lnk referres to an invalid directory");
                    }
                    else
                    {
                        Process.Start(lvi.Name);
                        break;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Message);
            }
        }

        private void Button_Search_Click(object sender, EventArgs e)
        {
            if (textBox_Search.Text.Equals("")) return;
            button_Search.Enabled = false;
            try
            {
                _backgroundworker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Message);
            }
        }

        private void SetButtonEnable(Button button, bool value)
        {
            Action action = () => button.Enabled = value;
            button.Invoke(action);
        }

        private Bitmap FromIconToBitmap(Icon icon)
        {
            var bmp = new Bitmap(icon.Width, icon.Height);
            using (var gp = Graphics.FromImage(bmp))
            {
                gp.Clear(Color.Transparent);
                gp.DrawIcon(icon, new Rectangle(0, 0, icon.Width, icon.Height));
            }
            return bmp;
        }

        private void SearchDirectory(string directoryString, string search)
        {
            try
            {
                var directory = new DirectoryInfo(directoryString);
                foreach (var file in directory.GetFiles())
                {
                    if (!file.Name.ToLower().Contains(search.ToLower())) continue;
                    var lvi = new ListViewItem(file.Name);
                    var lvsi = new ListViewItem.ListViewSubItem(lvi, file.Name);
                    lvi.SubItems.Add(lvsi);
                    lvsi = new ListViewItem.ListViewSubItem(lvi, file.CreationTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                    lvi.SubItems.Add(lvsi);
                    lvsi = new ListViewItem.ListViewSubItem(lvi, file.LastAccessTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                    lvi.SubItems.Add(lvsi);
                    lvsi = new ListViewItem.ListViewSubItem(lvi, file.LastWriteTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                    lvi.SubItems.Add(lvsi);
                    lvsi = new ListViewItem.ListViewSubItem(lvi, file.DirectoryName);
                    lvi.SubItems.Add(lvsi);
                    lvsi = new ListViewItem.ListViewSubItem(lvi, file.Extension);
                    lvi.SubItems.Add(lvsi);
                    lvsi = new ListViewItem.ListViewSubItem(lvi, file.FullName);
                    lvi.SubItems.Add(lvsi);
                    lvsi = new ListViewItem.ListViewSubItem(lvi, file.Length.ToString());
                    lvi.SubItems.Add(lvsi);

                    if (file.FullName.EndsWith(".lnk"))
                    {
                        IWshShell shell = new WshShell();
                        var shortcut = (IWshShortcut) shell.CreateShortcut(file.FullName);
                        if (shortcut.TargetPath.Contains("."))
                        {
                            //File
                            if (File.Exists(shortcut.TargetPath))
                            {
                                AddImageToImageList(listView_Search, _il,
                                    FromIconToBitmap(Icon.ExtractAssociatedIcon(shortcut.TargetPath)));
                            }
                            else
                            {
                                var tempTargetPath = shortcut.TargetPath.Replace(" (x86)", "");
                                if (File.Exists(tempTargetPath))
                                    AddImageToImageList(listView_Search, _il,
                                        FromIconToBitmap(Icon.ExtractAssociatedIcon(tempTargetPath)));
                                else
                                    throw new FileNotFoundException(".lnk referres to an invalid file");
                            }
                        }
                        else
                        {
                            //Directory
                            if (Directory.Exists(shortcut.TargetPath))
                                AddImageToImageList(listView_Search, _il,
                                    FromIconToBitmap(
                                        Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.BaseDirectory + "Folder.ico")));
                            else
                                throw new DirectoryNotFoundException(".lnk referres to an invalid directory");
                        }
                    }
                    else
                    {
                        AddImageToImageList(listView_Search, _il,
                            FromIconToBitmap(Icon.ExtractAssociatedIcon(file.FullName)));
                    }
                    lvi.Tag = file.FullName;
                    lvi.ImageIndex = _counter;
                    lvi.Name = file.FullName;
                    _counter++;
                    AddItemToListView(listView_Search, lvi);
                    SetImageLists(listView_Search, _il);
                }

                foreach (var folder in directory.GetDirectories())
                {
                    if (folder.Name.ToLower().Contains(search.ToLower()))
                    {
                        var lvi = new ListViewItem(folder.Name);
                        var lvsi = new ListViewItem.ListViewSubItem(lvi, folder.Name);
                        lvi.SubItems.Add(lvsi);
                        lvsi = new ListViewItem.ListViewSubItem(lvi,
                            folder.CreationTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                        lvi.SubItems.Add(lvsi);
                        lvsi = new ListViewItem.ListViewSubItem(lvi,
                            folder.LastAccessTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                        lvi.SubItems.Add(lvsi);
                        lvsi = new ListViewItem.ListViewSubItem(lvi,
                            folder.LastWriteTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                        lvi.SubItems.Add(lvsi);
                        lvsi = new ListViewItem.ListViewSubItem(lvi, folder.Extension);
                        lvi.SubItems.Add(lvsi);
                        lvsi = new ListViewItem.ListViewSubItem(lvi, folder.FullName);
                        lvi.SubItems.Add(lvsi);
                        AddImageToImageList(listView_Search, _il,
                            FromIconToBitmap(
                                Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.BaseDirectory + "Folder.ico")));
                        lvi.Tag = folder.FullName;
                        lvi.ImageIndex = _counter;
                        lvi.Name = folder.FullName;
                        _counter++;
                        AddItemToListView(listView_Search, lvi);
                        SetImageLists(listView_Search, _il);
                    }
                    SearchDirectory(folder.FullName, search);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Message);
            }
        }

        private void SetImageLists(ListView lv, ImageList il)
        {
            SetLargeImageList(lv, il);
            SetSmallImageList(lv, il);
        }

        private void ClearListViewItems(ListView lv)
        {
            Action action = () => lv.Items.Clear();
            lv.Invoke(action);
        }

        private void AddImageToImageList(ListView lv, ImageList il, Bitmap bm)
        {
            Action action = () => il.Images.Add(bm);
            lv.Invoke(action);
        }

        private void SetLargeImageList(ListView lv, ImageList il)
        {
            Action action = () => lv.LargeImageList = il;
            lv.Invoke(action);
        }

        private void SetSmallImageList(ListView lv, ImageList il)
        {
            Action action = () => lv.SmallImageList = il;
            lv.Invoke(action);
        }

        private void AddItemToListView(ListView lv, ListViewItem lvi)
        {
            Action action = () => lv.Items.Add(lvi);
            lv.Invoke(action);
        }

        private string GetCurrentUserPath()
        {
            var path =
                Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
                path = Directory.GetParent(path).ToString();
            return path;
        }
    }
}