﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Manina.Windows.Forms
{
    [ToolboxBitmap(typeof(DriveListBox))]
    public class DriveListBox : ListBox
    {
        #region Member Variables
        private DriveType driveTypes = DriveType.All;
        private ObjectCollection items;

        private int hoveredItemIndex = -1;

        private FileSystemNodeRenderer renderer = new FileSystemNodeRenderer();
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the drawing mode for the control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DrawMode DrawMode { get => DrawMode.OwnerDrawVariable; set => base.DrawMode = DrawMode.OwnerDrawVariable; }

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
        /// Gets the items of the listbox.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new ObjectCollection Items { get => items; }

        /// <summary>
        /// Gets a collection that contains the zero-based indices of selected items.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new SelectedIndexCollection SelectedIndices { get => base.SelectedIndices; }

        /// <summary>
        /// Gets a collection that contains the selected items.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new SelectedObjectCollection SelectedItems { get => base.SelectedItems; }

        /// <summary>
        /// Gets or sets the currently selected item.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new object SelectedItem { get => base.SelectedItem; set => base.SelectedItem = value; }

        /// <summary>
        /// Gets or sets the index of the currently selected item.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override int SelectedIndex { get => base.SelectedIndex; set => base.SelectedIndex = value; }

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

        /// <summary>
        /// Gets or sets the root paths of selected drive.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedDrive
        {
            get
            {
                if (base.SelectedItem == null)
                    return null;
                else
                    return (base.SelectedItem as FileSystemNode).Path;
            }
            set
            {
                base.SelectedItems.Clear();

                int i = 0;
                foreach (object item in items)
                {
                    if (string.Compare((item as FileSystemNode).Path, value, true) == 0)
                    {
                        SelectedIndices.Add(i);
                        break;
                    }
                    i++;
                }
            }
        }

        /// <summary>
        /// Gets or sets the root paths of selected drives.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] SelectedDrives
        {
            get
            {
                string[] drives = new string[base.SelectedItems.Count];

                int i = 0;
                foreach (object item in base.SelectedItems)
                {
                    drives[i] = (item as FileSystemNode).Path;
                    i++;
                }
                return drives;
            }
            set
            {
                base.SelectedItems.Clear();

                int i = 0;
                foreach (object item in items)
                {
                    foreach (string path in value)
                    {
                        if (string.Compare((item as FileSystemNode).Path, path, true) == 0)
                        {
                            SelectedIndices.Add(i);
                            break;
                        }
                    }
                    i++;
                }
            }
        }
        #endregion

        #region Constructor
        public DriveListBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;

            DrawMode = DrawMode.OwnerDrawVariable;
            RefreshDriveList();

            ContentPadding = new Size(4, 4);

            ShowFreeSpaceText = true;
            ShowFreeSpaceBar = true;

            renderer.GetLineCount += Renderer_GetLineCount;
            renderer.GetLineContents += Renderer_GetLineContents;
            renderer.GetLineHeight += Renderer_GetLineHeight;
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Refreshes the drive list.
        /// </summary>
        public void RefreshDriveList()
        {
            items = new ObjectCollection(this);

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

                items.Add(new FileSystemNode(drive.RootDirectory.FullName, ThumbnailSize));
            }
            base.SetItemsCore(items);
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
            bool hovered = hoveredItemIndex == e.Index;

            renderer.DrawItem(e.Graphics, e.Bounds, node, Enabled,
                (e.State & DrawItemState.Selected) == DrawItemState.Selected, hovered, Focused);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int index = IndexFromPoint(e.Location);
            if (index != hoveredItemIndex)
            {
                hoveredItemIndex = index;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (hoveredItemIndex > -1)
            {
                hoveredItemIndex = -1;
                Invalidate();
            }
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
    }
}
