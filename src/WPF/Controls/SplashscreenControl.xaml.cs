/* Author: Lukas Koch, June 2019 
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

namespace Dennkind.Framework.WPF.Controls
{
    /// <summary>
    /// Interaction logic for SplashscreenControl.xaml
    /// </summary>
    public partial class SplashscreenControl : UserControl
    {
        public event EventHandler SplashscreenHidden;

        private bool _isShown = false;
        private bool _isAnimationInProgress = false;

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

        public new object Content
        {
            get { return contentControl.Content; }
            set { contentControl.Content = value; }
        }

        public SplashscreenControl()
        {
            InitializeComponent();

            // hide the main grid
            mainGrid.Opacity = 0.0;
            mainGrid.Visibility = System.Windows.Visibility.Collapsed;

            // initialize storyboards
            _fadeOutStoryboard = (Storyboard)FindResource("FadeOutStoryboard");
            _fadeOutStoryboard.Completed += new EventHandler((sender, e) =>
            {
                _isShown = false;
                _isAnimationInProgress = false;

                mainGrid.Visibility = System.Windows.Visibility.Collapsed;

                OnSplashscreenHidden();
            });
        }

        /// <summary>
        /// Fades the content out.
        /// </summary>
        public void FadeOut(TimeSpan delayTime)
        {
            // check if animation is not in progress and the content is not already hidden
            if (_isAnimationInProgress || !_isShown)
                return;

            // indicate that the animation is in progress
            _isAnimationInProgress = true;

            // begin the animation
            _fadeOutStoryboard.BeginTime = delayTime;
            _fadeOutStoryboard.Begin(this, true);
        }

        protected virtual void OnSplashscreenHidden()
        {
            SplashscreenHidden?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Shows or hides the content depending on the IsShown property.
        /// </summary>
        private void ShowOrHide()
        {
            // stop storyboards if active
            if (_isAnimationInProgress)
            {
                _fadeOutStoryboard.Stop();
            }

            // check and apply the IsShown state
            if (IsShown)
            {
                mainGrid.Opacity = 1.0;
                mainGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                mainGrid.Opacity = 0.0;
                mainGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
