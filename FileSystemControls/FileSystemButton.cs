using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Manina.Windows.Forms
{
    public class FileSystemButton : FileSystemLabel
    {
        #region Member Variables
        private Color disabledBackColor = Color.FromArgb(217, 217, 217);
        private Color hoverBackColor = Color.FromArgb(229, 243, 255);
        private Color selectedBackColor = Color.FromArgb(204, 232, 255);
        private Color focusBorderColor = Color.FromArgb(153, 209, 255);

        private bool mouseOver = false;
        private bool mouseDown = false;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "252, 255, 255")]
        [Description("Gets or sets the background color for the control.")]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background color for the control when in its disabled state.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "217, 217, 217")]
        [Description("Gets or sets the background color for the control when in its disabled state.")]
        public virtual Color DisabledBackColor
        {
            get => disabledBackColor;
            set
            {
                disabledBackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background color for the control when it is clicked with the mouse.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "204, 232, 255")]
        [Description("Gets or sets the background color for the control when it is clicked with the mouse.")]
        public virtual Color SelectedBackColor
        {
            get => selectedBackColor;
            set
            {
                selectedBackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background color for the control when it has mouse focus.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "229, 243, 255")]
        [Description("Gets or sets the background color for the control when it has mouse focus.")]
        public virtual Color HoverBackColor
        {
            get => hoverBackColor;
            set
            {
                hoverBackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border color for the control when it has focus.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "153, 209, 255")]
        [Description("Gets or sets the border color for the control when it has focus.")]
        public virtual Color FocusBorderColor
        {
            get => focusBorderColor;
            set
            {
                focusBorderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border style of the user control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(BorderStyle), "FixedSingle")]
        [Description("Gets or sets the border style of the user control.")]
        public override BorderStyle BorderStyle
        {
            get => base.BorderStyle;
            set => base.BorderStyle = value;
        }

        /// <summary>
        /// Gets or sets the spacing between the border of the control and its contents.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Size), "8,8")]
        [Description("Gets or sets the spacing between the border of the control and its contents.")]
        public override Size ContentPadding
        {
            get => base.ContentPadding;
            set => base.ContentPadding = value;
        }
        #endregion

        #region Constructor
        public FileSystemButton()
        {
            BorderStyle = BorderStyle.FixedSingle;
            ContentPadding = new Size(8, 8);

            BackColor = Color.FromArgb(255, 255, 255);
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void DrawBackground(FileSystemLabelDrawEventArgs e)
        {
            using (Brush back = new SolidBrush(!Enabled ? disabledBackColor : mouseDown ? selectedBackColor : mouseOver ? hoverBackColor : BackColor))
            {
                e.Graphics.FillRectangle(back, e.Bounds);
            }
        }

        /// <summary>
        /// Paints the border of the control.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void DrawBorder(FileSystemLabelDrawEventArgs e)
        {
            if (mouseOver)
            {
                using (Pen bFocus = new Pen(focusBorderColor))
                {
                    e.Graphics.DrawRectangle(bFocus, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
                }
            }
            else
            {
                base.DrawBorder(e);
            }
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
        #endregion
    }
}
