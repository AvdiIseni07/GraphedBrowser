using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using SATAUiFramework.Controls;
using Transitions;

namespace GraphedBrowser
{
    /// <summary>
    /// A class which I use in order to organize the tabs. Each tab object is stored in a List from where I can modify them.
    /// </summary>
    public class TabClass
 
    {
        /// Variables
        private Button OpenTabButton {get;}
        private WebView2 TabWindow {get;} 
        private Button CloseTabButton {get;} 
        private int FulLTextWidth {get; set; } // The width it needs for dynamic sizing
        
        // Initialization
        
        public TabClass(Button openTabButton, WebView2 tabWindow, Button closeTabButton,string title, int width)
        {
            OpenTabButton = openTabButton;
            TabWindow = tabWindow;
            OpenTabButton.Text = title;
            FulLTextWidth = width;
            CloseTabButton = closeTabButton;
        }

        // Getters and setters
        public Button GetButton() {return OpenTabButton;}
        public WebView2 GetWindow() {return TabWindow;}
        public Button GetCloseTabButton() {return CloseTabButton;}
        public void SetWidth(int width) {FulLTextWidth = width;}
        public void SetTitle(string title) {OpenTabButton.Text = title;}
        public int GetWidth() {return FulLTextWidth;}
    
    }
    
    public partial class Form1 : Form
    {
        /// <summary>
        /// Turn the link into a more readable string which will be displayed in the main search bar.
        /// </summary>
        /// <param name="link">The URL</param>
        /// <returns></returns>
        private string RefineTitle(string link)
        {
            string title = "";

            if (link.StartsWith("https://duckduckgo.com/?t=h_&q="))
            {
                link = link.Replace("https://duckduckgo.com/?t=h_&q=", "");
                Console.WriteLine(link);

                bool plusBefore = false;
                title = "Search: ";   
                foreach (var c in link)
                {
                        if (c == '&')
                            break;

                        if (c != '+')
                        {
                            title += c;
                            plusBefore = false;
                        }
                        else
                        {
                            if (plusBefore)
                            {
                                title += "+";
                                plusBefore = false;
                            }else
                            {
                                title += " ";
                                plusBefore = true; 
                            }
                                
                        }
                }
            }
            else if (link.StartsWith("https://www."))
            {
                link = link.Replace("https://www.", "");
                string[] parts = link.Split('/');
                title = parts[0];
                Console.WriteLine(parts[0]);
            }
            else if (link == homeTabHtmlPath){title = "Home Tab";}
            else {title = link;}
            
            //Console.WriteLine("Refined link is: " + title);
            return title;
        }
        
        /// <summary>
        /// Method for when the user tries to acess an already opened tab (not create a new one)
        /// </summary>
        private void TabButton_Click(object sender, EventArgs e)
        {
            CurrentWebView.Visible = false;
            foreach (var tab in tabs)
            {
                if (tab.GetButton() == sender)
                {
                    (tab.GetWindow()).Visible = true;
                    CurrentWebView = tab.GetWindow();
                    (tab.GetButton()).FlatStyle = FlatStyle.Standard;

                    if (CurrentWebView.Source.ToString() == homeTabHtmlPath)
                    {
                        StartListeningForEnter();
                    }

                    UpdateMainSearchBarText();
                    (tab.GetButton()).Text = RefineTitle(CurrentWebView.Source.ToString());
                    Transition.run(tab.GetButton(), "Width", Math.Max(ButtonWidth_NotOpen, tab.GetWidth()), new TransitionType_CriticalDamping(500));
                }
                else
                {
                    (tab.GetButton()).FlatStyle = FlatStyle.Flat;
                    if ((tab.GetButton()).Width > ButtonWidth_NotOpen)
                    {
                        Transition.run(tab.GetButton(), "Width", ButtonWidth_NotOpen, new TransitionType_CriticalDamping(500));
                    }
                }
             }
        }
        
        
        private WebView2 preview; // The when hovering on a tab button
        /// <summary>
        /// Hides the overflow on the that can be caused due to the smaller window.
        /// </summary>
        private async void ActivatePreview(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            await preview.CoreWebView2.ExecuteScriptAsync(@"
                document.body.style.overflow = 'hidden';
                document.documentElement.style.overflow = 'hidden';");
        }
        /// <summary>
        /// Method for creating a new tab
        /// </summary>
        private void NewTabButton_Click(object? sender, EventArgs? e)
        {
            var tabButton = new Button();
            var closeButton = new Button();
            var tabWindow = tabs.Count == 0 ? HomeWebView : new WebView2();
            
            TabPanel.Controls.Add(tabButton);
 
            tabButton.Anchor = AnchorStyles.None;
            tabButton.Width = 0;
            tabButton.Height = 38;
            tabButton.Click += TabButton_Click;
            
            tabButton.ForeColor = Color.Beige;
            tabButton.BackColor = NewTabButton.BackColor;
            tabButton.TextAlign = ContentAlignment.MiddleLeft;

            if (tabs.Count > 0) tabButton.FlatStyle = FlatStyle.Flat;
            
            tabButton.FlatAppearance.BorderSize = 0;
            
            SATAEllipseControl ellipse = new SATAEllipseControl();
            ellipse.TargetControl = tabButton;
            ellipse.CornerRadius = 5;
            
            Transition.run(tabButton, "Width", ButtonWidth_NotOpen, new TransitionType_CriticalDamping(500));
            
            tabButton.MouseEnter += (sender, e) =>
            {
                if (tabWindow == CurrentWebView)
                    return;
             
                preview = new WebView2();
                ActiveForm.Controls.Add(preview);
                    
                preview.ZoomFactor = 0.1;
                preview.BringToFront();
                preview.NavigationCompleted += ActivatePreview;
                
                preview.Source = tabWindow.Source;
                preview.Size = new Size(tabButton.Width, 0);
                preview.Visible = true;
               
                Transition.run(preview, "Height", 100, new TransitionType_CriticalDamping(500));
                preview.Location = new Point(tabButton.Location.X, tabButton.Location.Y + 50);
            };
            tabButton.MouseLeave += (sender, e) =>
            {
                if (preview != null)
                {
                    preview.Dispose();
                }
            };

            WebViewPanel.Controls.Add(tabWindow);
            tabWindow.Width = WebViewPanel.Width;
            tabWindow.Height = WebViewPanel.Height;
            tabWindow.Location = new Point(0, 0);
            tabWindow.Visible = (tabs.Count == 0) ? true : false;
            
            tabWindow.Source = new Uri(homeTabHtmlPath);
            tabWindow.Dock = DockStyle.Fill;
            
            UpdateMainSearchBarText();

            tabWindow.NavigationCompleted += WebsiteChanged;

            //tabWindow.SourceChanged += WebsiteChanged;
            tabWindow.WebMessageReceived += WebView_MessageRecieved;

            closeButton.Size = new Size(40, 10);
            tabButton.Controls.Add(closeButton);
            closeButton.Dock = DockStyle.Right;
            closeButton.Text = "X";
            closeButton.BackColor = tabButton.BackColor;
            closeButton.SendToBack();

            closeButton.MouseEnter += (sender, e) =>
            {
                closeButton.BackColor = Color.Red;
                //    Transition.run(closeButton, "BackColor",  Color.Red, new TransitionType_CriticalDamping(350));
            };

            closeButton.MouseLeave += (sender, e) =>
            {
                closeButton.BackColor = tabButton.BackColor;
                //  Transition.run(closeButton, "BackColor", tabButton.BackColor, new TransitionType_CriticalDamping(350));
            };
            
            var tabObject  = new TabClass(tabButton, tabWindow, closeButton,"Home Tab", ButtonWidth_NotOpen);
            tabs.Add(tabObject);

            closeButton.Click += CloseButton_Click;
        }
        /// <summary>
        /// Closing the tab.
        /// </summary>
        private async void CloseButton_Click(object sender, EventArgs e)
        {
            foreach (var tab in tabs)
            {
                if (tab.GetCloseTabButton() == sender)
                {
                    await DeleteTab(tab);
                    break;
                }
            }
        }
        /// <summary>
        /// Deleting the tab
        /// </summary>
        /// <param name="tabObject">The tab which will be deleted.</param>
        private async Task DeleteTab(TabClass tabObject)
        {
            Transition.run(tabObject.GetButton(), "Width", 0, new TransitionType_CriticalDamping(350));

            await Task.Delay(350);
            
            tabObject.GetButton().Dispose();
            tabObject.GetWindow().Dispose();
            tabObject.GetCloseTabButton().Dispose();
            tabs.Remove(tabObject);
            
            if (tabs.Count == 0) Application.Exit();
        }
    }
}
