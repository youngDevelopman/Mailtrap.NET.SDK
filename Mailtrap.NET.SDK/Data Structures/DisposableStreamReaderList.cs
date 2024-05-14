using System.Security.Cryptography;

namespace Mailtrap.NET.SDK.DataStructures
{
    /// <summary>
    /// Used for managing disposable items in the list.
    /// Disposes all items in the list automatically, preventing client code from producing memory leaks.
    /// </summary>
    public class DisposableStreamReaderList: List<(StreamReader reader, string fileName)>, IDisposable
    {
        public DisposableStreamReaderList()
        {
        }

        public void Dispose()
        {
            foreach (var item in this)
            {
                item.reader.Dispose();
            }
        }

        public static DisposableStreamReaderList FromList(List<(StreamReader reader, string fileName)> list)
        {
            var result = new DisposableStreamReaderList();
            result.AddRange(list);
            return result;
        }
    }
}
