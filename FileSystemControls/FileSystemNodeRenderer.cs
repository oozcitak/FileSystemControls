using System;
using System.Drawing;
using System.Windows.Forms;

namespace Manina.Windows.Forms
{
    public class FileSystemNodeRenderer
    {
        #region Events
        public class GetLineCountEventArgs : EventArgs
        {
            public FileSystemNode Node { get; private set; }
            public int LineCount { get; set; }

            public GetLineCountEventArgs(FileSystemNode node)
            {
                Node = node;
                LineCount = 0;
            }
        }

        public delegate void GetLineCountEventHandler(object sender, GetLineCountEventArgs e);
        public event GetLineCountEventHandler GetLineCount;

        public class GetLineContentsEventArgs : EventArgs
        {
            public FileSystemNode Node { get; private set; }
            public int LineIndex { get; private set; }
            public string Contents { get; set; }
            public bool ShowBar { get; set; }
            public float BarPercentage { get; set; }
            public Color Color { get; set; }

            public GetLineContentsEventArgs(FileSystemNode node, int lineIndex, Color color)
            {
                Node = node;
                LineIndex = lineIndex;
                Color = color;
                Contents = "";
            }
        }

        public delegate void GetLineContentsEventHandler(object sender, GetLineContentsEventArgs e);
        public event GetLineContentsEventHandler GetLineContents;

        public class DrawNodeEventArgs : EventArgs
        {
            public Graphics Graphics { get; private set; }
            public Rectangle Bounds { get; private set; }
            public FileSystemNode Node { get; private set; }
            public bool Enabled { get; private set; }
            public bool Selected { get; private set; }
            public bool Hovered { get; private set; }
            public bool ControlHasFocus { get; private set; }
            public bool Handled { get; set; }

            public DrawNodeEventArgs(Graphics graphics, Rectangle bounds, FileSystemNode node, bool enabled, bool selected, bool hovered, bool controlHasFocus)
            {
                Graphics = graphics;
                Bounds = bounds;
                Node = node;
                Enabled = enabled;
                Selected = selected;
                Hovered = hovered;
                ControlHasFocus = controlHasFocus;
                Handled = false;
            }
        }

        public delegate void DrawNodeEventHandler(object sender, DrawNodeEventArgs e);
        public event DrawNodeEventHandler DrawBackground;
        public event DrawNodeEventHandler DrawBorder;
        public event DrawNodeEventHandler DrawContents;
        public event DrawNodeEventHandler DrawErrorMessage;

        public class DrawNodeLineEventArgs : DrawNodeEventArgs
        {
            public int LineIndex { get; private set; }

            public DrawNodeLineEventArgs(Graphics graphics, Rectangle bounds, FileSystemNode node, bool enabled, bool selected, bool hovered, bool controlHasFocus, int lineIndex)
                : base(graphics, bounds, node, enabled, selected, hovered, controlHasFocus)
            {
                LineIndex = lineIndex;
            }
        }

        public delegate void DrawNodeLineEventHandler(object sender, DrawNodeLineEventArgs e);
        public event DrawNodeLineEventHandler DrawLine;
        #endregion

        #region Properties
        public BorderStyle BorderStyle { get; set; } = BorderStyle.None;

        public Font Font { get; set; } = SystemFonts.DefaultFont;
        public float LineSpacing { get; set; } = 0.2f;

        public Color BackColor { get; set; } = Color.FromArgb(252, 255, 255);
        public Color ForeColor { get; set; } = Color.FromArgb(0, 0, 0);
        public Color DisabledBackColor { get; set; } = Color.FromArgb(217, 217, 217);
        public Color DetailTextColor { get; set; } = Color.FromArgb(96, 96, 96);
        public Color ErrorTextColor { get; set; } = Color.FromArgb(255, 0, 0);

        public Size ThumbnailSize { get; set; } = new Size(32, 32);
        public int ThumbnailTextSpacing { get; set; } = 2;
        public Size ContentPadding { get; set; } = new Size(2, 2);

        public Color UnfocusedItemBackColor { get; set; } = Color.FromArgb(217, 217, 217);
        public Color SelectedItemBackColor { get; set; } = Color.FromArgb(204, 232, 255);
        public Color HoveredItemBackColor { get; set; } = Color.FromArgb(229, 243, 255);

        public Color UnfocusedItemBorderColor { get; set; } = Color.FromArgb(153, 153, 153);
        public Color SelectedItemBorderColor { get; set; } = Color.FromArgb(153, 209, 255);
        public Color DisabledItemBorderColor { get; set; } = Color.FromArgb(153, 153, 153);

        public Color BarBackColor { get; set; } = Color.FromArgb(230, 230, 230);
        public Color BarFillColor { get; set; } = Color.FromArgb(38, 160, 218);
        public Color BarBorderColor { get; set; } = Color.FromArgb(188, 188, 188);
        #endregion

        #region Instance Methods
        /// <summary>
        /// Gets the minimum height of items.
        /// </summary>
        /// <returns>Minimum item height.</returns>
        public int GetFixedItemHeight()
        {
            var e = new GetLineCountEventArgs(null);
            OnGetLineCount(e);
            int lineHeight = Font.Height;
            float textHeight = e.LineCount * lineHeight + LineSpacing * (e.LineCount - 1) * lineHeight;
            float maxHeight = Math.Max(textHeight, ThumbnailSize.Height);

            return (int)(maxHeight + 2 * ContentPadding.Height);
        }

        /// <summary>
        /// Gets the minimum height of items.
        /// </summary>
        /// <param name="node">The node to measure.</param>
        /// <returns>Minimum item height.</returns>
        public int GetItemHeight(FileSystemNode node)
        {
            var e = new GetLineCountEventArgs(node);
            OnGetLineCount(e);
            int lineHeight = Font.Height;
            float textHeight = e.LineCount * lineHeight + LineSpacing * (e.LineCount - 1) * lineHeight;
            float maxHeight = Math.Max(textHeight, ThumbnailSize.Height);

            return (int)(maxHeight + 2 * ContentPadding.Height);
        }

        /// <summary>
        /// Renders a list item.
        /// </summary>
        /// <param name="graphics">The graphics object to draw on.</param>
        /// <param name="bounds">Bounds of the item.</param>
        /// <param name="node">The node to draw.</param>
        /// <param name="enabled">Whether the item is enabled.</param>
        /// <param name="selected">Whether the item is selected.</param>
        /// <param name="hovered">Whether the mouse cursor is over the item.</param>
        /// <param name="controlHasFocus">Whether the control has input focus.</param>
        public void DrawItem(Graphics graphics, Rectangle bounds, FileSystemNode node, bool enabled, bool selected, bool hovered, bool controlHasFocus)
        {
            DrawNodeEventArgs e = new DrawNodeEventArgs(graphics, bounds, node, enabled, selected, hovered, controlHasFocus);

            OnDrawBackground(e);
            OnDrawBorder(e);

            if (e.Node.IsPathValid)
                OnDrawContents(e);
            else
                OnDrawErrorMessage(e);
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Gets the line count of an item.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnGetLineCount(GetLineCountEventArgs e)
        {
            GetLineCount?.Invoke(this, e);
        }

        /// <summary>
        /// Gets the contents of a line of text of an item.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnGetLineContents(GetLineContentsEventArgs e)
        {
            GetLineContents?.Invoke(this, e);
        }

        /// <summary>
        /// Paints the background of the item.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnDrawBackground(DrawNodeEventArgs e)
        {
            DrawBackground?.Invoke(this, e);
            if (e.Handled) return;

            using (Brush back = new SolidBrush(!e.Enabled ? DisabledBackColor :
                !e.ControlHasFocus && e.Selected ? UnfocusedItemBackColor :
                e.Selected ? SelectedItemBackColor :
                e.Hovered ? HoveredItemBackColor : BackColor))
            {
                e.Graphics.FillRectangle(back, e.Bounds);
            }
        }

        /// <summary>
        /// Paints the border of the item.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnDrawBorder(DrawNodeEventArgs e)
        {
            DrawBorder?.Invoke(this, e);
            if (e.Handled) return;

            if (!e.Enabled && e.Selected)
            {
                using (Pen bFocus = new Pen(DisabledItemBorderColor))
                {
                    e.Graphics.DrawRectangle(bFocus, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
                }
            }
            else if (!e.ControlHasFocus && e.Selected)
            {
                using (Pen bFocus = new Pen(UnfocusedItemBorderColor))
                {
                    e.Graphics.DrawRectangle(bFocus, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
                }
            }
            else if (e.Selected)
            {
                using (Pen bFocus = new Pen(SelectedItemBorderColor))
                {
                    e.Graphics.DrawRectangle(bFocus, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
                }
            }
            else if (BorderStyle == BorderStyle.FixedSingle)
            {
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.Flat);
            }
            else if (BorderStyle == BorderStyle.Fixed3D)
            {
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedOuter);
            }
        }

        /// <summary>
        /// Paints the contents of the control.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnDrawContents(DrawNodeEventArgs e)
        {
            DrawContents?.Invoke(this, e);
            if (e.Handled) return;

            var eLines = new GetLineCountEventArgs(e.Node);
            OnGetLineCount(eLines);

            int lines = eLines.LineCount;
            int lineHeight = Font.Height;
            float textHeight = (lines) * lineHeight + LineSpacing * (lines - 1) * lineHeight;
            RectangleF iconRect = new RectangleF(e.Bounds.Left + ContentPadding.Width, e.Bounds.Top + (e.Bounds.Height - ThumbnailSize.Height) / 2f,
                ThumbnailSize.Width, ThumbnailSize.Height);
            RectangleF textRect = new RectangleF(e.Bounds.Left + ContentPadding.Width + ThumbnailSize.Width + ThumbnailTextSpacing, e.Bounds.Top + (e.Bounds.Height - textHeight) / 2f,
                e.Bounds.Width - iconRect.Width - 2 * ContentPadding.Width - ThumbnailTextSpacing, textHeight);

            // Draw the image
            if (e.Node.Thumbnail != null)
            {
                Rectangle pos = Utility.GetSizedIconBounds(e.Node.Thumbnail, Utility.ToRectangle(iconRect), 0.0f, 0.5f);
                e.Graphics.DrawImage(e.Node.Thumbnail, pos);
            }

            // Draw item text
            RectangleF lineBounds = textRect;
            lineBounds.Height = lineHeight;
            for (int i = 0; i < eLines.LineCount; i++)
            {
                // Draw line of text
                DrawNodeLineEventArgs eLine = new DrawNodeLineEventArgs(e.Graphics,
                    Utility.ToRectangle(lineBounds), e.Node, e.Enabled, e.Selected, e.Hovered, e.ControlHasFocus, i);
                OnDrawLine(eLine);

                // Offset the bounds to the next line below
                lineBounds.Offset(0, 1.2f * lineHeight);
            }
        }

        /// <summary>
        /// Draws a line of the content.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnDrawLine(DrawNodeLineEventArgs e)
        {
            DrawLine?.Invoke(this, e);
            if (e.Handled) return;

            var eContent = new GetLineContentsEventArgs(e.Node, e.LineIndex, Color.Empty);
            OnGetLineContents(eContent);

            if (eContent.ShowBar)
            {
                // Free space indicator
                using (Brush bBarBack = new SolidBrush(BarBackColor))
                using (Brush bBarFill = new SolidBrush(!eContent.Color.IsEmpty ? eContent.Color : BarFillColor))
                using (Pen pBarBorder = new Pen(BarBorderColor))
                {
                    e.Graphics.FillRectangle(bBarBack, e.Bounds);
                    e.Graphics.FillRectangle(bBarFill, new RectangleF(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width * eContent.BarPercentage, e.Bounds.Height));
                    e.Graphics.DrawRectangle(pBarBorder, Utility.ToRectangle(e.Bounds));
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(!eContent.Color.IsEmpty ? eContent.Color : e.LineIndex == 0 ? ForeColor : DetailTextColor))
                using (StringFormat stringFormat = new StringFormat())
                {
                    e.Graphics.DrawString(eContent.Contents, Font, brush, e.Bounds, stringFormat);
                }
            }
        }

        /// <summary>
        /// Draws an error message if the path is invalid.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnDrawErrorMessage(DrawNodeEventArgs e)
        {
            DrawErrorMessage?.Invoke(this, e);
            if (e.Handled) return;

            Rectangle bounds = e.Bounds;
            bounds.Inflate(-ContentPadding.Width, -ContentPadding.Height);

            // Draw item text
            using (Brush bError = new SolidBrush(ErrorTextColor))
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;
                stringFormat.Trimming = StringTrimming.EllipsisCharacter;

                e.Graphics.DrawString(e.Node.ErrorMessage, Font, bError, bounds, stringFormat);
            }
        }

        #endregion
    }
}
