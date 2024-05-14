namespace Mailtrap.NET.SDK.DataStructures
{
    /// <summary>
    /// Used for managing disposable items in the list.
    /// Disposes all items in the list automatically, preventing client code from producing memory leaks.
    /// </summary>
    public class DisposableStreamReaderList : List<(StreamReader reader, string fileName)>, IDisposable
    {
        private bool disposed = false;

        public DisposableStreamReaderList()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    foreach (var item in this)
                    {
                        item.reader.Dispose();
                    }
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableStreamReaderList()
        {
            Dispose(false);
        }

        public static DisposableStreamReaderList FromList(List<(StreamReader reader, string fileName)> list)
        {
            var result = new DisposableStreamReaderList();
            result.AddRange(list);
            return result;
        }
    }
}
