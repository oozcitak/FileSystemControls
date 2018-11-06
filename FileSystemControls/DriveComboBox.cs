using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Manina.Windows.Forms
{
    [Designer(typeof(DriveComboBoxDesigner))]
    public class DriveComboBox : ComboBox
    {
        #region Member Variables
        private DriveType driveTypes = DriveType.All;
        private ObjectCollection items;

        private FileSystemNodeRenderer renderer = new FileSystemNodeRenderer();

        private int maxItemHeight = 0;
        private bool initHeight = false;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the drawing mode for the control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DrawMode DrawMode { get => DrawMode.OwnerDrawVariable; set => base.DrawMode = DrawMode.OwnerDrawVariable; }

        /// <summary>
        /// Gets or sets a value specifying the style of the combo box.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBoxStyle DropDownStyle { get => ComboBoxStyle.DropDownList; set => base.DropDownStyle = ComboBoxStyle.DropDownList; }

        /// <summary>
        /// Gets or sets the drive types to show.
        /// </summary>
        [Category("Behaviour"), DefaultValue(typeof(DriveType), "All")]
        [Description("Gets or sets the drive types to show.")]
        public DriveType DriveTypes
        {
            get => driveTypes;
            set
            {
                driveTypes = value;
                RefreshDriveList();
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text { get => base.Text; set => base.Text = value; }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "252, 255, 255")]
        [Description("Gets or sets the background color for the control.")]
        public override Color BackColor { get => renderer.BackColor; set => renderer.BackColor = value; }

        /// <summary>
        /// Gets or sets the foreground color for the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "0, 0, 0")]
        [Description("Gets or sets the foreground color for the control.")]
        public override Color ForeColor { get => renderer.ForeColor; set => renderer.ForeColor = value; }

        /// <summary>
        /// Gets or sets the color of detail text.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "96, 96, 96")]
        [Description("Gets or sets the color of detail text.")]
        public Color DetailTextColor { get => renderer.DetailTextColor; set => renderer.DetailTextColor = value; }

        /// <summary>
        /// Gets or sets the color of error text.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "255, 0, 0")]
        [Description("Gets or sets the color of error text.")]
        public Color ErrorTextColor { get => renderer.ErrorTextColor; set => renderer.ErrorTextColor = value; }

        /// <summary>
        /// Gets or sets the size of the thumbnail image.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Size), "32, 32")]
        [Description("Gets or sets the size of the thumbnail image.")]
        public Size ThumbnailSize { get => renderer.ThumbnailSize; set => renderer.ThumbnailSize = value; }

        /// <summary>
        /// Gets or sets the spacing between thumbnail and the text.
        /// </summary>
        [Category("Appearance"), DefaultValue(2)]
        [Description("Gets or sets the spacing between thumbnail and the text.")]
        public int ThumbnailTextSpacing { get => renderer.ThumbnailTextSpacing; set => renderer.ThumbnailTextSpacing = value; }

        /// <summary>
        /// Gets or sets the spacing between the border of the control and its contents.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Size), "4, 4")]
        [Description("Gets or sets the spacing between the border of the control and its contents.")]
        public Size ContentPadding { get => renderer.ContentPadding; set => renderer.ContentPadding = value; }

        /// <summary>
        /// Gets or sets the background color for selected items when the control has lost focus.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "217, 217, 217")]
        [Description("Gets or sets the background color for selected items when the control has lost focus.")]
        public Color UnfocusedItemBackColor { get => renderer.UnfocusedItemBackColor; set => renderer.UnfocusedItemBackColor = value; }

        /// <summary>
        /// Gets or sets the background color of selected items.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "204, 232, 255")]
        [Description("Gets or sets the background color of selected items.")]
        public Color SelectedItemBackColor { get => renderer.SelectedItemBackColor; set => renderer.SelectedItemBackColor = value; }

        /// <summary>
        /// Gets or sets the background color for hovered items.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "229, 243, 255")]
        [Description("Gets or sets the background color for hovered items.")]
        public Color HoveredItemBackColor { get => renderer.HoveredItemBackColor; set => renderer.HoveredItemBackColor = value; }

        /// <summary>
        /// Gets or sets the border color for selected items.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "153, 209, 255")]
        [Description("Gets or sets the border color for selected items.")]
        public Color SelectedItemBorderColor { get => renderer.SelectedItemBorderColor; set => renderer.SelectedItemBorderColor = value; }

        /// <summary>
        /// Gets the items of the listbox.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ObjectCollection Items { get => items; }

        /// <summary>
        /// Gets or sets whether drive free space text is shown.
        /// </summary>
        [Category("Appearance"), DefaultValue(true)]
        [Description("Gets or sets whether drive free space text is shown.")]
        public bool ShowFreeSpaceText { get; set; }

        /// <summary>
        /// Gets or sets whether drive free space bar is shown.
        /// </summary>
        [Category("Appearance"), DefaultValue(true)]
        [Description("Gets or sets whether drive free space bar is shown.")]
        public bool ShowFreeSpaceBar { get; set; }
        #endregion

        #region Constructor
        public DriveComboBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;

            DrawMode = DrawMode.OwnerDrawVariable;
            DropDownStyle = ComboBoxStyle.DropDownList;

            ContentPadding = new Size(4, 4);

            ShowFreeSpaceText = true;
            ShowFreeSpaceBar = true;

            renderer.GetLineCount += Renderer_GetLineCount;
            renderer.GetLineContents += Renderer_GetLineContents;
            renderer.GetLineHeight += Renderer_GetLineHeight;

            RefreshDriveList();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Refreshes the drive list.
        /// </summary>
        public void RefreshDriveList()
        {
            items = new ObjectCollection(this);

            maxItemHeight = 0;
            foreach (var drive in System.IO.DriveInfo.GetDrives())
            {
                var type = drive.DriveType;
                if (type == System.IO.DriveType.Fixed && (driveTypes & DriveType.Fixed) == DriveType.None)
                    continue;
                else if (type == System.IO.DriveType.Removable && (driveTypes & DriveType.Removable) == DriveType.None)
                    continue;
                else if (type == System.IO.DriveType.Network && (driveTypes & DriveType.Network) == DriveType.None)
                    continue;
                else if (type == System.IO.DriveType.CDRom && (driveTypes & DriveType.CDRom) == DriveType.None)
                    continue;
                else if (type == System.IO.DriveType.Ram && (driveTypes & DriveType.Ram) == DriveType.None)
                    continue;

                var node = new FileSystemNode(drive.RootDirectory.FullName, ThumbnailSize);
                items.Add(node);

                maxItemHeight = Math.Max(maxItemHeight, renderer.GetItemHeight(node));
            }

            base.SetItemsCore(items);
            if (maxItemHeight > 0)
                ItemHeight = maxItemHeight;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (!initHeight)
            {
                initHeight = true;
                if (maxItemHeight > 0)
                    ItemHeight = maxItemHeight;
            }
        }

        private void Renderer_GetLineCount(object sender, FileSystemNodeRenderer.GetLineCountEventArgs e)
        {
            int count = 1;
            if (e.Node.DriveType == DriveType.Fixed || e.Node.DriveType == DriveType.Network || e.Node.DriveType == DriveType.Ram || e.Node.DriveType == DriveType.Removable)
            {
                if (ShowFreeSpaceText) count++;
                if (ShowFreeSpaceBar) count++;
            }
            e.LineCount = count;
        }

        private void Renderer_GetLineHeight(object sender, FileSystemNodeRenderer.GetLineHeightEventArgs e)
        {
            if (e.LineIndex == 1 && ShowFreeSpaceText && ShowFreeSpaceBar)
                e.LineHeight = Font.Height * 0.75f;
            else if (e.LineIndex == 1 && ShowFreeSpaceBar)
                e.LineHeight = Font.Height * 0.75f;
            else
                e.LineHeight = Font.Height;
        }

        private void Renderer_GetLineContents(object sender, FileSystemNodeRenderer.GetLineContentsEventArgs e)
        {
            if (e.LineIndex == 0)
            {
                e.Contents = e.Node.DisplayName;
            }
            else if (e.LineIndex == 1 && ShowFreeSpaceText && ShowFreeSpaceBar)
            {
                e.ShowBar = true;
                e.BarPercentage = (e.Node.DriveSize - e.Node.DriveFreeSpace) / (float)e.Node.DriveSize;
            }
            else if (e.LineIndex == 1 && ShowFreeSpaceText)
            {
                e.Contents = string.Format("{0} free of {1}", Utility.FormatSize(e.Node.DriveFreeSpace), Utility.FormatSize(e.Node.DriveSize));
            }
            else if (e.LineIndex == 1 && ShowFreeSpaceBar)
            {
                e.ShowBar = true;
                e.BarPercentage = (e.Node.DriveSize - e.Node.DriveFreeSpace) / (float)e.Node.DriveSize;
            }
            else if (e.LineIndex == 2)
            {
                e.Contents = string.Format("{0} free of {1}", Utility.FormatSize(e.Node.DriveFreeSpace), Utility.FormatSize(e.Node.DriveSize));
            }
        }
        #endregion

        #region Overriden Methods
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (e.Index < 0 || e.Index > Items.Count - 1)
                return;

            var node = Items[e.Index] as FileSystemNode;
            e.ItemHeight = renderer.GetItemHeight(node);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index > Items.Count - 1)
                return;

            var node = Items[e.Index] as FileSystemNode;

            renderer.DrawItem(e.Graphics, e.Bounds, node, Enabled,
                (e.State & DrawItemState.Selected) == DrawItemState.Selected,
                (e.State & DrawItemState.HotLight) == DrawItemState.HotLight, Focused);
        }

        [Obsolete]
        protected override void AddItemsCore(object[] value)
        {
            ;
        }

        protected override void SetItemCore(int index, object value)
        {
            ;
        }
        #endregion

        #region Control Designer
        internal class DriveComboBoxDesigner : ControlDesigner
        {
            private DriveComboBoxDesigner()
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
