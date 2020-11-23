// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainView.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopSearch2
{
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

    /// <summary>
    /// The main view.
    /// </summary>
    public partial class MainView : Form
    {
        /// <summary>
        /// The background worker.
        /// </summary>
        private readonly BackgroundWorker backgroundWorker = new BackgroundWorker();

        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly Config configuration;

        /// <summary>
        /// The language manager.
        /// </summary>
        private readonly ILanguageManager languageManager = new LanguageManager();

        /// <summary>
        /// The counter.
        /// </summary>
        private int counter;

        /// <summary>
        /// The image list.
        /// </summary>
        private ImageList imageList = new ImageList();

        /// <summary>
        /// The language.
        /// </summary>
        private ILanguage language;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            this.InitializeComponent();
            this.InitializeBackgroundWorker();
            this.InitializeLanguageManager();
            this.LoadLanguagesToCombo();
            var location = Assembly.GetExecutingAssembly().Location;
            this.configuration = InitConfiguration(Path.Combine(Directory.GetParent(location)?.FullName ?? string.Empty, "Config.xml"));
            this.InitViewType();
            this.textBoxSearch.Focus();
        }

        /// <summary>
        /// Enables or disables a button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="value">The value.</param>
        private static void SetButtonEnable(Control button, bool value)
        {
            // ReSharper disable once ConvertToLocalFunction
            Action action = () => button.Enabled = value;
            button.Invoke(action);
        }

        /// <summary>
        /// Converts the icon to a bitmap.
        /// </summary>
        /// <param name="icon">The icon.</param>
        /// <returns>The new <see cref="Bitmap"/> image.</returns>
        private static Bitmap FromIconToBitmap(Icon icon)
        {
            var bitmap = new Bitmap(icon.Width, icon.Height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.DrawIcon(icon, new Rectangle(0, 0, icon.Width, icon.Height));
            return bitmap;
        }

        /// <summary>
        /// Sets the image list to a list view.
        /// </summary>
        /// <param name="listView">The list view.</param>
        /// <param name="imageList">The image list.</param>
        private static void SetImageLists(ListView listView, ImageList imageList)
        {
            SetLargeImageList(listView, imageList);
            SetSmallImageList(listView, imageList);
        }

        /// <summary>
        /// Clears the list view items.
        /// </summary>
        /// <param name="listView">The list view.</param>
        private static void ClearListViewItems(ListView listView)
        {
            // ReSharper disable once ConvertToLocalFunction
            Action action = () => listView.Items.Clear();
            listView.Invoke(action);
        }

        /// <summary>
        /// Adds an image to the image list.
        /// </summary>
        /// <param name="listView">The list view.</param>
        /// <param name="imageList">The image list.</param>
        /// <param name="bitmap">The bitmap image.</param>
        private static void AddImageToImageList(Control listView, ImageList imageList, Image bitmap)
        {
            // ReSharper disable once ConvertToLocalFunction
            Action action = () => imageList.Images.Add(bitmap);
            listView.Invoke(action);
        }

        /// <summary>
        /// Sets the large image list.
        /// </summary>
        /// <param name="listView">The list view.</param>
        /// <param name="imageList">The image list.</param>
        private static void SetLargeImageList(ListView listView, ImageList imageList)
        {
            // ReSharper disable once ConvertToLocalFunction
            Action action = () => listView.LargeImageList = imageList;
            listView.Invoke(action);
        }

        /// <summary>
        /// Sets the small image list.
        /// </summary>
        /// <param name="listView">The list view.</param>
        /// <param name="imageList">The image list.</param>
        private static void SetSmallImageList(ListView listView, ImageList imageList)
        {
            // ReSharper disable once ConvertToLocalFunction
            Action action = () => listView.SmallImageList = imageList;
            listView.Invoke(action);
        }

        /// <summary>
        /// Adds the items to the list view.
        /// </summary>
        /// <param name="listView">The list view.</param>
        /// <param name="imageList">The image list.</param>
        private static void AddItemToListView(ListView listView, ListViewItem imageList)
        {
            // ReSharper disable once ConvertToLocalFunction
            Action action = () => listView.Items.Add(imageList);
            listView.Invoke(action);
        }

        /// <summary>
        /// Gets the current user's path.
        /// </summary>
        /// <returns>The current user's path as <see cref="string"/>.</returns>
        private static string GetCurrentUserPath()
        {
            var path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))?.FullName;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                path = Directory.GetParent(path ?? string.Empty)?.ToString();
            }

            return path;
        }

        /// <summary>
        /// Initializes the configuration.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>The read <see cref="Config"/>.</returns>
        private static Config InitConfiguration(string fileName)
        {
            try
            {
                var xDocument = XDocument.Load(fileName);
                return CreateObjectsFromString<Config>(xDocument);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Creates an <see cref="object"/> from a <see cref="string"/>.
        /// </summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="xDocument">The X document.</param>
        /// <returns>An object of type <see cref="T"/>.</returns>
        private static T CreateObjectsFromString<T>(XDocument xDocument)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(new StringReader(xDocument.ToString()));
        }

        /// <summary>
        /// Initializes the background worker.
        /// </summary>
        private void InitializeBackgroundWorker()
        {
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += this.BackgroundWorkerDoWork;
        }

        /// <summary>
        /// Handles the main view load event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void MainViewLoad(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + @" " + Application.ProductVersion;
        }

        /// <summary>
        /// Initializes the language manager.
        /// </summary>
        private void InitializeLanguageManager()
        {
            this.languageManager.SetCurrentLanguage("de-DE");
            this.languageManager.OnLanguageChanged += this.OnLanguageChanged;
        }

        /// <summary>
        /// Loads the languages to the combo box.
        /// </summary>
        private void LoadLanguagesToCombo()
        {
            foreach (var languageLocal in this.languageManager.GetLanguages())
            {
                this.comboBoxLanguage.Items.Add(languageLocal.Name);
            }

            this.comboBoxLanguage.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the selected index changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ComboBoxLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            this.languageManager.SetCurrentLanguageFromName(this.comboBoxLanguage.SelectedItem.ToString());
        }

        /// <summary>
        /// Handles the language changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnLanguageChanged(object sender, EventArgs e)
        {
            this.language = this.languageManager.GetCurrentLanguage();
            this.buttonSearch.Text = this.language.GetWord("Search");
        }

        /// <summary>
        /// Initializes the view type.
        /// </summary>
        private void InitViewType()
        {
            try
            {
                var index = this.comboBoxView.FindString(this.configuration.ItemView);
                this.comboBoxView.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.comboBoxView.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Runs the process on the background worker.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if (sender is BackgroundWorker worker && worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                ClearListViewItems(this.listViewSearch);
                this.counter = 0;
                this.imageList = new ImageList();
                this.SearchDirectory(Path.Combine(GetCurrentUserPath(), "Desktop"), this.textBoxSearch.Text);
                SetButtonEnable(this.buttonSearch, true);
            }
        }

        /// <summary>
        /// Handles the search key down event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TextBoxSearchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ButtonSearchClick(sender, e);
            }
        }

        /// <summary>
        /// Handles the selected index changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ComboBoxViewSelectedIndexChanged(object sender, EventArgs e)
        {
            this.listViewSearch.View = (View)Enum.Parse(typeof(View), this.comboBoxView.SelectedItem.ToString() ?? string.Empty);
        }

        /// <summary>
        /// Handles the selected index changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ListViewSearchSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = this.listViewSearch.SelectedItems;

                foreach (ListViewItem listViewItem in selectedItems)
                {
                    var process = new Process { StartInfo = { FileName = "explorer" } };

                    if (listViewItem.Name.EndsWith(".lnk"))
                    {
                        IWshShell shell = new WshShell();
                        var shortcut = (IWshShortcut)shell.CreateShortcut(listViewItem.Tag.ToString());
                        if (shortcut.TargetPath.Contains("."))
                        {
                            if (File.Exists(shortcut.TargetPath))
                            {
                                process.StartInfo.Arguments = $"\"{shortcut.TargetPath}\"";
                                process.Start();
                                break;
                            }

                            var tempTargetPath = shortcut.TargetPath.Replace(" (x86)", string.Empty);

                            if (!File.Exists(tempTargetPath))
                            {
                                throw new FileNotFoundException(".lnk refers to an invalid file");
                            }

                            process.StartInfo.Arguments = $"\"{tempTargetPath}\"";
                            process.Start();
                            break;
                        }

                        // Directory
                        if (!Directory.Exists(shortcut.TargetPath))
                        {
                            throw new DirectoryNotFoundException(".lnk refers to an invalid directory");
                        }

                        process.StartInfo.Arguments = $"\"{shortcut.TargetPath}\"";
                        process.Start();
                        break;
                    }

                    process.StartInfo.Arguments = $"\"{listViewItem.Name}\"";
                    process.Start();
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Message);
            }
        }

        /// <summary>
        /// Handles the search click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ButtonSearchClick(object sender, EventArgs e)
        {
            if (this.textBoxSearch.Text.Equals(string.Empty))
            {
                return;
            }

            this.buttonSearch.Enabled = false;
            
            try
            {
                this.backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Message);
            }
        }

        /// <summary>
        /// Searches the directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="search">The search.</param>
        private void SearchDirectory(string directoryPath, string search)
        {
            try
            {
                var directory = new DirectoryInfo(directoryPath);
                foreach (var file in directory.GetFiles("*", SearchOption.AllDirectories))
                {
                    if (!file.Name.ToLower().Contains(search.ToLower()))
                    {
                        continue;
                    }

                    var item = new ListViewItem(file.Name);
                    var subItem = new ListViewItem.ListViewSubItem(item, file.Name);
                    item.SubItems.Add(subItem);
                    subItem = new ListViewItem.ListViewSubItem(item, file.CreationTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                    item.SubItems.Add(subItem);
                    subItem = new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                    item.SubItems.Add(subItem);
                    subItem = new ListViewItem.ListViewSubItem(item, file.LastWriteTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                    item.SubItems.Add(subItem);
                    subItem = new ListViewItem.ListViewSubItem(item, file.DirectoryName);
                    item.SubItems.Add(subItem);
                    subItem = new ListViewItem.ListViewSubItem(item, file.Extension);
                    item.SubItems.Add(subItem);
                    subItem = new ListViewItem.ListViewSubItem(item, file.FullName);
                    item.SubItems.Add(subItem);
                    subItem = new ListViewItem.ListViewSubItem(item, file.Length.ToString());
                    item.SubItems.Add(subItem);

                    if (file.FullName.EndsWith(".lnk"))
                    {
                        IWshShell shell = new WshShell();
                        var shortcut = (IWshShortcut)shell.CreateShortcut(file.FullName);

                        if (shortcut.TargetPath.Contains("."))
                        {
                            // File
                            if (File.Exists(shortcut.TargetPath))
                            {
                                AddImageToImageList(
                                    this.listViewSearch,
                                    this.imageList,
                                    FromIconToBitmap(Icon.ExtractAssociatedIcon(shortcut.TargetPath)));
                            }
                            else
                            {
                                var tempTargetPath = shortcut.TargetPath.Replace(" (x86)", string.Empty);
                                if (File.Exists(tempTargetPath))
                                {
                                    AddImageToImageList(
                                        this.listViewSearch,
                                        this.imageList,
                                        FromIconToBitmap(Icon.ExtractAssociatedIcon(tempTargetPath)));
                                }
                                else
                                {
                                    throw new FileNotFoundException(".lnk refers to an invalid file");
                                }
                            }
                        }
                        else
                        {
                            // Directory
                            if (Directory.Exists(shortcut.TargetPath))
                            {
                                AddImageToImageList(this.listViewSearch, this.imageList, FromIconToBitmap(Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.BaseDirectory + "Folder.ico")));
                            }
                            else
                            {
                                throw new DirectoryNotFoundException(".lnk refers to an invalid directory");
                            }
                        }
                    }
                    else
                    {
                        AddImageToImageList(
                            this.listViewSearch,
                            this.imageList,
                            FromIconToBitmap(Icon.ExtractAssociatedIcon(file.FullName)));
                    }

                    item.Tag = file.FullName;
                    item.ImageIndex = this.counter;
                    item.Name = file.FullName;
                    this.counter++;
                    AddItemToListView(this.listViewSearch, item);
                    SetImageLists(this.listViewSearch, this.imageList);
                }

                foreach (var folder in directory.GetDirectories())
                {
                    if (folder.Name.ToLower().Contains(search.ToLower()))
                    {
                        var item = new ListViewItem(folder.Name);
                        var subItem = new ListViewItem.ListViewSubItem(item, folder.Name);
                        item.SubItems.Add(subItem);
                        subItem = new ListViewItem.ListViewSubItem(item, folder.CreationTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                        item.SubItems.Add(subItem);
                        subItem = new ListViewItem.ListViewSubItem(item, folder.LastAccessTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                        item.SubItems.Add(subItem);
                        subItem = new ListViewItem.ListViewSubItem(item, folder.LastWriteTime.ToString("dd.MMM.yyyy-hh:mm:ss"));
                        item.SubItems.Add(subItem);
                        subItem = new ListViewItem.ListViewSubItem(item, folder.Extension);
                        item.SubItems.Add(subItem);
                        subItem = new ListViewItem.ListViewSubItem(item, folder.FullName);
                        item.SubItems.Add(subItem);
                        AddImageToImageList(this.listViewSearch, this.imageList, FromIconToBitmap(Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.BaseDirectory + "Folder.ico")));
                        item.Tag = folder.FullName;
                        item.ImageIndex = this.counter;
                        item.Name = folder.FullName;
                        this.counter++;
                        AddItemToListView(this.listViewSearch, item);
                        SetImageLists(this.listViewSearch, this.imageList);
                    }

                    this.SearchDirectory(folder.FullName, search);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Message);
            }
        }
    }
}