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
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Dennkind.Framework.WPF.Controls
{
    public partial class ContentControl : UserControl
    {
        private bool _isShown = true;
        private bool _isAnimationInProgress = false;

        private Storyboard _fadeInStoryboard;
        private Storyboard _fadeOutStoryboard;

        /// <summary>
        /// Gets or sets a value that indicates whether the content is shown
        /// or hidden.
        /// </summary>
        public bool IsShown
        {
            get { return _isShown; }
            set { _isShown = value; ShowOrHide(); }
        }

        /// <summary>
        /// Initializes a new instance of the Dennkind.Framework.WPF.Controls.ContentControl class.
        /// </summary>
        public ContentControl()
        {
            InitializeComponent();

            // event subscriptions:
            contentFrame.Navigated += new NavigatedEventHandler((sender, e) =>
            {
                // clear the content frames history
                ClearContentFramesHistory();
            });

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
                _isAnimationInProgress = false;
            });
        }

        /// <summary>
        /// Displays the given page.
        /// </summary>
        /// <param name="page">Page</param>
        public void DisplayPage(Page page)
        {
            contentFrame.Navigate(page);
        }

        /// <summary>
        /// Fades the content in.
        /// </summary>
        public void FadeIn()
        {
            // check if animation is not in progress and the content is not already shown
            if (_isAnimationInProgress || _isShown)
                return;

            // indicate that the animation is in progress
            _isAnimationInProgress = true;

            // begin the animation
            _fadeInStoryboard.Begin(this, true);
        }

        /// <summary>
        /// Fades the content out.
        /// </summary>
        public void FadeOut()
        {
            // check if animation is not in progress and the content is not already hidden
            if (_isAnimationInProgress || !_isShown)
                return;

            // indicate that the animation is in progress
            _isAnimationInProgress = true;

            // begin the animation
            _fadeOutStoryboard.Begin(this, true);
        }

        /// <summary>
        /// Shows or hides the content depending on the IsShown property.
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
                mainGrid.Opacity = 1.0;
            else
                mainGrid.Opacity = 0.0;
        }

        /// <summary>
        /// Clears the content frames history.
        /// </summary>
        private void ClearContentFramesHistory()
        {
            if (contentFrame.CanGoBack)
                contentFrame.RemoveBackEntry();
        }
    }
}