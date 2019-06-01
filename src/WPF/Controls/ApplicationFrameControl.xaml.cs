/* Author: Lukas Koch, May 2019 
 *
 * MIT License
 *
 * Copyright(c) 2019 Lukas Koch
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

using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System;

namespace Dennkind.Framework.WPF.Controls
{
    public partial class ApplicationFrameControl : UserControl
    {
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
        /// Exposes the footer control.
        /// </summary>
        public FooterControl Footer
        {
            get { return footerControl; }
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
        }

        /// <summary>
        /// Adds a page and a navigation item.
        /// </summary>
        /// <param name="page">Page</param>
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
    }
}