using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Manina.Windows.Forms
{
    [Designer(typeof(FileSystemLabelDesigner))]
    public class FileSystemLabel : Control
    {
        #region Member Variables
        private string path = "";
        internal FileSystemNode node = null;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the renderer associated with this control.
        /// </summary>
        [Browsable(false)]
        [Description("Gets the renderer associated with this control.")]
        public FileSystemNodeRenderer Renderer { get; } = new FileSystemNodeRenderer();

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "Control")]
        [Description("Gets or sets the background color for the control.")]
        public override Color BackColor { get => Renderer.BackColor; set => Renderer.BackColor = value; }

        /// <summary>
        /// Gets or sets the foreground color for the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "ControlText")]
        [Description("Gets or sets the foreground color for the control.")]
        public override Color ForeColor { get => Renderer.ForeColor; set => Renderer.ForeColor = value; }

        /// <summary>
        /// Gets or sets the fill color of the drive free space bar when the amount of free space is below the critical percentage.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "218, 38, 38")]
        [Description("Gets or sets the fill color of the drive free space bar when the amount of free space is below the critical percentage.")]
        public Color BarCriticalFillColor { get; set; } = Color.FromArgb(218, 38, 38);

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
        #endregion

        #region Constructor
        public FileSystemLabel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);

            path = DriveInfo.GetDrives()[0].RootDirectory.FullName;
            node = new FileSystemNode(path);

            Renderer.BackColor = SystemColors.Control;
            Renderer.ForeColor = SystemColors.ControlText;
            Renderer.BorderStyle = BorderStyle.None;
            Renderer.ThumbnailSize = new Size(64, 64);

            Renderer.GetLineCount += Renderer_GetLineCount;
            Renderer.GetLineContents += Renderer_GetLineContents;
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

        private void Renderer_GetLineCount(object sender, FileSystemNodeRenderer.GetLineCountEventArgs e)
        {
            switch (e.Node.Type)
            {
                case NodeType.Drive:
                    if (e.Node.DriveType == DriveType.Fixed || e.Node.DriveType == DriveType.Network || e.Node.DriveType == DriveType.Ram || e.Node.DriveType == DriveType.Removable)
                        e.LineCount = 3;
                    else
                        e.LineCount = 1;
                    break;
                case NodeType.Directory:
                    e.LineCount = 2;
                    break;
                case NodeType.File:
                    e.LineCount = 4;
                    break;
                default:
                    e.LineCount = 0;
                    break;
            }
        }

        private void Renderer_GetLineContents(object sender, FileSystemNodeRenderer.GetLineContentsEventArgs e)
        {
            switch (e.LineIndex)
            {
                case 0:
                    // Display name
                    e.Contents = e.Node.DisplayName;
                    break;
                case 1:
                    if (e.Node.Type == NodeType.Drive)
                    {
                        // Free space indicator
                        e.ShowBar = true;
                        e.BarPercentage = (e.Node.DriveSize - e.Node.DriveFreeSpace) / (float)e.Node.DriveSize;
                        if (e.BarPercentage > BarCriticalPercentage)
                            e.Color = BarCriticalFillColor;
                    }
                    else
                    {
                        // Full path
                        e.Contents = e.Node.FullName;
                    }
                    break;
                case 2:
                    if (e.Node.Type == NodeType.Drive)
                    {
                        // Free space text
                        e.Contents = string.Format("{0} free of {1}", Utility.FormatSize(e.Node.DriveFreeSpace), Utility.FormatSize(e.Node.DriveSize));
                    }
                    else
                    {
                        // Last modified date
                        e.Contents = e.Node.DateModified.ToString("g");
                    }
                    break;
                case 3:
                    // File size
                    e.Contents = Utility.FormatSize(e.Node.FileSize);
                    break;
                default:
                    e.Contents = "";
                    break;
            }
        }
        #endregion

        #region Overriden Methods
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int itemHeight = Renderer.GetItemHeight(node);

            base.SetBoundsCore(x, y, width, Math.Max(23, itemHeight), specified);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Renderer.DrawItem(e.Graphics, ClientRectangle, node, Enabled, false, false, true);
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
