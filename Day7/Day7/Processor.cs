using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7;

internal class Processor
{
    private readonly Folder? _rootFolder = new() { Name = "/" };
    private Folder? _currentFolder;
    
    private const ulong TotalBounds = 100000;
    private ulong _totalOfSize = 0;

    private const ulong TotalDiskSpace = 70000000;
    private const ulong RequiredSpace = 30000000;
    private ulong _additionalSpaceRequired;

    private Folder? _nextLargerFolder;

    public void Run()
    {
        _currentFolder = _rootFolder;
        BuildTree();
        BuildFolderSize(_rootFolder);
        ReportFolderSizes(_rootFolder);

        Console.WriteLine($"\nTotal under {TotalBounds}: {_totalOfSize}");

        ulong totalAvailableSpace = TotalDiskSpace - _rootFolder.TotalSize;
        _additionalSpaceRequired = RequiredSpace - totalAvailableSpace;

        Console.WriteLine($"\nRequired space: {_additionalSpaceRequired}");

        _nextLargerFolder = _rootFolder;
        NextLargerFolder(_rootFolder);

        Console.WriteLine($"\nSize of next larger folder: {_nextLargerFolder.Name} - {_nextLargerFolder.TotalSize}");
    }

    private void NextLargerFolder(Folder folder)
    {
        if (folder.TotalSize > _additionalSpaceRequired)
        {
            if (folder.TotalSize < _nextLargerFolder.TotalSize)
                _nextLargerFolder = folder;
        }

        if (!folder.Children.Any()) return;

        foreach (var child in folder.Children)
        {
            NextLargerFolder(child);
        }
    }

    private void ReportFolderSizes(Folder folder)
    {
        Console.WriteLine($"Folder: {folder.Name}, Total size: {folder.TotalSize}");

        if (folder.TotalSize <= TotalBounds)
            _totalOfSize += folder.TotalSize;

        if (!folder.Children.Any()) return;
        
        foreach (var child in folder.Children)
        {
            ReportFolderSizes(child);
        }
    }

    private ulong BuildFolderSize(Folder folder)
    {
        ulong fileSizeTotal = 0;
        foreach (var file in folder.UnitFiles)
        {
            fileSizeTotal += file.Size;
        }

        if (!folder.Children.Any()) return fileSizeTotal;

        foreach (var child in folder.Children)
        {
            fileSizeTotal += BuildFolderSize(child);
        }

        folder.TotalSize = fileSizeTotal;
        return fileSizeTotal;
    }

    private void BuildTree()
    {
        foreach (string line in File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day7\Day7\data.txt"))
        {
            if (line.StartsWith('$'))
            {
                ExecuteCommand(line);
            }
            else
            {
                ProcessFileOrFolder(line);
            }
        }
    }

    private void ExecuteCommand(string line)
    {
        if (_currentFolder == null)
            throw new ArgumentNullException(nameof(_currentFolder));

        var command = line.Split(' ');
        switch (command)
        {
            case ["$", "ls"]:
                return;
            case ["$", "cd", var folderName]:
                if (folderName == "/")
                {
                    _currentFolder = _rootFolder;
                }
                else if (folderName == "..")
                {
                    _currentFolder = _currentFolder?.Parent ?? _rootFolder;
                }
                else
                {
                    var folder = _currentFolder.Children.First(c => c.Name == folderName);
                    _currentFolder = folder;
                }
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void ProcessFileOrFolder(string line)
    {
        if (_currentFolder == null)
            throw new ArgumentNullException(nameof(_currentFolder));

        var command = line.Split(' ');
        switch (command)
        {
            case ["dir", var folderName]:
                _currentFolder.Children.Add(new Folder(folderName, _currentFolder));
                break;
            case [var size, var fileName]:
                ulong fileSize = ulong.Parse(size);
                _currentFolder.TotalSize += fileSize;
                _currentFolder.UnitFiles.Add(new UnitFiles(fileName, fileSize));
                break;
            default:
                throw new NotImplementedException();
        }
    }
}