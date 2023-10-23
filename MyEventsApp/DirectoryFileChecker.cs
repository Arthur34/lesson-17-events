using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventsApp
{
    /// <summary>
    /// Написать класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла;
    /// Оформить событие и его аргументы с использованием.NET соглашений:
    /// public event EventHandler FileFound;
    /// FileArgs – будет содержать имя файла и наследоваться от EventArgs
    /// Добавить возможность отмены дальнейшего поиска из обработчика;
    /// </summary>
    public class DirectoryFileChecker
    {
        public event EventHandler FileFound;
        public bool IsStopSearching { get; set; } = false;

        public void Check(string path)
        {
            if (!Directory.Exists(path))
                return;

            foreach (var file in Directory.GetFiles(path))
            {
                if (!IsStopSearching)
                    FileFound?.Invoke(this, new FileArgs(file));
                else
                    break;
            }
        }
    }

    public class FileArgs : EventArgs
    {
        public string FileName { get; set; }
        public FileArgs(string fileName) => FileName = fileName;
    }
}
