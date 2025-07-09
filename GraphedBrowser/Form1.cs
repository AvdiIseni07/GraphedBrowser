using System.Diagnostics;
using Microsoft.Web.WebView2.WinForms;
using SATAUiFramework.Controls;
using Transitions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace GraphedBrowser;

public partial class Form1 : Form
{

    /// Variables
    private string homeTabHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HomePage.html");
    private List<TabClass> tabs = new List<TabClass>(); // List where all tabs are stored. Pretty much a RAM.
    private TabClass homeTab; // Just for when the app is first opened I need to initialize the first tab manually.
    private WebView2 CurrentWebView; // The currently used window at any time. Needed for all the operations.
    private bool JustEnteredSearchBar = false; // Making sure it only selectes the whole text when it is actually focused.


    public Form1()
    {
        InitializeComponent();

        homeTabHtmlPath.Replace("\\", "/");
        HomeWebView.Source = new Uri(homeTabHtmlPath);
        homeTabHtmlPath = HomeWebView.Source.ToString(); // For reasons I do not know I need to do this to replace the backslashes with normal slashes.
        CurrentWebView = HomeWebView;

        NewTabButton_Click(null, null);
    }

    /// Methods

    // Using the main (top) search bar
    private void SearchFromMainSearchBar(object sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            Search(MainSeachBar.Text);
        }
    }

    // This method is called everytime there is a navigation on the 'CurrentWebView'
    private void WebsiteChanged(object sender, EventArgs e)
    {
        // Just changing the tab title
        foreach (var tab in tabs)
        {
            if (tab.GetWindow() == CurrentWebView)
            {
                var tabButton = tab.GetButton();
                tabButton.Text = RefineTitle(CurrentWebView.Source.ToString());

                // Scale the button so it is enough for the text to fit
                int prevWidth = tabButton.Width;

                tabButton.AutoSize = true;
                tabButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                int targetWidth = tabButton.Width;
                tabButton.AutoSize = false;
                tabButton.Width = prevWidth;
                tab.SetWidth(targetWidth);

                Transition.run(tabButton, "Width", Math.Max(ButtonWidth_NotOpen, targetWidth), new TransitionType_CriticalDamping(500));
                CurrentWebView.Focus();
            }
        }

        if (MainSeachBar.Focused == false)
        {
            UpdateMainSearchBarText();

            // If it's the home screen start listening for enter pressed while the secondary search bar is selected
            if (CurrentWebView.Source.ToString() == homeTabHtmlPath)
            {
                StartListeningForEnter();
            }
        }
    }


    // The methods for the "three elementary buttons"
    private void BackButton_Click(object sender, EventArgs e) { HomeWebView.GoBack(); }
    private void ForwardButton_Click(object sender, EventArgs e) { HomeWebView.GoForward(); }
    private void RefreshButton_Click(object sender, EventArgs e)
    {
        string url = CurrentWebView.Source.ToString();

        if (!string.IsNullOrEmpty(url))
        {
            CurrentWebView.Source = new Uri(homeTabHtmlPath);
            CurrentWebView.Source = new Uri(url);
        }
    }

    private void MainSearchBar_Enter(object sender, EventArgs e) { JustEnteredSearchBar = true; MainSeachBar.SelectAll(); }
    private void MainSeachBar_Click(object sender, EventArgs e)
    {
        if (JustEnteredSearchBar)
        {
            MainSeachBar.SelectAll();
            JustEnteredSearchBar = false;
        }
    }
}