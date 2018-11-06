using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Manina.Windows.Forms
{
    public class FileSystemNode
    {
        private string path = "";
        private Size thumbnailSize = new Size(64, 64);

        public string Path { get => path; set { path = value; UpdateNode(); } }
        public Size ThumbnailSize { get => thumbnailSize; set { thumbnailSize = value; UpdateNode(); } }

        public string DisplayName { get; private set; }
        public string FullName { get; private set; }
        public Image Thumbnail { get; private set; }
        public string FileType { get; private set; }
        public Image SmallIcon { get; private set; }
        public Image LargeIcon { get; private set; }

        public NodeType Type { get; private set; }

        public DateTime DateCreated { get; private set; }
        public DateTime DateAccessed { get; private set; }
        public DateTime DateModified { get; private set; }
        public long FileSize { get; private set; }

        public long DriveFreeSpace { get; private set; }
        public long DriveSize { get; private set; }
        public DriveType DriveType { get; private set; }
        public string DriveFormat { get; private set; }

        public bool IsPathValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public FileSystemNode(string nodePath, Size thumbSize)
        {
            path = nodePath;
            thumbnailSize = thumbSize;
            UpdateNode();
        }

        public FileSystemNode(string nodePath)
        {
            path = nodePath;
            UpdateNode();
        }

        public FileSystemNode()
        {
            path = DriveInfo.GetDrives()[0].RootDirectory.FullName;
            UpdateNode();
        }

        private void ResetProperties()
        {
            DisplayName = "";
            FullName = "";
            Thumbnail = null;
            FileType = "";
            SmallIcon = null;
            LargeIcon = null;

            DateCreated = DateTime.MinValue;
            DateAccessed = DateTime.MinValue;
            DateModified = DateTime.MinValue;
            FileSize = 0;

            DriveFreeSpace = 0;
            DriveSize = 0;
            DriveType = DriveType.None;
            DriveFormat = "";
        }

        private void UpdateNode()
        {
            ResetProperties();

            try
            {

                NativeMethods.SHCreateItemFromParsingName(path, IntPtr.Zero, NativeMethods.IID_IShellItem, out var shItem);

                // Display name
                shItem.GetDisplayName(NativeMethods.SIGDN.NORMALDISPLAY, out var ptrDisplay);
                DisplayName = Marshal.PtrToStringAuto(ptrDisplay);

                // Full name
                shItem.GetDisplayName(NativeMethods.SIGDN.FILESYSPATH, out var ptrFull);
                FullName = Marshal.PtrToStringAuto(ptrFull);

                // Large thumbnail
                ((NativeMethods.IShellItemImageFactory)shItem).GetImage(new NativeMethods.SIZE(thumbnailSize.Width, thumbnailSize.Height), NativeMethods.SIIGBF.NONE, out var hbitmap);
                Thumbnail = ConvertToAlphaBitmap(Bitmap.FromHbitmap(hbitmap));

                // Get the small icon and shell file type
                NativeMethods.SHGFI flags = NativeMethods.SHGFI.Icon | NativeMethods.SHGFI.SmallIcon | NativeMethods.SHGFI.TypeName | NativeMethods.SHGFI.UseFileAttributes;
                IntPtr hImg = NativeMethods.SHGetFileInfo(path, FileAttributes.Normal, out var shinfo, (uint)Marshal.SizeOf<NativeMethods.SHFILEINFO>(), flags);

                // Get mime type
                FileType = shinfo.szTypeName;

                // Get small icon 
                if (hImg != IntPtr.Zero && shinfo.hIcon != IntPtr.Zero)
                {
                    using (Icon newIcon = Icon.FromHandle(shinfo.hIcon))
                    {
                        SmallIcon = newIcon.ToBitmap();
                    }
                    NativeMethods.DestroyIcon(shinfo.hIcon);
                }
                else
                    SmallIcon = null;

                // Get large icon
                flags = NativeMethods.SHGFI.Icon | NativeMethods.SHGFI.LargeIcon | NativeMethods.SHGFI.UseFileAttributes;
                hImg = NativeMethods.SHGetFileInfo(path, FileAttributes.Normal, out shinfo, (uint)Marshal.SizeOf<NativeMethods.SHFILEINFO>(), flags);

                if (hImg != IntPtr.Zero && shinfo.hIcon != IntPtr.Zero)
                {
                    using (Icon newIcon = Icon.FromHandle(shinfo.hIcon))
                    {
                        LargeIcon = newIcon.ToBitmap();
                    }
                    NativeMethods.DestroyIcon(shinfo.hIcon);
                }

                if (string.Compare(FullName, System.IO.Path.GetPathRoot(FullName), true) == 0 && Directory.Exists(path))
                {
                    Type = NodeType.Drive;

                    DriveInfo info = new DriveInfo(path.TrimEnd('\\').TrimEnd(':'));

                    DriveFreeSpace = info.TotalFreeSpace;
                    DriveSize = info.TotalSize;
                    DriveType = info.DriveType == System.IO.DriveType.CDRom ? DriveType.CDRom : info.DriveType == System.IO.DriveType.Fixed ? 
                        DriveType.Fixed : info.DriveType == System.IO.DriveType.Network ? DriveType.Network : info.DriveType == System.IO.DriveType.Ram ? 
                        DriveType.Ram : info.DriveType == System.IO.DriveType.Removable ? DriveType.Removable : DriveType.None;
                    DriveFormat = info.DriveFormat;

                    DateCreated = DateTime.MinValue;
                    DateAccessed = DateTime.MinValue;
                    DateModified = DateTime.MinValue;
                    FileSize = 0;
                }
                else if (Directory.Exists(path))
                {
                    Type = NodeType.Directory;

                    DirectoryInfo info = new DirectoryInfo(path);
                    DateCreated = info.CreationTime;
                    DateAccessed = info.LastAccessTime;
                    DateModified = info.LastWriteTime;
                    FileSize = 0;

                    DriveFreeSpace = 0;
                    DriveSize = 0;
                }
                else if (File.Exists(path))
                {
                    Type = NodeType.File;

                    FileInfo info = new FileInfo(path);
                    DateCreated = info.CreationTime;
                    DateAccessed = info.LastAccessTime;
                    DateModified = info.LastWriteTime;
                    FileSize = info.Length;

                    DriveFreeSpace = 0;
                    DriveSize = 0;
                }

                IsPathValid = true;
                ErrorMessage = "";
            }
            catch (Exception e)
            {
                ResetProperties();

                IsPathValid = false;
                ErrorMessage = e.Message;
            }
        }

        private Bitmap ConvertToAlphaBitmap(Bitmap source)
        {
            Bitmap result = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);

            BitmapData sourceData = null;
            BitmapData resultData = null;
            try
            {
                sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, source.PixelFormat);
                resultData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, result.PixelFormat);

                for (int y = 0; y <= sourceData.Height - 1; y++)
                {
                    for (int x = 0; x <= sourceData.Width - 1; x++)
                    {
                        int color = Marshal.ReadInt32(sourceData.Scan0, (sourceData.Stride * y) + (4 * x));
                        Marshal.WriteInt32(resultData.Scan0, (resultData.Stride * y) + (4 * x), color);
                    }
                }
            }
            finally
            {
                result.UnlockBits(resultData);
                source.UnlockBits(sourceData);
            }

            return result;
        }
    }
}
