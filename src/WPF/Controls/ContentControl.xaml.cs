﻿/* Author: Lukas Koch, May 2019 
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
using System.Windows.Navigation;

namespace Dennkind.Framework.WPF.Controls
{
    public partial class ContentControl : UserControl
    {
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
        /// Clears the content frames history.
        /// </summary>
        private void ClearContentFramesHistory()
        {
            if (contentFrame.CanGoBack)
                contentFrame.RemoveBackEntry();
        }
    }
}