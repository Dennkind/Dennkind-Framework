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

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dennkind.Framework.WPF.Controls
{
    public partial class NavigationItemControl : UserControl
    {
        // holds the items active state
        private bool _isActive;

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        public ImageSource Icon
        {
            get { return iconImage.Source; }
            set { iconImage.Source = value; }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public object Title
        {
            get { return titleLabel.Content; }
            set { titleLabel.Content = value; }
        }

        /// <summary>
        /// Gets or sets the active state.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;

                // visualize the active state
                if (_isActive)
                    isActiveCanvas.Visibility = System.Windows.Visibility.Visible;
                else
                    isActiveCanvas.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Dennkind.Framework.WPF.Controls.NavigationItemControl class.
        /// </summary>
        public NavigationItemControl()
        {
            InitializeComponent();

            // event subscriptions:
            mainGrid.MouseEnter += new MouseEventHandler((sender, e) =>
            {
                // visualize the mouse enter state
                mouseEnterCanvas.Visibility = System.Windows.Visibility.Visible;
            });

            mainGrid.MouseLeave += new MouseEventHandler((sender, e) =>
            {
                // visualize the mouse leave state
                mouseEnterCanvas.Visibility = System.Windows.Visibility.Collapsed;
            });
        }
    }
}