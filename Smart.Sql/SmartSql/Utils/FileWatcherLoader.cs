﻿using System;
using System.Collections.Generic;
using System.IO;

namespace SmartSql.Utils
{
    /// <summary>
    /// 文件监控加载器
    /// </summary>
    public class FileWatcherLoader : IDisposable
    {
        private readonly IList<FileSystemWatcher> _fileWatchers = new List<FileSystemWatcher>();

        public void Watch(FileInfo fileInfo, Action onFileChanged)
        {
            if (onFileChanged is not null)
            {
                WatchFileChange(fileInfo, onFileChanged);
            }
        }
        private void WatchFileChange(FileInfo fileInfo, Action onFileChanged)
        {
            FileSystemWatcher fileWatcher = new(fileInfo.DirectoryName)
            {
                Filter = fileInfo.Name,
                NotifyFilter = NotifyFilters.LastWrite
            };
            #region OnChanged
            DateTime lastChangedTime = DateTime.Now;
            int twoTimeInterval = 1000;
            fileWatcher.Changed += (sender, e) =>
            {
                var timerInterval = (DateTime.Now - lastChangedTime).TotalMilliseconds;
                if (timerInterval < twoTimeInterval) { return; }
                onFileChanged?.Invoke();
                lastChangedTime = DateTime.Now;
            };
            #endregion
            fileWatcher.EnableRaisingEvents = true;
            _fileWatchers.Add(fileWatcher);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (int i = 0; i < _fileWatchers.Count; i++)
                {
                    FileSystemWatcher fileWatcher = _fileWatchers[i];
                    fileWatcher.EnableRaisingEvents = false;
                    fileWatcher.Dispose();
                }
            }
        }
    }
}
