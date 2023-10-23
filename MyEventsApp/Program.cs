using MyEventsApp;
using System.Text;

/// <summary>
/// Написать обобщённую функцию расширения, находящую и возвращающую максимальный элемент коллекции.
/// Функция должна принимать на вход делегат, преобразующий входной тип в число для возможности поиска максимального значения.
/// public static T GetMax(this IEnumerable e, Func<T, float> getParameter) where T : class;
/// </summary>
public class GetMaxItemInCollection
{
    private static readonly List<string> _collection = new()
    {
        "Hello World",
        "Californian Soil",
        "Carribbean Blue",
        "Lips as Cold as Diamond",
        "Californian Carpark Concrete",
        "Blues Is My Best Friend"
    };

    public static string GetCollectionForConsole()
    {
        var sb = new StringBuilder();
        int i = 0;
        foreach (var v in _collection)
            sb.AppendLine($"{++i}. {v}");
        return sb.ToString();
    }

    public static string GetMax()
    {
        return _collection.GetMax(GetParameter);
    }

    /// <summary>
    /// Определяем максимальный элемент по длине строки.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private static float GetParameter(string s)
    {
        return (s == null) ? -1 : s.Length;
    }
}

internal class Program
{
    /// <summary>
    /// Событие: файл был найден.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">Тут будет имя файла</param>
    private static void Event_OnFileFound(object sender, EventArgs e)
    {
        // выводим в консоль сообщение с полным именем файла, возникающие при срабатывании события (файл найден)
        Console.WriteLine(((FileArgs)e).FileName);
        Console.Write("Press X to stop or any other key to continue: ");
        
        var key = Console.ReadKey().Key;
        if (key == ConsoleKey.X)
            ((DirectoryFileChecker)sender).IsStopSearching = true;
        else
            Console.WriteLine();
    }

    /// <summary>
    /// Точка входа.
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        // выводим коллекцию и результат поиска максимального элемента
        Console.WriteLine("Collection:");
        Console.WriteLine(GetMaxItemInCollection.GetCollectionForConsole());
        var maxItem = GetMaxItemInCollection.GetMax();
        Console.WriteLine($"\nMax collection item: {maxItem}");
        Console.WriteLine();

        // обходим каталог файлов и выдаем событие при нахождении каждого файла
        var dir = new DirectoryFileChecker();
        dir.FileFound += Event_OnFileFound;
        Console.WriteLine("\nLet's start searching for files:");
        dir.Check(Directory.GetCurrentDirectory());
        dir.FileFound -= Event_OnFileFound;
    }
}
