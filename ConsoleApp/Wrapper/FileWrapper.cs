namespace ConsoleApp;
class FileWrapper : IFile
{
    public FileStream OpenRead(string path)
        => File.OpenRead(path);

    public bool Exists(string path)
        => File.Exists(path);
}

interface IFile
{
    FileStream OpenRead(string path);
    bool Exists(string path);
}