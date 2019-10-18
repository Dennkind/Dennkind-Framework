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

namespace Dennkind.Framework.WPF.Controls
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Interaction logic for NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl : UserControl
    {
        private Storyboard _fadeInStoryboard;
        private Storyboard _fadeOutStoryboard;

        private bool _isAnimationInProgress = false;

        /// <summary>
        /// Gets or sets a value that indicates whether the content is shown
        /// or hidden.
        /// </summary>
        public bool IsShown { get; private set; }

        public ImageSource Icon
        {
            get { return iconImage.Source; }
            set { iconImage.Source = value; }
        }

        public new object Content
        {
            get { return contentControl.Content; }
            set { contentControl.Content = value; }
        }

        public NotificationControl()
        {
            InitializeComponent();

            // set default values
            mainGrid.Height = 0;
            mainGrid.Width = 60;
            contentGrid.Opacity = 0.0;

            // initialize storyboards
            _fadeInStoryboard = FindResource("FadeInStoryboard") as Storyboard;
            _fadeInStoryboard.Completed += new System.EventHandler((sender, e) =>
            {
                _isAnimationInProgress = false;
                IsShown = true;
            });

            _fadeOutStoryboard = FindResource("FadeOutStoryboard") as Storyboard;
            _fadeOutStoryboard.Completed += new System.EventHandler((sender, e) =>
            {
                _isAnimationInProgress = false;
                IsShown = false;
            });
        }

        /// <summary>
        /// Fades the content in.
        /// </summary>
        public void FadeIn()
        {
            // check if animation is not in progress and the content is not already shown
            if (_isAnimationInProgress || IsShown)
                return;

            // indicate that the animation is in progress
            _isAnimationInProgress = true;

            // begin the animation
            _fadeInStoryboard.Begin();
        }

        /// <summary>
        /// Fades the content out.
        /// </summary>
        public void FadeOut()
        {
            // check if animation is not in progress and the content is not already hidden
            if (_isAnimationInProgress || !IsShown)
                return;

            // indicate that the animation is in progress
            _isAnimationInProgress = true;

            // begin the animation
            _fadeOutStoryboard.Begin();
        }
    }
}