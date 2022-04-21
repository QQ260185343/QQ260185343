using System;
using System.IO;
/// <summary>
/// º”√‹Ω‚√‹Editor
/// </summary>
/// 
namespace AssetBundleBrowser
{
    public class EncryStreamEditor : FileStream
    {

        const byte KEY = 110;
        public EncryStreamEditor(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync) : base(path, mode, access, share, bufferSize, useAsync)
        {
        }
        public EncryStreamEditor(string path, FileMode mode) : base(path, mode)
        {
        }
        public override int Read(byte[] array, int offset, int count)
        {
            var index = base.Read(array, offset, count);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] ^= KEY;
            }
            return index;
        }
        public override void Write(byte[] array, int offset, int count)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] ^= KEY;
            }
            base.Write(array, offset, count);
        }
    }
}