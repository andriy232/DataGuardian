﻿using System;
using System.Threading.Tasks;
using NightKeeper.Helper.Backups;
using NightKeeper.Helper.Settings;

namespace NightKeeper.Helper
{
    public class Script
    {
        public PeriodicitySettings Period { get; }
        public IConnection Connection { get; }
        public string TargetPath { get; }

        public string BackupFileName { get; }
        public string Name { get; }
        public int Id { get; private set; }

        public Script(int id,
            IConnection connection,
            string targetPath,
            PeriodicitySettings period,
            string backupFileName = "Backup",
            string name = "Sample script")
        {
            Id = id;
            Connection = connection ?? throw new ArgumentException(nameof(connection));
            TargetPath = targetPath ?? throw new ArgumentException(nameof(targetPath));
            Period = period;
            BackupFileName = backupFileName;
            Name = name;
        }

        public async Task PerformAsync()
        {
            using (var backup = new LocalBackup(TargetPath, BackupFileName))
            {
                await Connection.Upload(backup);
            }
        }

        public override string ToString()
        {
            return $"Script: {TargetPath}, {Connection}, {Period}";
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}