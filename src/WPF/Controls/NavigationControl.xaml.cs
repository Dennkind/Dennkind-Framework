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

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dennkind.Framework.WPF.Controls
{
    public partial class NavigationControl : UserControl
    {
        /// <summary>
        /// Occures when a navigation item was clicked.
        /// </summary>
        public event EventHandler<NavigationItemControl> NavigationItemClicked;

        private bool _isAnimationInProgress = false;
        private bool _isCollapsed = true;

        private List<NavigationItemControl> _navigationItemControls;

        /// <summary>
        /// Initializes a new instance of the Dennkind.Framework.WPF.Controls.NavigationControl class.
        /// </summary>
        public NavigationControl()
        {
            InitializeComponent();

            // initialize the navigation items list
            _navigationItemControls = new List<NavigationItemControl>();

            // event subscriptions:
            haburgerMenuIconGrid.MouseLeftButtonUp += new MouseButtonEventHandler((sender, e) =>
            {
                if (_isCollapsed)
                    Expand();
                else
                    Collapse();
            });
        }

        /// <summary>
        /// Creates a new navigation item
        /// </summary>
        /// <param name="icon">Icon</param>
        /// <param name="title">Title</param>
        /// <param name="name">Name</param>
        /// <returns>
        /// A new instance of the Dennkind.Framework.WPF.Controls.NavigationItemControl class
        /// </returns>
        public NavigationItemControl CreateNewNavigationItem(ImageSource icon, object title, string name)
        {
            // create a new navigation item
            var navigationItemControl = new NavigationItemControl
            {
                Icon = icon,
                Title = title,
                Name = name
            };

            navigationItemControl.MouseLeftButtonUp += new MouseButtonEventHandler((sender, e) =>
            {
                // raise the NavigationItemClicked event
                OnNavigationItemClicked((NavigationItemControl)sender);
            });

            // add the new navigation item to the navigationItemsPanel
            navigationItemsPanel.Children.Add(navigationItemControl);

            // add the new navigation item to the navigation items list
            _navigationItemControls.Add(navigationItemControl);

            return navigationItemControl;
        }

        /// <summary>
        /// Activates the given navigation item.
        /// </summary>
        /// <param name="navigationItemControl">
        /// Dennkind.Framework.WPF.Controls.NavigationItemControl
        /// </param>
        public void ActivateNavigationItem(NavigationItemControl navigationItemControl)
        {
            // deactivate all navigation items
            foreach (var item in _navigationItemControls)
            {
                item.IsActive = false;
            }

            // activate the navigation item
            navigationItemControl.IsActive = true;
        }

        /// <summary>
        /// Expands the navigation area.
        /// </summary>
        public void Expand()
        {
            if (_isAnimationInProgress || !_isCollapsed)
                return;

            //todo: implement animation
            mainGrid.Width = 200;
            _isCollapsed = false;
        }

        /// <summary>
        /// Collapses the navigation area.
        /// </summary>
        public void Collapse()
        {
            if (_isAnimationInProgress || _isCollapsed)
                return;

            //todo: implement animation
            mainGrid.Width = 64;
            _isCollapsed = true;
        }

        /// <summary>
        /// Raises the NavigationItemClicked event.
        /// </summary>
        /// <param name="navigationItemControl">
        /// The clicked navigation item.
        /// </param>
        protected virtual void OnNavigationItemClicked(NavigationItemControl navigationItemControl)
        {
            NavigationItemClicked?.Invoke(this, navigationItemControl);
        }
    }
}