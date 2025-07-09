using FrameworkTest;

namespace GraphedBrowser;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
        TabPanel = new FlowLayoutPanel();
        NewTabButton = new Button();
        MainSeachBar = new TextBox();
        HomeWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
        flowLayoutPanel1 = new Panel();
        BackButton = new Button();
        ForwardButton = new Button();
        RefreshButton = new Button();
        WebViewPanel = new GroupBox();
        TabPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)HomeWebView).BeginInit();
        flowLayoutPanel1.SuspendLayout();
        WebViewPanel.SuspendLayout();
        SuspendLayout();
        // 
        // TabPanel
        // 
        TabPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        TabPanel.BackColor = Color.FromArgb(8, 46, 59);
        TabPanel.BorderStyle = BorderStyle.FixedSingle;
        TabPanel.Controls.Add(NewTabButton);
        TabPanel.Dock = DockStyle.Top;
        TabPanel.Location = new Point(0, 0);
        TabPanel.Name = "TabPanel";
        TabPanel.Size = new Size(1082, 52);
        TabPanel.TabIndex = 0;
        // 
        // NewTabButton
        // 
        NewTabButton.BackColor = Color.DarkCyan;
        NewTabButton.FlatAppearance.BorderColor = SystemColors.HotTrack;
        NewTabButton.FlatAppearance.BorderSize = 0;
        NewTabButton.Font = new Font("Century Gothic", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
        NewTabButton.ForeColor = Color.White;
        NewTabButton.Location = new Point(6, 6);
        NewTabButton.Margin = new Padding(6);
        NewTabButton.Name = "NewTabButton";
        NewTabButton.Size = new Size(45, 40);
        NewTabButton.TabIndex = 0;
        NewTabButton.Text = "+";
        NewTabButton.UseVisualStyleBackColor = false;
        NewTabButton.Click += NewTabButton_Click;
        // 
        // MainSeachBar
        // 
        MainSeachBar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        MainSeachBar.BackColor = Color.FromArgb(20, 85, 90);
        MainSeachBar.ForeColor = SystemColors.Control;
        MainSeachBar.Location = new Point(165, 8);
        MainSeachBar.Margin = new Padding(3, 8, 3, 3);
        MainSeachBar.Name = "MainSeachBar";
        MainSeachBar.Size = new Size(911, 27);
        MainSeachBar.TabIndex = 1;
        MainSeachBar.Click += MainSeachBar_Click;
        MainSeachBar.Enter += MainSearchBar_Enter;
        MainSeachBar.PreviewKeyDown += SearchFromMainSearchBar;
        // 
        // HomeWebView
        // 
        HomeWebView.AllowExternalDrop = false;
        HomeWebView.BackColor = Color.FromArgb(8, 46, 59);
        HomeWebView.CreationProperties = null;
        HomeWebView.DefaultBackgroundColor = Color.FromArgb(192, 255, 192);
        HomeWebView.Dock = DockStyle.Fill;
        HomeWebView.Location = new Point(3, 23);
        HomeWebView.Name = "HomeWebView";
        HomeWebView.Size = new Size(1076, 542);
        HomeWebView.Source = new Uri("https://www.bing.com", UriKind.Absolute);
        HomeWebView.TabIndex = 2;
        HomeWebView.ZoomFactor = 1D;
        HomeWebView.WebMessageReceived += WebView_MessageRecieved;
        HomeWebView.SourceChanged += WebsiteChanged;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flowLayoutPanel1.BackColor = Color.FromArgb(8, 46, 59);
        flowLayoutPanel1.Controls.Add(BackButton);
        flowLayoutPanel1.Controls.Add(ForwardButton);
        flowLayoutPanel1.Controls.Add(RefreshButton);
        flowLayoutPanel1.Controls.Add(MainSeachBar);
        flowLayoutPanel1.Dock = DockStyle.Top;
        flowLayoutPanel1.Location = new Point(0, 52);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(1082, 47);
        flowLayoutPanel1.TabIndex = 3;
        // 
        // BackButton
        // 
        BackButton.ForeColor = Color.FromArgb(255, 192, 192);
        BackButton.Location = new Point(3, 3);
        BackButton.Name = "BackButton";
        BackButton.Size = new Size(48, 44);
        BackButton.TabIndex = 0;
        BackButton.UseVisualStyleBackColor = true;
        BackButton.Click += BackButton_Click;
        // 
        // ForwardButton
        // 
        ForwardButton.Location = new Point(57, 3);
        ForwardButton.Name = "ForwardButton";
        ForwardButton.Size = new Size(48, 44);
        ForwardButton.TabIndex = 0;
        ForwardButton.UseVisualStyleBackColor = true;
        ForwardButton.Click += ForwardButton_Click;
        // 
        // RefreshButton
        // 
        RefreshButton.Location = new Point(111, 3);
        RefreshButton.Name = "RefreshButton";
        RefreshButton.Size = new Size(48, 44);
        RefreshButton.TabIndex = 0;
        RefreshButton.UseVisualStyleBackColor = true;
        RefreshButton.Click += RefreshButton_Click;
        // 
        // WebViewPanel
        // 
        WebViewPanel.BackColor = Color.FromArgb(8, 46, 50);
        WebViewPanel.Controls.Add(HomeWebView);
        WebViewPanel.Dock = DockStyle.Fill;
        WebViewPanel.ForeColor = SystemColors.ControlText;
        WebViewPanel.Location = new Point(0, 99);
        WebViewPanel.Name = "WebViewPanel";
        WebViewPanel.Size = new Size(1082, 568);
        WebViewPanel.TabIndex = 4;
        WebViewPanel.TabStop = false;
        // 
        // Form1
        // 
        AutoScaleMode = AutoScaleMode.None;
        BackColor = Color.FromArgb(8, 46, 50);
        ClientSize = new Size(1082, 667);
        Controls.Add(WebViewPanel);
        Controls.Add(flowLayoutPanel1);
        Controls.Add(TabPanel);
        Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        Location = new Point(15, 15);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        TabPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)HomeWebView).EndInit();
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel1.PerformLayout();
        WebViewPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button NewTabButton;

    private System.Windows.Forms.GroupBox WebViewPanel;

    private System.Windows.Forms.TextBox MainSeachBar;

    #endregion

    private Microsoft.Web.WebView2.WinForms.WebView2 HomeWebView;
    private System.Windows.Forms.Panel flowLayoutPanel1;
    private System.Windows.Forms.Button BackButton;
    private System.Windows.Forms.Button ForwardButton;
    private System.Windows.Forms.FlowLayoutPanel TabPanel;
    private System.Windows.Forms.Button RefreshButton;
}