/* Author: Lukas Koch, May 2019 
 *
 * MIT License
 *
 * Copyright(c) Lukas Koch
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dennkind.Framework.WPF.Controls
{
    public partial class ApplicationFrameControl : UserControl
    {
        public event EventHandler SplashscreenHidden;
        public event EventHandler NotificationShown;
        public event EventHandler NotificationHidden;

        // contains the page navigation items
        private Dictionary<Page, NavigationItemControl> _pageNavigationItems;

        /// <summary>
        /// Gets or sets the header logo.
        /// </summary>
        public object HeaderLogo
        {
            get { return headerControl.Logo; }
            set { headerControl.Logo = value; }
        }

        /// <summary>
        /// Gets or sets the header title.
        /// </summary>
        public object HeaderTitle
        {
            get { return headerControl.Title; }
            set { headerControl.Title = value; }
        }

        /// <summary>
        /// Gets or sets the footer version.
        /// </summary>
        public object FooterVersion
        {
            get { return footerControl.Version; }
            set { footerControl.Version = value; }
        }

        /// <summary>
        /// Gets or sets the footer copyright.
        /// </summary>
        public object FooterCopyright
        {
            get { return footerControl.Copyright; }
            set { footerControl.Copyright = value; }
        }

        /// <summary>
        /// Exposes the header control.
        /// </summary>
        public HeaderControl Header
        {
            get { return headerControl; }
        }

        /// <summary>
        /// Exposes the navigation control.
        /// </summary>
        public NavigationControl Navigation
        {
            get { return navigationControl; }
        }

        /// <summary>
        /// Exposes the content control.
        /// </summary>
        public new ContentControl Content
        {
            get { return contentControl; }
        }

        /// <summary>
        /// Exposes the overlay control.
        /// </summary>
        public OverlayControl Overlay
        {
            get { return overlayControl; }
        }

        /// <summary>
        /// Exposes the footer control.
        /// </summary>
        public FooterControl Footer
        {
            get { return footerControl; }
        }

        /// <summary>
        /// Exposes the splashscreen control.
        /// </summary>
        public SplashscreenControl Splashscreen
        {
            get { return splashscreenControl; }
        }

        /// <summary>
        /// Initializes a new instance of the Dennkind.Framework.WPF.Controls.ApplicationFrameControl class.
        /// </summary>
        public ApplicationFrameControl()
        {
            InitializeComponent();

            // initialize the page navigation items list
            _pageNavigationItems = new Dictionary<Page, NavigationItemControl>();

            // event subscriptions:
            navigationControl.NavigationItemClicked += new EventHandler<NavigationItemControl>((sender, e) =>
            {
                // find the page navigation item in the dictionary
                var pageNavigationItem = _pageNavigationItems.First(x => x.Value == e);

                // display the associated page
                contentControl.DisplayPage(pageNavigationItem.Key);

                // activate the navigation item
                navigationControl.ActivateNavigationItem(pageNavigationItem.Value);
            });

            splashscreenControl.SplashscreenHidden += new EventHandler((sender, e) =>
            {
                OnSplashscreenHidden();
            });

            notificationControl.Shown += new EventHandler((sender, e) =>
            {
                OnNotificationShown();
            });
            notificationControl.Hidden += new EventHandler((sender, e) =>
            {
                OnNotificationHidden();
            });
        }

        /// <summary>
        /// Adds a page and a navigation item.
        /// </summary>
        /// <param name="page">Page (Name and Title property must be set)</param>
        public void AddPage(Page page)
        {
            AddPage(page, null);
        }

        /// <summary>
        /// Adds a page and a navigation item.
        /// </summary>
        /// <param name="page">Page (Name and Title property must be set)</param>
        /// <param name="icon">Navigation item icon</param>
        public void AddPage(Page page, ImageSource icon)
        {
            var title = page.Title;
            var name = page.Name;

            // create new navigation item
            var navigationItemControl = navigationControl.CreateNewNavigationItem(icon, title, name);

            // add page and navigation item to dictionary
            _pageNavigationItems.Add(page, navigationItemControl);
        }

        /// <summary>
        /// Displays a specific page.
        /// </summary>
        /// <param name="name">Page name</param>
        public void DisplayPage(string name)
        {
            // find the page navigation item by name
            var pageNavigationItem = _pageNavigationItems.First(x => x.Key.Name == name);

            // display the page
            contentControl.DisplayPage(pageNavigationItem.Key);

            // activate the navigation item
            navigationControl.ActivateNavigationItem(pageNavigationItem.Value);
        }

        /// <summary>
        /// Displays the overlay with the given content.
        /// </summary>
        /// <param name="content"></param>
        public void DisplayOverlay(object content)
        {
            // display the overlay
            overlayControl.Display(content);

            // disable the other controls
            navigationControl.IsEnabled = false;
            contentControl.IsEnabled = false;
        }

        public void DisplayDialog(string title, string message)
        {
            var dialogControl = new DialogControl();
            dialogControl.OkButtonClicked += new EventHandler((sender, e) =>
            {
                HideOverlay();
            });
            dialogControl.Display(title, message);

            overlayControl.Display(dialogControl);

            // disable the other controls
            navigationControl.IsEnabled = false;
            contentControl.IsEnabled = false;
        }

        /// <summary>
        /// Displays the splashscreen with the given content.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="showTime">The time the splashscreen is shown</param>
        public void DisplaySplashscreen(object content, TimeSpan showTime)
        {
            // hide all other controls
            //Splashscreen.IsShown = true;
            //Header.IsShown = false;
            //Navigation.IsShown = false;
            //Content.IsShown = false;
            //Footer.IsShown = false;

            // initialize the splashscreen
            Splashscreen.IsShown = true;
            Splashscreen.Content = content;
            Splashscreen.FadeOut(showTime);
        }

        public void DisplayNotification(ImageSource icon, object content)
        {
            notificationControl.Icon = icon;
            notificationControl.Content = content;
            notificationControl.FadeIn();
        }

        public void HideNotification()
        {
            notificationControl.FadeOut();
        }

        /// <summary>
        /// Hides the overlay.
        /// </summary>
        public void HideOverlay()
        {
            // hide the overlay
            overlayControl.Hide();

            // enable the other controls
            navigationControl.IsEnabled = true;
            contentControl.IsEnabled = true;
        }

        /// <summary>
        /// Is called when the Splashscreen was hidden.
        /// </summary>
        protected virtual void OnSplashscreenHidden()
        {
            SplashscreenHidden?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnNotificationShown()
        {
            NotificationShown?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnNotificationHidden()
        {
            NotificationHidden?.Invoke(this, EventArgs.Empty);
        }
    }
}