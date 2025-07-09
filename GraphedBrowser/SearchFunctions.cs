using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using Transitions;

namespace GraphedBrowser
{
    /// <summary>
    /// Any methods I have regarding the search function I try to put here.
    /// I am still new to all of this so maybe I miss some stuff or do them incorrectly
    /// But I like this structure very much
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The "main" function where I actually do the searching.
        /// </summary>
        /// <param name="link">The URL.</param>
        public void Search(string link)
        {
            if (link.Trim() == string.Empty)
                return;
            
            // Check if search is a pasted link
            // I am not sure if I am doing these checks correctly 
            if (link.StartsWith("http://") || link.StartsWith("https://"))
                CurrentWebView.Source = new Uri(link);
            else if (link.EndsWith(".com"))
            {
                CurrentWebView.Source = new Uri("https://" + link);
            }
            else if (link.StartsWith("file")) // Check if it's a file
            {
                CurrentWebView.Source = new Uri(link);
            }
            else // It's a search
            {
                CurrentWebView.Source = new Uri("https://duckduckgo.com/?t=h_&q=" + link);
            }
            UpdateMainSearchBarText();
        }
        
        /// <summary>
        /// The method for updating the text of the main search bar.
        /// </summary>
        private void UpdateMainSearchBarText()
        {
            string currentPage = CurrentWebView.Source.ToString();

            if (currentPage == homeTabHtmlPath)
            {
                MainSeachBar.Text = "Home Page";
            }
            else
            {
                MainSeachBar.Text = currentPage;
            }
        }
        
        /// <summary>
        /// This method is called whenever the home screen is loaded.
        /// It sends a message everytime 'Enter' is clicked while the secondary search bar is focused.
        /// </summary>
        private async void StartListeningForEnter()
        {
            if (CurrentWebView == null)
                return;
            string listenForEnterScript = @"
                            document.getElementById('search').addEventListener('keydown', function(e) {
                                if (e.key === 'Enter') {
                                    window.chrome.webview.postMessage('enterPressed');
                                }
                            });
                        ";
            Console.WriteLine("Listening for enter");
            Debug.WriteLine("Listening for enter");

            await CurrentWebView.ExecuteScriptAsync(listenForEnterScript);
        }
        
        /// <summary>
        /// Method that recieves the message when "Enter" is pressed when using the secondary search bar
        /// </summary>
        private async void WebView_MessageRecieved(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            Debug.WriteLine("Message recieved");
            string message = e.TryGetWebMessageAsString();

            if (message == "enterPressed")
            {
                string result = await CurrentWebView.ExecuteScriptAsync("document.getElementById('search').value");
                    
                if (result.StartsWith("\"") && result.EndsWith("\""))
                {
                    result = result.Substring(1, result.Length - 2);
                    result = result.Replace("\\\"", "\""); 
                }
                
                // The raw data from the search bar
                Search(result);
            }
        }
    }
}