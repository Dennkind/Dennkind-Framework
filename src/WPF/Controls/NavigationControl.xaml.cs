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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dennkind.Framework.WPF.Controls
{
    public partial class NavigationControl : UserControl
    {
        /// <summary>
        /// Occures when a navigation item was clicked.
        /// </summary>
        public event EventHandler<NavigationItemControl> NavigationItemClicked;

        private bool _isShown = true;
        private bool _isCollapsed = true;
        private bool _isAnimationInProgress = false;

        private Storyboard _fadeInStoryboard;
        private Storyboard _fadeOutStoryboard;
        private Storyboard _expandStoryboard;
        private Storyboard _collapseStoryboard;

        private int _collapsedWidth;
        private int _expandedWidth;

        private List<NavigationItemControl> _navigationItemControls;

        /// <summary>
        /// Gets or sets a value that indicates whether the navigation
        /// is collapsed or expanded.
        /// </summary>
        public bool IsCollapsed
        {
            get { return _isCollapsed; }
            set { _isCollapsed = value; ExpandOrCollapse(); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the navigation is shown
        /// or hidden.
        /// </summary>
        public bool IsShown
        {
            get { return _isShown; }
            set { _isShown = value; ShowOrHide(); }
        }

        /// <summary>
        /// Gets or sets the default width for the collapsed state.
        /// </summary>
        public int CollapsedWidth
        {
            get { return _collapsedWidth; }
            set { _collapsedWidth = value; }
        }

        /// <summary>
        /// Gets or sets the default width for the expanded state.
        /// </summary>
        public int ExpandedWidth
        {
            get { return _expandedWidth; }
            set { _expandedWidth = value; }
        }

        /// <summary>
        /// Initializes a new instance of the Dennkind.Framework.WPF.Controls.NavigationControl class.
        /// </summary>
                public NavigationControl()
        {
            InitializeComponent();

            // initialize storyboards
            _fadeInStoryboard = (Storyboard)FindResource("FadeInStoryboard");
            _fadeInStoryboard.Completed += new EventHandler((sender, e) =>
            {
                _isShown = true;
                _isAnimationInProgress = false;
            });

            _fadeOutStoryboard = (Storyboard)FindResource("FadeOutStoryboard");
            _fadeOutStoryboard.Completed += new EventHandler((sender, e) =>
            {
                _isShown = false;
                _isCollapsed = true;
                _isAnimationInProgress = false;
            });

            _expandStoryboard = (Storyboard)FindResource("ExpandStoryboard");
            _expandStoryboard.Completed += new EventHandler((sender, e) =>
            {
                _isAnimationInProgress = false;
                _isCollapsed = false;
            });

            _collapseStoryboard = (Storyboard)FindResource("CollapseStoryboard");
            _collapseStoryboard.Completed += new EventHandler((sender, e) =>
            {
                _isAnimationInProgress = false;
                _isCollapsed = true;
            });

            // set default values
            _collapsedWidth = 64;
            _expandedWidth = 260;

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
        /// Fades the navigation in.
        /// </summary>
        public void FadeIn()
        {
            // check if animation is not in progress and the header is not already shown
            if (_isAnimationInProgress || _isShown)
                return;

            // indicate that the animation is in progress
            _isAnimationInProgress = true;

            // begin the animation
            _fadeInStoryboard.Begin(this, true);
        }

        /// <summary>
        /// Fades the navigation out.
        /// </summary>
        public void FadeOut()
        {
            // check if animation is not in progress and the header is not already hidden
            if (_isAnimationInProgress || !_isShown)
                return;

            // indicate that the animation is in progress
            _isAnimationInProgress = true;

            // begin the animation
            _fadeOutStoryboard.Begin(this, true);
        }

        /// <summary>
        /// Expands the navigation area.
        /// </summary>
        public void Expand()
        {
            // check if animation is not in progress and the navigation is not already expanded
            if (_isAnimationInProgress || !_isCollapsed)
                return;

            // apply default values to animation
            var expandAnimation = (DoubleAnimationUsingKeyFrames)_expandStoryboard.Children[0];
            expandAnimation.KeyFrames[0].Value = _collapsedWidth;
            expandAnimation.KeyFrames[1].Value = _expandedWidth;

            // indicate that the animation is in progress
            _isAnimationInProgress = true;

            // begin the animation
            _expandStoryboard.Begin(this, true);
        }

        /// <summary>
        /// Collapses the navigation area.
        /// </summary>
        public void Collapse()
        {
            // check if animation is not in progress and the navigation is not already collapsed
            if (_isAnimationInProgress || _isCollapsed)
                return;

            // apply default values to animation
            var collapseAnimation = (DoubleAnimationUsingKeyFrames)_expandStoryboard.Children[0];
            collapseAnimation.KeyFrames[0].Value = _expandedWidth;
            collapseAnimation.KeyFrames[1].Value = _collapsedWidth;

            // indicate that the animation is in progress
            _isAnimationInProgress = true;

            // begin the animation
            _collapseStoryboard.Begin(this, true);
        }

        /// <summary>
        /// Expands or collapses the navigation depending on the
        /// IsCollapsed property.
        /// </summary>
        private void ExpandOrCollapse()
        {
            // stop storyboards if active
            if (_isAnimationInProgress)
            {
                _expandStoryboard.Stop();
                _collapseStoryboard.Stop();
            }

            // check and apply the IsCollapsed state
            if (IsCollapsed)
                mainGrid.Width = _collapsedWidth;
            else
                mainGrid.Width = _expandedWidth;
        }

        /// <summary>
        /// Shows or hides the navigation depending on the IsShown property.
        /// </summary>
        private void ShowOrHide()
        {
            // stop storyboards if active
            if (_isAnimationInProgress)
            {
                _fadeInStoryboard.Stop();
                _fadeOutStoryboard.Stop();
            }

            // check and apply the IsShown state
            if (IsShown)
                mainGrid.Width = _collapsedWidth;
            else
                mainGrid.Width = 0;
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