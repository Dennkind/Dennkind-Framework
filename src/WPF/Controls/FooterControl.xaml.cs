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
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dennkind.Framework.WPF.Controls
{
    public partial class FooterControl : UserControl
    {
        private bool _isShown = true;
        private bool _isAnimationInProgress = false;

        private Storyboard _fadeInStoryboard;
        private Storyboard _fadeOutStoryboard;

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public object Version
        {
            get { return versionLabel.Content; }
            set { versionLabel.Content = value; }
        }

        /// <summary>
        /// Gets or sets the copyright.
        /// </summary>
        public object Copyright
        {
            get { return copyrightLabel.Content; }
            set { copyrightLabel.Content = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the header is shown
        /// or hidden.
        /// </summary>
        public bool IsShown
        {
            get { return _isShown; }
            set { _isShown = value; ShowOrHide(); }
        }

        /// <summary>
        /// Initializes a new instance of the Dennkind.Framework.WPF.Controls.FooterControl class.
        /// </summary>
        public FooterControl()
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
                _isAnimationInProgress = false;
            });
        }

        /// <summary>
        /// Fades the footer in.
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
        /// Fades the footer out.
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
        /// Shows or hides the footer depending on the IsShown property.
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
                mainGrid.Height = 40;
            else
                mainGrid.Height = 0;
        }
    }
}