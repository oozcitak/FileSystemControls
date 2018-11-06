using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Manina.Windows.Forms
{
    public class FileSystemButton : FileSystemLabel
    {
        #region Member Variables
        private bool mouseOver = false;
        private bool mouseDown = false;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "252, 255, 255")]
        [Description("Gets or sets the background color for the control.")]
        public override Color BackColor { get => Renderer.BackColor; set => Renderer.BackColor = value; }
        #endregion

        #region Constructor
        public FileSystemButton()
        {
            TabStop = true;
            SetStyle(ControlStyles.StandardClick | ControlStyles.Selectable | ControlStyles.StandardDoubleClick, true);

            Renderer.BackColor = Color.FromArgb(252, 255, 255);
            Renderer.BorderStyle = BorderStyle.FixedSingle;
            Renderer.ContentPadding = new Size(8, 8);
        }
        #endregion

        #region Overriden Methods
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            mouseOver = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            mouseOver = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!mouseDown)
            {
                mouseDown = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (mouseDown)
            {
                mouseDown = false;
                Invalidate();
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Renderer.DrawItem(e.Graphics, ClientRectangle, node, Enabled, mouseDown, mouseOver, true);
        }
        #endregion
    }
}
