using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Manina.Windows.Forms
{
    [Designer(typeof(FileSystemLabelDesigner))]
    public class FileSystemLabel : Control
    {
        #region Member Variables
        private BorderStyle borderStyle = BorderStyle.None;

        private Color detailColor = Color.FromArgb(96, 96, 96);
        private Color errorColor = Color.FromArgb(255, 0, 0);

        private Size thumbnailSize = new Size(64, 64);
        private Size contentPadding = new Size(0, 0);
        private int thumbnailTextSpacing = 8;

        private RectangleF iconRect;
        private RectangleF textRect;
        private readonly float lineSpacing = 0.2f;

        private string path = "";

        private FileSystemNode node = null;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the border style of the user control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(BorderStyle), "None")]
        [Description("Gets or sets the border style of the user control.")]
        public virtual BorderStyle BorderStyle
        {
            get => borderStyle;
            set
            {
                borderStyle = value;
                UpdateSize();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "Control")]
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
        /// Gets or sets the color of detail text.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "96,96,96")]
        [Description("Gets or sets the color of detail text.")]
        public virtual Color DetailTextColor
        {
            get => detailColor;
            set
            {
                detailColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of error text.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "255,0,0")]
        [Description("Gets or sets the color of error text.")]
        public virtual Color ErrorTextColor
        {
            get => errorColor;
            set
            {
                errorColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background color of the drive free space bar.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "230, 230, 230")]
        [Description("Gets or sets the background color of the drive free space bar.")]
        public Color BarBackColor { get; set; } = Color.FromArgb(230, 230, 230);

        /// <summary>
        /// Gets or sets the fill color of the drive free space bar.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "38, 160, 218")]
        [Description("Gets or sets the fill color of the drive free space bar.")]
        public Color BarFillColor { get; set; } = Color.FromArgb(38, 160, 218);

        /// <summary>
        /// Gets or sets the fill color of the drive free space bar when the amount of free space is below the critical percentage.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "218, 38, 38")]
        [Description("Gets or sets the fill color of the drive free space bar when the amount of free space is below the critical percentage.")]
        public Color BarCriticalFillColor { get; set; } = Color.FromArgb(218, 38, 38);

        /// <summary>
        /// Gets or sets the border color of the drive free space bar.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "188, 188, 188")]
        [Description("Gets or sets the border color of the drive free space bar.")]
        public Color BarBorderColor { get; set; } = Color.FromArgb(188, 188, 188);

        /// <summary>
        /// Gets or sets critical percentage for drive free space.
        /// </summary>
        [Category("Appearance"), DefaultValue(0.9f)]
        [Description("Gets or sets critical percentage for drive free space.")]
        public float BarCriticalPercentage { get; set; } = 0.9f;

        /// <summary>
        /// Gets or sets a value indicating whether this control should redraw its surface
        /// using a secondary buffer to reduce or prevent flicker.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override bool DoubleBuffered { get => true; set => base.DoubleBuffered = true; }

        /// <summary>
        /// Gets the rectangle that represents the client area of the control.
        /// </summary>
        [Browsable(false)]
        public new Rectangle ClientRectangle
        {
            get
            {
                Rectangle rect = base.ClientRectangle;
                if (borderStyle != BorderStyle.None)
                {
                    rect.Inflate(-2, -2);
                }
                return rect;
            }
        }

        /// <summary>
        /// Gets or sets the spacing between the border of the control and its contents.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Size), "0,0")]
        [Description("Gets or sets the spacing between the border of the control and its contents.")]
        public virtual Size ContentPadding
        {
            get => contentPadding;
            set
            {
                contentPadding = value;
                UpdateSize();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the path of the file system object.
        /// </summary>
        [Category("Data")]
        [Description("Gets or sets the path of the file system object.")]
        public string Path
        {
            get => path;
            set
            {
                path = value;
                node.Path = path;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text { get => base.Text; set => base.Text = value; }

        /// <summary>
        /// Gets or sets the size of the thumbnail image.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Size), "64,64")]
        [Description("Gets or sets the size of the thumbnail image.")]
        public Size ThumbnailSize
        {
            get => thumbnailSize;
            set
            {
                thumbnailSize = value;
                node.ThumbnailSize = thumbnailSize;
                UpdateSize();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the spacing between thumbnail and the text.
        /// </summary>
        [Category("Appearance"), DefaultValue(8)]
        [Description("Gets or sets the spacing between thumbnail and the text.")]
        public int ThumbnailTextSpacing
        {
            get => thumbnailTextSpacing;
            set
            {
                thumbnailTextSpacing = value;
                UpdateSize();
                Invalidate();
            }
        }
        #endregion

        #region Constructor
        public FileSystemLabel()
        {
            DoubleBuffered = true;
            path = DriveInfo.GetDrives()[0].RootDirectory.FullName;
            node = new FileSystemNode(path);
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Measures the contents of the control.
        /// </summary>
        /// <returns>The minimum size required to fit the contents of the control.</returns>
        protected virtual Size MeasureContents()
        {
            int lines = GetLineCount();
            int lineHeight = Font.Height;
            float textHeight = (lines) * lineHeight + lineSpacing * (lines - 1) * lineHeight;
            float maxHeight = Math.Max(textHeight, thumbnailSize.Height);
            int borderOffset = (BorderStyle == BorderStyle.None ? 0 : 1);

            // Calculate item size
            iconRect = new RectangleF(borderOffset + contentPadding.Width, contentPadding.Height, thumbnailSize.Width, thumbnailSize.Height);
            textRect = new RectangleF(borderOffset + contentPadding.Width + thumbnailSize.Width + thumbnailTextSpacing, contentPadding.Height,
                        base.ClientRectangle.Width - 2 * contentPadding.Width - thumbnailSize.Width - thumbnailTextSpacing - 2 * borderOffset - 1, textHeight);
            if (thumbnailSize.Height > textHeight)
                textRect.Offset(0, (maxHeight - textRect.Height) / 2f);
            else
                iconRect.Offset(0, (maxHeight - iconRect.Height) / 2f);

            return new Size((int)(iconRect.Width + textRect.Width + 2 * contentPadding.Width + thumbnailTextSpacing), (int)(maxHeight + 2 * contentPadding.Height));
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void DrawBackground(DrawWithBoundsEventArgs e)
        {
            using (Brush back = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(back, e.Bounds);
            }
        }

        /// <summary>
        /// Paints the contents of the control.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void DrawContents(DrawWithBoundsEventArgs e)
        {
            // Draw the image
            if (node.Thumbnail != null)
            {
                Rectangle pos = Utility.GetSizedIconBounds(node.Thumbnail, Utility.ToRectangle(iconRect), 0.0f, 0.5f);
                e.Graphics.DrawImage(node.Thumbnail, pos);
            }

            // Draw item text
            int lineHeight = Font.Height;
            RectangleF lineBounds = textRect;
            lineBounds.Height = lineHeight;
            using (Brush bItemFore = new SolidBrush(ForeColor))
            using (Brush bItemDetails = new SolidBrush(detailColor))
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Trimming = StringTrimming.EllipsisCharacter;

                for (int i = 0; i < GetLineCount(); i++)
                {
                    // Draw line of text
                    DrawLine(e.Graphics, i, lineBounds, (i == 0 ? bItemFore : bItemDetails), stringFormat);

                    // Offset the bounds to the next line below
                    lineBounds.Offset(0, 1.2f * lineHeight);
                }
            }
        }

        /// <summary>
        /// Draws an error message if the path is invalid.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void DrawErrorMessage(DrawWithBoundsEventArgs e)
        {
            Rectangle bounds = e.Bounds;
            bounds.Inflate(-contentPadding.Width, -contentPadding.Height);

            // Draw item text
            using (Brush bError = new SolidBrush(errorColor))
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;
                stringFormat.Trimming = StringTrimming.EllipsisCharacter;

                e.Graphics.DrawString(node.ErrorMessage, Font, bError, bounds, stringFormat);
            }
        }

        /// <summary>
        /// Paints the border of the control.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void DrawBorder(DrawWithBoundsEventArgs e)
        {
            if (borderStyle == BorderStyle.FixedSingle)
            {
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.Flat);
            }
            else if (borderStyle == BorderStyle.Fixed3D)
            {
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedOuter);
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Updates the size of the control. The height of the control is automatically
        /// calculated from its contents.
        /// </summary>
        private void UpdateSize()
        {
            // Trigger a size change so that overriden SetBoundsCore is called
            this.Height = 0;
        }

        /// <summary>
        /// Gets the number of content lines to draw.
        /// </summary>
        /// <returns>The number of content lines.</returns>
        private int GetLineCount()
        {
            switch (node.Type)
            {
                case NodeType.Drive:
                    if (node.DriveType == DriveType.Fixed || node.DriveType == DriveType.Network || node.DriveType == DriveType.Ram || node.DriveType == DriveType.Removable)
                        return 3;
                    else
                        return 1;
                case NodeType.Directory:
                    return 2;
                case NodeType.File:
                    return 4;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets the contents of a given line.
        /// </summary>
        /// <param name="lineIndex">The 0 based index of the line to draw.</param>
        /// <returns>Line contents.</returns>
        private string GetLineContents(int lineIndex)
        {
            switch (lineIndex)
            {
                case 0:
                    // Display name
                    return node.DisplayName;
                case 1:
                    if (node.Type == NodeType.Drive)
                    {
                        // Free space indicator will be drawn in DrawLine()
                        return "";
                    }
                    else
                    {
                        // Full path
                        return node.FullName;
                    }
                case 2:
                    if (node.Type == NodeType.Drive)
                    {
                        // Free space text
                        return string.Format("{0} free of {1}", Utility.FormatSize(node.DriveFreeSpace), Utility.FormatSize(node.DriveSize));
                    }
                    else
                    {
                        // Last modified date
                        return node.DateModified.ToString("g");
                    }
                case 3:
                    // File size
                    return Utility.FormatSize(node.FileSize);
                default:
                    return "";
            }
        }

        /// <summary>
        /// Draws a line of the content.
        /// </summary>
        /// <param name="g">The graphics to draw on.</param>
        /// <param name="lineIndex">The 0 based index of the line to draw.</param>
        /// <param name="bounds">The bounding rectangle of the line of text.</param>
        /// <param name="brush">The brush to use when drawing text.</param>
        /// <param name="format">The string format to use.</param>
        private void DrawLine(Graphics g, int lineIndex, RectangleF bounds, Brush brush, StringFormat format)
        {
            if (lineIndex == 1 && node.Type == NodeType.Drive)
            {
                // Free space indicator
                float percentFull = (node.DriveSize - node.DriveFreeSpace) / (float)node.DriveSize;
                using (Brush bBarBack = new SolidBrush(BarBackColor))
                using (Brush bBarFill = new SolidBrush(percentFull > 0.9f ? BarCriticalFillColor : BarFillColor))
                using (Pen pBarBorder = new Pen(BarBorderColor))
                {
                    g.FillRectangle(bBarBack, bounds);
                    g.FillRectangle(bBarFill, new RectangleF(bounds.Left, bounds.Top, bounds.Width * percentFull, bounds.Height));
                    g.DrawRectangle(pBarBorder, Utility.ToRectangle(bounds));
                }
            }
            else
            {
                string text = GetLineContents(lineIndex);
                g.DrawString(text, Font, brush, bounds, format);
            }
        }
        #endregion

        #region Overriden Methods
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Size sz = MeasureContents();

            if (borderStyle != BorderStyle.None)
                sz = new Size(sz.Width + 2, sz.Height + 2);

            base.SetBoundsCore(x, y, width, Math.Max(23, sz.Height), specified);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            MeasureContents();

            DrawBackground(new DrawWithBoundsEventArgs(e.Graphics, base.ClientRectangle));
            DrawBorder(new DrawWithBoundsEventArgs(e.Graphics, base.ClientRectangle));

            if (node.IsPathValid)
                DrawContents(new DrawWithBoundsEventArgs(e.Graphics, ClientRectangle));
            else
                DrawErrorMessage(new DrawWithBoundsEventArgs(e.Graphics, ClientRectangle));
        }
        #endregion

        #region Control Designer
        internal class FileSystemLabelDesigner : ControlDesigner
        {
            private FileSystemLabelDesigner()
            {
                base.AutoResizeHandles = true;
            }
            public override SelectionRules SelectionRules
            {
                get
                {
                    return SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable;
                }
            }
        }
        #endregion
    }
}
